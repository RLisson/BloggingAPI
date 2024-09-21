using System.ComponentModel.DataAnnotations.Schema;

namespace BloggingAPI.Model.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public int Id { get; set; }
    }
}
