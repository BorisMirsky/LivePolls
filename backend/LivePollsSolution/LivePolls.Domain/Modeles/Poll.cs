
using System.ComponentModel.DataAnnotations;


namespace LivePolls.Domain.Modeles
{
    public class Poll
    {
        public int Id { get; set; }   //Guid 
        public int CreatorId { get; set; }   //Guid 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Boolean IsActive { get; set; } = false;
        public string Question { get; set; } = String.Empty;
        public string[] Responses { get; set; } = [];
        public ICollection<PollOption> Options { get; set; } = new List<PollOption>();
    }
}

