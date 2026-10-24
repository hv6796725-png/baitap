using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySinhVienDAO
{
    public class BookCopy
    {
        [Key]
        public int BookCopyId { get; set; } // Mã bản sao (Khóa chính)

        [StringLength(50)]
        public string Status { get; set; } = string.Empty; // Trạng thái (VD: Cho mượn, Sẵn sàng, Mất...)

        [StringLength(100)]
        public string Condition { get; set; } = string.Empty; // Tình trạng (VD: Mới, Cũ, Rách, Hỏng...)

        // Khóa ngoại liên kết tới Book (1 Book - N BookCopy)
        public int BookId { get; set; }
        [ForeignKey(nameof(BookId))]
        public virtual Book? Book { get; set; }
    }
}