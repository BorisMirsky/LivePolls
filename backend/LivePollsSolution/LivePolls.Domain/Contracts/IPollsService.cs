using LivePolls.Domain.Modeles;
using LivePolls.Domain.Abstractions;



namespace LivePolls.Domain.Contracts
{
    public interface IPollsService
    {
        Task<List<Poll>> GetPolls();            
        Task<Poll> GetOnePoll(Guid id);   
        Task<Poll> CreatePoll(CreatePollRequestDTO request);
        Task<PollDetailsDTO?> GetPollDetailsAsync(Guid pollId);
    }
}
