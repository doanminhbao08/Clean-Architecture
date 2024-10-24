using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookService.Features.Books.Dtos;
using MediatR;
using BookService.BookContext;

namespace BookService.Features.Books.Commands
{
	using MediatR;

	public class DeleteBookCommand : IRequest<bool>
	{
		public int _id { get; set; } // ID của quyển sách cần xóa

        public DeleteBookCommand(int id)
        {
			_id = id;
        }
    }

	public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, bool>
	{
		private readonly BookDBContext _context; // Sử dụng BookDBContext

		public DeleteBookHandler(BookDBContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
		{
			// Tìm quyển sách theo ID
			var book = await _context.BookTable.FindAsync(request._id);
			if (book == null)
			{
				return false; // Nếu không tìm thấy sách, trả về false
			}

			// Xóa quyển sách khỏi cơ sở dữ liệu
			_context.BookTable.Remove(book);
			await _context.SaveChangesAsync(cancellationToken); // Lưu thay đổi vào cơ sở dữ liệu

			return true; // Trả về true nếu xóa thành công
		}

	}
}
