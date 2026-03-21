using LivePolls.DataAccess;
using LivePolls.Domain.Abstractions;
using LivePolls.Domain.Modeles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;



namespace LivePolls.Web.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class PollsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PollsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить список всех опросов (без деталей вариантов).
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<PollSummaryDTO>>> GetPolls()
        {
            var polls = await _context.Polls
                .Where(p => p.IsActive)
                .Select(p => new PollSummaryDTO
                (
                    p.Id,
                    p.Question,
                    p.CreatedAt,
                    p.Options.Count
                ))
                .ToListAsync();

            return Ok(polls);
        }


        [HttpGet]
        //public async Task<ActionResult<DoctorResponse>> GetDoctor(Guid id)
        public async Task<ActionResult<PollSummaryDTO> GetOnePoll(Guid id)
        {
            Poll p = await _doctorService.Get(id);

            if (p != null)
            {
                return Ok(p);
            }

            return BadRequest(new { message = "Poll is not recognized" });
        }


        /// <summary>
        /// Создать новый опрос.
        /// </summary>
        /// <param name="request">Модель создания опроса</param>
        [HttpPost]
        //public async Task<ActionResult<PollCreatedResponseDTO>> CreatePoll([FromBody] CreatePollRequestDTO request)
        public async Task<ActionResult<PollCreatedResponseDTO>> CreatePoll([FromBody] CreatePollRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Создаём опрос
            var poll = new Poll
            {
                Question = request.Question,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            // Добавляем варианты ответов
            foreach (var optionText in request.Options)
            {
                poll.Options.Add(new PollOption { Text = optionText });
            }

            _context.Polls.Add(poll);
            await _context.SaveChangesAsync();

            // Возвращаем ID созданного опроса
            return Ok(new PollCreatedResponseDTO (poll.Id ));
            //return Ok();
        }
    }
}
