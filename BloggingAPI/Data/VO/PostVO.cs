using BloggingAPI.Hypermedia;
using BloggingAPI.Hypermedia.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloggingAPI.Data.VO
{
    public class PostVO : ISupportsHyperMedia
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
