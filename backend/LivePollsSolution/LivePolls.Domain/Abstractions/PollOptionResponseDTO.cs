using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivePolls.Domain.Abstractions
{
    public record PollOptionResponseDTO
    (
        Guid Id,
        string Text,
        int Order
    );
}
