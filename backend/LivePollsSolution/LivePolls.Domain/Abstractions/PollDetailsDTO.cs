using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivePolls.Domain.Abstractions
{
    public record PollDetailsDTO
    (
        Guid Id, 
        string Question,
        bool IsActive,
        List<PollOptionResultDTO> Options
     );


    //public record PollOptionResultDTO
    //(
    //    Guid OptionId,
    //    string Text,
    //    int VoteCount,
    //    int Percentage
    //);
}
