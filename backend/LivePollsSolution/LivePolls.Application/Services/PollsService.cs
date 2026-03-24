using LivePolls.Application.Services;
using LivePolls.Domain.Abstractions;
using LivePolls.Domain.Modeles;
using Microsoft.Extensions.Options;

namespace LivePolls.Application.Services
{
    public class PollsService : IPollsService
    {
        private readonly IPollsRepo _pollsRepo;

        public PollsService(IPollsRepo pollsRepo)
        {
               _pollsRepo = pollsRepo;
        }

        public async Task<List<Poll>> GetPolls()
        {
               return await _pollsRepo.GetPolls();
        }

        public async Task<Poll> GetOnePoll(Guid id)
        {
                return await _pollsRepo.GetOnePoll(id);
        }

        public async Task<Poll> CreatePoll(CreatePollRequestDTO request)
        {
            //request.EndDate = request.EndDate ?? DateTime.UtcNow.AddDays(7);
            return await _pollsRepo.CreatePoll(request);
        }
    }
}

