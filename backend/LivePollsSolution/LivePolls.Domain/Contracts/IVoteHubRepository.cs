using LivePolls.Domain.Modeles;

namespace LivePolls.Domain.Abstractions
{
    public interface IVoteHubRepository
    {

        Task<Poll?> GetPollWithOptionsAsync(Guid pollId);

        Task<bool> HasUserVotedAsync(Guid pollId, Guid userId);

        Task<Vote> AddVoteAsync(Vote vote);

        Task<PollOption?> GetPollOptionAsync(Guid optionId);

        Task UpdatePollOptionAsync(PollOption option);

        Task AddUserConnectionAsync(UserConnection connection);

        Task<UserConnection?> GetUserConnectionAsync(string connectionId);

        Task RemoveUserConnectionAsync(UserConnection connection);

        Task<IEnumerable<UserConnection>> GetPollConnectionsAsync(Guid pollId);

    }
}