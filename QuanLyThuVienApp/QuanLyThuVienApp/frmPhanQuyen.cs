using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVienApp
{
    public partial class frmPhanQuyen : Form
    {
        public frmPhanQuyen()
        {
            InitializeComponent();
        }

        private void frmCapQuyen_Load(object sender, EventArgs e)
        {
            radioUser.Checked = true;
        }

        private void radioUser_CheckedChanged(object sender, EventArgs e)
        {
            loadUser();
        }

        private void radioAdmin_CheckedChanged(object sender, EventArgs e)
        {
            loadAdmin();
        }

        private void radioDangKhoa_CheckedChanged(object sender, EventArgs e)
        {
            loadDangKhoa();
        }

        private void loadUser()
        {
            QLTVEntities db = new QLTVEntities();
            dgvNguoiDung.DataSource = db.NguoiDungs.Where(p => p.TrangThaiXacThuc == true && p.QuyenHan == "user" && p.BiKhoa == false)
                .Select(p => new
                {
                    ID = "BD" + p.ID,
                    p.HoTen,
                    p.Email,
                    p.NgayDangKi,
                    p.QuyenHan
                }).ToList();

            btnCapQuyenAdmin.Show();
            btnKhoaTaiKhoan.Show();
            btnHuyQuyenAdmin.Hide();
            btnMoKhoa.Hide();

            loadChiTiet();
        }

        private void loadAdmin()
        {
            QLTVEntities db = new QLTVEntities();
            dgvNguoiDung.DataSource = db.NguoiDungs.Where(p => p.TrangThaiXacThuc == true && p.QuyenHan == "admin")
                .Select(p => new
                {
                    ID = "AD" + p.ID,
                    p.HoTen,
                    p.Email,
                    p.NgayDangKi,
                    p.QuyenHan
                }).ToList();

            btnCapQuyenAdmin.Hide();
            btnKhoaTaiKhoan.Hide();
            btnHuyQuyenAdmin.Show();
            btnMoKhoa.Hide();

            loadChiTiet();
        }

        private void loadDangKhoa()
        {
            QLTVEntities db = new QLTVEntities();
            dgvNguoiDung.DataSource = db.NguoiDungs.Where(p => p.TrangThaiXacThuc == true && p.BiKhoa == true)
                .Select(p => new
                {
                    ID = "BD" + p.ID,
                    p.HoTen,
                    p.Email,
                    p.NgayDangKi,
                    p.QuyenHan
                }).ToList();

            btnCapQuyenAdmin.Hide();
            btnKhoaTaiKhoan.Hide();
            btnHuyQuyenAdmin.Hide();
            btnMoKhoa.Show();

            loadChiTiet();
        }

        private void loadChiTiet(int rowIndex = 0)
        {
            if (dgvNguoiDung.Rows.Count > 0)
            {
                DateTime date = (DateTime)dgvNguoiDung.Rows[rowIndex].Cells["NgayDangKi"].Value;
                txtID.Text = dgvNguoiDung.Rows[rowIndex].Cells["ID"].Value.ToString();
                txtTen.Text = dgvNguoiDung.Rows[rowIndex].Cells["HoTen"].Value.ToString();
                txtEmail.Text = dgvNguoiDung.Rows[rowIndex].Cells["Email"].Value.ToString();
                txtNgayDangKy.Text = date.ToString("dd/MM/yyyy");
                txtQuyenHan.Text = dgvNguoiDung.Rows[rowIndex].Cells["QuyenHan"].Value.ToString();
            }
            else
            {
                txtID.Clear();
                txtTen.Clear();
                txtEmail.Clear();
                txtNgayDangKy.Clear();
                txtQuyenHan.Clear();
            }
        }

       

        private void dgvNguoiDung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            loadChiTiet(e.RowIndex);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string luaChon = cbTimKiem.Text;
            if (luaChon == "") return;

            QLTVEntities db = new QLTVEntities();
            List<NguoiDung> nguoiDungs = new List<NguoiDung>();

            if (radioUser.Checked)
            {
                if (luaChon == "ID")
                    nguoiDungs = db.NguoiDungs.Where(p => p.QuyenHan == "user" && p.TrangThaiXacThuc == true
                    && p.BiKhoa == false && ("BD" + p.ID.ToString()).Contains(txtTimKiem.Text)).ToList();
                else if (luaChon == "Tên người dùng")
                    nguoiDungs = db.NguoiDungs.Where(p => p.QuyenHan == "user" && p.TrangThaiXacThuc == true
                    && p.HoTen.Contains(txtTimKiem.Text)).ToList();
                else if (luaChon == "Email")
                    nguoiDungs = db.NguoiDungs.Where(p => p.QuyenHan == "user" && p.TrangThaiXacThuc == true
                    && p.Email.Contains(txtTimKiem.Text)).ToList();
                else return;

                dgvNguoiDung.DataSource = nguoiDungs.Select(p => new
                {
                    ID = "BD" + p.ID,
                    p.HoTen,
                    p.Email,
                    p.NgayDangKi,
                    p.QuyenHan
                }).ToList();
            }
            else if (radioAdmin.Checked)
            {
                if (luaChon == "ID")
                    nguoiDungs = db.NguoiDungs.Where(p => p.QuyenHan == "admin" && p.TrangThaiXacThuc == true
                    && ("AD" + p.ID.ToString()).Contains(txtTimKiem.Text)).ToList();
                else if (luaChon == "Tên người dùng")
                    nguoiDungs = db.NguoiDungs.Where(p => p.QuyenHan == "admin" && p.TrangThaiXacThuc == true
                    && p.HoTen.Contains(txtTimKiem.Text)).ToList();
                else if (luaChon == "Email")
                    nguoiDungs = db.NguoiDungs.Where(p => p.QuyenHan == "admin" && p.TrangThaiXacThuc == true
                    && p.Email.Contains(txtTimKiem.Text)).ToList();
                else return;

                dgvNguoiDung.DataSource = nguoiDungs.Select(p => new
                {
                    ID = "AD" + p.ID,
                    p.HoTen,
                    p.Email,
                    p.NgayDangKi,
                    p.QuyenHan
                }).ToList();
            }
            else if(radioDangKhoa.Checked)
            {
                if (luaChon == "ID")
                    nguoiDungs = db.NguoiDungs.Where(p => p.TrangThaiXacThuc == true && p.BiKhoa == true
                    && ("BD" + p.ID.ToString()).Contains(txtTimKiem.Text)).ToList();
                else if (luaChon == "Tên người dùng")
                    nguoiDungs = db.NguoiDungs.Where(p => p.QuyenHan == "user" && p.TrangThaiXacThuc == true
                    && p.HoTen.Contains(txtTimKiem.Text)).ToList();
                else if (luaChon == "Email")
                    nguoiDungs = db.NguoiDungs.Where(p => p.QuyenHan == "user" && p.TrangThaiXacThuc == true
                    && p.Email.Contains(txtTimKiem.Text)).ToList();
                else return;

                dgvNguoiDung.DataSource = nguoiDungs.Select(p => new
                {
                    ID = "BD" + p.ID,
                    p.HoTen,
                    p.Email,
                    p.NgayDangKi,
                    p.QuyenHan
                }).ToList();
            }

            loadChiTiet();
        }

        

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            if (radioUser.Checked) loadUser();
            else if (radioAdmin.Checked) loadAdmin();
            else loadDangKhoa();
        }

        private void btnCapQuyenAdmin_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "") return;

            DialogResult result = MessageBox.Show(
                "Bạn có chắc cấp quyền admin cho tài khoản này?",
                "Thông báo!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No) return;

            int ID = int.Parse(txtID.Text.Substring(2));
            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == ID).FirstOrDefault();

            nguoiDung.QuyenHan = "admin";
            db.SaveChanges();
            loadUser();

            MessageBox.Show("Cấp quyền admin thành công!", "Thonpg báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnHuyQuyenAdmin_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "") return;

            DialogResult result = MessageBox.Show(
                "Bạn có chắc hủy quyền admin của tài khoản này?",
                "Thông báo!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No) return;

            int ID = int.Parse(txtID.Text.Substring(2));
            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == ID).FirstOrDefault();

            nguoiDung.QuyenHan = "user";
            db.SaveChanges();
            loadAdmin();

            MessageBox.Show("Hủy quyền admin thành công!", "Thonpg báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        

        

        private void btnKhoaTaiKhoan_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "") return;

            DialogResult result = MessageBox.Show(
                "Tài khoản này sẽ không thể mượn sách mới?",
                "Thông báo!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No) return;

            QLTVEntities db = new QLTVEntities();
            int ID = int.Parse(txtID.Text.Substring(2));

            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == ID).FirstOrDefault();
            nguoiDung.BiKhoa = true;
            db.SaveChanges();
            loadUser();

            MessageBox.Show("Khóa tài khoản thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMoKhoa_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "") return;

            DialogResult result = MessageBox.Show(
                "Tài khoản này sẽ có thể mượn sách?",
                "Thông báo!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No) return;


            QLTVEntities db = new QLTVEntities();
            int ID = int.Parse(txtID.Text.Substring(2));

            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == ID).FirstOrDefault();
            nguoiDung.BiKhoa = false;
            db.SaveChanges();
            loadDangKhoa();

            MessageBox.Show("Mở khóa tài khoản thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
