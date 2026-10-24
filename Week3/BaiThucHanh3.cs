#:property TargetFramework=net10.0-windows
#:property UseWindowsForms=true
#:property OutputType=WinExe
#:property PublishAot=false

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new Form1());
    }
}

class Form1 : Form
{
    private Label lblMaSV;
    private TextBox txtMaSV;
    private Label lblHoTen;
    private TextBox txtHoTen;
    private Label lblGioiTinh;
    private RadioButton radNam;
    private RadioButton radNu;
    private Label lblNgaySinh;
    private DateTimePicker dtpNgaySinh;
    private Label lblLop;
    private ComboBox cboLop;
    private Label lblDiem;
    private TextBox txtDiem;
    private Label lblDiaChi;
    private TextBox txtDiaChi;
    private Button btnNhap;
    private Button btnSua;
    private Button btnXoa;
    private Button btnLamMoi;
    private DataGridView dgvSinhVien;

    public Form1()
    {
        TaoGiaoDien();
        TaoSuKien();
        this.Load += Form1_Load;
    }

    private void TaoGiaoDien()
    {
        this.ClientSize = new Size(760, 545);
        this.Font = new Font("Segoe UI", 9F);
        this.MinimumSize = new Size(780, 590);
        this.Name = "Form1";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "Quản lý sinh viên";

        lblMaSV = new Label { AutoSize = true, Location = new Point(20, 25), Name = "lblMaSV", Text = "Mã SV:" };
        txtMaSV = new TextBox { Location = new Point(110, 22), Name = "txtMaSV", Size = new Size(140, 23), TabIndex = 0 };
        lblHoTen = new Label { AutoSize = true, Location = new Point(280, 25), Name = "lblHoTen", Text = "Họ tên:" };
        txtHoTen = new TextBox { Location = new Point(370, 22), Name = "txtHoTen", Size = new Size(300, 23), TabIndex = 1 };
        lblGioiTinh = new Label { AutoSize = true, Location = new Point(20, 62), Name = "lblGioiTinh", Text = "Giới tính:" };
        radNam = new RadioButton { AutoSize = true, Checked = true, Location = new Point(110, 60), Name = "radNam", TabIndex = 2, Text = "Nam" };
        radNu = new RadioButton { AutoSize = true, Location = new Point(190, 60), Name = "radNu", TabIndex = 3, Text = "Nữ" };
        lblNgaySinh = new Label { AutoSize = true, Location = new Point(280, 62), Name = "lblNgaySinh", Text = "Ngày sinh:" };
        dtpNgaySinh = new DateTimePicker { Format = DateTimePickerFormat.Short, Location = new Point(370, 60), Name = "dtpNgaySinh", Size = new Size(130, 23), TabIndex = 4 };
        lblLop = new Label { AutoSize = true, Location = new Point(20, 100), Name = "lblLop", Text = "Lớp:" };
        cboLop = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(110, 97), Name = "cboLop", Size = new Size(140, 23), TabIndex = 5 };
        lblDiem = new Label { AutoSize = true, Location = new Point(280, 100), Name = "lblDiem", Text = "Điểm:" };
        txtDiem = new TextBox { Location = new Point(370, 97), Name = "txtDiem", Size = new Size(80, 23), TabIndex = 6 };
        lblDiaChi = new Label { AutoSize = true, Location = new Point(20, 138), Name = "lblDiaChi", Text = "Địa chỉ:" };
        txtDiaChi = new TextBox { Location = new Point(110, 135), Name = "txtDiaChi", Size = new Size(560, 23), TabIndex = 7 };

        btnNhap = new Button { Location = new Point(20, 178), Name = "btnNhap", Size = new Size(95, 32), TabIndex = 8, Text = "Nhập" };
        btnSua = new Button { Enabled = false, Location = new Point(125, 178), Name = "btnSua", Size = new Size(95, 32), TabIndex = 9, Text = "Sửa" };
        btnXoa = new Button { Enabled = false, Location = new Point(230, 178), Name = "btnXoa", Size = new Size(95, 32), TabIndex = 10, Text = "Xóa" };
        btnLamMoi = new Button { Location = new Point(335, 178), Name = "btnLamMoi", Size = new Size(95, 32), TabIndex = 11, Text = "Làm mới" };

        dgvSinhVien = new DataGridView
        {
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
            AutoGenerateColumns = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            Location = new Point(20, 224),
            MultiSelect = false,
            Name = "dgvSinhVien",
            ReadOnly = true,
            RowHeadersVisible = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            Size = new Size(720, 300),
            TabIndex = 12
        };

