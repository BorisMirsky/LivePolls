using LivePolls.Domain.Abstractions;
using LivePolls.Domain.Modeles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;



namespace LivePolls.DataAccess.Repo
{
    public class VoteHubRepository : IVoteHubRepository
    {

        private readonly AppDbContext _context;
        private readonly ILogger<VoteHubRepository> _logger;


        public VoteHubRepository(AppDbContext context, ILogger<VoteHubRepository> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<Poll?> GetPollWithOptionsAsync(Guid pollId)
        {
            return await _context.Polls
                .Include(p => p.Options)
                .FirstOrDefaultAsync(p => p.Id == pollId);
        }

        public async Task<bool> HasUserVotedAsync(Guid pollId, Guid userId)
        {
            return await _context.Votes
                .AnyAsync(v => v.PollId == pollId && v.UserId == userId);
        }

        public async Task<Vote> AddVoteAsync(Vote vote)
        {
            await _context.Votes.AddAsync(vote);
            await _context.SaveChangesAsync();
            return vote;
        }

        public async Task<PollOption?> GetPollOptionAsync(Guid optionId)
        {
            return await _context.PollOptions
                .FirstOrDefaultAsync(o => o.Id == optionId);
        }

        public async Task UpdatePollOptionAsync(PollOption option)
        {
            _context.PollOptions.Update(option);
            await _context.SaveChangesAsync();
        }

        public async Task AddUserConnectionAsync(UserConnection connection)
        {
            await _context.UserConnections.AddAsync(connection);
            await _context.SaveChangesAsync();
        }

        public async Task<UserConnection?> GetUserConnectionAsync(string connectionId)
        {
            return await _context.UserConnections
                .FirstOrDefaultAsync(c => c.ConnectionId == connectionId);
        }

        public async Task RemoveUserConnectionAsync(UserConnection connection)
        {
            _context.UserConnections.Remove(connection);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserConnection>> GetPollConnectionsAsync(Guid pollId)
        {
            return await _context.UserConnections
                .Where(c => c.PollId == pollId)
                .ToListAsync();
        }
    }
}
