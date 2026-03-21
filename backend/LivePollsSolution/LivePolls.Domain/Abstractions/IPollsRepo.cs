using LivePolls.Domain.Abstractions;



namespace LivePolls.Application.Services
{
    public interface IPollsRepo
    {
        Task<List<PollSummaryDTO>> GetPolls();
        Task<PollSummaryDTO> GetOnePoll(Guid id);
        Task<PollCreatedResponseDTO> CreatePoll(CreatePollRequestDTO request);
    }
}
