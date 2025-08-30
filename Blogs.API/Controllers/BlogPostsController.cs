using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blogs.API.Data;
using Blogs.API.Models;
using Microsoft.AspNetCore.Authorization;

namespace Blogs.API.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly BlogDbContext _context;

        public BlogPostsController(BlogDbContext context)
        {
            _context = context;
        }

        // GET: api/BlogPosts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetBlogPosts()
        {
            try
            {
                var posts = await _context.BlogPosts.ToListAsync();
                if (posts == null || !posts.Any())
                    return NotFound(new { message = "No blog posts found." });
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving blog posts.", error = ex.Message });
            }
        }

        // GET: api/BlogPosts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPost>> GetBlogPost(int id)
        {
            try
            {
                var blogPost = await _context.BlogPosts.FindAsync(id);

                if (blogPost == null)
                {
                    return NotFound(new { message = $"Blog postwith ID {id} not found." });
                }

                return Ok(blogPost);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the blog post.", error = ex.Message });
            }
        }

        // PUT: api/BlogPosts/5
       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogPost(int id, [FromBody] UpdateBlogPostDTO updateblogPostdto)
        {
            try
            {
                var existingPost = await _context.BlogPosts.FindAsync(id);
                if (existingPost == null)
                    return NotFound(new { message = $"Blog post with ID {id} not found." });

                existingPost.BlogTitle = updateblogPostdto.BlogTitle;
                existingPost.BlogDescription = updateblogPostdto.BlogDescription;
                existingPost.BlogAuthor = updateblogPostdto.BlogAuthor;
                existingPost.BlogContent = updateblogPostdto.BlogContent;

                _context.Entry(existingPost).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return Ok(new { message = "Blog post updated successfully.", blogPost = existingPost });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the blog post.", error = ex.Message });
            }
        }

        // POST: api/BlogPosts
        
        [HttpPost]
        public async Task<ActionResult<BlogPost>> PostBlogPost([FromBody] AddBlogPostDTO blogPostdto)
        {
            try
            {
                var blogPost = new BlogPost
                {
                    BlogTitle = blogPostdto.BlogTitle,
                    BlogDescription = blogPostdto.BlogDescription,
                    BlogAuthor = blogPostdto.BlogAuthor,
                    BlogContent = blogPostdto.BlogContent,
                };
                _context.BlogPosts.Add(blogPost);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetBlogPost), new { id = blogPost.BlogId }, blogPost);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the blog post.", error = ex.Message });
            }
        }

        // DELETE: api/BlogPosts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            try
            {
                var blogPost = await _context.BlogPosts.FindAsync(id);
                if (blogPost == null)
                {
                    return NotFound(new { message = $"Blog postwith ID {id} not found." });
                }

                _context.BlogPosts.Remove(blogPost);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Blog post deleted successfully.", blogPost });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the blog post.", error = ex.Message });
            }
        }

        //private bool BlogPostExists(int id)
        //{
        //    return _context.BlogPosts.Any(e => e.BlogId == id);
        //}
    }
}
