

namespace LivePolls.Domain.Abstractions
{
    public record PollOptionResultDTO
    (
        Guid OptionId,
        string Text, 
        int VoteCount,
        int Percentage
    );
}
