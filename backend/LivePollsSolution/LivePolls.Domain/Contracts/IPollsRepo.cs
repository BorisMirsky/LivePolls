
using LivePolls.Domain.Abstractions;
using LivePolls.Domain.Modeles;



namespace LivePolls.Application.Services
{
    public interface IPollsRepo
    {
        Task<List<Poll>> GetPolls();
        Task<List<Poll>> GetUpdatedPolls();
        Task<Poll> GetOnePoll(Guid id); 
        Task<Poll> CreatePoll(CreatePollRequestDTO request); 
    }
}
