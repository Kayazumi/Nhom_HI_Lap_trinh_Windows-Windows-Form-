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
    public partial class frmDoiMatKhau : MetroFramework.Forms.MetroForm
    {
        public static int ID;
        public frmDoiMatKhau()
        {
            InitializeComponent();
        }

        public frmDoiMatKhau(int _ID)
        {
            ID = _ID;   
            InitializeComponent();
        }

        private void frmDoiMatKhau_Load(object sender, EventArgs e)
        {
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if(txtMKCu.Text == "" || txtMKMoi1.Text == "" || txtMKMoi2.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == ID).FirstOrDefault();

            MD5 mD5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(txtMKCu.Text);
            byte[] matKhauCu = mD5.ComputeHash(inputBytes);

            if (!matKhauCu.SequenceEqual(nguoiDung.MatKhau))
            {
                MessageBox.Show("Mật khẩu cũ sai!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(txtMKMoi1.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu có tối thiểu 6 ký tự!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtMKMoi1.Text == txtMKCu.Text)
            {
                MessageBox.Show("Không đặt lại mật khẩu cũ!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtMKMoi1.Text != txtMKMoi2.Text)
            {
                MessageBox.Show("Xác nhận lại mật khẩu mới sai!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            inputBytes = System.Text.Encoding.ASCII.GetBytes(txtMKMoi2.Text);
            byte[] matKhauMoi = mD5.ComputeHash(inputBytes);
            nguoiDung.MatKhau = matKhauMoi;
            db.SaveChanges();

            MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
