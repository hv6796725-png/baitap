using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using QuanLySinhVien.Data.DAL;
using QuanLySinhVien.Data.Entity;
using QuanLySinhVien.Data.Exceptions;

namespace QuanLySinhVien.Business
{
    // Ném BusinessException khi vi phạm nghiệp vụ.
    // DataAccessException từ tầng DAL được để nổi lên (không nuốt) cho tầng View xử lý.
    public class SinhVienBLL
    {
        private readonly SinhVienDAL sinhVienDAL = new SinhVienDAL();
        private readonly LopHocDAL lopHocDAL = new LopHocDAL();

        public List<SinhVien> GetAll()
        {
            return sinhVienDAL.GetAllSinhVien();
        }

        public SinhVien GetByMaSV(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV))
                throw new BusinessException("Mã sinh viên không được để trống.");

            SinhVien sv = sinhVienDAL.GetSinhVienByMaSV(maSV.Trim());
            if (sv == null)
                throw new BusinessException($"Không tìm thấy sinh viên có mã '{maSV}'.");
            return sv;
        }

        public List<SinhVien> GetByMaLop(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop))
                throw new BusinessException("Mã lớp không được để trống.");
            return sinhVienDAL.GetSinhViensByMaLop(maLop.Trim());
        }

        public void Add(SinhVien sv)
        {
            Validate(sv);
            if (sinhVienDAL.Exists(sv.MaSV))
                throw new BusinessException($"Mã sinh viên '{sv.MaSV}' đã tồn tại.");
            if (!lopHocDAL.Exists(sv.MaLop))
                throw new BusinessException($"Lớp '{sv.MaLop}' không tồn tại.");

            sinhVienDAL.AddSinhVien(sv);
        }

        public void Update(SinhVien sv)
        {
            Validate(sv);
            if (!sinhVienDAL.Exists(sv.MaSV))
                throw new BusinessException($"Không tìm thấy sinh viên có mã '{sv.MaSV}' để cập nhật.");
            if (!lopHocDAL.Exists(sv.MaLop))
                throw new BusinessException($"Lớp '{sv.MaLop}' không tồn tại.");

            sinhVienDAL.UpdateSinhVien(sv);
        }

        public void Delete(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV))
                throw new BusinessException("Vui lòng chọn sinh viên cần xóa.");
            if (!sinhVienDAL.Exists(maSV.Trim()))
                throw new BusinessException($"Không tìm thấy sinh viên có mã '{maSV}'.");

            sinhVienDAL.DeleteSinhVien(maSV.Trim());
        }

        // Kiểm tra dữ liệu đầu vào, chuẩn hóa khoảng trắng
        private void Validate(SinhVien sv)
        {
            if (sv == null)
                throw new BusinessException("Thông tin sinh viên không hợp lệ.");

            sv.MaSV = sv.MaSV?.Trim();
            sv.HoTen = sv.HoTen?.Trim();
            sv.MaLop = sv.MaLop?.Trim();
            sv.SoDienThoai = sv.SoDienThoai?.Trim();
            sv.Email = sv.Email?.Trim();

            if (string.IsNullOrEmpty(sv.MaSV))
                throw new BusinessException("Mã sinh viên không được để trống.");
            if (string.IsNullOrEmpty(sv.HoTen))
                throw new BusinessException("Họ tên không được để trống.");
            if (string.IsNullOrEmpty(sv.MaLop))
                throw new BusinessException("Vui lòng chọn lớp.");
            if (sv.Diem < 0 || sv.Diem > 10)
                throw new BusinessException("Điểm phải nằm trong khoảng từ 0 đến 10.");
            if (!string.IsNullOrEmpty(sv.SoDienThoai) && !Regex.IsMatch(sv.SoDienThoai, @"^0\d{9,10}$"))
                throw new BusinessException("Số điện thoại phải bắt đầu bằng 0 và gồm 10-11 chữ số.");
            if (!string.IsNullOrEmpty(sv.Email) && !Regex.IsMatch(sv.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new BusinessException("Email không đúng định dạng.");
        }
    }
}