        var colMaSV = new DataGridViewTextBoxColumn { DataPropertyName = "MaSV", FillWeight = 12F, HeaderText = "Mã SV", Name = "colMaSV", ReadOnly = true };
        var colHoTen = new DataGridViewTextBoxColumn { DataPropertyName = "HoTen", FillWeight = 24F, HeaderText = "Họ tên", Name = "colHoTen", ReadOnly = true };
        var colGioiTinh = new DataGridViewCheckBoxColumn { DataPropertyName = "GioiTinh", FillWeight = 10F, HeaderText = "Giới tính", Name = "colGioiTinh", ReadOnly = true };
        var colNgaySinh = new DataGridViewTextBoxColumn { DataPropertyName = "NgaySinh", FillWeight = 13F, HeaderText = "Ngày sinh", Name = "colNgaySinh", ReadOnly = true };
        colNgaySinh.DefaultCellStyle.Format = "dd/MM/yyyy";
        var colTenLop = new DataGridViewTextBoxColumn { DataPropertyName = "TenLop", FillWeight = 15F, HeaderText = "Lớp", Name = "colTenLop", ReadOnly = true };
        var colDiem = new DataGridViewTextBoxColumn { DataPropertyName = "Diem", FillWeight = 8F, HeaderText = "Điểm", Name = "colDiem", ReadOnly = true };
        var colDiaChi = new DataGridViewTextBoxColumn { DataPropertyName = "DiaChi", FillWeight = 18F, HeaderText = "Địa chỉ", Name = "colDiaChi", ReadOnly = true };
        dgvSinhVien.Columns.AddRange(new DataGridViewColumn[] { colMaSV, colHoTen, colGioiTinh, colNgaySinh, colTenLop, colDiem, colDiaChi });

