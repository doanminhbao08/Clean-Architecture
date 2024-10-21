using AnhBach.Models;
using Microsoft.AspNetCore.Mvc;
using System;

using AnhBach.Context;
using Microsoft.EntityFrameworkCore;

namespace AnhBach.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BookController : ControllerBase
	{
		private readonly AnhBachDBContext _context;

		public BookController(AnhBachDBContext context)
		{
			_context = context;
		}


		[HttpGet] //Read
		public ActionResult<IEnumerable<Book>> GetBooks()
		// ActionResult trả về kết quả của một hành động, nó sẽ trả về thông tin trạng thái HTTP: 200,400
		// và trả về dữ liệu mình muốn, trong case này là IEnumerable<Book>
		// IEnumerable có ý nghĩa là danh sách các đối tượng, nó giúp trả về toàn bộ record trong bảng Book
		// Book là tên Bảng trong DB được sử dụng EF, check file AnhBachDBContext.cs

		{
			// Khi gọi ToList, nó sẽ tạo một danh sách mới chứa tất cả các phần tử của tập hợp hoặc kết quả của truy vấn.
			var books = _context.Book_table.ToList(); 

			if (books == null || !books.Any())
			{
				return NotFound(); // Trả về mã 404 nếu không có sách
			}

			return Ok(books); // Trả về mã 200 cùng với danh sách sách
		}


		[HttpPost] // Create
		public async Task<ActionResult<Book>> PostBook(Book book)
		{
			// Kiểm tra xem đối tượng book có hợp lệ không
			if (book == null)
			{
				return BadRequest(); // Trả về mã trạng thái 400 nếu dữ liệu không hợp lệ
			}

			// Thêm sách vào DbSet
			_context.Book_table.Add(book);

			// Lưu thay đổi vào cơ sở dữ liệu
			await _context.SaveChangesAsync();

			// Trả về đối tượng sách vừa được thêm kèm theo mã trạng thái 201 (CreatedAtAction)
			return CreatedAtAction(nameof(PostBook), new { id = book.Id }, book); 
		}



		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateBook(int id, Book book)
		{

			// Trả về mã trạng thái 400 nếu dữ liệu không hợp lệ
			// Tức là id truyền vào để update khác với id trong URL
			if (id != book.Id)
			{
				return BadRequest();
			}

			// Dòng này để cập nhật record mới mà người dùng nhập vào
			// Nó biết để thay thế theo id vì đường link có id và nó chỉ thay thế đúng record đó chứ không phải đi duyệt
			_context.Entry(book).State = EntityState.Modified;  

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!BookExists(id))
				{
					return NotFound(); // Lỗi 404
				}
				else
				{
					throw; // Trả ra lỗi khác để debug
				}
			}

			return NoContent(); // Trả về mã trạng thái 204 No Content là OK
		}

		// Kiểm tra xem id được truyền vào có cái nào giống với các id trong DB không (e.Id)
		private bool BookExists(int id)
		{
			return _context.Book_table.Any(e => e.Id == id);  // Sai thì trả lỗi 404 là not found
			// e đại diện cho từng record trong Book_table
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBook(int id)
		{
			// Tìm quyển sách theo ID
			var book = await _context.Book_table.FindAsync(id);
			if (book == null)
			{
				return NotFound(); // Trả về mã trạng thái 404 nếu không tìm thấy quyển sách
			}

			_context.Book_table.Remove(book); // Xóa quyển sách
			await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu

			return NoContent(); // Trả về mã trạng thái 204 No Content
		}


	}
}

