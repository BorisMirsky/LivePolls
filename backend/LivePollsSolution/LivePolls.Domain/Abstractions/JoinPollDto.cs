using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivePolls.Domain.Abstractions
{
    public record JoinPollDto
    (
        Guid PollId,
        Guid UserId,
        string UserName
    );
}
