using System.ComponentModel.DataAnnotations;

namespace Blogs.API.Models
{
    public class BlogPost
    {
        [Key]
        public int  BlogId { get; set; }

        [Required]
        public String BlogTitle { get; set; } = String.Empty;

        [Required]
        public String BlogDescription { get; set;} = String.Empty;
        
        [Required]
        public String BlogAuthor { get; set;} = String.Empty;

        [Required]
        public String BlogContent { get; set;} = String.Empty;

        public DateTime PublishedDate { get; set; }






    }
}
