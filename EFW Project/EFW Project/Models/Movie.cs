using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFW_Project.Models
{
	public class Movie
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public int Duration { get; set; }
		public string Rating { get; set; }

		public int ProducerID { get; set; }
		public Producer? Producer { get; set; }

		public List<Customer_Movie> MovieCustomers { get; set; }
	}
}
