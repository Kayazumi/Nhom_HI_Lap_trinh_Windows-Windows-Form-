using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVienApp
{
    /// <summary>
    /// Form tìm tài khoản bằng email để đặt lại mật khẩu
    /// Gửi OTP đến email để xác thực trước khi cho phép đặt lại mật khẩu
    /// </summary>
    public partial class frmTimTaiKhoan : MetroFramework.Forms.MetroForm
    {
        /// <summary>
        /// Khởi tạo form tìm tài khoản
        /// </summary>
        public frmTimTaiKhoan()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện Load form
        /// </summary>
        private void frmTimTaiKhoan_Load(object sender, EventArgs e)
        {

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
        /// Đóng form
        /// </summary>
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Tìm tài khoản theo email và gửi OTP để xác thực
        /// Sau khi xác thực thành công, mở form đặt lại mật khẩu
        /// </summary>
        private void btnTiepTuc_Click(object sender, EventArgs e)
        {
            // Kiểm tra email không được rỗng
            if(txtEmail.Text == "")
            {
                MessageBox.Show("Vui lòng nhập email!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra định dạng email
            if(!isEmail(txtEmail.Text))
            {
                MessageBox.Show("Email không hợp lệ", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            QLTVEntities db = new QLTVEntities();
            // Tìm tài khoản theo email
            NguoiDung nguoiDung = db.NguoiDungs.Where(p=>p.Email == txtEmail.Text).FirstOrDefault();

            // Kiểm tra tài khoản có tồn tại không
            if(nguoiDung == null)
            {
                MessageBox.Show("Không tồn tại tài khoản liên kết email này!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tạo và gửi OTP
            Random random = new Random();
            int OTP = random.Next(100000, 999999);

            GuiEmail.guiEmail(txtEmail.Text, "Mã xác thực của bạn là " + OTP);
            // Lưu OTP và thời gian nhận OTP
            nguoiDung.MaOTP = OTP.ToString();
            nguoiDung.ThoiGianNhanOTP = DateTime.Now;
            db.SaveChanges();

            // Mở form xác thực OTP
            frmXacThuc frm = new frmXacThuc(nguoiDung.ID, xacNhan =>
            {
                if (xacNhan)
                {
                    // Nếu xác thực thành công, mở form đặt lại mật khẩu
                    frmDatLaiMatKhau frm2 = new frmDatLaiMatKhau(nguoiDung.ID);
                    this.Hide();
                    frm2.ShowDialog();
                    this.Close();
                }
            });

            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        
    }
}
