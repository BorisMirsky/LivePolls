using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace LivePolls.Domain.Modeles
{
    public class Poll
    {
        public int Id { get; set; }   //Guid 
        public int CreatorId { get; set; }   //Guid 
        public DateTime CreatedAt { get; set; }
        public Boolean IsActive { get; set; } = false;
        public string Question { get; set; } = String.Empty;
        public string[] Responses { get; set; } = [];
    }
}

