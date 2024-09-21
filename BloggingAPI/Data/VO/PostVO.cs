using System.ComponentModel.DataAnnotations.Schema;

namespace BloggingAPI.Data.VO
{
    public class PostVO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
