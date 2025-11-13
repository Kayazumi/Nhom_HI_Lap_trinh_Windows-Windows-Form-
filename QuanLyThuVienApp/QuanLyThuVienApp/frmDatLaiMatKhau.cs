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
    public partial class frmDatLaiMatKhau : MetroFramework.Forms.MetroForm
    {
        private int ID;
        public frmDatLaiMatKhau()
        {
            InitializeComponent();
        }

        public frmDatLaiMatKhau(int _ID)
        {
            ID = _ID;
            InitializeComponent();
        }

        private void frmDatLaiMatKhau_Load(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (txtMK1.Text == "" || txtMK2.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtMK1.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu có tối thiểu 6 ký tự!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMK2.Focus();
                return;
            }

            if (txtMK1.Text != txtMK2.Text)
            {
                MessageBox.Show("Xác nhận mật khẩu sai!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p=>p.ID == ID).FirstOrDefault();

            if (nguoiDung == null) return;

            MD5 mD5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(txtMK2.Text);
            byte[] hashBytes = mD5.ComputeHash(inputBytes);

            nguoiDung.MatKhau = hashBytes;
            db.SaveChanges();
            MessageBox.Show("Đặt lại mật khẩu thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
