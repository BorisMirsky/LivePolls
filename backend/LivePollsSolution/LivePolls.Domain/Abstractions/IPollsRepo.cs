using LivePolls.Domain.Abstractions;
using LivePolls.Domain.Modeles;



namespace LivePolls.Application.Services
{
    public interface IPollsRepo
    {
        Task<List<PollSummaryDTO>> GetPolls();
        Task<PollSummaryDTO> GetOnePoll(Guid id);   // Guid  int
        //Task<PollCreatedResponseDTO> CreatePoll(CreatePollRequestDTO request);
        Task<Poll> CreatePoll(Poll request);
    }
}
