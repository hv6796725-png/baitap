using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySinhVienDAO
{
    public class Book
    {
        [Key] // Khóa chính
        public int BookId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        // Khóa ngoại liên kết đến Category
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category? Category { get; set; }
    }
}
