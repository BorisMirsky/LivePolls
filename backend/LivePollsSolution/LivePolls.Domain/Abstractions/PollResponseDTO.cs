using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivePolls.Domain.Abstractions
{
    public record PollResponseDTO
    (
        Guid Id,
        Guid CreatorId,
        DateTime? CreatedAt,
        DateTime? EndDate,
        bool? IsActive,
        string Question,
        List<PollOptionResponseDTO> PollOptions
    );
}
