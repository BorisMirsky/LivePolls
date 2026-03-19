using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivePolls.Domain.Modeles
{
    public class Poll
    {
        public Guid Id { get; set; }
        public Guid CreatorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Boolean IsActive { get; set; } = false;
        public string Question { get; set; } = String.Empty;
        public string[] Responses { get; set; } = [];
    }
}

