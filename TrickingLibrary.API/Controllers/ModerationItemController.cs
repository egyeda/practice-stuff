﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrickingLibrary.API.ViewModels;
using TrickingLibrary.Data;
using TrickingLibrary.Models;
using TrickingLibrary.Models.Moderation;

namespace TrickingLibrary.API.Controllers
{
    [ApiController]
    [Route("api/moderation-items")]
    public class ModerationItemController : ControllerBase
    {
        private readonly AppDbContext _ctx;

        public ModerationItemController(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IEnumerable<ModerationItem> All() => _ctx.ModerationItems
                                                        .ToList();

        [HttpGet("{id}")]
        public ModerationItem Get(int id) =>
            _ctx.ModerationItems
                .FirstOrDefault(t => t.Id.Equals(id));

        [HttpGet("{id}/comments")]
        public IEnumerable<object> GetComments(int id) =>
            _ctx.Comments
                .Where(x => x.ModerationItemId.Equals(id))
                .Select(CommentsViewModel.Projection)
                .ToList();

        [HttpPost("{id}/comments")]
        public async Task<IActionResult> Comment(int id, [FromBody] Comment comment)
        {
            var moderationItem = _ctx.ModerationItems.FirstOrDefault(x => x.Id == id);

            if (moderationItem == null) 
                return NoContent();
            
            var regex = new Regex(@"\B(?<tag>@[a-zA-Z0-9-_]+)");

            comment.HtmlContent = regex.Matches(comment.Content)
                                       .Aggregate(comment.Content,
                                           (content, match) =>
                                           {
                                               var tag = match.Groups["tag"].Value;
                                               return content
                                                   .Replace(tag, $"<a href=\"{tag}-user-link\">{tag}</a>");
                                           });

            moderationItem.Comments.Add(comment);
            await _ctx.SaveChangesAsync();


            return Ok(CommentsViewModel.Create(comment));
        }
    }
}