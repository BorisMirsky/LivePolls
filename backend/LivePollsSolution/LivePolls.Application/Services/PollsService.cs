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

        public async Task<List<Poll>> GetPolls();
        {
               return await _pollsRepo.GetPolls();
        }

        public async Task<Poll> GetOnePoll(Guid id);
        {
                return await _pollsRepo.GetOnePoll();
        }

        public async Task<Poll> CreatePoll(string Question,
                                            List<string>? Options,
                                            DateTime? EndDate,
                                            Guid CreatorId);
        {
            return await _pollsRepo.CreatePoll(Question,Options?,EndDate?,CreatorId);
        }
    }
}

//public async Task<Admin> Register(string email, string password)
//{
//    return await _adminRepo.Register(email, password);
//}

//public async Task<Admin> LoginAccount(string email, string password)
//{
//    return await _adminRepo.Login(email, password);
//}