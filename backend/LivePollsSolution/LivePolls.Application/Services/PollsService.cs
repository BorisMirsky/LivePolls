using LivePolls.Application.Services;
using LivePolls.Domain.Abstractions;
using LivePolls.Domain.Modeles;

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

        public async Task<List<Poll>> GetOnePoll(Guid id);
        {
                return await _pollsRepo.GetOnePoll();
        }

        public async Task<Poll> CreatePoll(Poll request);
        {
            return await _pollsRepo.CreatePoll(request);
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