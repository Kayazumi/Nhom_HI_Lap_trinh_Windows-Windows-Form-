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

namespace QuanLyThuVienApp
{
    public partial class frmQuanLyBanDoc : Form
    {
        public static int OTP;
        public static DateTime thoiGian;

        public frmQuanLyBanDoc()
        {
            InitializeComponent();
        }

        private void frmQuanLyBanDoc_Load(object sender, EventArgs e)
        {
            loadDuLieu();
            btnDangKy.Enabled = false;
        }

        private void loadDuLieu()
        {
            QLTVEntities db = new QLTVEntities();
            dgvBanDoc.DataSource = db.NguoiDungs.Where(p => p.QuyenHan == "user" && p.TrangThaiXacThuc == true && p.BiKhoa == false)
                .Select(p => new
                {
                    MaBanDoc = "BD" + p.ID,
                    p.HoTen,
                    p.Email,
                    p.NgayDangKi,
                    p.SoSachMuon
                }).ToList();

            if(dgvBanDoc.Rows.Count > 0)
            {
                txtID.Text = dgvBanDoc.Rows[0].Cells["MaBanDoc"].Value.ToString();
                txtSuaEmail.Text = dgvBanDoc.Rows[0].Cells["Email"].Value.ToString();
                txtSuaTen.Text = dgvBanDoc.Rows[0].Cells["HoTen"].Value.ToString();
            }
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

        private void btnGuiMa_Click(object sender, EventArgs e)
        {
            if(txtEmail.Text == "")
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
            NguoiDung nguoiDung = db.NguoiDungs.Where(p=>p.Email == txtEmail.Text).FirstOrDefault();

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

            Random random = new Random();
            OTP = random.Next(100000, 999999);
            thoiGian = DateTime.Now;

            GuiEmail.guiEmail(txtEmail.Text, "Mã xác thực  của bạn là " + OTP);
            txtEmail.ReadOnly = true;
            btnDangKy.Enabled = true;

            MessageBox.Show("Vui lòng kiểm tra email!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            txtEmail.Text = "";
            txtTen.Text = "";
            txtMa.Text = "";

            txtEmail.ReadOnly = false;
            btnDangKy.Enabled = false;
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có muốn đăng ký tài khoản mới không?",
                "Thông báo!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No) return;

            if (txtEmail.Text == "" || txtTen.Text == "" || txtMa.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            QLTVEntities db = new QLTVEntities();
            NguoiDung nd = db.NguoiDungs.Where(p => p.Email == txtEmail.Text).FirstOrDefault();

            if(nd != null)
            {
                db.NguoiDungs.Remove(nd);
                db.SaveChanges();
            }

            if (txtMa.Text != OTP.ToString())
            {
                MessageBox.Show("Mã xác thực không đúng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if((DateTime.Now - thoiGian).TotalMinutes > 5)
            {
                MessageBox.Show("Mã xác thực đã hết hạn!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Random random = new Random();
            string matKhau = random.Next(100000, 999999).ToString();

            MD5 mD5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(matKhau);
            byte[] hashBytes = mD5.ComputeHash(inputBytes);

            NguoiDung nguoiDung = new NguoiDung();
            nguoiDung.Email = txtEmail.Text;
            nguoiDung.TrangThaiXacThuc = true;
            nguoiDung.BiKhoa = false;
            nguoiDung.HoTen = txtTen.Text;
            nguoiDung.QuyenHan = "user";
            nguoiDung.NgayDangKi = DateTime.Now;
            nguoiDung.SoSachMuon = 0;
            nguoiDung.MaOTP = txtMa.Text;
            nguoiDung.ThoiGianNhanOTP = thoiGian;
            nguoiDung.MatKhau = hashBytes;
            nguoiDung.TenDangNhap = txtEmail.Text;

            db.NguoiDungs.Add(nguoiDung);
            db.SaveChanges();
            GuiEmail.guiEmail(txtEmail.Text, "Mật khẩu đăng nhập của bạn là " + matKhau);

            txtEmail.Clear();
            txtMa.Clear();
            txtTen.Clear();
            loadDuLieu();

            btnDangKy.Enabled = false;
            txtEmail.ReadOnly = false;
            MessageBox.Show("Tạo tài khoản bạn đọc thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            loadDuLieu();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string luaChon = cbTimKiem.Text;
            if (luaChon == "") return;

            QLTVEntities db = new QLTVEntities();
            List<NguoiDung> nguoiDungs = new List<NguoiDung>();

            if (luaChon == "Mã bạn đọc")
                nguoiDungs = db.NguoiDungs.Where(p => p.QuyenHan == "user" && p.TrangThaiXacThuc == true
                && p.BiKhoa == false && ("BD" + p.ID.ToString()).Contains(txtTimKiem.Text)).ToList();
            else if (luaChon == "Tên bạn đọc")
                nguoiDungs = db.NguoiDungs.Where(p => p.QuyenHan == "user" && p.TrangThaiXacThuc == true
                && p.BiKhoa == false && p.HoTen.Contains(txtTimKiem.Text)).ToList();
            else if (luaChon == "Email")
                nguoiDungs = db.NguoiDungs.Where(p => p.QuyenHan == "user" && p.TrangThaiXacThuc == true
                && p.BiKhoa == false && p.Email.Contains(txtTimKiem.Text)).ToList();
            else return;

            dgvBanDoc.DataSource = nguoiDungs.Select(p => new
            {
                MaBanDoc = "BD" + p.ID,
                p.HoTen,
                p.Email,
                p.NgayDangKi,
                p.SoSachMuon
            }).ToList();

            if (dgvBanDoc.Rows.Count > 0)
            {
                txtID.Text = dgvBanDoc.Rows[0].Cells["MaBanDoc"].Value.ToString();
                txtSuaEmail.Text = dgvBanDoc.Rows[0].Cells["Email"].Value.ToString();
                txtSuaTen.Text = dgvBanDoc.Rows[0].Cells["HoTen"].Value.ToString();
            }
            else
            {
                txtID.Clear();
                txtSuaEmail.Clear();
                txtSuaTen.Clear();
            }

        }

        private void btnSuaEmail_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "") return;

            DialogResult result = MessageBox.Show(
                "Bạn có muốn thay đổi email cho tài khoản này không?",
                "Thông báo!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No) return;

            if (txtSuaEmail.Text == "")
            {
                MessageBox.Show("Vui lòng nhập email mới!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!isEmail(txtSuaEmail.Text))
            {
                MessageBox.Show("Email không hợp lệ!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.Email == txtSuaEmail.Text).FirstOrDefault();

            if(nguoiDung != null)
            {
                if(nguoiDung.TrangThaiXacThuc == true)
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

            int id = int.Parse(txtID.Text.Substring(2));
            nguoiDung = db.NguoiDungs.Where(p => p.ID == id).FirstOrDefault();

            if (txtSuaEmail.Text == nguoiDung.Email)
            {
                MessageBox.Show("Cần nhập email mới!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Random random = new Random();
            string OTP = random.Next(100000, 999999).ToString();

            GuiEmail.guiEmail(txtSuaEmail.Text, "Mã xác thực của bạn là " + OTP);

            nguoiDung.MaOTP = OTP;
            nguoiDung.ThoiGianNhanOTP = DateTime.Now;
            db.SaveChanges();

            frmXacThuc frm = new frmXacThuc(nguoiDung.ID, xacNhan =>
            {
                if (xacNhan)
                {
                    nguoiDung.Email = txtSuaEmail.Text;
                    db.SaveChanges();
                    loadDuLieu();
                }
            });
            frm.ShowDialog();
        }

        private void btnSuaTen_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "") return;

            DialogResult result = MessageBox.Show(
                "Bạn có muốn sửa tên người dùng cho tài khoản này không?",
                "Thông báo!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No) return;

            if (txtSuaTen.Text == "")
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            QLTVEntities db = new QLTVEntities();
            int id = int.Parse(txtID.Text.Substring(2));

            NguoiDung nguoiDung = db.NguoiDungs.Where(p=>p.ID == id).FirstOrDefault();

            nguoiDung.HoTen = txtSuaTen.Text;
            db.SaveChanges();
            loadDuLieu();
            MessageBox.Show("Cập nhật tên thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnResetMK_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "") return;

            DialogResult result = MessageBox.Show(
                "Bạn có muốn đặt lại mật khẩu cho tài khoản này không?",
                "Thông báo!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No) return;


            Random random = new Random();
            string matKhau = random.Next(100000, 999999).ToString();

            QLTVEntities db = new QLTVEntities();
            int id = int.Parse(txtID.Text.Substring(2));

            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == id).FirstOrDefault();

            MD5 mD5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(matKhau);
            byte[] hashBytes = mD5.ComputeHash(inputBytes);

            nguoiDung.MatKhau = hashBytes;
            db.SaveChanges();

            GuiEmail.guiEmail(nguoiDung.Email, "Mật khẩu mới của bạn là " + matKhau);

            MessageBox.Show("Mật khẩu mới sẽ được gửi về email đăng ký!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvBanDoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            if (dgvBanDoc.Rows.Count > 0)
            {
                txtID.Text = dgvBanDoc.Rows[e.RowIndex].Cells["MaBanDoc"].Value.ToString();
                txtSuaEmail.Text = dgvBanDoc.Rows[e.RowIndex].Cells["Email"].Value.ToString();
                txtSuaTen.Text = dgvBanDoc.Rows[e.RowIndex].Cells["HoTen"].Value.ToString();
            }
            else
            {
                txtID.Clear();
                txtEmail.Clear();
                txtSuaTen.Clear();
            }
        }
    }
}
