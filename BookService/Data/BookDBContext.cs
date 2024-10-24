using BookService.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookService.BookContext
{
	public class BookDBContext : DbContext // Class context thừa kế lớp DbContext của EFcore 
	{
		// ctor tab tab = tạo constructor nhanh
		public BookDBContext(DbContextOptions options) : base(options)
		{

		}
		// Tạm thời coi như đây là một câu lệnh bắt buộc

		public DbSet<Book> BookTable { get; set; } // Book_table là tên bản ở trong DB
		// Trong ASP.NET, bảng có kiểu dữ liệu là DB<T>, với T là thực thể mà bảng quản lý
		// Trong trường hợp này là entity Book trong folder Models


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=DESKTOP-7PPNR21\\MSSQLSERVER01;Initial Catalog=Clean_Architecture;Integrated Security=True;Pooling=False;Encrypt=False;Trust Server Certificate=False");
		} // Dòng này để khai báo server và database mà EF sử dụng
	}
}
