using LivePolls.Application.Services;
using LivePolls.Domain.Abstractions;
using LivePolls.Domain.Modeles;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Numerics;



namespace LivePolls.DataAccess.Repo
{
    public class PollsRepo : IPollsRepo
    {
        private readonly AppDbContext _context;

        public PollsRepo(AppDbContext context)
        {
            _context = context;
        }

        //public async Task<List<PollSummaryDTO>> GetPolls()
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

        //PollCreatedResponseDTO   CreatePollRequestDTO
        public async Task<Poll> CreatePoll(string Question,
                                            List<string>? Options,
                                            DateTime? EndDate,
                                            Guid CreatorId
            )
        {

        }
    }
}
