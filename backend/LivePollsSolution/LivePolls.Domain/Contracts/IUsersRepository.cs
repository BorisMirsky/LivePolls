using LivePolls.Domain.Modeles;



namespace LivePolls.Domain.Contracts
{
    public interface IUsersRepository
    {
        Task<User?> GetUserByIdAsync(Guid userId);
        Task<User?> GetUserByNameAsync(string userName);
        Task<User> CreateUserAsync(string userName);
    }
}