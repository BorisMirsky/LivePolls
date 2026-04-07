using LivePolls.Domain.Modeles;


namespace LivePolls.Domain.Abstractions
{
    public interface IUsersService
    {
        Task<User> GetOrCreateUserAsync(Guid userId, string userName);
    }
}
