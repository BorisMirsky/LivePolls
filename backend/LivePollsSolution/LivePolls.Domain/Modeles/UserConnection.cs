using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LivePolls.Domain.Modeles
{
    [Table("userconnections")]
    public class UserConnection
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("userid")]
        public Guid UserId { get; set; }

        [Column("connectionid")]
        public string ConnectionId { get; set; } = string.Empty;

        [Column("pollid")]
        public Guid? PollId { get; set; }

        [Column("connectedat")]
        public DateTime ConnectedAt { get; set; } = DateTime.UtcNow;

        [Column("lastactivity")]
        public DateTime LastActivity { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}