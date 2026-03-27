

namespace LivePolls.Domain.Abstractions
{
    public record CreatePollRequestDTO
    (
        string Question,
        List<string>? Options,
        int? EndDate,
        Guid CreatorId
    );
}

