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

        public async Task<User> GetOrCreateUserAsync(string userName)
        {
            var user = await _repository.GetUserByNameAsync(userName);
            if (user == null)
            {
                user = await _repository.CreateUserAsync(userName);
            }
            return user;
        }
    }
}