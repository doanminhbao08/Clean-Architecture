using System.ComponentModel.DataAnnotations;

namespace BookService.Entities

{
	public class Book
	{

		[Key]
		public int Id { get; set; }
		public string? Title { get; set; }

		public int Price { get; set; }

	}

}
