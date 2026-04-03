

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LivePolls.Domain.Modeles
{
    [Table("polloptions")]
    public class PollOption
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("pollid")]
        public Guid PollId { get; set; }

        [Column("text")]
        public string Text { get; set; } = String.Empty;

        [Column("order")]
        public int Order { get; set; } = 0;

        public Poll? Poll { get; set; }
    }
}
