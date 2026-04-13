
using LivePolls.Domain.Abstractions;
using LivePolls.Domain.Modeles;


namespace LivePolls.Application.Services
{
    public class PollsService : IPollsService
    {

        private readonly IPollsRepo _pollsRepo;

        public PollsService(IPollsRepo pollsRepo)
        {
               _pollsRepo = pollsRepo;
        }

        public async Task<List<Poll>> GetPolls()
        {
            return await _pollsRepo.GetPolls();
        }

        public async Task<Poll> GetOnePoll(Guid id)
        {
            //return await _pollsRepo.GetOnePoll(id);
            var poll = await _pollsRepo.GetOnePoll(id);
            if (poll != null)
            {
                foreach (var option in poll.Options)
                {
                    option.Order = await _pollsRepo.GetVoteCount(option.Id);
                }
            }
            return poll;
        }


        public async Task<PollDetailsDTO?> GetPollDetailsAsync(Guid pollId)
        {
            var poll = await _pollsRepo.GetOnePoll(pollId);
            if (poll == null) return null;

            // Получаем голоса для каждого варианта
            var optionsWithVotes = new List<PollOptionResultDTO>();
            var totalVotes = 0;

            foreach (var option in poll.Options)
            {
                var voteCount = await _pollsRepo.GetVoteCount(option.Id);
                totalVotes += voteCount;
                optionsWithVotes.Add(new PollOptionResultDTO(
                    OptionId: option.Id,
                    Text: option.Text,
                    VoteCount: voteCount,
                    Percentage: 0
                ));
            }

            // Пересчитываем проценты (создаём НОВЫЙ список, не изменяя старый)
            var finalOptions = new List<PollOptionResultDTO>();
            foreach (var option in optionsWithVotes)
            {
                var percentage = totalVotes > 0
                    ? (int)((double)option.VoteCount / totalVotes * 100)
                    : 0;
                finalOptions.Add(option with { Percentage = percentage });
            }

            return new PollDetailsDTO(
                Id: poll.Id,
                Question: poll.Question,
                IsActive: poll.IsActive ?? false,
                Options: finalOptions
            );
        }


        public async Task<Poll> CreatePoll(CreatePollRequestDTO request)
        {
            return await _pollsRepo.CreatePoll(request);
        }
    }
}

