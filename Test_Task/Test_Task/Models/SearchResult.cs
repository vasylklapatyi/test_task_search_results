using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Task.Models
{
	public class SearchResult
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Url { get; set; }
		public string SearchEngine { get; set; }
		public DateTime TimeStamp { get; set; }
	}
}
