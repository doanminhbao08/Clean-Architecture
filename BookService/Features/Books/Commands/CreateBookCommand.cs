using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookService.Features.Books.Dtos;
using MediatR;
using BookService.BookContext;
using BookService.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookService.Features.Books.Commands
{
	public class CreateBookCommand : IRequest<Book>
	{
		public string Title { get; set; }
		public int Price { get; set; }

        public CreateBookCommand(string title, int price)
        {
				Title = title;
				Price = price;	
        }

    }

	public class CreateBookHandler : IRequestHandler<CreateBookCommand, Book>
	{
		private readonly BookDBContext _context; 

		public CreateBookHandler(BookDBContext context)
		{
			_context = context;
		}

		public async Task<Book> Handle(CreateBookCommand request, CancellationToken cancellationToken)
		{
			// Tạo một thực thể mới từ yêu cầu
			var book = new Book
			{
				Title = request.Title,
				Price = request.Price

				// Thêm các thuộc tính khác nếu cần
			};

			// Lưu vào cơ sở dữ liệu
			await _context.BookTable.AddAsync(book); // Thêm đối tượng vào DbSet (tự động thêm id)
			await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
			return book;
		}
	}

}
