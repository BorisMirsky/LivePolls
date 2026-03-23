

namespace LivePolls.Domain.Modeles
{
    public class PollOption
    {
        public Guid Id { get; set; }
        public Guid PollId { get; set; }
        public string Text { get; set; } = String.Empty;
        public int Order { get; set; } = 0;
        public Poll? Poll { get; set; }
    }
}
