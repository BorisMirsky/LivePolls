using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivePolls.Domain.Modeles
{
    public class Vote
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("pollid")]
        public Guid PollId { get; set; }

        [Column("optionid")]
        public Guid OptionId { get; set; }

        [Column("userid")]
        public Guid UserId { get; set; }

        [Column("votedat")]
        public DateTime VotedAt { get; set; } = DateTime.UtcNow;

        // Навигационные свойства
        [ForeignKey("PollId")]
        public Poll? Poll { get; set; }

        [ForeignKey("OptionId")]
        public PollOption? Option { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
