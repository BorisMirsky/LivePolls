using LivePolls.Application.Services;
using LivePolls.Domain.Modeles;
using Microsoft.EntityFrameworkCore;



namespace LivePolls.DataAccess.Repo
{
    public class PollsRepo : IPollsRepo
    {

        private readonly AppDbContext _context;

        public PollsRepo(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<List<Poll>> GetPolls()
        {
            return await _context.Polls
                .Include(p => p.Options)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Poll?> GetOnePoll(Guid id)
        {
            return await _context.Polls
                .Include(p => p.Options)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> GetVoteCount(Guid optionId)
        {
            return await _context.Votes.CountAsync(v => v.OptionId == optionId);
        }

        public async Task<Poll> CreatePoll(Poll poll)
        {
            await _context.Polls.AddAsync(poll);
            await _context.SaveChangesAsync();
            return poll;
        }

        public async Task UpdatePoll(Poll poll)
        {
            _context.Polls.Update(poll);
            await _context.SaveChangesAsync();
        }

    }
}
