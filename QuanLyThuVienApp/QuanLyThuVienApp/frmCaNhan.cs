using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVienApp
{
    /// <summary>
    /// Form quản lý thông tin cá nhân
    /// Cho phép người dùng xem và chỉnh sửa thông tin cá nhân: tên, email, tên đăng nhập
    /// </summary>
    public partial class frmCaNhan : Form
    {
        /// <summary>
        /// ID của người dùng hiện tại
        /// </summary>
        public static int ID;

        /// <summary>
        /// Khởi tạo form thông tin cá nhân (không có ID)
        /// </summary>
        public frmCaNhan()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Khởi tạo form thông tin cá nhân với ID người dùng
        /// </summary>
        /// <param name="_ID">ID của người dùng</param>
        public frmCaNhan(int _ID)
        {
            ID = _ID;
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện Load form - Load thông tin cá nhân và ẩn các nút chỉnh sửa
        /// </summary>
        private void frmCaNhan_Load(object sender, EventArgs e)
        {
            loadDuLieu();
            // Ẩn các nút chỉnh sửa ban đầu
            btnLuuTen.Hide();
            btnLuuEmail.Hide();
            btnHuyEmail.Hide();
            btnHuyTen.Hide();
            btnLuuTenDN.Hide();
            btnHuyTenDN.Hide();
        }

        /// <summary>
        /// Load thông tin cá nhân từ database và hiển thị lên form
        /// Format ID: "DB" + ID cho user, "AD" + ID cho admin
        /// </summary>
        private void loadDuLieu()
        {
            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p=>p.ID == ID).FirstOrDefault();

            // Hiển thị thông tin cá nhân
            txtTenDangNhap.Text = nguoiDung.TenDangNhap.ToString();
            txtHoVaTen.Text = nguoiDung.HoTen;
            txtEmail.Text = nguoiDung.Email;
            txtSoSachMuon.Text = nguoiDung.SoSachMuon.ToString() + "/10";
            
            // Format ID theo quyền
            if (nguoiDung.QuyenHan == "user")
                txtID.Text = "DB" + nguoiDung.ID;
            else txtID.Text = "AD" + nguoiDung.ID;
        }

        /// <summary>
        /// Mở form đổi mật khẩu
        /// </summary>
        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            frmDoiMatKhau frm = new frmDoiMatKhau(ID);
            frm.ShowDialog();
        }

        /// <summary>
        /// Cho phép chỉnh sửa tên - Mở khóa textbox và hiển thị nút Lưu/Hủy
        /// </summary>
        private void btnDoiTen_Click(object sender, EventArgs e)
        {
            txtHoVaTen.ReadOnly = false;
            btnLuuTen.Show();
            btnHuyTen.Show();
        }

        /// <summary>
        /// Lưu thay đổi tên vào database
        /// Cập nhật text trên form chính (frmMainAdmin hoặc frmMainUser)
        /// </summary>
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra tên không được rỗng
            if (txtHoVaTen.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == ID).FirstOrDefault();

            // Cập nhật tên
            nguoiDung.HoTen = txtHoVaTen.Text;
            db.SaveChanges();
            
            // Khóa textbox và ẩn nút
            txtHoVaTen.ReadOnly = true;
            btnLuuTen.Hide();
            btnHuyTen.Hide();
            loadDuLieu();

            // Cập nhật text trên form chính
            if (nguoiDung.QuyenHan == "admin") frmMainAdmin.text = "Chào mừng: " + txtHoVaTen.Text;
            else if (nguoiDung.BiKhoa == false) frmMainUser.text = "Chào mừng: " + txtHoVaTen.Text;

            MessageBox.Show("Thay đổi tên thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Hủy thay đổi tên - Khôi phục giá trị ban đầu
        /// </summary>
        private void btnHuyTen_Click(object sender, EventArgs e)
        {
            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == ID).FirstOrDefault();
            // Khôi phục tên ban đầu
            txtHoVaTen.Text = nguoiDung.HoTen.ToString();
            btnHuyTen.Hide();
            btnLuuTen.Hide();
            txtHoVaTen.ReadOnly = true;
        }

        /// <summary>
        /// Cho phép chỉnh sửa email - Mở khóa textbox và hiển thị nút Lưu/Hủy
        /// </summary>
        private void btnDoiEmail_Click(object sender, EventArgs e)
        {
            btnLuuEmail.Show();
            btnHuyEmail.Show();
            txtEmail.ReadOnly = false;
        }

        /// <summary>
        /// Hủy thay đổi email - Khôi phục giá trị ban đầu
        /// </summary>
        private void btnHuyEmail_Click(object sender, EventArgs e)
        {
            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == ID).FirstOrDefault();
            // Khôi phục email ban đầu
            txtEmail.Text = nguoiDung.Email.ToString();
            btnHuyEmail.Hide();
            btnLuuEmail.Hide();
            txtEmail.ReadOnly = true;
        }

        /// <summary>
        /// Kiểm tra định dạng email có hợp lệ không
        /// </summary>
        /// <param name="inputEmail">Email cần kiểm tra</param>
        /// <returns>True nếu email hợp lệ, False nếu không hợp lệ</returns>
        private bool isEmail(string inputEmail)
        {
            inputEmail = inputEmail ?? string.Empty;
            // Regex pattern để kiểm tra định dạng email
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        /// <summary>
        /// Lưu thay đổi email - Gửi OTP để xác thực email mới
        /// Kiểm tra email hợp lệ, không trùng với email đã được xác thực
        /// </summary>
        private void btnLuuEmail_Click(object sender, EventArgs e)
        {
            // Kiểm tra email không được rỗng
            if (txtEmail.Text == "")
            {
                MessageBox.Show("Vui lòng nhập email!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra định dạng email
            if (!isEmail(txtEmail.Text))
            {
                MessageBox.Show("Email không hợp lệ!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            QLTVEntities db = new QLTVEntities();
            // Kiểm tra email đã được sử dụng chưa
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.Email == txtEmail.Text).FirstOrDefault();

            if (nguoiDung != null)
            {
                // Nếu email đã được xác thực thì không cho sử dụng
                if (nguoiDung.TrangThaiXacThuc == true)
                {
                    MessageBox.Show("Email đã được sử dụng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    // Xóa tài khoản chưa xác thực có email này
                    db.NguoiDungs.Remove(nguoiDung);
                    db.SaveChanges();
                }
            }

            nguoiDung = db.NguoiDungs.Where(p => p.ID == ID).FirstOrDefault();

            // Kiểm tra email mới phải khác email cũ
            if (txtEmail.Text == nguoiDung.Email)
            {
                MessageBox.Show("Cần nhập email mới!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tạo và gửi OTP
            Random random = new Random();
            string OTP = random.Next(100000, 999999).ToString();

            GuiEmail.guiEmail(txtEmail.Text, "Mã xác thực của bạn là " + OTP);

            // Lưu OTP và thời gian nhận OTP
            nguoiDung.MaOTP = OTP;
            nguoiDung.ThoiGianNhanOTP = DateTime.Now;
            db.SaveChanges();

            // Mở form xác thực OTP
            frmXacThuc frm = new frmXacThuc(ID, xacNhan =>
            {
                if (xacNhan)
                {
                    // Nếu xác thực thành công, cập nhật email
                    nguoiDung.Email = txtEmail.Text;
                    db.SaveChanges();

                    txtEmail.ReadOnly = true;
                    btnLuuEmail.Hide();
                    btnHuyEmail.Hide();
                    loadDuLieu();
                }
            });
            frm.ShowDialog();

            
        }

        /// <summary>
        /// Cho phép chỉnh sửa tên đăng nhập - Mở khóa textbox và hiển thị nút Lưu/Hủy
        /// </summary>
        private void btnDoiTenDangNhap_Click(object sender, EventArgs e)
        {
            btnLuuTenDN.Show();
            btnHuyTenDN.Show();
            txtTenDangNhap.ReadOnly = false;
        }

        /// <summary>
        /// Hủy thay đổi tên đăng nhập - Khôi phục giá trị ban đầu
        /// </summary>
        private void btnHuyTenDN_Click(object sender, EventArgs e)
        {
            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == ID).FirstOrDefault();
            // Khôi phục tên đăng nhập ban đầu
            txtTenDangNhap.Text = nguoiDung.TenDangNhap.ToString();
            btnHuyTenDN.Hide();
            btnLuuTenDN.Hide();
            txtTenDangNhap.ReadOnly = true;
        }

        /// <summary>
        /// Lưu thay đổi tên đăng nhập vào database
        /// Kiểm tra tên đăng nhập không trùng với tài khoản đã được xác thực
        /// </summary>
        private void btnLuuTenDN_Click(object sender, EventArgs e)
        {
            // Kiểm tra tên đăng nhập không được rỗng
            if(txtTenDangNhap.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập mới!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            QLTVEntities db = new QLTVEntities();
            // Kiểm tra tên đăng nhập đã được sử dụng chưa
            NguoiDung nguoiDung = db.NguoiDungs.Where(p=>p.TenDangNhap == txtTenDangNhap.Text).FirstOrDefault();

            if (nguoiDung != null)
            {
                // Nếu tên đăng nhập đã được xác thực thì không cho sử dụng
                if (nguoiDung.TrangThaiXacThuc == true)
                {
                    MessageBox.Show("Tên đăng nhập đã được sử dụng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    // Xóa tài khoản chưa xác thực có tên đăng nhập này
                    db.NguoiDungs.Remove(nguoiDung);
                    db.SaveChanges();
                }
            }

            // Cập nhật tên đăng nhập
            nguoiDung = db.NguoiDungs.Where(p => p.ID == ID).FirstOrDefault();
            nguoiDung.TenDangNhap = txtTenDangNhap.Text;
            db.SaveChanges();

            txtTenDangNhap.ReadOnly = true;
            btnLuuTenDN.Hide();
            btnHuyTenDN.Hide();
            loadDuLieu();

            MessageBox.Show("Đổi tên đăng nhập thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
