using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFW_Project.Models
{
    public class MovieFeedback
    {
        public int Rating { get; set; }
        public string? Comments { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int MovieID { get; set; }
        public Movie Movie { get; set; }
    }
}
