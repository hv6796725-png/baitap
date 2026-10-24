using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySinhVienDAO
{
    public class Book
    {
        [Key]
        public int BookId { get; set; } // Mã sách (Khóa chính)

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty; // Tiêu đề

        [StringLength(100)]
        public string Publisher { get; set; } = string.Empty; // Nhà xuất bản

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } // Giá thành

        public int Copies { get; set; } // Số bản sao tổng quan

        [StringLength(100)]
        public string Author { get; set; } = string.Empty; // Tác giả

        public int PublicationYear { get; set; } // Năm xuất bản

        // Khóa ngoại liên kết tới Category (1 Category - N Book)
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category? Category { get; set; }

        // Mối quan hệ 1 - Nhiều: 1 Book có nhiều BookCopy
        public virtual ICollection<BookCopy> BookCopies { get; set; } = new List<BookCopy>();
    }
}