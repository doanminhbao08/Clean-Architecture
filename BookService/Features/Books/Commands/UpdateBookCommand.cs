using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookService.Features.Books.Dtos;
using MediatR;
using BookService.BookContext;
using BookService.Entities;

namespace BookService.Features.Books.Commands
{



	public class UpdateBookCommand : IRequest<Book>
	{
		public int Id { get; set; } // ID của sách cần cập nhật
		public string Title { get; set; }
		public int Price { get; set; }

		public UpdateBookCommand(string title, int price)
		{
			Title = title;
			Price = price;
		}

		// Có thể thêm các thuộc tính khác nếu cần
	}
	public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, Book>
	{
		private readonly BookDBContext _context;

		public UpdateBookHandler(BookDBContext context)
		{
			_context = context;
		}

		public async Task<Book> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
		{

			//book naỳ là một record trong Table
			var book = await _context.BookTable.FindAsync(request.Id); 
			// FindAsync trả về một record thực trong table, tức là book không phải một object mới, mà là record
			if (book == null)
			{
				return null; // Hoặc ném ra ngoại lệ tùy thuộc vào yêu cầu
			}

			// Cập nhật thông tin sách
			book.Title = request.Title;
			book.Price = request.Price;

			// Lưu thay đổi vào cơ sở dữ liệu
			await _context.SaveChangesAsync(cancellationToken);

			// Chuyển đổi đối tượng Book sang BookDto để trả về
			return new Book
			{
				Id = book.Id,
				Title = book.Title,
				Price = book.Price

			};
		}
	}

}
