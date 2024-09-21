using System.ComponentModel.DataAnnotations.Schema;

namespace BloggingAPI.Model
{
    [Table("posts")]
    public class Post
    {
        [Column("title")]
        string Title { get; set; }

        [Column("content")]
        string Content { get; set; }

        [Column("category")]
        string Category { get; set; }

        [Column("tags")]
        string[] Tags { get; set; } = new string[2];

        [Column("created_at")]
        DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        DateTime UpdatedAt { get; set; }
    }
}
