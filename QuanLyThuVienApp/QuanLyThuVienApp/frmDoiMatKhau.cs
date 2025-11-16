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
    /// Form đổi mật khẩu
    /// Cho phép người dùng thay đổi mật khẩu của tài khoản
    /// Yêu cầu nhập mật khẩu cũ để xác thực
    /// </summary>
    public partial class frmDoiMatKhau : MetroFramework.Forms.MetroForm
    {
        /// <summary>
        /// ID của người dùng cần đổi mật khẩu
        /// </summary>
        public static int ID;
        
        /// <summary>
        /// Khởi tạo form đổi mật khẩu (không có ID)
        /// </summary>
        public frmDoiMatKhau()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Khởi tạo form đổi mật khẩu với ID người dùng
        /// </summary>
        /// <param name="_ID">ID của người dùng</param>
        public frmDoiMatKhau(int _ID)
        {
            ID = _ID;   
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện Load form
        /// </summary>
        private void frmDoiMatKhau_Load(object sender, EventArgs e)
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
        /// Xác nhận và thực hiện đổi mật khẩu
        /// Kiểm tra mật khẩu cũ đúng, mật khẩu mới hợp lệ (tối thiểu 6 ký tự), và xác nhận mật khẩu mới khớp
        /// Mã hóa mật khẩu bằng MD5 trước khi lưu vào database
        /// </summary>
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            // Kiểm tra đầy đủ thông tin
            if(txtMKCu.Text == "" || txtMKMoi1.Text == "" || txtMKMoi2.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == ID).FirstOrDefault();

            // Mã hóa mật khẩu cũ để so sánh
            MD5 mD5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(txtMKCu.Text);
            byte[] matKhauCu = mD5.ComputeHash(inputBytes);

            // Kiểm tra mật khẩu cũ có đúng không
            if (!matKhauCu.SequenceEqual(nguoiDung.MatKhau))
            {
                MessageBox.Show("Mật khẩu cũ sai!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra độ dài mật khẩu mới (tối thiểu 6 ký tự)
            if(txtMKMoi1.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu có tối thiểu 6 ký tự!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra mật khẩu mới phải khác mật khẩu cũ
            if (txtMKMoi1.Text == txtMKCu.Text)
            {
                MessageBox.Show("Không đặt lại mật khẩu cũ!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra xác nhận mật khẩu mới
            if (txtMKMoi1.Text != txtMKMoi2.Text)
            {
                MessageBox.Show("Xác nhận lại mật khẩu mới sai!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Mã hóa và lưu mật khẩu mới
            inputBytes = System.Text.Encoding.ASCII.GetBytes(txtMKMoi2.Text);
            byte[] matKhauMoi = mD5.ComputeHash(inputBytes);
            nguoiDung.MatKhau = matKhauMoi;
            db.SaveChanges();

            MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
