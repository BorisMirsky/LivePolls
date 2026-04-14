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


    //public async Task<Poll?> GetPollWithOptionsAsync(Guid pollId)
    //{
    //    return await _context.Polls
    //        .Include(p => p.Options)
    //        .FirstOrDefaultAsync(p => p.Id == pollId);
    //}
    //public async Task<bool> HasUserVotedAsync(Guid pollId, Guid userId)
    //{
    //    return await _context.Set<Vote>()
    //        .AnyAsync(v => v.PollId == pollId && v.UserId == userId);
    //}
    //public async Task<Vote> AddVoteAsync(Guid pollId, Guid optionId, Guid userId)
    //{
    //    using var transaction = await _context.Database.BeginTransactionAsync();

    //    try
    //    {
    //        // 1. Увеличиваем счетчик голосов у варианта
    //        var option = await _context.PollOptions
    //            .FirstOrDefaultAsync(o => o.Id == optionId);

    //        if (option == null)
    //            throw new InvalidOperationException("Вариант не найден");

    //        option.Order++;
    //        _context.PollOptions.Update(option);

    //        // 2. Сохраняем голос
    //        var vote = new Vote
    //        {
    //            Id = Guid.NewGuid(),
    //            PollId = pollId,
    //            OptionId = optionId,
    //            UserId = userId,
    //            VotedAt = DateTime.UtcNow
    //        };

    //        await _context.Set<Vote>().AddAsync(vote);
    //        await _context.SaveChangesAsync();
    //        await transaction.CommitAsync();

    //        _logger.LogInformation("User {UserId} voted for option {OptionId} in poll {PollId}",
    //            userId, optionId, pollId);

    //        return vote;
    //    }
    //    catch (Exception ex)
    //    {
    //        await transaction.RollbackAsync();
    //        _logger.LogError(ex, "Error adding vote for user {UserId} in poll {PollId}",
    //            userId, pollId);
    //        throw;
    //    }
    //}
    //public async Task AddUserConnectionAsync(Guid userId, string connectionId, Guid? pollId = null)
    //{
    //    var connection = new UserConnection
    //    {
    //        Id = Guid.NewGuid(),
    //        UserId = userId,
    //        ConnectionId = connectionId,
    //        PollId = pollId,
    //        ConnectedAt = DateTime.UtcNow,
    //        LastActivity = DateTime.UtcNow
    //    };

    //    await _context.Set<UserConnection>().AddAsync(connection);
    //    await _context.SaveChangesAsync();
    //}
    //public async Task RemoveUserConnectionAsync(string connectionId)
    //{
    //    var connection = await _context.Set<UserConnection>()
    //        .FirstOrDefaultAsync(c => c.ConnectionId == connectionId);

    //    if (connection != null)
    //    {
    //        _context.Set<UserConnection>().Remove(connection);
    //        await _context.SaveChangesAsync();
    //    }
    //}
    //public async Task<IEnumerable<UserConnection>> GetPollConnectionsAsync(Guid pollId)
    //{
    //    return await _context.Set<UserConnection>()
    //        .Where(c => c.PollId == pollId)
    //        .ToListAsync();
    //}
