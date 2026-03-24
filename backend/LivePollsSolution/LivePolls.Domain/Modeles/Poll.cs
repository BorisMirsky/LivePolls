
using System.ComponentModel.DataAnnotations;


namespace LivePolls.Domain.Modeles
{
    public class Poll
    {
        public Guid Id { get; set; }   
        public Guid CreatorId { get; set; }   
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? EndDate { get; set; } //= DateTime.UtcNow;
        public Boolean? IsActive { get; set; } = false;
        public string Question { get; set; } = String.Empty;
        public List<PollOption> Options { get; set; } = [];
        //public ICollection<PollOption> Options { get; set; } = new List<PollOption>();
    }
}
//string Question,List<string>? Options,DateTime? EndDate,Guid CreatorId

