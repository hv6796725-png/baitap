using Microsoft.Data.SqlClient;
using QuanLySinhVien.Data.Exceptions;
using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLySinhVien.Data.DAL
{
    public class DataHelper
    {
        private readonly string connectionString =
            @"Server=LAPTOP-6SN65NCS\HUECTMDB; Database=QLSV; Integrated Security=True; TrustServerCertificate=true";

        // Đổi SqlException thành DataAccessException với thông báo dễ hiểu
        private DataAccessException Wrap(SqlException ex)
        {
            string msg;
            switch (ex.Number)
            {
                case 2627:
                case 2601:
                    msg = "Dữ liệu bị trùng khóa chính hoặc giá trị duy nhất.";
                    break;
                case 547:
                    msg = "Vi phạm ràng buộc khóa ngoại: dữ liệu đang được sử dụng hoặc mã liên quan không tồn tại.";
                    break;
                case -2:
                    msg = "Hết thời gian chờ khi truy vấn cơ sở dữ liệu.";
                    break;
                case -1:
                case 2:
                case 53:
                case 4060:
                case 18456:
                    msg = "Không thể kết nối đến cơ sở dữ liệu. Vui lòng kiểm tra lại máy chủ, tên database hoặc quyền đăng nhập.";
                    break;
                default:
                    msg = "Lỗi truy cập cơ sở dữ liệu: " + ex.Message;
                    break;
            }
            return new DataAccessException(msg, ex);
        }

        public int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex) { throw Wrap(ex); }
        }

        public object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex) { throw Wrap(ex); }
        }

        public DataTable GetDataTable(string sql, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    if (parameters != null) da.SelectCommand.Parameters.AddRange(parameters);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (SqlException ex) { throw Wrap(ex); }
        }
    }
}
