using Azure.Core;
using Azure;
using BookService.Features.Books.Dtos;
using MediatR;
using BookService.BookContext;
using BookService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace BookService.Features.Books.Queries
{
	// Request (Query)
	public class GetBookByIdQuery : IRequest<Book> 
	// Là một phần của thư viện MediatR, sử dụng để định nghĩa một yêu cầu (request) mà khi được gửi đi, sẽ trả về một giá trị kiểu BookDto.
	{
		public int BookId { get; set; } // Attribute của GetBookByIdQuery 

		public GetBookByIdQuery(int bookId)
		{
			BookId = bookId;
		}
	}

	// class GetBookByIdHandler sẽ triển khai Interface RequestHandler
	public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, Book>
	// Cấu trúc IRequestHandler<TRequest, TResponse>
	{

		// Muốn trả về thông tin Book từ DB, nên attribute sẽ là context
		private readonly BookDBContext _context;

		// Inject BookDBContext qua constructor
		public GetBookByIdHandler(BookDBContext context)
		{
			_context = context;
		}

		// Hàm Handle trả về BookDto
		public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
		// 1 tham số là là request
		// 1 tham số là một CancellationToken, cho phép bạn hủy bỏ tác vụ nếu cần thiết,
		// chưa hiểu lắm nhưng đây là tham số cần thiết
		{
			// Sử dụng BookDBContext để lấy dữ liệu sách từ database
			// FindAsync là một phương thức trong Entity Framework Core (EF Core)
			// được sử dụng để tìm kiếm một thực thể (entity) theo khóa chính (primary key) trong cơ sở dữ liệu.
			var book = await _context.BookTable.FindAsync(request.BookId); // 

			// Kiểm tra nếu sách không tồn tại
			if (book == null) return null;

			// Chuyển đổi dữ liệu từ entity Book sang DTO BookDto, phải dùng DTO để tăng tính bảo mật
			var bookDto = new Book
			{
				Id = book.Id,
				Title = book.Title,
				Price = book.Price
			};

			return bookDto;
		}
	}

}
