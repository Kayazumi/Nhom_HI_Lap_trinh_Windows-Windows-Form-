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
    /// Form quản lý nhà xuất bản
    /// Cho phép Admin thực hiện các thao tác CRUD (Create, Read, Update, Delete) với nhà xuất bản
    /// </summary>
    public partial class frmNXB : MetroFramework.Forms.MetroForm
    {
        /// <summary>
        /// Khởi tạo form quản lý nhà xuất bản
        /// </summary>
        public frmNXB()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện Load form - Load dữ liệu nhà xuất bản từ database
        /// </summary>
        private void frmNXB_Load(object sender, EventArgs e)
        {
            loadDuLieu();
        }

        /// <summary>
        /// Load danh sách nhà xuất bản từ database và hiển thị lên DataGridView
        /// Format mã nhà xuất bản: "NXB" + MaNXB
        /// </summary>
        private void loadDuLieu()
        {
            QLTVEntities db = new QLTVEntities();
            // Load danh sách nhà xuất bản với mã được format
            dgvNXB.DataSource = db.NhaXuatBans.Select(p => new
            {
                MaNXB = "NXB" + p.MaNXB,
                p.TenNXB,
                p.SoMaSach,
                p.MoTa
            }).ToList();

            // Hiển thị thông tin nhà xuất bản đầu tiên vào các textbox
            if (dgvNXB.Rows.Count > 0)
            {
                txtMa.Text = dgvNXB.Rows[0].Cells["MaNXB"].Value.ToString();
                txtTen.Text = dgvNXB.Rows[0].Cells["TenNXB"].Value.ToString();
                txtMoTa.Text = dgvNXB.Rows[0].Cells["MoTa"].Value.ToString();
            }
        }

        /// <summary>
        /// Tìm kiếm nhà xuất bản theo tiêu chí (Mã NXB hoặc Tên NXB)
        /// </summary>
        private void btnTim_Click(object sender, EventArgs e)
        {
            QLTVEntities db = new QLTVEntities();
            List<NhaXuatBan> nxbs = new List<NhaXuatBan>();

            // Tìm kiếm theo mã NXB (format: "NXB" + MaNXB)
            if (cbTim.Text == "Mã NXB")
            {
                nxbs = db.NhaXuatBans.Where(p=> ("NXB" + p.MaNXB).Contains(txtTim.Text)).ToList();
            }
            // Tìm kiếm theo tên NXB
            else if (cbTim.Text == "Tên NXB")
            {
                nxbs = db.NhaXuatBans.Where(p => p.TenNXB.Contains(txtTim.Text)).ToList();

            }
            else return;

            // Hiển thị kết quả tìm kiếm
            dgvNXB.DataSource = nxbs.Select(p => new
            {
                MaNXB = "NXB" + p.MaNXB,
                p.TenNXB,
                p.SoMaSach,
                p.MoTa
            }).ToList();

            // Hiển thị thông tin nhà xuất bản đầu tiên nếu có kết quả
            if (dgvNXB.Rows.Count > 0)
            {
                txtMa.Text = dgvNXB.Rows[0].Cells["MaNXB"].Value.ToString();
                txtTen.Text = dgvNXB.Rows[0].Cells["TenNXB"].Value.ToString();
                txtMoTa.Text = dgvNXB.Rows[0].Cells["MoTa"].Value.ToString();
            }
            else
            {
                // Xóa thông tin nếu không tìm thấy
                txtMa.Clear();
                txtTen.Clear();
                txtMoTa.Clear();
            }
        }

        /// <summary>
        /// Làm mới danh sách - Load lại toàn bộ dữ liệu từ database
        /// </summary>
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            loadDuLieu();
        }

        /// <summary>
        /// Sự kiện click vào cell của DataGridView - Hiển thị thông tin nhà xuất bản được chọn
        /// </summary>
        private void dgvNXB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Bỏ qua nếu click vào header
            if (e.RowIndex == -1) return;

            // Hiển thị thông tin nhà xuất bản được chọn vào các textbox
            txtMa.Text = dgvNXB.Rows[e.RowIndex].Cells["MaNXB"].Value.ToString();
            txtTen.Text = dgvNXB.Rows[e.RowIndex].Cells["TenNXB"].Value.ToString();
            txtMoTa.Text = dgvNXB.Rows[e.RowIndex].Cells["MoTa"].Value.ToString();
        }

        /// <summary>
        /// Thêm nhà xuất bản mới vào database
        /// Kiểm tra thông tin đầy đủ trước khi thêm
        /// </summary>
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Xác nhận với người dùng
            DialogResult result = MessageBox.Show(
               "Bạn có muốn thêm nhà xuất bản mới không?",
               "Thông báo!",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question
           );

            if (result == DialogResult.No) return;

            // Kiểm tra thông tin đầy đủ
            if (txtTenNXB.Text == "" || txtMoTaNXB.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tạo nhà xuất bản mới
            NhaXuatBan nxb = new NhaXuatBan();
            nxb.TenNXB = txtTenNXB.Text;
            nxb.MoTa = txtMoTaNXB.Text;
            nxb.SoMaSach = 0; // Khởi tạo số lượng sách = 0

            // Lưu vào database
            QLTVEntities db = new QLTVEntities();
            db.NhaXuatBans.Add(nxb);
            db.SaveChanges();
            loadDuLieu();
            
            // Xóa dữ liệu nhập
            txtTenNXB.Clear();
            txtMoTaNXB.Clear();
            MessageBox.Show("Thêm nhà xuất bản thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Sửa thông tin nhà xuất bản đã chọn
        /// Lấy mã nhà xuất bản từ textbox (bỏ prefix "NXB")
        /// </summary>
        private void btnSua_Click(object sender, EventArgs e)
        {
            // Xác nhận với người dùng
            DialogResult result = MessageBox.Show(
               "Bạn có muốn sửa thông tin nhà xuất bản này không?",
               "Thông báo!",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question
           );

            if (result == DialogResult.No) return;

            // Kiểm tra thông tin đầy đủ
            if (txtTen.Text == "" || txtMoTa.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            QLTVEntities db = new QLTVEntities();
            // Lấy mã nhà xuất bản (bỏ prefix "NXB")
            int maNXB = int.Parse(txtMa.Text.Substring(3));
            NhaXuatBan nxb = db.NhaXuatBans.Where(p => p.MaNXB == maNXB).FirstOrDefault();

            // Cập nhật thông tin
            nxb.TenNXB = txtTen.Text;
            nxb.MoTa = txtMoTa.Text;

            // Lưu thay đổi
            db.SaveChanges();
            loadDuLieu();
            MessageBox.Show("Sửa nhà xuất bản thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Xóa nhà xuất bản khỏi database
        /// Chỉ cho phép xóa nếu nhà xuất bản chưa có sách (SoMaSach = 0)
        /// </summary>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Xác nhận với người dùng
            DialogResult result = MessageBox.Show(
               "Bạn có muốn xóa nhà xuất bản này không?",
               "Thông báo!",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question
           );

            if (result == DialogResult.No) return;

            QLTVEntities db = new QLTVEntities();
            // Lấy mã nhà xuất bản (bỏ prefix "NXB")
            int maNXB = int.Parse(txtMa.Text.Substring(3));
            NhaXuatBan nxb = db.NhaXuatBans.Where(p => p.MaNXB == maNXB).FirstOrDefault();

            // Kiểm tra nhà xuất bản có đang được sử dụng không
            if (nxb.SoMaSach != 0)
            {
                MessageBox.Show("Nhà xuất bản đang có sách trong thư viện!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Xóa nhà xuất bản
            db.NhaXuatBans.Remove(nxb);
            db.SaveChanges();
            loadDuLieu();
            MessageBox.Show("Xóa nhà xuất bản thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
