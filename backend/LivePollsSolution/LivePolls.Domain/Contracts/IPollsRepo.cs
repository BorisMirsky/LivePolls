
using LivePolls.Domain.Abstractions;
using LivePolls.Domain.Modeles;



namespace LivePolls.Domain.Contracts
{
    public interface IPollsRepo
    {
        Task<List<Poll>> GetPolls();
        Task<Poll?> GetOnePoll(Guid id);
        Task<int> GetVoteCount(Guid optionId);
        Task<Poll> CreatePoll(Poll poll);
        Task UpdatePoll(Poll poll);
    }
}
