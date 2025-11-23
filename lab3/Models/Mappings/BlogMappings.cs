using lab3.Models.Entities;

namespace lab3.Models.Mappings
{
    public static class BlogMappings
    {
        public static BlogArticleViewModel ToViewModel(this BlogArticle blogArticle) => new() {
            Id = blogArticle.Id,
            Title = blogArticle.Title,
            Description = blogArticle.Description,
            Content = blogArticle.Content,
            Comments = blogArticle.Comments
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => x.ToViewModel())
                .ToList()
        };

        public static BlogArticle ToEntity(this CreateBlogArticleModel blogArticle) => new() {
            Id = blogArticle.Id,
            Title = blogArticle.Title,
            Description = blogArticle.Description ?? string.Empty,
            Content = blogArticle.Content
        };
        
        public static BlogComment ToEntity(this CreateBlogCommentModel blogComment) => new() {
            ArticleId = blogComment.ArticleId,
            Author = blogComment.Author,
            Content = blogComment.Content,
            CreatedAt = DateTime.UtcNow
        };
        
        public static BlogCommentViewModel ToViewModel(this BlogComment blogComment) => new() {
            Id = blogComment.Id,
            Author = blogComment.Author,
            Content = blogComment.Content,
            CreatedAt = blogComment.CreatedAt
        };
    }
}