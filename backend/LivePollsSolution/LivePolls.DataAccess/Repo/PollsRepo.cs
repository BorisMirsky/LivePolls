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


        public async Task<List<Poll>> GetPolls()
        {
            var entities = await _context.Polls.ToListAsync();
            foreach (var ent in entities)
            {
                var options = _context.PollOptions
                                .Where(o => o.PollId == ent.Id)
                                .ToList();
                ent.Options = options; 
                //foreach (var opt in options)
                //{
                //    ent.Options.Add(opt);
                //}
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
                entity.Options.Add(opt);

            }
                //if (entity.Equals(0))
                //{
                //    Debug.WriteLine("there are not poll with such id");
                //    //throw new Exception($"Doctors with speciality {speciality} not found");
                //}
                return entity!;
        }


        public async Task<Poll> CreatePoll(CreatePollRequestDTO request)
        {
            var pollId = Guid.NewGuid();
            var poll = new Poll();
            poll.CreatorId = Guid.NewGuid();    // request.CreatorId;                   
            poll.CreatedAt = DateTime.UtcNow;
            DateTime today = DateTime.Now;
            poll.EndDate = today.AddDays(7);
            poll.IsActive = true;
            poll.Question = request.Question;
            poll.Options = request.Options.Select(o => new PollOption { Id = Guid.NewGuid(), Text = o, PollId = pollId }).ToList();
            await _context.Polls.AddAsync(poll);
            foreach (var opt in poll.Options)
            {
                await _context.PollOptions.AddAsync(opt);
            }
            //await _context.PollOptions.AddAsync(poll.Options);
            await _context.SaveChangesAsync();
            return poll;
        }
    }
}
