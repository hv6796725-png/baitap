using System;
using System.Collections.Generic;
using QuanLySinhVien.Data.DAL;
using QuanLySinhVien.Data.Entity;
using QuanLySinhVien.Data.Exceptions;

namespace QuanLySinhVien.Business
{
    public class LopHocBLL
    {
        private readonly LopHocDAL lopHocDAL = new LopHocDAL();

        public List<LopHoc> GetAll()
        {
            return lopHocDAL.GetAllLopHoc();
        }

        public LopHoc GetByMaLop(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop))
                throw new BusinessException("Mã lớp không được để trống.");

            LopHoc lop = lopHocDAL.GetLopHocByMaLop(maLop.Trim());
            if (lop == null)
                throw new BusinessException($"Không tìm thấy lớp có mã '{maLop}'.");
            return lop;
        }

        public void Add(LopHoc lop)
        {
            Validate(lop);
            if (lopHocDAL.Exists(lop.MaLop))
                throw new BusinessException($"Mã lớp '{lop.MaLop}' đã tồn tại.");

            lopHocDAL.AddLopHoc(lop);
        }

        public void Update(LopHoc lop)
        {
            Validate(lop);
            if (!lopHocDAL.Exists(lop.MaLop))
                throw new BusinessException($"Không tìm thấy lớp có mã '{lop.MaLop}' để cập nhật.");

            lopHocDAL.UpdateLopHoc(lop);
        }

        public void Delete(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop))
                throw new BusinessException("Vui lòng chọn lớp cần xóa.");

            maLop = maLop.Trim();
            if (!lopHocDAL.Exists(maLop))
                throw new BusinessException($"Không tìm thấy lớp có mã '{maLop}'.");

            int soSV = lopHocDAL.CountSinhVien(maLop);
            if (soSV > 0)
                throw new BusinessException($"Không thể xóa lớp '{maLop}' vì còn {soSV} sinh viên.");

            lopHocDAL.DeleteLopHoc(maLop);
        }

        private void Validate(LopHoc lop)
        {
            if (lop == null)
                throw new BusinessException("Thông tin lớp học không hợp lệ.");

            lop.MaLop = lop.MaLop?.Trim();
            lop.TenLop = lop.TenLop?.Trim();

            if (string.IsNullOrEmpty(lop.MaLop))
                throw new BusinessException("Mã lớp không được để trống.");
            if (string.IsNullOrEmpty(lop.TenLop))
                throw new BusinessException("Tên lớp không được để trống.");
        }
    }
}
