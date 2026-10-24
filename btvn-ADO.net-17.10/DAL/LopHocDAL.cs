using Microsoft.Data.SqlClient;
using QuanLySinhVien.Data.Entity;
using QuanLySinhVien.Data.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuanLySinhVien.Data.DAL
{
    // Giả định entity LopHoc có: MaLop, TenLop
    public class LopHocDAL
    {
        private readonly DataHelper dataHelper = new DataHelper();

        private LopHoc MapRow(DataRow r)
        {
            return new LopHoc
            {
                MaLop = r["MaLop"].ToString(),
                TenLop = r["TenLop"].ToString()
            };
        }

        private List<LopHoc> MapTable(DataTable dt)
        {
            try
            {
                List<LopHoc> list = new List<LopHoc>();
                foreach (DataRow r in dt.Rows) list.Add(MapRow(r));
                return list;
            }
            catch (Exception ex) when (ex is FormatException || ex is InvalidCastException || ex is ArgumentException)
            {
                throw new DataAccessException("Dữ liệu lớp học trong cơ sở dữ liệu không đúng định dạng hoặc thiếu cột.", ex);
            }
        }

        public List<LopHoc> GetAllLopHoc()
        {
            return MapTable(dataHelper.GetDataTable("SELECT * FROM LopHoc"));
        }

        public LopHoc GetLopHocByMaLop(string maLop)
        {
            DataTable dt = dataHelper.GetDataTable(
                "SELECT * FROM LopHoc WHERE MaLop = @MaLop",
                new SqlParameter("@MaLop", maLop));
            List<LopHoc> list = MapTable(dt);
            return list.Count > 0 ? list[0] : null;
        }

        public bool Exists(string maLop)
        {
            object kq = dataHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM LopHoc WHERE MaLop = @MaLop",
                new SqlParameter("@MaLop", maLop));
            return Convert.ToInt32(kq) > 0;
        }

        public int CountSinhVien(string maLop)
        {
            object kq = dataHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM SinhVien WHERE MaLop = @MaLop",
                new SqlParameter("@MaLop", maLop));
            return Convert.ToInt32(kq);
        }

        public void AddLopHoc(LopHoc lop)
        {
            int rows = dataHelper.ExecuteNonQuery(
                "INSERT INTO LopHoc (MaLop, TenLop) VALUES (@MaLop, @TenLop)",
                new SqlParameter("@MaLop", lop.MaLop),
                new SqlParameter("@TenLop", (object)lop.TenLop ?? DBNull.Value));
            if (rows == 0)
                throw new DataAccessException("Không thể thêm lớp học vào cơ sở dữ liệu.");
        }

        public void UpdateLopHoc(LopHoc lop)
        {
            int rows = dataHelper.ExecuteNonQuery(
                "UPDATE LopHoc SET TenLop = @TenLop WHERE MaLop = @MaLop",
                new SqlParameter("@MaLop", lop.MaLop),
                new SqlParameter("@TenLop", (object)lop.TenLop ?? DBNull.Value));
            if (rows == 0)
                throw new DataAccessException($"Không tìm thấy lớp có mã '{lop.MaLop}' để cập nhật.");
        }

        public void DeleteLopHoc(string maLop)
        {
            int rows = dataHelper.ExecuteNonQuery(
                "DELETE FROM LopHoc WHERE MaLop = @MaLop",
                new SqlParameter("@MaLop", maLop));
            if (rows == 0)
                throw new DataAccessException($"Không tìm thấy lớp có mã '{maLop}' để xóa.");
        }
    }
}
