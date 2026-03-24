using LivePolls.Domain.Modeles;
using LivePolls.Domain.Abstractions;
//using LivePolls.Domain.Con


namespace LivePolls.Application.Services
{
    public interface IPollsService
    {
        Task<List<Poll>> GetPolls();            
        Task<Poll> GetOnePoll(Guid id);   
        Task<Poll> CreatePoll(CreatePollRequestDTO request); 
        //string Question,List<string>? Options,DateTime? EndDate,Guid CreatorId); 
    }
}
