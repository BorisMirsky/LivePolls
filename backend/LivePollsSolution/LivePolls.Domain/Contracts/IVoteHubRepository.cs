using LivePolls.Domain.Modeles;

namespace LivePolls.Domain.Abstractions
{
    public interface IVoteHubRepository
    {
        /// <summary>
        /// Получает опрос со всеми вариантами ответов
        /// </summary>
        Task<Poll?> GetPollWithOptionsAsync(Guid pollId);

        /// <summary>
        /// Проверяет, голосовал ли пользователь в опросе
        /// </summary>
        Task<bool> HasUserVotedAsync(Guid pollId, Guid userId);

        /// <summary>
        /// Добавляет голос и увеличивает счетчик варианта
        /// </summary>
        Task<Vote> AddVoteAsync(Guid pollId, Guid optionId, Guid userId);

        /// <summary>
        /// Регистрирует подключение пользователя
        /// </summary>
        Task AddUserConnectionAsync(Guid userId, string connectionId, Guid? pollId = null);

        /// <summary>
        /// Удаляет подключение пользователя
        /// </summary>
        Task RemoveUserConnectionAsync(string connectionId);

        /// <summary>
        /// Получает активные подключения к опросу
        /// </summary>
        Task<IEnumerable<UserConnection>> GetPollConnectionsAsync(Guid pollId);
    }
}