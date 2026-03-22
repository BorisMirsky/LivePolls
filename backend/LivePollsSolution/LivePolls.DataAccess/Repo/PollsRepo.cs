using LivePolls.Application.Services;
using LivePolls.Domain.Abstractions;
using LivePolls.Domain.Modeles;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;



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
               //.Select(g => g.OrderByDescending(item => item.CreatedAt).FirstOrDefault())
               .ToListAsync();

            if (entities.Equals(0))
            {
                Debug.WriteLine("there are not any polls");
            }

            //new PollSummaryDTO
            //var Dtos = entities
            //    .Select(b => new PollSummaryDTO(b.Id,
            //                                b.Question, 
            //                                b.CreatedAt))
            //    .ToList();

            return entities; // Dtos;
        }

        public async Task<PollSummaryDTO> GetOnePoll(Guid id)
        {

        }

        //PollCreatedResponseDTO   CreatePollRequestDTO
        public async Task<Poll> CreatePoll(Poll request)
        {

        }
    }
}
