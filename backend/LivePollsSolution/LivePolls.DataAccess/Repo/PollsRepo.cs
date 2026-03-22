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

        public async Task<List<PollSummaryDTO>> GetPolls()
        {
            var entities = await _context.Polls
               //.Select(g => g.OrderByDescending(item => item.CreatedAt).FirstOrDefault())
               .ToListAsync();

            if (entities.Equals(0))
            {
                Debug.WriteLine("there are not any polls");
            }

            var Dtos = entities
                .Select(b => new BookingDTO(b.Id, b.DoctorId, b.PatientId,
                                            b.TimeslotId, b.IsBooked, b.IsClosed,
                                            b.CreatedAt,
                                            b.Doctor?.UserName,
                                            b.Doctor?.Speciality,
                                            b.Patient?.UserName,
                                            b.Timeslot?.DatetimeStart,
                                            b.Timeslot?.DatetimeStop))
                .ToList();

            return Dtos;
        }

        public async Task<PollSummaryDTO> GetOnePoll(Guid id){
        }

        public async Task<PollCreatedResponseDTO> CreatePoll(CreatePollRequestDTO request)
        {

        }
    }
}
