using Microsoft.AspNetCore.SignalR;
using LivePolls.Domain.Abstractions;


namespace LivePolls.Web.Hubs
{
    public class VoteHub : Hub
    {

        private readonly IVoteHubService _voteHubService;
        private readonly ILogger<VoteHub> _logger;

        public VoteHub(IVoteHubService voteHubService, ILogger<VoteHub> logger) 
        {
            _voteHubService = voteHubService;
            _logger = logger;
        }


        public async Task JoinPollGroup(Guid pollId, Guid userId, string userName)
        {
            _logger.LogInformation("JoinPollGroup START: pollId={PollId}, userId={UserId}, userName={UserName}", pollId, userId, userName);

            try
            {
                var poll = await _voteHubService.GetPollWithOptionsAsync(pollId);
                _logger.LogInformation("Poll found: {PollQuestion}", poll?.Question);

                await Groups.AddToGroupAsync(Context.ConnectionId, pollId.ToString());
                _logger.LogInformation("Added to group");

                await _voteHubService.RegisterUserConnectionAsync(userId, Context.ConnectionId, pollId);
                _logger.LogInformation("Connection registered");

                var results = await _voteHubService.GetPollResultsAsync(pollId);
                await Clients.Caller.SendAsync("PollResults", results);
                _logger.LogInformation("Results sent to client");

                _logger.LogInformation("JoinPollGroup SUCCESS");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in JoinPollGroup");
                throw;
            }
        }


        public async Task Vote(Guid pollId, Guid optionId, Guid userId)
        {
            try
            {
                await _voteHubService.ProcessVoteAsync(pollId, optionId, userId);
                var updatedResults = await _voteHubService.GetPollResultsAsync(pollId);

                _logger.LogInformation("Sending results to group {PollId}, results: {@Results}", pollId, updatedResults);

                await Clients.Group(pollId.ToString()).SendAsync("PollResults", updatedResults);

                _logger.LogInformation("Vote processed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing vote");
                throw new HubException(ex.Message);
            }
        }


        public async Task GetCurrentResults(Guid pollId)
        {
            var results = await _voteHubService.GetPollResultsAsync(pollId);
            await Clients.Caller.SendAsync("PollResults", results);
        }


        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            try
            {
                await _voteHubService.UnregisterUserConnectionAsync(Context.ConnectionId);
                _logger.LogInformation("Connection {ConnectionId} disconnected", Context.ConnectionId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on disconnect");
            }

            await base.OnDisconnectedAsync(exception);
        }

    }
}