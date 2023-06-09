using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFW_Project.Models
{
	public class Customer_Movie
	{
		public int MovieID { get; set; }
		public Movie? Movie { get; set; }

		public int CustomerId { get; set; }
		public Customer? Customer { get; set; }

		public DateTime? DateRented { get; set; }
		public DateTime? DueDate { get; set; }
	}
}