        this.Controls.Add(lblMaSV);
        this.Controls.Add(txtMaSV);
        this.Controls.Add(lblHoTen);
        this.Controls.Add(txtHoTen);
        this.Controls.Add(lblGioiTinh);
        this.Controls.Add(radNam);
        this.Controls.Add(radNu);
        this.Controls.Add(lblNgaySinh);
        this.Controls.Add(dtpNgaySinh);
        this.Controls.Add(lblLop);
        this.Controls.Add(cboLop);
        this.Controls.Add(lblDiem);
        this.Controls.Add(txtDiem);
        this.Controls.Add(lblDiaChi);
        this.Controls.Add(txtDiaChi);
        this.Controls.Add(btnNhap);
        this.Controls.Add(btnSua);
        this.Controls.Add(btnXoa);
        this.Controls.Add(btnLamMoi);
        this.Controls.Add(dgvSinhVien);
    }

    private void TaoSuKien()
    {
        txtMaSV.TextChanged += txtMaSV_TextChanged;
        btnNhap.Click += btnNhap_Click;
        btnSua.Click += btnSua_Click;
        btnXoa.Click += btnXoa_Click;
        btnLamMoi.Click += btnLamMoi_Click;
        dgvSinhVien.CellClick += dgvSinhVien_CellClick;
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        cboLop.DataSource = DuLieu.LopHocs;
        cboLop.DisplayMember = "TenLop";
        cboLop.ValueMember = "MaLop";
        cboLop.SelectedIndex = -1;
        dtpNgaySinh.Value = new DateTime(2005, 1, 1);
        TaiLaiGridView();
        DatTrangThaiNut();
        txtMaSV.Focus();
    }

    private void DatTrangThaiNut()
    {
        bool tonTai = DuLieu.TimSinhVien(txtMaSV.Text.Trim()) != null;
        btnNhap.Enabled = !tonTai;
        btnSua.Enabled = tonTai;
        btnXoa.Enabled = tonTai;
    }

    private void TaiLaiGridView()
    {
        dgvSinhVien.DataSource = null;
        dgvSinhVien.DataSource = DuLieu.SinhViens;
    }

    private void HienThiSinhVien(SinhVien sv)
    {
        txtHoTen.Text = sv.HoTen;
        radNam.Checked = sv.GioiTinh;
        radNu.Checked = !sv.GioiTinh;
        dtpNgaySinh.Value = sv.NgaySinh;
        cboLop.SelectedValue = sv.MaLop;
        txtDiem.Text = sv.Diem?.ToString();
        txtDiaChi.Text = sv.DiaChi;
    }

    private void XoaCacDieuKhienKhac()
    {
        txtHoTen.Clear();
        txtDiem.Clear();
        txtDiaChi.Clear();
        radNam.Checked = true;
        dtpNgaySinh.Value = new DateTime(2005, 1, 1);
        cboLop.SelectedIndex = -1;
    }

    private void LamMoiForm()
    {
        XoaCacDieuKhienKhac();
        txtMaSV.Clear();
        btnNhap.Enabled = true;
        btnSua.Enabled = false;
        btnXoa.Enabled = false;
        txtMaSV.Focus();
    }

    private void txtMaSV_TextChanged(object sender, EventArgs e)
    {
        SinhVien? sv = DuLieu.TimSinhVien(txtMaSV.Text.Trim());
        if (sv != null)
        {
            HienThiSinhVien(sv);
        }
        else
        {
            XoaCacDieuKhienKhac();
        }
        DatTrangThaiNut();
    }

    private void btnNhap_Click(object sender, EventArgs e)
    {
        if (!KiemTraDuLieu(out SinhVien sv, out string loi))
        {
            MessageBox.Show(loi, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (DuLieu.TimSinhVien(sv.MaSV) != null)
        {
            MessageBox.Show("Mã sinh viên đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        DuLieu.ThemSinhVien(sv);
        TaiLaiGridView();
        MessageBox.Show("Thêm sinh viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        LamMoiForm();
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        if (!XacNhan("Bạn có chắc chắn muốn sửa thông tin sinh viên này không?"))
        {
            return;
        }
        if (!KiemTraDuLieu(out SinhVien sv, out string loi))
        {
            MessageBox.Show(loi, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (DuLieu.TimSinhVien(sv.MaSV) == null)
        {
            MessageBox.Show("Mã sinh viên không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        DuLieu.SuaSinhVien(sv.MaSV, sv);
        TaiLaiGridView();
        MessageBox.Show("Cập nhật sinh viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        if (!XacNhan("Bạn có chắc chắn muốn xóa sinh viên này không?"))
        {
            return;
        }
        string maSV = txtMaSV.Text.Trim();
        if (DuLieu.TimSinhVien(maSV) == null)
        {
            MessageBox.Show("Mã sinh viên không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        DuLieu.XoaSinhVien(maSV);
        TaiLaiGridView();
        MessageBox.Show("Xóa sinh viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        LamMoiForm();
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        LamMoiForm();
    }

    private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }
        object? giaTri = dgvSinhVien.Rows[e.RowIndex].Cells["colMaSV"].Value;
        if (giaTri != null)
        {
            txtMaSV.Text = giaTri.ToString();
        }
    }

    private SinhVien LaySinhVienTuForm()
    {
        double? diem = null;
        if (double.TryParse(txtDiem.Text.Trim(), out double giaTri))
        {
            diem = giaTri;
        }
        return new SinhVien
        {
            MaSV = txtMaSV.Text.Trim(),
            HoTen = txtHoTen.Text.Trim(),
            GioiTinh = radNam.Checked,
            NgaySinh = dtpNgaySinh.Value,
            Diem = diem,
            MaLop = cboLop.SelectedValue?.ToString() ?? string.Empty,
            DiaChi = txtDiaChi.Text.Trim()
        };
    }

    private bool KiemTraDuLieu(out SinhVien sv, out string loi)
    {
        sv = LaySinhVienTuForm();
        loi = string.Empty;
        if (txtDiem.Text.Trim().Length > 0 && sv.Diem == null)
        {
            loi = "Điểm không hợp lệ";
            return false;
        }
        return sv.IsValid(out loi);
    }

    private bool XacNhan(string cauHoi)
    {
        return MessageBox.Show(cauHoi, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
    }
}

class SinhVien
{
    [Required(ErrorMessage = "Mã sinh viên không được để trống")]
    [StringLength(10, MinimumLength = 2, ErrorMessage = "Mã sinh viên phải có từ 2 đến 10 ký tự")]
    public string MaSV { get; set; } = string.Empty;

    [Required(ErrorMessage = "Họ tên không được để trống")]
    [StringLength(100, ErrorMessage = "Họ tên không được quá 100 ký tự")]
    public string HoTen { get; set; } = string.Empty;

    public bool GioiTinh { get; set; }

    public DateTime NgaySinh { get; set; }

    [Required(ErrorMessage = "Điểm không được để trống")]
    [Range(0, 10, ErrorMessage = "Điểm phải nằm trong khoảng từ 0 đến 10")]
    public double? Diem { get; set; }

    [Required(ErrorMessage = "Phải chọn lớp học")]
    public string MaLop { get; set; } = string.Empty;

    [StringLength(200, ErrorMessage = "Địa chỉ không được quá 200 ký tự")]
    public string DiaChi { get; set; } = string.Empty;

    public LopHoc? Lop { get; set; }

    public string TenLop => Lop?.TenLop ?? string.Empty;

    public bool IsValid(out string thongBao)
    {
        var ketQua = new List<ValidationResult>();
        var context = new ValidationContext(this);
        bool hopLe = Validator.TryValidateObject(this, context, ketQua, true);
        var sb = new StringBuilder();
        foreach (var item in ketQua)
        {
            sb.AppendLine(item.ErrorMessage);
        }
        if (NgaySinh < new DateTime(1900, 1, 1) || NgaySinh > DateTime.Now)
        {
            hopLe = false;
            sb.AppendLine("Ngày sinh phải từ năm 1900 đến nay");
        }
        thongBao = sb.ToString().Trim();
        return hopLe;
    }
}

class LopHoc
{
    [Required(ErrorMessage = "Mã lớp không được để trống")]
    [StringLength(10, MinimumLength = 2, ErrorMessage = "Mã lớp phải có từ 2 đến 10 ký tự")]
    public string MaLop { get; set; } = string.Empty;

    [Required(ErrorMessage = "Tên lớp không được để trống")]
    [StringLength(50, ErrorMessage = "Tên lớp không được quá 50 ký tự")]
    public string TenLop { get; set; } = string.Empty;

    [Required(ErrorMessage = "Khoa không được để trống")]
    [StringLength(50, ErrorMessage = "Khoa không được quá 50 ký tự")]
    public string Khoa { get; set; } = string.Empty;

    public List<SinhVien> SinhViens { get; set; } = new();

    public bool IsValid(out string thongBao)
    {
        var ketQua = new List<ValidationResult>();
        var context = new ValidationContext(this);
        bool hopLe = Validator.TryValidateObject(this, context, ketQua, true);
        var sb = new StringBuilder();
        foreach (var item in ketQua)
        {
            sb.AppendLine(item.ErrorMessage);
        }
        thongBao = sb.ToString().Trim();
        return hopLe;
    }
}

class DuLieu
{
    public static List<LopHoc> LopHocs { get; } = new()
    {
        new LopHoc { MaLop = "CNTT1", TenLop = "Công nghệ thông tin 1", Khoa = "Công nghệ thông tin" },
        new LopHoc { MaLop = "CNTT2", TenLop = "Công nghệ thông tin 2", Khoa = "Công nghệ thông tin" },
        new LopHoc { MaLop = "KT1", TenLop = "Kinh tế 1", Khoa = "Kinh tế" },
        new LopHoc { MaLop = "NN1", TenLop = "Ngoại ngữ 1", Khoa = "Ngoại ngữ" }
    };

    public static List<SinhVien> SinhViens { get; } = new()
    {
        new SinhVien { MaSV = "SV001", HoTen = "Nguyễn Văn An", GioiTinh = true, NgaySinh = new DateTime(2005, 1, 15), Diem = 8.5, MaLop = "CNTT1", DiaChi = "Hà Nội" },
        new SinhVien { MaSV = "SV002", HoTen = "Trần Thị Bình", GioiTinh = false, NgaySinh = new DateTime(2005, 3, 20), Diem = 9, MaLop = "CNTT1", DiaChi = "Hải Phòng" },
        new SinhVien { MaSV = "SV003", HoTen = "Lê Văn Cường", GioiTinh = true, NgaySinh = new DateTime(2004, 7, 10), Diem = 7.25, MaLop = "KT1", DiaChi = "Nghệ An" },
        new SinhVien { MaSV = "SV004", HoTen = "Phạm Thị Dung", GioiTinh = false, NgaySinh = new DateTime(2005, 11, 2), Diem = 6.75, MaLop = "NN1", DiaChi = "Huế" }
    };

    static DuLieu()
    {
        GanLopChoSinhVien();
    }

    public static void GanLopChoSinhVien()
    {
        foreach (var lop in LopHocs)
        {
            lop.SinhViens.Clear();
        }
        foreach (var sv in SinhViens)
        {
            sv.Lop = LopHocs.FirstOrDefault(l => l.MaLop == sv.MaLop);
            if (sv.Lop != null)
            {
                sv.Lop.SinhViens.Add(sv);
            }
        }
    }

    public static SinhVien? TimSinhVien(string maSV)
    {
        return SinhViens.FirstOrDefault(sv => sv.MaSV.Equals(maSV, StringComparison.OrdinalIgnoreCase));
    }

    public static void ThemSinhVien(SinhVien sv)
    {
        SinhViens.Add(sv);
        GanLopChoSinhVien();
    }

    public static void SuaSinhVien(string maSV, SinhVien svMoi)
    {
        SinhVien? svCu = TimSinhVien(maSV);
        if (svCu == null)
        {
            return;
        }
        svCu.HoTen = svMoi.HoTen;
        svCu.GioiTinh = svMoi.GioiTinh;
        svCu.NgaySinh = svMoi.NgaySinh;
        svCu.Diem = svMoi.Diem;
        svCu.MaLop = svMoi.MaLop;
        svCu.DiaChi = svMoi.DiaChi;
        GanLopChoSinhVien();
    }

    public static void XoaSinhVien(string maSV)
    {
        SinhVien? sv = TimSinhVien(maSV);
        if (sv != null)
        {
            SinhViens.Remove(sv);
            GanLopChoSinhVien();
        }
    }
}
