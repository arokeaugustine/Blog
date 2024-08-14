using Blog.Models;

namespace Blog.Services
{
    public interface IPostsService
    {
        Task CreatePost(Post item);
        Task<Post?> UpdatePost(int Id, Post item);
        Task<Post?> GetPost(int Id);
        Task<List<Post>> GetAllPosts();
        Task DeletePost(int Id);

    }
}
