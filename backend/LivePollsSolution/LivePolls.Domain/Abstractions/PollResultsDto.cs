

namespace LivePolls.Domain.Abstractions
{
    public record PollResultsDto
    (
        Guid PollId,
        string Question,
        List<PollOptionResultDTO> Options,
        int TotalVotes,
        DateTime? EndDate,
        bool IsActive
    );
}
