using Microsoft.AspNetCore.SignalR;
using LivePolls.Domain.Abstractions;
using Microsoft.Extensions.Logging;

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
            try
            {
                // Проверяем существование опроса
                var poll = await _voteHubService.GetPollWithOptionsAsync(pollId);

                // Добавляем в группу SignalR
                await Groups.AddToGroupAsync(Context.ConnectionId, pollId.ToString());

                // Регистрируем подключение в БД
                await _voteHubService.RegisterUserConnectionAsync(userId, Context.ConnectionId, pollId);

                // Отправляем текущие результаты
                var results = await _voteHubService.GetPollResultsAsync(pollId);
                await Clients.Caller.SendAsync("PollResults", results);

                _logger.LogInformation("User {UserId} ({UserName}) joined poll {PollId}",
                    userId, userName, pollId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error joining poll group");
                throw new HubException(ex.Message);
            }
        }

        public async Task Vote(Guid pollId, Guid optionId, Guid userId)
        {
            try
            {
                // Обрабатываем голосование через сервис
                await _voteHubService.ProcessVoteAsync(pollId, optionId, userId);

                // Получаем обновленные результаты
                var updatedResults = await _voteHubService.GetPollResultsAsync(pollId);

                // Рассылаем всем в группе
                await Clients.Group(pollId.ToString()).SendAsync("PollResults", updatedResults);

                _logger.LogInformation("Vote processed successfully for user {UserId} in poll {PollId}",
                    userId, pollId);
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