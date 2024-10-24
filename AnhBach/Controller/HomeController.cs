using Microsoft.AspNetCore.Mvc;
using MediatR;
using BookService.Features.Books.Dtos;
using BookService.Features.Books.Queries;
using BookService.Features.Books.Commands;

namespace AnhBach.Controller


{
	[ApiController]
	[Route("api/[controller]")]
	public class BookController : ControllerBase
	{
		//Đây là interface do MediatR cung cấp, đóng vai trò là một mediator (trung gian) để gửi các request từ controller đến handler tương ứng.
		//Thay vì gọi trực tiếp các service hoặc repository từ controller, bạn sử dụng _mediator để gửi yêu cầu.
		private readonly IMediator _mediator;
		//_mediator là một đối tượng sẽ được sử dụng để gửi các yêu cầu từ controller đến các handler tương ứng.

		public BookController(IMediator mediator)
		{
			_mediator = mediator;
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> GetBookById(int id)
		{
			// MEdiatR gồm request và handler xử lý logic tìm sách, và các thông tin trả về cho user
			var query = new GetBookByIdQuery(id);

			// Nhờ trung gian(_mediator) gửi request đến handler và nhận kết quả ở book
			// Trong kiến trúc MediatR, phương thức Send của IMediator thực sự thay thế cho việc gọi trực tiếp
			// phương thức Handle trong handler. 
			var book = await _mediator.Send(query);

			// Logic trả về kết quả cho user
			if (book == null)
			{
				return NotFound();
			}

			return Ok(book);


			// Kết thúc HttpGet, có thể thấy trong phần này, ta không cần gọi DB,
			// mà logic này được xử lý trong GetBookQuery.cs (request, handler)
		}

		[HttpPost]
		public async Task<IActionResult> CreateBook([FromBody] CreateBookCommand command)
		{
			if (command == null) return BadRequest();

			// Gửi yêu cầu tạo sách
			var bookDto = await _mediator.Send(command);

			return CreatedAtAction(nameof(GetBookById), new { id = bookDto.Id }, bookDto);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookCommand command)
		// Phải nhập id 2 lần cho khớp
		{
			if (command == null || command.Id != id) return BadRequest();

			// Gửi yêu cầu cập nhật sách
			var updatedBook = await _mediator.Send(command);

			if (updatedBook == null) return NotFound(); // Nếu không tìm thấy sách

			return Ok(updatedBook); // Trả về thông tin sách đã cập nhật
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBook(int id)
		{
			var command = new DeleteBookCommand (id);

			// Gửi yêu cầu xóa quyển sách
			var result = await _mediator.Send(command);

			if (!result) return NotFound(); // Nếu không tìm thấy sách để xóa

			return NoContent(); // Trả về trạng thái No Content nếu xóa thành công
		}

	}
}
