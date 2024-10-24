using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookService.Features.Books.Dtos
{
	public class BookDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public int Price { get; set; }

	}
}
