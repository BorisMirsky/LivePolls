
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using LivePolls.DataAccess;
using LivePolls.Domain.Modeles;
using LivePolls.Domain.Abstractions;



public class VoteHub : Hub
{

    private readonly AppDbContext _context;

    public VoteHub(AppDbContext context)
    {
        _context = context;
    }


    /// <summary>
    /// Пользователь вступает в группу опроса. При вступлении отправляем текущие результаты.
    /// </summary>
    /// <param name="pollId">ID опроса</param>
    /// <param name="userName">Имя пользователя (введённое на клиенте)</param>
    public async Task JoinPollGroup(Guid pollId, string userName)
    {
        // Добавляем соединение в группу SignalR
        await Groups.AddToGroupAsync(Context.ConnectionId, pollId.ToString());

        // Отправляем текущие результаты опроса новому участнику
        var results = await GetPollResults(pollId);
        await Clients.Caller.SendAsync("PollResults", results);
    }


    /// <summary>
    /// Голосование за вариант ответа.
    /// </summary>
    /// <param name="pollId">ID опроса</param>
    /// <param name="optionId">ID выбранного варианта</param>
    /// <param name="userName">Имя пользователя</param>
    public async Task Vote(Guid pollId, Guid optionId, Guid CreatorId ) //string userName)
    {
        // Проверяем, существует ли опрос и вариант
        var poll = await _context.Polls
            .Include(p => p.Options)
            .FirstOrDefaultAsync(p => p.Id == pollId); // && p.IsActive);
        if (poll == null)
            throw new HubException("Опрос не найден или уже закрыт");

        var option = poll.Options.FirstOrDefault(o => o.Id == optionId);
        if (option == null)
            throw new HubException("Вариант ответа не найден");

        // Проверяем, не голосовал ли уже пользователь в этом опросе
        var existingVote = await _context.Polls //Votes
            .FirstOrDefaultAsync(p => p.Id == pollId && p.CreatorId == CreatorId);
        if (existingVote != null)
            throw new HubException("Вы уже голосовали в этом опросе");

        // Используем транзакцию для атомарного обновления счётчика и сохранения голоса
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Увеличиваем счётчик голосов варианта
            //option.VoteCount++;
            _context.PollOptions.Update(option);

            // Сохраняем голос             ЭТО МОДЕЛЬ
            var vote = new Vote
            {
                PollId = pollId,
                OptionId = optionId,
                //UserName = userName
            };
            //_context.Votes.Add(vote);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (DbUpdateException)
        {
            await transaction.RollbackAsync();
            throw new HubException("Не удалось сохранить голос. Возможно, вы уже голосовали.");
        }

        // Получаем обновлённые результаты и рассылаем всем в группе
        var updatedResults = await GetPollResults(pollId);
        await Clients.Group(pollId.ToString()).SendAsync("PollResults", updatedResults);
    }


    /// <summary>
    /// Вспомогательный метод: получает текущие результаты опроса (список вариантов с количеством голосов).
    /// </summary>
    private async Task<List<PollOptionResultDTO>> GetPollResults(Guid pollId)
    {
        var options = await _context.PollOptions
            .Where(o => o.Id == pollId)
            .Select(o => new PollOptionResultDTO
            (
                o.Id,
                o.Text,
                o.Order   //VoteCount
            ))
            .ToListAsync();

        return options;
    }
}

