
using LivePolls.Domain.Abstractions;
using LivePolls.Domain.Modeles;
using LivePolls.Application.Services;
using Microsoft.AspNetCore.Mvc;



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


        [Route("GetPolls")]
        [HttpGet]
        public async Task<ActionResult<List<Poll>>> GetPolls()
        {
            List<Poll> polls = await _pollsService.GetPolls();
            if (polls != null)
            {
                return Ok(polls);
            }

            return BadRequest(new { message = "there'are not any polls" });
        }


        [Route("GetOnePoll")]
        [HttpGet]
        public async Task<ActionResult<Poll>> GetOnePoll([FromQuery]  Guid id)
        {

            Poll p = await _pollsService.GetOnePoll(id);        
            if (p != null)
            {
                return Ok(p);
            }
            return BadRequest(new { message = "Poll is not recognized" });
        }


        [Route("CreatePoll")]
        [HttpPost]
        public async Task<ActionResult<Poll>> CreatePoll([FromBody] CreatePollRequestDTO request)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _pollsService.CreatePoll(request); 
            return Ok();

        }
    }
}
