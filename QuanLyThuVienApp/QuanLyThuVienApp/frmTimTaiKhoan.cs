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
    public partial class frmTimTaiKhoan : MetroFramework.Forms.MetroForm
    {
        public frmTimTaiKhoan()
        {
            InitializeComponent();
        }

        private void frmTimTaiKhoan_Load(object sender, EventArgs e)
        {

        }

        private bool isEmail(string inputEmail)
        {
            inputEmail = inputEmail ?? string.Empty;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTiepTuc_Click(object sender, EventArgs e)
        {
            if(txtEmail.Text == "")
            {
                MessageBox.Show("Vui lòng nhập email!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(!isEmail(txtEmail.Text))
            {
                MessageBox.Show("Email không hợp lệ", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p=>p.Email == txtEmail.Text).FirstOrDefault();

            if(nguoiDung == null)
            {
                MessageBox.Show("Không tồn tại tài khoản liên kết email này!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Random random = new Random();
            int OTP = random.Next(100000, 999999);

            GuiEmail.guiEmail(txtEmail.Text, "Mã xác thực của bạn là " + OTP);
            nguoiDung.MaOTP = OTP.ToString();
            nguoiDung.ThoiGianNhanOTP = DateTime.Now;
            db.SaveChanges();

            frmXacThuc frm = new frmXacThuc(nguoiDung.ID, xacNhan =>
            {
                if (xacNhan)
                {
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
