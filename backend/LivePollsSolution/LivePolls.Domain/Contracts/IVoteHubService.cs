using LivePolls.Domain.Abstractions;
using LivePolls.Domain.Modeles;

namespace LivePolls.Domain.Abstractions
{
    public interface IVoteHubService
    {
        /// <summary>
        /// Проверяет существование опроса и возвращает его с вариантами
        /// </summary>
        Task<Poll?> GetPollWithOptionsAsync(Guid pollId);

        /// <summary>
        /// Проверяет, голосовал ли уже пользователь в этом опросе
        /// </summary>
        Task<bool> HasUserVotedAsync(Guid pollId, Guid userId);

        /// <summary>
        /// Обрабатывает голосование пользователя
        /// </summary>
        Task<Vote> ProcessVoteAsync(Guid pollId, Guid optionId, Guid userId);

        /// <summary>
        /// Получает текущие результаты опроса
        /// </summary>
        Task<PollResultsDto> GetPollResultsAsync(Guid pollId);

        /// <summary>
        /// Регистрирует подключение пользователя
        /// </summary>
        Task RegisterUserConnectionAsync(Guid userId, string connectionId, Guid? pollId = null);

        /// <summary>
        /// Удаляет подключение пользователя
        /// </summary>
        Task UnregisterUserConnectionAsync(string connectionId);
    }
}