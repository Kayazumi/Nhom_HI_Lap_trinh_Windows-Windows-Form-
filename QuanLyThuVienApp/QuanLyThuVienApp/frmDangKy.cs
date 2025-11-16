using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace QuanLyThuVienApp
{
    public partial class frmDangKy : MetroFramework.Forms.MetroForm
    {
        public frmDangKy()
        {
            InitializeComponent();
        }

        private void frmDangKy_Load(object sender, EventArgs e)
        {
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();   
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

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if(txtEmail.Text=="" || txtTenDangNhap.Text=="" || txtMatKhau.Text=="" || txtMatKhau2.Text == "" || txtHoTen.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(!isEmail(txtEmail.Text)) 
            {
                MessageBox.Show("Email không hợp lệ!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return;
            }

            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p=>p.TenDangNhap==txtTenDangNhap.Text).SingleOrDefault();

            if(nguoiDung != null)
            {
                if(nguoiDung.TrangThaiXacThuc == true)
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenDangNhap.Focus();
                    return;
                }
                else
                {
                    db.NguoiDungs.Remove(nguoiDung);
                    db.SaveChanges();
                }
            }

            nguoiDung = db.NguoiDungs.SingleOrDefault(p=>p.Email==txtEmail.Text);
            if (nguoiDung != null)
            {
                if (nguoiDung.TrangThaiXacThuc == true)
                {
                    MessageBox.Show("Email đã được sử dụng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenDangNhap.Focus();
                    return;
                }
                else
                {
                    db.NguoiDungs.Remove(nguoiDung);
                    db.SaveChanges();
                }
            }
            
            if(txtMatKhau.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu có tối thiểu 6 ký tự!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMatKhau.Focus();
                return;
            }

            if (txtMatKhau2.Text != txtMatKhau.Text)
            {
                MessageBox.Show("Xác nhận mật khẩu sai!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMatKhau2.Focus();
                return;
            }

            // Mã hóa mật khẩu
            MD5 mD5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(txtMatKhau.Text);
            byte[] hashBytes = mD5.ComputeHash(inputBytes);

            // Tạo OTP
            Random rand = new Random();
            string OTP = rand.Next(100000, 999999).ToString();

            NguoiDung nd = new NguoiDung();

            nd.TenDangNhap = txtTenDangNhap.Text;
            nd.HoTen = txtHoTen.Text;
            nd.MatKhau = hashBytes;
            nd.Email = txtEmail.Text;
            nd.MaOTP = OTP;
            db.NguoiDungs.Add(nd);
            db.SaveChanges();
            
            GuiEmail.guiEmail(txtEmail.Text, "Mã xác thực của bạn là " + OTP);
            nd.ThoiGianNhanOTP = DateTime.Now;
            db.SaveChanges();
            frmXacThuc frm = new frmXacThuc(nd.ID);
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }
    }
}
