
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

        private async Task UpdatePollsStatusAsync()
        {
            var polls = await _pollsRepo.GetPolls();
            var now = DateTime.UtcNow;
            var updatedPolls = polls.Where(p => p.EndDate < now && p.IsActive == true).ToList();

            foreach (var poll in updatedPolls)
            {
                poll.IsActive = false;
                await _pollsRepo.UpdatePoll(poll);
            }
        }

        public async Task<List<Poll>> GetPolls()
        {
            await UpdatePollsStatusAsync(); 
            return await _pollsRepo.GetPolls();
        }

        public async Task<Poll?> GetOnePoll(Guid id)
        {
            await UpdatePollsStatusAsync(); 
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

        public async Task<Poll> CreatePoll(CreatePollRequestDTO request)
        {
            var pollId = Guid.NewGuid();
            var now = DateTime.UtcNow;

            var poll = new Poll
            {
                Id = pollId,
                CreatorId = Guid.NewGuid(),
                CreatedAt = now,
                Question = request.Question,
                IsActive = request.Lifespan > 0,
                EndDate = request.Lifespan > 0 ? now.AddDays(request.Lifespan) : now,
                Options = request.Options.Select(o => new PollOption
                {
                    Id = Guid.NewGuid(),
                    Text = o,
                    PollId = pollId
                }).ToList()
            };

            return await _pollsRepo.CreatePoll(poll);
        }

        public async Task<PollDetailsDTO?> GetPollDetailsAsync(Guid pollId)
        {
            await UpdatePollsStatusAsync(); 

            var poll = await _pollsRepo.GetOnePoll(pollId);
            if (poll == null) return null;

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

    }
}

