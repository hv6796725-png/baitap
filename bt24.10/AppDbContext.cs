using Microsoft.EntityFrameworkCore;

namespace QuanLySinhVienDAO
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Thay 'your_password' bằng mật khẩu MySQL trên máy bạn
                string connectionString = "Server=localhost;Database=QuanLySinhVienDb;User=root;Password=your_password;";
                optionsBuilder.UseMySQL(connectionString);
            }
        }
    }
}