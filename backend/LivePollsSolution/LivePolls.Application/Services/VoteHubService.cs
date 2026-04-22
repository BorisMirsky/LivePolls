using LivePolls.Domain.Abstractions;
using LivePolls.Domain.Modeles;
using Microsoft.Extensions.Logging;

namespace LivePolls.Application.Services
{
    public class VoteHubService : IVoteHubService
    {

        private readonly IVoteHubRepository _repository;
        private readonly ILogger<VoteHubService> _logger;


        public VoteHubService(IVoteHubRepository repository, ILogger<VoteHubService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Poll?> GetPollWithOptionsAsync(Guid pollId)
        {
            var poll = await _repository.GetPollWithOptionsAsync(pollId);
            if (poll == null)
                throw new InvalidOperationException("Опрос не найден");

            if (poll.IsActive == false)
                throw new InvalidOperationException("Опрос закрыт");

            return poll;
        }


        public async Task<bool> HasUserVotedAsync(Guid pollId, Guid userId)
        {
            return await _repository.HasUserVotedAsync(pollId, userId);
        }


        public async Task<Vote> ProcessVoteAsync(Guid pollId, Guid optionId, Guid userId)
        {

            var poll = await _repository.GetPollWithOptionsAsync(pollId);
            if (poll == null)
                throw new InvalidOperationException("Опрос не найден");

            if (poll.IsActive == false)
                throw new InvalidOperationException("Опрос уже закрыт для голосования");

            var option = await _repository.GetPollOptionAsync(optionId);
            if (option == null)
                throw new InvalidOperationException("Вариант ответа не найден");

            var hasVoted = await _repository.HasUserVotedAsync(pollId, userId);
            if (hasVoted)
                throw new InvalidOperationException("Вы уже голосовали в этом опросе");

            if (poll.EndDate.HasValue && poll.EndDate.Value < DateTime.UtcNow)
                throw new InvalidOperationException("Время голосования истекло");

            option.Order++;
            await _repository.UpdatePollOptionAsync(option);

            var vote = new Vote
            {
                Id = Guid.NewGuid(),
                PollId = pollId,
                OptionId = optionId,
                UserId = userId,
                VotedAt = DateTime.UtcNow
            };

            return await _repository.AddVoteAsync(vote);
        }


        public async Task<PollResultsDto> GetPollResultsAsync(Guid pollId)
        {
            var poll = await _repository.GetPollWithOptionsAsync(pollId);
            if (poll == null)
                throw new InvalidOperationException("Опрос не найден");

            var options = poll.Options.OrderBy(o => o.Order).ToList();
            var totalVotes = options.Sum(o => o.Order);

            var optionResults = options.Select(o => new PollOptionResultDTO(
                o.Id,
                o.Text,
                o.Order,
                totalVotes > 0 ? (int)((double)o.Order / totalVotes * 100) : 0
            )).ToList();

            return new PollResultsDto(
                poll.Id,
                poll.Question,
                optionResults,
                totalVotes,
                poll.EndDate,
                poll.IsActive ?? false
            );
        }


        public async Task RegisterUserConnectionAsync(Guid userId, string connectionId, Guid? pollId = null)
        {
            var connection = new UserConnection
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ConnectionId = connectionId,
                PollId = pollId,
                ConnectedAt = DateTime.UtcNow,
                LastActivity = DateTime.UtcNow
            };

            await _repository.AddUserConnectionAsync(connection);
        }


        public async Task UnregisterUserConnectionAsync(string connectionId)
        {
            var connection = await _repository.GetUserConnectionAsync(connectionId);
            if (connection != null)
            {
                await _repository.RemoveUserConnectionAsync(connection);
            }
        }

    }
}