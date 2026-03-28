
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LivePolls.Domain.Modeles
{
    [Table("polls")]
    public class Poll
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("creatorid")]
        public Guid CreatorId { get; set; }

        [Column("createdat")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("enddate")]
        public DateTime? EndDate { get; set; } //= DateTime.UtcNow;

        [Column("isactive")]
        public Boolean? IsActive { get; set; } = false;

        [Column("question")]
        public string Question { get; set; } = String.Empty;

        public List<PollOption> Options { get; set; } = [];
    }
}

