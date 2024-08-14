using Blog.Models;
using Blog.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController(IPostsService postsService) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<Post>>> GetPost()
        {
            var posts = await postsService.GetAllPosts();
            return Ok(posts);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPosts(int id)
        {
            var post = await postsService.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost(Post post)
        {
            await postsService.CreatePost(post);
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePost(int id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }
            var updatedPost = await postsService.UpdatePost(id, post);
            if (updatedPost == null)
            {
                return NotFound();
            }
            return Ok(post);
        }


    }
}
