using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrickingLibrary.API.Form;
using TrickingLibrary.Data;
using TrickingLibrary.Models;

namespace TrickingLibrary.API.Controllers
{
    [ApiController]
    [Route("api/tricks")]
    public class TricksController : ControllerBase
    {
        private readonly AppDbContext _ctx;

        public TricksController(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        // /api/tricks
        [HttpGet]
        public IEnumerable<Trick> All() => _ctx.Tricks.ToList();
        
        // /api/tricks/{id}
        [HttpGet("{id}")]
        public Trick Get(string id) => 
            _ctx.Tricks
                .FirstOrDefault(t => t.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));
        
        // /api/tricks/{id}/submissions
        [HttpGet("{trickId}/submissions")]
        public IEnumerable<Submission> ListSubmissionsForTrick(string trickId) => 
            _ctx.Submissions
                .Where(t => t.TrickId.Equals(trickId, StringComparison.InvariantCultureIgnoreCase)).ToList();

        // /api/tricks
        [HttpPost]
        public async Task<Trick> Create([FromBody] TrickForm trickForm)
        {
            var trick = new Trick
            {
                Id = trickForm.Name.Replace(" ", "-").ToLowerInvariant(),
                Name = trickForm.Name,
                Description = trickForm.Description,
                Difficulty = trickForm.Difficulty,
                Categories = trickForm.Categories.Select(x => new TrickCategory() {CategoryId = x} ).ToList()
            };
            _ctx.Add(trick);
            await _ctx.SaveChangesAsync();
            return trick;
        }
        
        // /api/tricks
        [HttpPut]
        public async Task<Trick> Update([FromBody] Trick trick)
        {
            if (string.IsNullOrEmpty(trick.Id))
            {
                return null;
            }
            _ctx.Update(trick);
            await _ctx.SaveChangesAsync();
            return trick;
        }
        
         // /api/tricks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var trick = _ctx.Tricks.FirstOrDefault(t => t.Id.Equals(id));
            if (trick == null)
            {
                return null;
            }
            trick.Deleted = true;
            await _ctx.SaveChangesAsync();
            return Ok();
        }
    }
}