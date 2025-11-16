using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVienApp
{
    /// <summary>
    /// Form chính cho Admin (MDI Parent)
    /// Cung cấp menu điều hướng đến các chức năng quản lý: Sách, Bạn đọc, Phiếu mượn, Thống kê, Phân quyền
    /// </summary>
    public partial class frmMainAdmin : MetroFramework.Forms.MetroForm
    {
        /// <summary>
        /// ID của Admin đang đăng nhập
        /// </summary>
        public static int ID;
        
        /// <summary>
        /// Thông báo hiển thị trên status bar (thông tin người dùng hoặc cảnh báo)
        /// </summary>
        public static string text;
        
        /// <summary>
        /// Constructor mặc định
        /// </summary>
        public frmMainAdmin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor với thông tin người dùng
        /// </summary>
        /// <param name="_ID">ID của Admin</param>
        /// <param name="_tenNguoiDung">Tên Admin</param>
        /// <param name="_biKhoa">Trạng thái khóa tài khoản (true = bị khóa, false = không bị khóa)</param>
        public frmMainAdmin(int _ID, string _tenNguoiDung, bool? _biKhoa)
        {
            ID = _ID; // Lưu ID Admin
            InitializeComponent();

            // Kiểm tra trạng thái tài khoản
            if (_biKhoa == true)
            {
                // Tài khoản bị khóa, hiển thị cảnh báo màu đỏ
                text = "Tài khoản của bạn đang bị khóa, vui lòng đến thư viện để được xử lý!";
                tslbThongTin.ForeColor = Color.Red;
            }
            else if (_biKhoa == false)
                // Tài khoản bình thường, hiển thị lời chào
                text = "Chào mừng: " + _tenNguoiDung;

        }

        /// <summary>
        /// Sự kiện Load form - được gọi khi form được hiển thị
        /// Khởi động timer và hiển thị form thông tin
        /// </summary>
        /// <param name="sender">Form này</param>
        /// <param name="e">Thông tin sự kiện Load</param>
        private void frmMainAdmin_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true; // Bật timer để cập nhật thời gian
            // Hiển thị form thông tin khi khởi động
            frmInfor frm = new frmInfor();
            frm.MdiParent = this; // Set form này là MDI Parent
            frm.Show();
        }

        /// <summary>
        /// Sự kiện Tick của timer - được gọi mỗi giây
        /// Cập nhật thông tin người dùng và thời gian hiện tại trên status bar
        /// </summary>
        /// <param name="sender">Timer</param>
        /// <param name="e">Thông tin sự kiện Tick</param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            tslbThongTin.Text = text; // Cập nhật thông tin người dùng
            tslbTimer.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss  "); // Cập nhật thời gian
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút Thông tin
        /// Đóng tất cả form con và mở form thông tin hệ thống
        /// </summary>
        private void btnInfor_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
                form.Close(); // Đóng tất cả form con
            frmInfor frm = new frmInfor();
            frm.MdiParent = this; // Set form này là MDI Parent
            frm.Show();
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút Cá nhân
        /// Đóng tất cả form con và mở form quản lý thông tin cá nhân
        /// </summary>
        private void btnCaNhan_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
                form.Close();
            frmCaNhan frm = new frmCaNhan(ID); // Truyền ID Admin
            frm.MdiParent = this;
            frm.Show();
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút Đăng xuất
        /// Đóng form này và quay lại form đăng nhập
        /// </summary>
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            frmDangNhap frm = new frmDangNhap();
            this.Hide(); // Ẩn form này
            frm.ShowDialog(); // Hiển thị form đăng nhập
            this.Close(); // Đóng form này
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút Sách
        /// Đóng tất cả form con và mở form xem danh sách sách
        /// </summary>
        private void btnSach_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
                form.Close();
            frmSach frm = new frmSach();
            frm.MdiParent = this;
            frm.Show();
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút Quản lý Sách
        /// Đóng tất cả form con và mở form quản lý sách (CRUD)
        /// </summary>
        private void btnQLSach_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
                form.Close();
            frmQuanLySach frm = new frmQuanLySach();
            frm.MdiParent = this;
            frm.Show();
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút Quản lý Bạn đọc
        /// Đóng tất cả form con và mở form quản lý bạn đọc
        /// </summary>
        private void btnQLBanDoc_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
                form.Close();
            frmQuanLyBanDoc frm = new frmQuanLyBanDoc();
            frm.MdiParent = this;
            frm.Show();
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút Quản lý Phiếu mượn
        /// Đóng tất cả form con và mở form quản lý phiếu mượn
        /// </summary>
        private void btnQLPhieuMuon_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
                form.Close();
            frmQuanLyPhieuMuon frm = new frmQuanLyPhieuMuon();
            frm.MdiParent = this;
            frm.Show();
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút Thống kê
        /// Đóng tất cả form con và mở form biểu đồ thống kê sách theo thể loại
        /// </summary>
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
                form.Close();
            frmCColumn_SachTheoTheLoai frm = new frmCColumn_SachTheoTheLoai();
            frm.MdiParent = this;
            frm.Show();
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút Phân quyền
        /// Đóng tất cả form con và mở form phân quyền tài khoản
        /// </summary>
        private void btnPhanQuyen_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
                form.Close();
            frmPhanQuyen frm = new frmPhanQuyen();
            frm.MdiParent = this;
            frm.Show();
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn icon Cá nhân (iconButton1)
        /// Đóng tất cả form con và mở form quản lý thông tin cá nhân
        /// </summary>
        private void iconButton1_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
                form.Close();
            frmCaNhan frm = new frmCaNhan();
            frm.MdiParent = this;
            frm.Show();
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút Trợ giúp
        /// Đóng tất cả form con và mở form trợ giúp
        /// </summary>
        private void btnTroGiup_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
                form.Close();
            frmTroGiup frm = new frmTroGiup();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
