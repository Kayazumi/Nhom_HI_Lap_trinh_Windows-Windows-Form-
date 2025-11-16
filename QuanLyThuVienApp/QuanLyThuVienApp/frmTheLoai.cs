using MetroFramework.Controls;
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
    /// Form quản lý thể loại sách
    /// Cho phép Admin thực hiện các thao tác CRUD (Create, Read, Update, Delete) với thể loại
    /// </summary>
    public partial class frmTheLoai : MetroFramework.Forms.MetroForm
    {
        /// <summary>
        /// Khởi tạo form quản lý thể loại
        /// </summary>
        public frmTheLoai()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện Load form - Load dữ liệu thể loại từ database
        /// </summary>
        private void frmTheLoai_Load(object sender, EventArgs e)
        {
            loadDuLieu();
        }

        /// <summary>
        /// Load danh sách thể loại từ database và hiển thị lên DataGridView
        /// Format mã thể loại: "TL" + MaTheLoai
        /// </summary>
        private void loadDuLieu()
        {
            QLTVEntities db = new QLTVEntities();
            // Load danh sách thể loại với mã được format
            dgvTheLoai.DataSource = db.TheLoais.Select(p => new
            {
                MaTheLoai = "TL" + p.MaTheLoai,
                p.TenTheLoai,
                p.SoMaSach,
                p.MoTa
            }).ToList();

            // Hiển thị thông tin thể loại đầu tiên vào các textbox
            if (dgvTheLoai.Rows.Count > 0)
            {
                txtMa.Text = dgvTheLoai.Rows[0].Cells["MaTheLoai"].Value.ToString();
                txtTen.Text = dgvTheLoai.Rows[0].Cells["TenTheLoai"].Value.ToString();
                txtMoTa.Text = dgvTheLoai.Rows[0].Cells["MoTa"].Value.ToString();
            }
        }

        /// <summary>
        /// Sự kiện click vào cell của DataGridView - Hiển thị thông tin thể loại được chọn
        /// </summary>
        private void dgvTheLoai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Bỏ qua nếu click vào header
            if (e.RowIndex == -1) return;

            // Hiển thị thông tin thể loại được chọn vào các textbox
            txtMa.Text = dgvTheLoai.Rows[e.RowIndex].Cells["MaTheLoai"].Value.ToString();
            txtTen.Text = dgvTheLoai.Rows[e.RowIndex].Cells["TenTheLoai"].Value.ToString();
            txtMoTa.Text = dgvTheLoai.Rows[e.RowIndex].Cells["MoTa"].Value.ToString();
        }

        /// <summary>
        /// Tìm kiếm thể loại theo tiêu chí (Mã thể loại hoặc Tên thể loại)
        /// </summary>
        private void btnTim_Click(object sender, EventArgs e)
        {
            QLTVEntities db = new QLTVEntities();
            List<TheLoai> theLoais = new List<TheLoai>();

            // Tìm kiếm theo mã thể loại (format: "TL" + MaTheLoai)
            if (cbTim.Text == "Mã thể loại")
            {
                theLoais = db.TheLoais.Where(p => ("TL" + p.MaTheLoai).Contains(txtTim.Text)).ToList();
            }
            // Tìm kiếm theo tên thể loại
            else if (cbTim.Text == "Tên thể loại")
            {
                theLoais = db.TheLoais.Where(p => p.TenTheLoai.Contains(txtTim.Text)).ToList();
            }
            else return;

            // Hiển thị kết quả tìm kiếm
            dgvTheLoai.DataSource = theLoais.Select(p => new
            {
                MaTheLoai = "TL" + p.MaTheLoai,
                p.TenTheLoai,
                p.SoMaSach,
                p.MoTa
            }).ToList();

            // Hiển thị thông tin thể loại đầu tiên nếu có kết quả
            if (dgvTheLoai.Rows.Count > 0)
            {
                txtMa.Text = dgvTheLoai.Rows[0].Cells["MaTheLoai"].Value.ToString();
                txtTen.Text = dgvTheLoai.Rows[0].Cells["TenTheLoai"].Value.ToString();
                txtMoTa.Text = dgvTheLoai.Rows[0].Cells["MoTa"].Value.ToString();
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
        /// Thêm thể loại mới vào database
        /// Kiểm tra thông tin đầy đủ trước khi thêm
        /// </summary>
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Xác nhận với người dùng
            DialogResult result = MessageBox.Show(
                "Bạn có muốn thêm thể loại mới không?",
                "Thông báo!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No) return;

            // Kiểm tra thông tin đầy đủ
            if (txtTenTL.Text == "" || txtMoTaTL.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tạo thể loại mới
            TheLoai theLoai = new TheLoai();
            theLoai.TenTheLoai = txtTenTL.Text;
            theLoai.MoTa = txtMoTaTL.Text;
            theLoai.SoMaSach = 0; // Khởi tạo số lượng sách = 0

            // Lưu vào database
            QLTVEntities db = new QLTVEntities();
            db.TheLoais.Add(theLoai);
            db.SaveChanges();
            loadDuLieu();
            
            // Xóa dữ liệu nhập
            txtTenTL.Clear();
            txtMoTaTL.Clear();
            MessageBox.Show("Thêm thể loại thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Sửa thông tin thể loại đã chọn
        /// Lấy mã thể loại từ textbox (bỏ prefix "TL")
        /// </summary>
        private void btnSua_Click(object sender, EventArgs e)
        {
            // Xác nhận với người dùng
            DialogResult result = MessageBox.Show(
               "Bạn có muốn sửa thông tin thể loại này không?",
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
            // Lấy mã thể loại (bỏ prefix "TL")
            int maTL = int.Parse(txtMa.Text.Substring(2));
            TheLoai theLoai = db.TheLoais.Where(p => p.MaTheLoai == maTL).FirstOrDefault();

            // Cập nhật thông tin
            theLoai.TenTheLoai = txtTen.Text;
            theLoai.MoTa = txtMoTa.Text;

            // Lưu thay đổi
            db.SaveChanges();
            loadDuLieu();
            MessageBox.Show("Sửa thể loại thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Xóa thể loại khỏi database
        /// Chỉ cho phép xóa nếu thể loại chưa có sách (SoMaSach = 0)
        /// </summary>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Xác nhận với người dùng
            DialogResult result = MessageBox.Show(
               "Bạn có muốn xóa thể loại này không?",
               "Thông báo!",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question
           );

            if (result == DialogResult.No) return;
            
            QLTVEntities db = new QLTVEntities();
            // Lấy mã thể loại (bỏ prefix "TL")
            int maTL = int.Parse(txtMa.Text.Substring(2));
            TheLoai theLoai = db.TheLoais.Where(p => p.MaTheLoai == maTL).FirstOrDefault();

            // Kiểm tra thể loại có đang được sử dụng không
            if (theLoai.SoMaSach != 0)
            {
                MessageBox.Show("Thể loại này đang có sách trong thư viện!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Xóa thể loại
            db.TheLoais.Remove(theLoai);
            db.SaveChanges();
            loadDuLieu();
            MessageBox.Show("Xóa thể loại thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
