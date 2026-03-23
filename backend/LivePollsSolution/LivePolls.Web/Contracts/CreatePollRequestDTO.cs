

namespace LivePolls.Domain.Abstractions
{
    public record CreatePollRequestDTO
    (
        string Question,
        List<string> Options,
        DateTime EndDate
        //Guid CreatorId
    );
}

//Id, CreatorId, CreatedAt, EndDate, IsActive, Question