using LivePolls.Application.Services;
using LivePolls.Domain.Abstractions;
using LivePolls.Domain.Modeles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;



namespace LivePolls.DataAccess.Repo
{
    public class PollsRepo : IPollsRepo
    {

        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public PollsRepo(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        // проверка на срок жизни опроса
        public async Task<List<Poll>> GetUpdatedPolls()
        {
            var entities = await _context.Polls.ToListAsync();
            foreach (var ent in entities)
            {
                var endDate = ent.EndDate;
                await _context.Polls
                                .Where(item => item.EndDate < DateTime.UtcNow)
                                .ExecuteUpdateAsync(s => s
                                .SetProperty(s => s.IsActive, s => false)
                );
            }
            return entities;
        }


        public async Task<List<Poll>> GetPolls()
        {
            var entities = await GetUpdatedPolls();
            foreach (var ent in entities)
            {
                var options = _context.PollOptions
                                .Where(o => o.PollId == ent.Id)
                                .ToList();
                ent.Options = options; 
            }

            if (entities.Equals(0))
            {
                Debug.WriteLine("there are not any polls");
            }
            return entities; 
        }


        public async Task<Poll> GetOnePoll(Guid id)
        {

            var options = _context.PollOptions
                .Where(o => o.PollId == id)
                .ToList();

            Poll? entity = await _context.Polls
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            foreach (var opt in options)
            {
                entity?.Options.Add(opt);

            }

            return entity!;
        }


        public async Task<Poll> CreatePoll(CreatePollRequestDTO request)
        {
            var pollId = Guid.NewGuid();
            var poll = new Poll();
            poll.CreatorId = Guid.NewGuid();
            DateTime dt = DateTime.Now;
            poll.CreatedAt = dt;
            if (request.Lifespan > 0)
            {
                poll.EndDate = dt.AddDays(request.Lifespan);
                poll.IsActive = true;
            }
            else
            {
                poll.EndDate = dt;
                poll.IsActive = false;
            }
            poll.Question = request.Question;
            poll.Options = request.Options.Select(o => new PollOption { Id = Guid.NewGuid(), Text = o, PollId = pollId }).ToList();
            await _context.Polls.AddAsync(poll);
            foreach (var opt in poll.Options)
            {
                await _context.PollOptions.AddAsync(opt);
            }
            await _context.SaveChangesAsync();
            return poll;
        }
    }
}
