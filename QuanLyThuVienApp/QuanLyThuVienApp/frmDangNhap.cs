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
    public partial class frmDangNhap : MetroFramework.Forms.MetroForm
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }
       
        private void frmDangNhap_Load(object sender, EventArgs e)
        {
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if(txtTenDangNhap.Text=="" || txtMatKhau.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.TenDangNhap == txtTenDangNhap.Text || p.Email == txtTenDangNhap.Text)
                .SingleOrDefault();

            if(nguoiDung != null)
            {
                MD5 mD5 = MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(txtMatKhau.Text);
                byte[] hashBytes = mD5.ComputeHash(inputBytes);
                if (hashBytes.SequenceEqual(nguoiDung.MatKhau))
                {
                    if (nguoiDung.TrangThaiXacThuc == null || nguoiDung.TrangThaiXacThuc == false)
                    {
                        Random random = new Random();
                        string OTP = random.Next(100000, 999999).ToString();
                        GuiEmail.guiEmail(nguoiDung.Email, "Mã xác thực của bạn là " + OTP);
                        nguoiDung.MaOTP = OTP;
                        nguoiDung.ThoiGianNhanOTP = DateTime.Now;
                        db.SaveChanges();
                        frmXacThuc frmXacThuc = new frmXacThuc(nguoiDung.ID);
                        this.Hide();
                        frmXacThuc.ShowDialog();
                        this.Show();
                        return;
                    }

                    this.Hide();

                    if(nguoiDung.QuyenHan == "user")
                    {
                        frmMainUser frm = new frmMainUser(nguoiDung.ID, nguoiDung.HoTen, nguoiDung.BiKhoa);
                        frm.ShowDialog();
                    }
                    else
                    {
                        frmMainAdmin frm = new frmMainAdmin(nguoiDung.ID, nguoiDung.HoTen, nguoiDung.BiKhoa);
                        frm.ShowDialog();
                    }
                    this.Close();
                    return;
                }
            }

            MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        private void linkDangKyTK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDangKy frm = new frmDangKy();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void linkQuenMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmTimTaiKhoan frm = new frmTimTaiKhoan();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }
    }
}
