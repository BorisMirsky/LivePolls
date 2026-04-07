using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace LivePolls.Domain.Modeles
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = String.Empty;

        [Column("login")]
        public string? Login { get; set; } = String.Empty;

        [Column("password")]
        public string? Password { get; set; } = String.Empty;

        public List<UserConnection>? Connections { get; set; }
    }
}
