using BloggingAPI.Data.Converter.Contract;
using BloggingAPI.Data.VO;
using BloggingAPI.Model;

namespace BloggingAPI.Data.Converter.Implementations
{
    public class PostConverter : IParser<PostVO, Post>, IParser<Post, PostVO>

    {
        public Post Parse(PostVO origin)
        {
            if (origin == null) return null;
            return new Post
            {
                Id = origin.Id,
                Category = origin.Category,
                Content = origin.Content,
                CreatedAt = origin.CreatedAt,   
                Title = origin.Title,
                UpdatedAt = origin.UpdatedAt,
            };
        }

        public PostVO Parse(Post origin)
        {
            if (origin == null) return null;
            return new PostVO
            {
                Id = origin.Id,
                Category = origin.Category,
                Content = origin.Content,
                CreatedAt = origin.CreatedAt,
                Title = origin.Title,
                UpdatedAt = origin.UpdatedAt,
            };
        }

        public List<Post> Parse(List<PostVO> origin)
        {
            return origin.Select(item => Parse(item)).ToList();
        }
        public List<PostVO> Parse(List<Post> origin)
        {
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
