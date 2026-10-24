using Microsoft.Data.SqlClient;
using QuanLySinhVien.Data.Entity;
using QuanLySinhVien.Data.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuanLySinhVien.Data.DAL
{
    // Mọi phương thức ném DataAccessException khi có lỗi truy cập dữ liệu
    public class SinhVienDAL
    {
        private readonly DataHelper dataHelper = new DataHelper();

        private SinhVien MapRow(DataRow r)
        {
            return new SinhVien
            {
                MaSV = r["MaSV"].ToString(),
                HoTen = r["HoTen"].ToString(),
                GioiTinh = r["GioiTinh"].ToString(),
                NgaySinh = r["NgaySinh"].ToString(),
                SoDienThoai = r["SoDienThoai"].ToString(),
                MaLop = r["MaLop"].ToString(),
                Diem = r["Diem"] == DBNull.Value ? 0 : Convert.ToDouble(r["Diem"]),
                Email = r["Email"].ToString()
            };
        }

        private List<SinhVien> MapTable(DataTable dt)
        {
            try
            {
                List<SinhVien> list = new List<SinhVien>();
                foreach (DataRow r in dt.Rows) list.Add(MapRow(r));
                return list;
            }
            catch (Exception ex) when (ex is FormatException || ex is InvalidCastException || ex is ArgumentException)
            {
                throw new DataAccessException("Dữ liệu sinh viên trong cơ sở dữ liệu không đúng định dạng hoặc thiếu cột.", ex);
            }
        }

        public List<SinhVien> GetAllSinhVien()
        {
            return MapTable(dataHelper.GetDataTable("SELECT * FROM SinhVien"));
        }

        public SinhVien GetSinhVienByMaSV(string maSV)
        {
            DataTable dt = dataHelper.GetDataTable(
                "SELECT * FROM SinhVien WHERE MaSV = @MaSV",
                new SqlParameter("@MaSV", maSV));
            List<SinhVien> list = MapTable(dt);
            return list.Count > 0 ? list[0] : null;   // không tìm thấy -> null, BLL quyết định có ném lỗi hay không
        }

        public List<SinhVien> GetSinhViensByMaLop(string maLop)
        {
            DataTable dt = dataHelper.GetDataTable(
                "SELECT * FROM SinhVien WHERE MaLop = @MaLop",
                new SqlParameter("@MaLop", maLop));
            return MapTable(dt);
        }

        public bool Exists(string maSV)
        {
            object kq = dataHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM SinhVien WHERE MaSV = @MaSV",
                new SqlParameter("@MaSV", maSV));
            return Convert.ToInt32(kq) > 0;
        }

        public void AddSinhVien(SinhVien sv)
        {
            string sql = @"INSERT INTO SinhVien (MaSV, MaLop, HoTen, GioiTinh, NgaySinh, Diem, SoDienThoai, Email)
                           VALUES (@MaSV, @MaLop, @HoTen, @GioiTinh, @NgaySinh, @Diem, @SoDienThoai, @Email)";
            int rows = dataHelper.ExecuteNonQuery(sql, BuildParams(sv));
            if (rows == 0)
                throw new DataAccessException("Không thể thêm sinh viên vào cơ sở dữ liệu.");
        }

        public void UpdateSinhVien(SinhVien sv)
        {
            string sql = @"UPDATE SinhVien
                           SET HoTen = @HoTen, MaLop = @MaLop, GioiTinh = @GioiTinh, NgaySinh = @NgaySinh,
                               Diem = @Diem, SoDienThoai = @SoDienThoai, Email = @Email
                           WHERE MaSV = @MaSV";
            int rows = dataHelper.ExecuteNonQuery(sql, BuildParams(sv));
            if (rows == 0)
                throw new DataAccessException($"Không tìm thấy sinh viên có mã '{sv.MaSV}' để cập nhật.");
        }

        public void DeleteSinhVien(string maSV)
        {
            int rows = dataHelper.ExecuteNonQuery(
                "DELETE FROM SinhVien WHERE MaSV = @MaSV",
                new SqlParameter("@MaSV", maSV));
            if (rows == 0)
                throw new DataAccessException($"Không tìm thấy sinh viên có mã '{maSV}' để xóa.");
        }

        private SqlParameter[] BuildParams(SinhVien sv)
        {
            return new[]
            {
                new SqlParameter("@MaSV", sv.MaSV),
                new SqlParameter("@MaLop", (object)sv.MaLop ?? DBNull.Value),
                new SqlParameter("@HoTen", (object)sv.HoTen ?? DBNull.Value),
                new SqlParameter("@GioiTinh", (object)sv.GioiTinh ?? DBNull.Value),
                new SqlParameter("@NgaySinh", (object)sv.NgaySinh ?? DBNull.Value),
                new SqlParameter("@Diem", sv.Diem),
                new SqlParameter("@SoDienThoai", (object)sv.SoDienThoai ?? DBNull.Value),
                new SqlParameter("@Email", (object)sv.Email ?? DBNull.Value)
            };
        }
    }
}
