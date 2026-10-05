using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class LopHoc
{
    [Key]
    [Required(ErrorMessage = "Mã lớp không được để trống.")]
    public string MaLop { get; set; }

    [Required(ErrorMessage = "Tên lớp không được để trống.")]
    public string TenLop { get; set; }

    // Quan hệ 1 - n: Một lớp học chứa danh sách nhiều sinh viên
    public virtual ICollection<SinhVien> DanhSachSinhVien { get; set; } = new List<SinhVien>();

    public bool IsValid()
    {
        var context = new ValidationContext(this, serviceProvider: null, items: null);
        var results = new List<ValidationResult>();
        return Validator.TryValidateObject(this, context, results, true);
    }
}

public class SinhVien
{
    [Key]
    [Required(ErrorMessage = "Mã sinh viên không được để trống.")]
    [StringLength(10, MinimumLength = 3, ErrorMessage = "Mã sinh viên phải từ 3 đến 10 ký tự.")]
    public string MaSV { get; set; }

    [Required(ErrorMessage = "Họ tên không được để trống.")]
    public string HoTen { get; set; }

    [DataType(DataType.Date)]
    public DateTime NgaySinh { get; set; }

    public bool GioiTinh { get; set; } // True: Nam, False: Nữ

    [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
    public string DienThoai { get; set; }

    [EmailAddress(ErrorMessage = "Địa chỉ Email không hợp lệ.")]
    public string Email { get; set; }

    [Range(0.0, 10.0, ErrorMessage = "Điểm phải nằm trong khoảng từ 0.0 đến 10.0.")]
    public double Diem { get; set; }

    [Required(ErrorMessage = "Trạng thái không được để trống.")]
    public string TrangThai { get; set; }

    // Khóa ngoại liên kết tới LopHoc
    [Required(ErrorMessage = "Phải chọn lớp học cho sinh viên.")]
    public string MaLop { get; set; }
    public virtual LopHoc LopHoc { get; set; }

    public bool IsValid()
    {
        var context = new ValidationContext(this, serviceProvider: null, items: null);
        var results = new List<ValidationResult>();
        return Validator.TryValidateObject(this, context, results, true);
    }
}