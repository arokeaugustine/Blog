using Blog.Models;

namespace Blog.Services
{
    public class PostsService
    {
        private static readonly List<Post> AllPosts = new ();

        public Task CreatePost(Post item)
        {
            AllPosts.Add(item);
            return Task.CompletedTask;
        }

        public Task<Post?> UpdatePost(int Id,Post item)
        {
            var posts = AllPosts.FirstOrDefault(x => x.Id == Id);
            if (posts != null)
            {
                posts.Title = item.Title;
                posts.Body = item.Body;
                posts.UserId = item.UserId;
            }
            return Task.FromResult(posts);
        }

        public Task<Post?> GetPost(int Id)
        {
            return Task.FromResult(AllPosts.FirstOrDefault(x => x.Id == Id));
        }
        public Task<List<Post>> GetAllPosts()
        {
            return Task.FromResult(AllPosts);
        }

        public Task DeletePost(int Id)
        {
            var post = AllPosts.FirstOrDefault(x => x.Id == Id);
            if (post != null) 
            { 
                AllPosts.Remove(post);
            }
            return Task.CompletedTask;
        }


    }
}
