

/// <summary>
/// DTO для передачи результатов опроса клиенту.
/// </summary>
namespace LivePolls.Domain.Abstractions
{
    public record PollOptionResultDTO
    (
        Guid OptionId,
        string Text, 
        int VoteCount 
    );
}
