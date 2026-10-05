using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Forms;

namespace quan_ly_sv
{
    public partial class Form1 : Form
    {
        // Giả lập cơ sở dữ liệu hoặc danh sách bộ nhớ tạm thời phục vụ form
        private List<LopHoc> listLop;
        private List<SinhVien> listSinhVien;

        public Form1()
        {
            InitializeComponent();
            KhoaDuLieuGia();
        }

        private void KhoaDuLieuGia()
        {
            // Dữ liệu mẫu để test
            listLop = new List<LopHoc>()
            {
                new LopHoc { MaLop = "CNTT1", TenLop = "Công nghệ thông tin 1" },
                new LopHoc { MaLop = "CNTT2", TenLop = "Công nghệ thông tin 2" }
            };

            listSinhVien = new List<SinhVien>()
            {
                new SinhVien { MaSV = "SV001", HoTen = "Nguyễn Văn A", NgaySinh = new DateTime(2004, 1, 1), GioiTinh = true, DienThoai = "0901234567", Email = "a@gmail.com", Diem = 8.5, TrangThai = "Đang học", MaLop = "CNTT1" },
                new SinhVien { MaSV = "SV002", HoTen = "Trần Thị B", NgaySinh = new DateTime(2004, 5, 2), GioiTinh = false, DienThoai = "0907654321", Email = "b@gmail.com", Diem = 7.0, TrangThai = "Đang học", MaLop = "CNTT2" }
            };
        }

        // 3. Khi FormLoad
        private void Form1_Load(object sender, EventArgs e)
        {
            // a. Thiết lập thứ tự tab điều khiển và con trỏ mặc định tại txtMaSV, cấu hình trạng thái ban đầu nút
            ThietLapTabOrderVaTrangThaiBanDau();

            // b. Lấy về danh sách lớp học hiển thị lên ComboBox
            LoadComboBoxLop();

            // c. Lấy về danh sách sinh viên hiển thị lên DataGridView
            LoadDataGridViewSinhVien();
        }

        private void ThietLapTabOrderVaTrangThaiBanDau()
        {
            // Đặt thứ tự TabIndex theo đúng yêu cầu từ trên xuống dưới, trái qua phải trên groupBox1
            txtMaSV.TabIndex = 0;
            dtpNgaySinh.TabIndex = 1;
            // (TextBox Email được gán qua biến ẩn trong Designer hoặc bạn có thể sắp xếp lại TabIndex thủ công tại đây nếu cần)

            txtMaSV.Focus();

            // Thiết lập trạng thái ban đầu của các Button (Chưa nhập mã hoặc mã chưa tồn tại)
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void LoadComboBoxLop()
        {
            cboLop.DataSource = listLop;
            cboLop.DisplayMember = "TenLop";
            cboLop.ValueMember = "MaLop";
        }

        private void LoadDataGridViewSinhVien()
        {
            dgvSinhVien.Rows.Clear();
            foreach (var sv in listSinhVien)
            {
                dgvSinhVien.Rows.Add(sv.TrangThai, sv.MaSV, sv.HoTen, sv.NgaySinh.ToString("dd/MM/yyyy"), sv.GioiTinh ? "Nam" : "Nữ", sv.DienThoai, sv.Diem, sv.MaLop);
            }
            label15.Text = $"Tổng số: {listSinhVien.Count} sinh viên";
        }

        // 4. Khi người dùng nhập mã sinh viên (Sự kiện TextChanged của txtMaSV)
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string maSV = txtMaSV.Text.Trim();
            SinhVien svTimDuoc = listSinhVien.Find(s => s.MaSV.Equals(maSV, StringComparison.OrdinalIgnoreCase));

            if (svTimDuoc != null)
            {
                // Nếu mã sinh viên tồn tại: hiển thị thông tin lên các điều khiển, disable chức năng nhập (Thêm), enable sửa, xóa
                txtHoTen.Text = svTimDuoc.HoTen;
                dtpNgaySinh.Value = svTimDuoc.NgaySinh;
                if (svTimDuoc.GioiTinh) radNam.Checked = true; else radNu.Checked = true;
                // txtEmail.Text = svTimDuoc.Email; // nếu cần liên kết
                txtSDT.Text = svTimDuoc.DienThoai;
                numDiem.Value = (decimal)svTimDuoc.Diem;
                cboLop.SelectedValue = svTimDuoc.MaLop;
                cboTrangThai.Text = svTimDuoc.TrangThai;

                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
            else
            {
                // Chưa tồn tại: xóa giá trị các textbox khác, enable chức năng nhập (Thêm), disable sửa, xóa
                ClearInputControls(false); // false nghĩa là giữ lại txtMaSV đang gõ
                btnThem.Enabled = true;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }

        // 5. Khi người dùng nhấn Button Làm mới
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInputControls(true); // Xóa trắng toàn bộ kể cả mã SV
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtMaSV.Focus();
        }

        private void ClearInputControls(bool clearMaSV)
        {
            if (clearMaSV) txtMaSV.Clear();
            txtHoTen.Clear();
            txtSDT.Clear();
            radNam.Checked = true;
            numDiem.Value = 0;
            if (cboLop.Items.Count > 0) cboLop.SelectedIndex = 0;
            if (cboTrangThai.Items.Count > 0) cboTrangThai.SelectedIndex = 0;
        }

        // 6. Khi người dùng thực hiện các chức năng nguy hiểm (Sửa, Xóa) cần xác thực trước khi thực hiện
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này không?", "Xác thực hành động nguy hiểm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                // Thực hiện logic xóa ở đây
                string maSV = txtMaSV.Text.Trim();
                listSinhVien.RemoveAll(s => s.MaSV == maSV);
                LoadDataGridViewSinhVien();
                btnLamMoi_Click(sender, e);
                MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn cập nhật thông tin sinh viên này?", "Xác thực", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                // Kiểm tra validate trước khi lưu
                SinhVien sv = new SinhVien
                {
                    MaSV = txtMaSV.Text,
                    HoTen = txtHoTen.Text,
                    NgaySinh = dtpNgaySinh.Value,
                    GioiTinh = radNam.Checked,
                    DienThoai = txtSDT.Text,
                    Diem = (double)numDiem.Value,
                    TrangThai = cboTrangThai.Text,
                    MaLop = cboLop.SelectedValue?.ToString()
                };

                if (!sv.IsValid())
                {
                    MessageBox.Show("Dữ liệu không hợp lệ, vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Thực hiện cập nhật dữ liệu...
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Các sự kiện phát sinh từ giao diện thiết kế sẵn
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void groupBox2_Enter(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}