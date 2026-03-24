using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivePolls.Domain.Modeles
{
    public class Vote
    {
        public Guid Id { get; set; }
        public Guid PollId { get; set; }
        public Guid OptionId { get; set; }
        //public string UserName { get; set; } = String.Empty;
        public DateTime VotedAt { get; set; } = DateTime.UtcNow;

        public Poll? Poll { get; set; }
        public PollOption? Option { get; set; }
    }
}
