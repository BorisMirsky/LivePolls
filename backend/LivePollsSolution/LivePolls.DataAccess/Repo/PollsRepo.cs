using LivePolls.Application.Services;
using LivePolls.Domain.Abstractions;
using LivePolls.Domain.Modeles;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;



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
            var entities = await _context.Polls
               .ToListAsync();

            if (entities.Equals(0))
            {
                Debug.WriteLine("there are not any polls");
            }
            return entities; 
        }

        public async Task<Poll> GetOnePoll(Guid id)
        {
            Poll? entity = await _context.Polls
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (entity.Equals(0))
            {
                Debug.WriteLine("there are not poll with such id");
                //throw new Exception($"Doctors with speciality {speciality} not found");
            }
            return entity;
        }


        public async Task<Poll> CreatePoll(CreatePollRequestDTO request)
        {
            var pollId = Guid.NewGuid();
            var poll = new Poll();
            poll.CreatorId = request.CreatorId;
            poll.CreatedAt = DateTime.UtcNow;
            DateTime today = DateTime.Now;
            poll.EndDate = today.AddDays(7);
            poll.IsActive = true;
            poll.Question = request.Question;
            poll.Options = request.Options.Select(o => new PollOption { Id = Guid.NewGuid(), Text = o, PollId = pollId }).ToList();
            await _context.Polls.AddAsync(poll);
            //await _context.PollOptions.AddAsync(poll.Options);
            await _context.SaveChangesAsync();
            return poll;
        }
    }
}
