using LivePolls.Domain.Abstractions;
using LivePolls.Domain.Modeles;

namespace LivePolls.Domain.Abstractions
{
    public interface IVoteHubService
    {

        Task<Poll?> GetPollWithOptionsAsync(Guid pollId);

        Task<bool> HasUserVotedAsync(Guid pollId, Guid userId);

        Task<Vote> ProcessVoteAsync(Guid pollId, Guid optionId, Guid userId);

        Task<PollResultsDto> GetPollResultsAsync(Guid pollId);

        Task RegisterUserConnectionAsync(Guid userId, string connectionId, Guid? pollId = null);

        Task UnregisterUserConnectionAsync(string connectionId);

    }
}