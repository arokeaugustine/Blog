using Blog.Models;
using Blog.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController(IPostsService postsService, IConfiguration configuration) : ControllerBase
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

        [HttpGet]
        [Route("my-key")]
        public ActionResult GetMyKey()
        {
            var myKey = configuration["MyKey"];
            return Ok(myKey);
        }

        [HttpGet]
        [Route("database-configuration")]
        public ActionResult GetDatabaseConfiguration()
        {
            var type = configuration["Database:Type"];
            var connectionString = configuration["Database:ConnectionString"];
            return Ok(new
            {
                Type = type,
                ConnectionString = connectionString
            });
        }

        [HttpGet]
        [Route("databae-configuration-with-bind")]
        public ActionResult GetDatabaseConfigurationWithBind()
        {
            var databaseOption = new DatabaseOption();
            configuration.GetSection(DatabaseOption.SectionName).Bind(databaseOption);
            return Ok(new
            {
                databaseOption.Type,
                databaseOption.ConnectionString
            });
        }

        [HttpGet]
        [Route("database-configuration-with-bind")]
        public ActionResult GetDatabaseConfigurationWithGenericType()
        {
            var databaseOption = configuration.GetSection(DatabaseOption.SectionName).Get<DatabaseOption>();
            return Ok(new
            {
                databaseOption.Type,
                databaseOption.ConnectionString
            });
        }

        [HttpGet]
        [Route("database-configuration-with-ioptions")]
        public ActionResult GetDatabaseConfigurationWithIOptions([FromServices]IOptions<DatabaseOption> options)
        {
            var databaseOption = options.Value;
            return Ok(new
            {
                databaseOption.Type,
                databaseOption.ConnectionString
            });
        }

        [HttpGet]
        [Route("database-configuration-with-ioptions-snapshot")]
        public ActionResult GetDatabaseConfigurationWithIOptionsSnapshot([FromServices] IOptionsSnapshot<DatabaseOption> options)
        {
            var databaseOption = options.Value;
            return Ok(new
            {
                databaseOption.Type,
                databaseOption.ConnectionString
            });
        }

    }
}
