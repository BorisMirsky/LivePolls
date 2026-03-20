
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using LivePolls.DataAccess;
using LivePolls.Domain.Models;

public class VoteHub : Hub
{

    private readonly AppDbContext _context;

    public PollHub(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Пользователь вступает в группу опроса. При вступлении отправляем текущие результаты.
    /// </summary>
    /// <param name="pollId">ID опроса</param>
    /// <param name="userName">Имя пользователя (введённое на клиенте)</param>
    public async Task JoinPollGroup(int pollId, string userName)
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
    public async Task Vote(int pollId, int optionId, string userName)
    {
        // Проверяем, существует ли опрос и вариант
        var poll = await _context.Polls
            .Include(p => p.Options)
            .FirstOrDefaultAsync(p => p.Id == pollId && p.IsActive);
        if (poll == null)
            throw new HubException("Опрос не найден или уже закрыт");

        var option = poll.Options.FirstOrDefault(o => o.Id == optionId);
        if (option == null)
            throw new HubException("Вариант ответа не найден");

        // Проверяем, не голосовал ли уже пользователь в этом опросе
        var existingVote = await _context.Votes
            .FirstOrDefaultAsync(v => v.PollId == pollId && v.UserName == userName);
        if (existingVote != null)
            throw new HubException("Вы уже голосовали в этом опросе");

        // Используем транзакцию для атомарного обновления счётчика и сохранения голоса
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Увеличиваем счётчик голосов варианта
            option.VoteCount++;
            _context.PollOptions.Update(option);

            // Сохраняем голос             ЭТО МОДЕЛЬ
            var vote = new Vote
            {
                PollId = pollId,
                OptionId = optionId,
                UserName = userName
            };
            _context.Votes.Add(vote);

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
    private async Task<List<PollOptionResult>> GetPollResults(int pollId)
    {
        var options = await _context.PollOptions
            .Where(o => o.PollId == pollId)
            .Select(o => new PollOptionResult
            {
                OptionId = o.Id,
                Text = o.Text,
                VoteCount = o.VoteCount
            })
            .ToListAsync();

        return options;
    }
}

/// <summary>
/// DTO для передачи результатов опроса клиенту.
/// </summary>
public class PollOptionResult
{
    public int OptionId { get; set; }
    public string Text { get; set; }
    public int VoteCount { get; set; }
}