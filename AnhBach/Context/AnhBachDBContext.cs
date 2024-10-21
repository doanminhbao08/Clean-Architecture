using AnhBach.Models;
using Microsoft.EntityFrameworkCore;

namespace AnhBach.Context
{
	public class AnhBachDBContext : DbContext
	{
		// ctor tab tab = tạo constructor nhanh
		public AnhBachDBContext(DbContextOptions options) : base(options)
		{

		}
		public DbSet<Book> Book { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=DESKTOP-7PPNR21\\MSSQLSERVER01;Initial Catalog=myapp_database;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=True");
		}
	}
}
