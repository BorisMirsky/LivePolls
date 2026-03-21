

namespace LivePolls.Domain.Abstractions
{
    public record PollSummaryDTO
    (
        int Id,
        string Question,
        DateTime CreatedAt,
        int OptionsCount
     );
}
