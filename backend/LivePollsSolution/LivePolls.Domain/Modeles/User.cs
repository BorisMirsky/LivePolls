using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace LivePolls.Domain.Modeles
{
    public class User
    {
        public Guid Id { get; set; }   //Guid 
        public string Name { get; set; } = String.Empty;
        public string? Login { get; set; } = String.Empty;
        public string? Password { get; set; } = String.Empty;

    }
}
