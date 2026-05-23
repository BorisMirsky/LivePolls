using LivePolls.Domain.Modeles;



namespace LivePolls.Domain.Contracts
{
    public interface IUsersService
    {
        Task<User> GetOrCreateUserAsync(string userName);
    }
}
