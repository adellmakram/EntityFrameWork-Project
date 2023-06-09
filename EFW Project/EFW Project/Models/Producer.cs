using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFW_Project.Models
{
	public class Producer
	{
		public int Id { get; set; }
		public string CompanyName { get; set; }
		public string Country { get; set; }

		public List<Movie> ProducerMovies { get; set; }
	}
}
