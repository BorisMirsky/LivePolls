
using System.ComponentModel.DataAnnotations;


namespace LivePolls.Domain.Modeles
{
    public class Poll
    {
        public Guid Id { get; set; }   
        public Guid CreatorId { get; set; }   
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Boolean IsActive { get; set; } = false;
        public string Question { get; set; } = String.Empty;
        public Dictionary<string, int> Responses { get; set; } = new Dictionary<string, int>();
        //public string[] Responses { get; set; } = [];
        //public ICollection<PollOption> Options { get; set; } = new List<PollOption>();
    }
}

