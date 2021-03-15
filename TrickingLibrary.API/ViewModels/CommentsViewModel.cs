using System;
using System.Linq;
using System.Linq.Expressions;
using TrickingLibrary.Models;

namespace TrickingLibrary.API.ViewModels
{
    public static class CommentsViewModel
    {
        public static readonly Func<Comment, object> Create = Projection.Compile();

        public static Expression<Func<Comment, object>> Projection =>
            comment => new
            {
                comment.Id,
                comment.Content,
                comment.HtmlContent,
                comment.ParentId
            };
    }
}