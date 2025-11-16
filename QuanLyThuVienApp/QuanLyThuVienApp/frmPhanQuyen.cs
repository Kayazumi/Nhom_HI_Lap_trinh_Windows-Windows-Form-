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
    /// <summary>
    /// Form phân quyền và quản lý tài khoản
    /// Cho phép Admin cấp/hủy quyền admin, khóa/mở khóa tài khoản
    /// Hiển thị danh sách User, Admin, và tài khoản đang bị khóa
    /// </summary>
    public partial class frmPhanQuyen : Form
    {
        /// <summary>
        /// Khởi tạo form phân quyền
        /// </summary>
        public frmPhanQuyen()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện Load form - Mặc định chọn radio User
        /// </summary>
        private void frmCapQuyen_Load(object sender, EventArgs e)
        {
            radioUser.Checked = true;
        }

        /// <summary>
        /// Sự kiện khi chọn radio User - Load danh sách user
        /// </summary>
        private void radioUser_CheckedChanged(object sender, EventArgs e)
        {
            loadUser();
        }

        /// <summary>
        /// Sự kiện khi chọn radio Admin - Load danh sách admin
        /// </summary>
        private void radioAdmin_CheckedChanged(object sender, EventArgs e)
        {
            loadAdmin();
        }

        /// <summary>
        /// Sự kiện khi chọn radio Đang khóa - Load danh sách tài khoản bị khóa
        /// </summary>
        private void radioDangKhoa_CheckedChanged(object sender, EventArgs e)
        {
            loadDangKhoa();
        }

        /// <summary>
        /// Load danh sách user (đã xác thực, không bị khóa)
        /// Hiển thị nút Cấp quyền Admin và Khóa tài khoản
        /// </summary>
        private void loadUser()
        {
            QLTVEntities db = new QLTVEntities();
            // Load danh sách user đã xác thực và không bị khóa
            dgvNguoiDung.DataSource = db.NguoiDungs.Where(p => p.TrangThaiXacThuc == true && p.QuyenHan == "user" && p.BiKhoa == false)
                .Select(p => new
                {
                    ID = "BD" + p.ID,
                    p.HoTen,
                    p.Email,
                    p.NgayDangKi,
                    p.QuyenHan
                }).ToList();

            // Hiển thị/ẩn các nút chức năng
            btnCapQuyenAdmin.Show();
            btnKhoaTaiKhoan.Show();
            btnHuyQuyenAdmin.Hide();
            btnMoKhoa.Hide();

            loadChiTiet();
        }

        /// <summary>
        /// Load danh sách admin (đã xác thực)
        /// Hiển thị nút Hủy quyền Admin
        /// </summary>
        private void loadAdmin()
        {
            QLTVEntities db = new QLTVEntities();
            // Load danh sách admin đã xác thực
            dgvNguoiDung.DataSource = db.NguoiDungs.Where(p => p.TrangThaiXacThuc == true && p.QuyenHan == "admin")
                .Select(p => new
                {
                    ID = "AD" + p.ID,
                    p.HoTen,
                    p.Email,
                    p.NgayDangKi,
                    p.QuyenHan
                }).ToList();

            // Hiển thị/ẩn các nút chức năng
            btnCapQuyenAdmin.Hide();
            btnKhoaTaiKhoan.Hide();
            btnHuyQuyenAdmin.Show();
            btnMoKhoa.Hide();

            loadChiTiet();
        }

        /// <summary>
        /// Load danh sách tài khoản đang bị khóa (đã xác thực, BiKhoa = true)
        /// Hiển thị nút Mở khóa
        /// </summary>
        private void loadDangKhoa()
        {
            QLTVEntities db = new QLTVEntities();
            // Load danh sách tài khoản đang bị khóa
            dgvNguoiDung.DataSource = db.NguoiDungs.Where(p => p.TrangThaiXacThuc == true && p.BiKhoa == true)
                .Select(p => new
                {
                    ID = "BD" + p.ID,
                    p.HoTen,
                    p.Email,
                    p.NgayDangKi,
                    p.QuyenHan
                }).ToList();

            // Hiển thị/ẩn các nút chức năng
            btnCapQuyenAdmin.Hide();
            btnKhoaTaiKhoan.Hide();
            btnHuyQuyenAdmin.Hide();
            btnMoKhoa.Show();

            loadChiTiet();
        }

        /// <summary>
        /// Load chi tiết thông tin người dùng được chọn vào các textbox
        /// </summary>
        /// <param name="rowIndex">Chỉ số dòng được chọn (mặc định = 0)</param>
        private void loadChiTiet(int rowIndex = 0)
        {
            if (dgvNguoiDung.Rows.Count > 0)
            {
                // Hiển thị thông tin người dùng được chọn
                DateTime date = (DateTime)dgvNguoiDung.Rows[rowIndex].Cells["NgayDangKi"].Value;
                txtID.Text = dgvNguoiDung.Rows[rowIndex].Cells["ID"].Value.ToString();
                txtTen.Text = dgvNguoiDung.Rows[rowIndex].Cells["HoTen"].Value.ToString();
                txtEmail.Text = dgvNguoiDung.Rows[rowIndex].Cells["Email"].Value.ToString();
                txtNgayDangKy.Text = date.ToString("dd/MM/yyyy");
                txtQuyenHan.Text = dgvNguoiDung.Rows[rowIndex].Cells["QuyenHan"].Value.ToString();
            }
            else
            {
                // Xóa thông tin nếu không có dữ liệu
                txtID.Clear();
                txtTen.Clear();
                txtEmail.Clear();
                txtNgayDangKy.Clear();
                txtQuyenHan.Clear();
            }
        }

       

        /// <summary>
        /// Sự kiện click vào cell của DataGridView - Load chi tiết người dùng được chọn
        /// </summary>
        private void dgvNguoiDung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Bỏ qua nếu click vào header
            if (e.RowIndex == -1) return;

            loadChiTiet(e.RowIndex);
        }

        /// <summary>
        /// Tìm kiếm người dùng theo tiêu chí (ID, Tên người dùng, Email)
        /// Tìm kiếm theo loại đã chọn (User, Admin, hoặc Đang khóa)
        /// </summary>
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

        

        /// <summary>
        /// Làm mới danh sách - Load lại dữ liệu theo loại đã chọn
        /// </summary>
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            if (radioUser.Checked) loadUser();
            else if (radioAdmin.Checked) loadAdmin();
            else loadDangKhoa();
        }

        /// <summary>
        /// Cấp quyền admin cho user được chọn
        /// Chuyển QuyenHan từ "user" sang "admin"
        /// </summary>
        private void btnCapQuyenAdmin_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "") return;

            // Xác nhận với người dùng
            DialogResult result = MessageBox.Show(
                "Bạn có chắc cấp quyền admin cho tài khoản này?",
                "Thông báo!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No) return;

            // Lấy ID từ textbox (bỏ prefix "BD")
            int ID = int.Parse(txtID.Text.Substring(2));
            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == ID).FirstOrDefault();

            // Cấp quyền admin
            nguoiDung.QuyenHan = "admin";
            db.SaveChanges();
            loadUser();

            MessageBox.Show("Cấp quyền admin thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Hủy quyền admin của admin được chọn
        /// Chuyển QuyenHan từ "admin" sang "user"
        /// </summary>
        private void btnHuyQuyenAdmin_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "") return;

            // Xác nhận với người dùng
            DialogResult result = MessageBox.Show(
                "Bạn có chắc hủy quyền admin của tài khoản này?",
                "Thông báo!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No) return;

            // Lấy ID từ textbox (bỏ prefix "AD")
            int ID = int.Parse(txtID.Text.Substring(2));
            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == ID).FirstOrDefault();

            // Hủy quyền admin
            nguoiDung.QuyenHan = "user";
            db.SaveChanges();
            loadAdmin();

            MessageBox.Show("Hủy quyền admin thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        

        

        /// <summary>
        /// Khóa tài khoản user được chọn
        /// Set BiKhoa = true, user sẽ không thể mượn sách mới
        /// </summary>
        private void btnKhoaTaiKhoan_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "") return;

            // Xác nhận với người dùng
            DialogResult result = MessageBox.Show(
                "Tài khoản này sẽ không thể mượn sách mới?",
                "Thông báo!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No) return;

            QLTVEntities db = new QLTVEntities();
            // Lấy ID từ textbox (bỏ prefix "BD")
            int ID = int.Parse(txtID.Text.Substring(2));

            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == ID).FirstOrDefault();
            // Khóa tài khoản
            nguoiDung.BiKhoa = true;
            db.SaveChanges();
            loadUser();

            MessageBox.Show("Khóa tài khoản thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Mở khóa tài khoản đang bị khóa
        /// Set BiKhoa = false, user sẽ có thể mượn sách lại
        /// </summary>
        private void btnMoKhoa_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "") return;

            // Xác nhận với người dùng
            DialogResult result = MessageBox.Show(
                "Tài khoản này sẽ có thể mượn sách?",
                "Thông báo!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No) return;


            QLTVEntities db = new QLTVEntities();
            // Lấy ID từ textbox (bỏ prefix "BD")
            int ID = int.Parse(txtID.Text.Substring(2));

            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == ID).FirstOrDefault();
            // Mở khóa tài khoản
            nguoiDung.BiKhoa = false;
            db.SaveChanges();
            loadDangKhoa();

            MessageBox.Show("Mở khóa tài khoản thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
