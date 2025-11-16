using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVienApp
{
    /// <summary>
    /// Form đặt lại mật khẩu (sau khi quên mật khẩu)
    /// Cho phép người dùng đặt mật khẩu mới sau khi đã xác thực OTP
    /// </summary>
    public partial class frmDatLaiMatKhau : MetroFramework.Forms.MetroForm
    {
        /// <summary>
        /// ID của người dùng cần đặt lại mật khẩu
        /// </summary>
        private int ID;
        
        /// <summary>
        /// Khởi tạo form đặt lại mật khẩu (không có ID)
        /// </summary>
        public frmDatLaiMatKhau()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Khởi tạo form đặt lại mật khẩu với ID người dùng
        /// </summary>
        /// <param name="_ID">ID của người dùng</param>
        public frmDatLaiMatKhau(int _ID)
        {
            ID = _ID;
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện Load form
        /// </summary>
        private void frmDatLaiMatKhau_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Đóng form
        /// </summary>
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Xác nhận và thực hiện đặt lại mật khẩu
        /// Kiểm tra mật khẩu mới hợp lệ (tối thiểu 6 ký tự) và xác nhận mật khẩu khớp
        /// Mã hóa mật khẩu bằng MD5 trước khi lưu vào database
        /// </summary>
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            // Kiểm tra đầy đủ thông tin
            if (txtMK1.Text == "" || txtMK2.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra độ dài mật khẩu mới (tối thiểu 6 ký tự)
            if (txtMK1.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu có tối thiểu 6 ký tự!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMK2.Focus();
                return;
            }

            // Kiểm tra xác nhận mật khẩu
            if (txtMK1.Text != txtMK2.Text)
            {
                MessageBox.Show("Xác nhận mật khẩu sai!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p=>p.ID == ID).FirstOrDefault();

            if (nguoiDung == null) return;

            // Mã hóa mật khẩu mới bằng MD5
            MD5 mD5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(txtMK2.Text);
            byte[] hashBytes = mD5.ComputeHash(inputBytes);

            // Lưu mật khẩu mới vào database
            nguoiDung.MatKhau = hashBytes;
            db.SaveChanges();
            MessageBox.Show("Đặt lại mật khẩu thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
