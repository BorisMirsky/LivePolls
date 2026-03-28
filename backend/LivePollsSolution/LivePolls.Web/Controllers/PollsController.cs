using LivePolls.DataAccess;
using LivePolls.Domain.Abstractions;
using LivePolls.Domain.Modeles;
using LivePolls.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;




namespace LivePolls.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PollsController : ControllerBase
    {
        private readonly IPollsService _pollsService;

        public PollsController(IPollsService pollsService)
        {
            _pollsService = pollsService;
        }

        /// <summary>
        /// Получить список всех опросов (без деталей вариантов).
        /// </summary>
        [Route("GetPolls")]
        [HttpGet]
        //public async Task<ActionResult<List<PollSummaryDTO>>> GetPolls()
        public async Task<ActionResult<List<Poll>>> GetPolls()
        {
            var polls = await _pollsService.GetPolls();
            if (polls != null)
            {
                return Ok(polls);
            }

            return BadRequest(new { message = "there'are not any polls" });
            //var polls = await _context.Polls
            //    .Where(p => p.IsActive)
            //    .Select(p => new PollSummaryDTO
            //    (
            //        p.Id,
            //        p.Question,
            //        p.CreatedAt,
            //        p.Options.Count
            //    ))
            //    .ToListAsync();

            //return Ok(polls);
        }


        [Route("GetOnePoll")]
        [HttpGet]
        //public async Task<ActionResult<PollSummaryDTO>> GetOnePoll(Guid id)
        public async Task<ActionResult<Poll>> GetOnePoll([FromQuery]  Guid id)
        {
            Poll p = await _pollsService.GetOnePoll(id);
            
            if (p != null)
            {
                //Debug.WriteLine("");
                //Debug.WriteLine("");
                //Debug.WriteLine(p.Options[0]);
                //Debug.WriteLine("");
                //Debug.WriteLine("");
                return Ok(p);
            }

            return BadRequest(new { message = "Poll is not recognized" });
        }


        /// <summary>
        /// Создать новый опрос.
        /// </summary>
        /// <param name="request">Модель создания опроса</param>
        [Route("CreatePoll")]
        [HttpPost]
        //public async Task<ActionResult<CreatePollRequestDTO>> CreatePoll([FromBody] CreatePollRequestDTO request)
        public async Task<ActionResult<Poll>> CreatePoll([FromBody] CreatePollRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _pollsService.CreatePoll(request); //.CreatorId, request.Question);
            return Ok();


            //// Создаём опрос
            //var poll = new Poll
            //{
            //    Question = request.Question,
            //    CreatedAt = DateTime.UtcNow,
            //    IsActive = true
            //};

            //// Добавляем варианты ответов
            //foreach (var optionText in request.Options)
            //{
            //    poll.Options.Add(new PollOption { Text = optionText });
            //}

            //_context.Polls.Add(poll);
            //await _context.SaveChangesAsync();

            //// Возвращаем ID созданного опроса
            //return Ok(new PollCreatedResponseDTO (poll.Id ));
            //return Ok();
        }
    }
}
