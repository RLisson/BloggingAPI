using BloggingAPI.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloggingAPI.Model
{
    [Table("posts")]
    public class Post : BaseEntity
    {
        [Column("title")]
        public string Title { get; set; }

        [Column("content")]
        public string Content { get; set; }

        [Column("category")]
        public string Category { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
