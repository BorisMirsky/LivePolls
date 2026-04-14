using LivePolls.Domain.Abstractions;
using LivePolls.Domain.Modeles;

namespace LivePolls.Domain.Abstractions
{
    public interface IVoteHubService
    {

        // Проверяет существование опроса и возвращает его с вариантами
        Task<Poll?> GetPollWithOptionsAsync(Guid pollId);

        // Проверяет, голосовал ли уже пользователь в этом опросе
        Task<bool> HasUserVotedAsync(Guid pollId, Guid userId);

        // Обрабатывает голосование пользователя
        Task<Vote> ProcessVoteAsync(Guid pollId, Guid optionId, Guid userId);

        // Получает текущие результаты опроса
        Task<PollResultsDto> GetPollResultsAsync(Guid pollId);

        // Регистрирует подключение пользователя
        Task RegisterUserConnectionAsync(Guid userId, string connectionId, Guid? pollId = null);

        // Удаляет подключение пользователя
        Task UnregisterUserConnectionAsync(string connectionId);

    }
}