using System;
using System.Linq;

namespace QuanLySinhVienDAO
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new AppDbContext())
            {
                // 1. Tự động tạo cơ sở dữ liệu và các bảng nếu chưa có
                context.Database.EnsureCreated();
                Console.WriteLine("Ket noi CSDL va tao bang thanh cong!");

                // 2. Thêm dữ liệu mẫu (nếu database chưa có Category nào)
                if (!context.Categories.Any())
                {
                    var category = new Category
                    {
                        Description = "Lap trinh & Cong nghe"
                    };

                    var book = new Book
                    {
                        Title = "Lap trinh C# co ban",
                        Publisher = "NXB Giao Duc",
                        Price = 150000,
                        Copies = 5,
                        Author = "Nguyen Van A",
                        PublicationYear = 2024,
                        Category = category
                    };

                    var bookCopy1 = new BookCopy
                    {
                        Status = "San sang",
                        Condition = "Moi",
                        Book = book
                    };

                    var bookCopy2 = new BookCopy
                    {
                        Status = "Cho muon",
                        Condition = "Moi",
                        Book = book
                    };

                    context.Categories.Add(category);
                    context.Books.Add(book);
                    context.BookCopies.AddRange(bookCopy1, bookCopy2);

                    context.SaveChanges(); // Lưu dữ liệu vào MySQL
                    Console.WriteLine("Da them du lieu mau thanh cong!");
                }

                // 3. Đọc dữ liệu từ CSDL ra màn hình Console
                Console.WriteLine("\n--- DANH SACH SACH TRONG CSDL ---");
                var books = context.Books.ToList();
                foreach (var b in books)
                {
                    Console.WriteLine($"ID: {b.BookId} | Tieu de: {b.Title} | Tac gia: {b.Author} | Gia: {b.Price} VNĐ");
                }
            }

            Console.WriteLine("\nAn phim bat ky de thoat...");
            Console.ReadKey();
        }
    }
}