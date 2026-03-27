using LivePolls.Domain.Modeles;
using LivePolls.Domain.Abstractions;



namespace LivePolls.Application.Services
{
    public interface IPollsService
    {
        Task<List<Poll>> GetPolls();            
        Task<Poll> GetOnePoll(Guid id);   
        Task<Poll> CreatePoll(CreatePollRequestDTO request); 
    }
}
