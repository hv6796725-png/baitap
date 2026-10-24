using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVienDAO
{
    public class Category
    {
        [Key] // Khóa chính (Auto increment)
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; } = string.Empty;

        // Mối quan hệ 1 - Nhiều: 1 Category chứa nhiều Book
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
