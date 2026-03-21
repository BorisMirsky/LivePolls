

/// <summary>
/// DTO для передачи результатов опроса клиенту.
/// </summary>
namespace LivePolls.Domain.Abstractions
{
    public record PollOptionResultDTO
    (
        int OptionId,
        string Text, 
        int VoteCount 
    );
}
