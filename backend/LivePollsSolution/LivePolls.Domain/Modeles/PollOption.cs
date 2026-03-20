using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivePolls.Domain.Modeles
{
    public class PollOption
    {
        public int Id { get; set; }
        public int PollId { get; set; }
        [Required]
        public string Text { get; set; } = String.Empty;
        public int VoteCount { get; set; } = 0;

        public Poll? Poll { get; set; }
    }
}
