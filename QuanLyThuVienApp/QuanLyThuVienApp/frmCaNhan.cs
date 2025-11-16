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
    public partial class frmCaNhan : Form
    {
        public static int ID;

        public frmCaNhan()
        {
            InitializeComponent();
        }

        public frmCaNhan(int _ID)
        {
            ID = _ID;
            InitializeComponent();
        }

        private void frmCaNhan_Load(object sender, EventArgs e)
        {
            loadDuLieu();
            btnLuuTen.Hide();
            btnLuuEmail.Hide();
            btnHuyEmail.Hide();
            btnHuyTen.Hide();
            btnLuuTenDN.Hide();
            btnHuyTenDN.Hide();
        }

        private void loadDuLieu()
        {
            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p=>p.ID == ID).FirstOrDefault();

            txtTenDangNhap.Text = nguoiDung.TenDangNhap.ToString();
            txtHoVaTen.Text = nguoiDung.HoTen;
            txtEmail.Text = nguoiDung.Email;
            txtSoSachMuon.Text = nguoiDung.SoSachMuon.ToString() + "/10";
            if (nguoiDung.QuyenHan == "user")
                txtID.Text = "DB" + nguoiDung.ID;
            else txtID.Text = "AD" + nguoiDung.ID;
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            frmDoiMatKhau frm = new frmDoiMatKhau(ID);
            frm.ShowDialog();
        }

        private void btnDoiTen_Click(object sender, EventArgs e)
        {
            txtHoVaTen.ReadOnly = false;
            btnLuuTen.Show();
            btnHuyTen.Show();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtHoVaTen.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == ID).FirstOrDefault();

            nguoiDung.HoTen = txtHoVaTen.Text;
            db.SaveChanges();
            txtHoVaTen.ReadOnly = true;
            btnLuuTen.Hide();
            btnHuyTen.Hide();
            loadDuLieu();

            if (nguoiDung.QuyenHan == "admin") frmMainAdmin.text = "Chào mừng: " + txtHoVaTen.Text;
            else if (nguoiDung.BiKhoa == false) frmMainUser.text = "Chào mừng: " + txtHoVaTen.Text;

            MessageBox.Show("Thay đổi tên thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnHuyTen_Click(object sender, EventArgs e)
        {
            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == ID).FirstOrDefault();
            txtHoVaTen.Text = nguoiDung.HoTen.ToString();
            btnHuyTen.Hide();
            btnLuuTen.Hide();
            txtHoVaTen.ReadOnly = true;
        }

        private void btnDoiEmail_Click(object sender, EventArgs e)
        {
            btnLuuEmail.Show();
            btnHuyEmail.Show();
            txtEmail.ReadOnly = false;
        }

        private void btnHuyEmail_Click(object sender, EventArgs e)
        {
            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == ID).FirstOrDefault();
            txtEmail.Text = nguoiDung.Email.ToString();
            btnHuyEmail.Hide();
            btnLuuEmail.Hide();
            txtEmail.ReadOnly = true;
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

        private void btnLuuEmail_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text == "")
            {
                MessageBox.Show("Vui lòng nhập email!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!isEmail(txtEmail.Text))
            {
                MessageBox.Show("Email không hợp lệ!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.Email == txtEmail.Text).FirstOrDefault();

            if (nguoiDung != null)
            {
                if (nguoiDung.TrangThaiXacThuc == true)
                {
                    MessageBox.Show("Email đã được sử dụng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    db.NguoiDungs.Remove(nguoiDung);
                    db.SaveChanges();
                }
            }

            nguoiDung = db.NguoiDungs.Where(p => p.ID == ID).FirstOrDefault();

            if (txtEmail.Text == nguoiDung.Email)
            {
                MessageBox.Show("Cần nhập email mới!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Random random = new Random();
            string OTP = random.Next(100000, 999999).ToString();

            GuiEmail.guiEmail(txtEmail.Text, "Mã xác thực của bạn là " + OTP);

            nguoiDung.MaOTP = OTP;
            nguoiDung.ThoiGianNhanOTP = DateTime.Now;
            db.SaveChanges();

            frmXacThuc frm = new frmXacThuc(ID, xacNhan =>
            {
                if (xacNhan)
                {
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

        private void btnDoiTenDangNhap_Click(object sender, EventArgs e)
        {
            btnLuuTenDN.Show();
            btnHuyTenDN.Show();
            txtTenDangNhap.ReadOnly = false;
        }

        private void btnHuyTenDN_Click(object sender, EventArgs e)
        {
            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == ID).FirstOrDefault();
            txtTenDangNhap.Text = nguoiDung.TenDangNhap.ToString();
            btnHuyTenDN.Hide();
            btnLuuTenDN.Hide();
            txtTenDangNhap.ReadOnly = true;
        }

        private void btnLuuTenDN_Click(object sender, EventArgs e)
        {
            if(txtTenDangNhap.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập mới!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p=>p.TenDangNhap == txtTenDangNhap.Text).FirstOrDefault();

            if (nguoiDung != null)
            {
                if (nguoiDung.TrangThaiXacThuc == true)
                {
                    MessageBox.Show("Tên đăng nhập đã được sử dụng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    db.NguoiDungs.Remove(nguoiDung);
                    db.SaveChanges();
                }
            }

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
