using System.ComponentModel.DataAnnotations;


namespace Blogs.API.Models
{
    public class UpdateBlogPostDTO
    {
        [Required]
        public String BlogTitle { get; set; } = String.Empty;

        [Required]
        public String BlogDescription { get; set; } = String.Empty;

        [Required]
        public String BlogAuthor { get; set; } = String.Empty;

        [Required]
        public String BlogContent { get; set; } = String.Empty;
    }
}
