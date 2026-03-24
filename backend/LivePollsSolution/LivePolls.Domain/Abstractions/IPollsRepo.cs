using LivePolls.Domain.Abstractions;
using LivePolls.Domain.Modeles;



namespace LivePolls.Application.Services
{
    public interface IPollsRepo
    {
        Task<List<Poll>> GetPolls();
        Task<Poll> GetOnePoll(Guid id); 
        Task<Poll> CreatePoll(string Question,
                                List<string>? Options,
                                DateTime? EndDate,
                                Guid CreatorId);
    }
}
