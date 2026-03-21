using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LivePolls.DataAccess;
using LivePolls.Domain.Modeles;
using LivePolls.Domain.Abstractions;



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
        public async Task<ActionResult<IEnumerable<PollSummaryDTO>>> GetPolls()
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


        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<PollSummaryDTO>>> GetOnePoll()
        //{
        //
        //}


        /// <summary>
        /// Создать новый опрос.
        /// </summary>
        /// <param name="request">Модель создания опроса</param>
        [HttpPost]
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
        }
    }




    // DTO для ответа со списком опросов
    //public class PollSummary
    //{
    //    public int Id { get; set; }
    //    public string Question { get; set; }
    //    public DateTime CreatedAt { get; set; }
    //    public int OptionsCount { get; set; }
    //}

    // DTO для запроса создания опроса
    //public class CreatePollRequest
    //{
    //    [Required]
    //    public string Question { get; set; }

    //    [Required]
    //    [MinLength(2, ErrorMessage = "Должно быть минимум 2 варианта ответа")]
    //    public List<string> Options { get; set; }
    //}

    // DTO для ответа с ID созданного опроса
    //public class PollCreatedResponse
    //{
    //    public int PollId { get; set; }
    //}
}
