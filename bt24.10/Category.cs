using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVienDAO
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; } // Mã thể loại (Khóa chính)

        [Required]
        [StringLength(250)]
        public string Description { get; set; } = string.Empty; // Mô tả

        // Mối quan hệ 1 - Nhiều: 1 Category có nhiều Book
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}