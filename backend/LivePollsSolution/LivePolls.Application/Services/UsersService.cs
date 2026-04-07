using LivePolls.Domain.Abstractions;
using LivePolls.Domain.Modeles;

namespace LivePolls.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repository;

        public UsersService(IUsersRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> GetOrCreateUserAsync(Guid userId, string userName)
        {
            var user = await _repository.GetUserByIdAsync(userId);
            if (user == null)
            {
                user = await _repository.CreateUserAsync(userId, userName);
            }
            return user;
        }
    }
}