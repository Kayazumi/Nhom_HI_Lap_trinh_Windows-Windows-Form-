using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVienApp
{
    /// <summary>
    /// Form xem danh sách sách (chỉ đọc)
    /// Hiển thị thông tin sách và số lượng sách còn sẵn
    /// </summary>
    public partial class frmSach : Form
    {
        /// <summary>
        /// Khởi tạo form xem danh sách sách
        /// </summary>
        public frmSach()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện Load form - Load danh sách sách từ database
        /// </summary>
        private void frmSach_Load(object sender, EventArgs e)
        {
            loadDuLieu();
        }

        /// <summary>
        /// Load danh sách sách từ database và hiển thị lên DataGridView
        /// Tính số lượng sách còn sẵn = SoLuong - SoSachMuon
        /// </summary>
        private void loadDuLieu()
        {
            QLTVEntities db = new QLTVEntities();
            // Load danh sách sách với thông tin đầy đủ
            dgvSach.DataSource = db.Saches.Select(p => new {
                MaSach = "S" + p.ID, 
                p.TenSach, 
                p.TacGia.TenTG, 
                p.NhaXuatBan.TenNXB, 
                p.TheLoai.TenTheLoai, 
                p.SoLuong, 
                p.SoSachMuon
            }).ToList();

            // Hiển thị thông tin sách đầu tiên
            if (dgvSach.Rows.Count > 0)
            {
                // Tính số lượng sách còn sẵn
                int soLuongSachCon = int.Parse(dgvSach.Rows[0].Cells[5].Value.ToString()) - int.Parse(dgvSach.Rows[0].Cells[6].Value.ToString());

                txtMaSach.Text = dgvSach.Rows[0].Cells[0].Value.ToString();
                txtTenSach.Text = dgvSach.Rows[0].Cells[1].Value.ToString();
                txtTacGia.Text = dgvSach.Rows[0].Cells[2].Value.ToString();
                txtNXB.Text = dgvSach.Rows[0].Cells[3].Value.ToString();
                txtTheLoai.Text = dgvSach.Rows[0].Cells[4].Value.ToString();
                txtConSan.Text = soLuongSachCon.ToString();
            }
        }
    
        /// <summary>
        /// Sự kiện click vào cell của DataGridView - Hiển thị thông tin sách được chọn
        /// </summary>
        private void dgvSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                int row = e.RowIndex;
                // Tính số lượng sách còn sẵn
                int soLuongSachCon = int.Parse(dgvSach.Rows[row].Cells[5].Value.ToString()) - int.Parse(dgvSach.Rows[row].Cells[6].Value.ToString());

                // Hiển thị thông tin sách được chọn
                txtMaSach.Text = dgvSach.Rows[row].Cells[0].Value.ToString();
                txtTenSach.Text = dgvSach.Rows[row].Cells[1].Value.ToString();
                txtTacGia.Text = dgvSach.Rows[row].Cells[2].Value.ToString();
                txtNXB.Text = dgvSach.Rows[row].Cells[3].Value.ToString();
                txtTheLoai.Text = dgvSach.Rows[row].Cells[4].Value.ToString();
                txtConSan.Text = soLuongSachCon.ToString();
            }
        }

        /// <summary>
        /// Tìm kiếm sách theo nhiều tiêu chí: Mã sách, Tên sách, Tác giả, Nhà xuất bản, Thể loại
        /// </summary>
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string luaChon = cbTimKiem.Text;
            if (luaChon == "") return;

            QLTVEntities db = new QLTVEntities();
            List<Sach> sach = new List<Sach>();

            // Tìm kiếm theo các tiêu chí khác nhau
            if (luaChon == "Mã sách")
                sach = db.Saches.Where(p => ("S" + p.ID.ToString()).Contains(txtTimKiem.Text)).ToList();
            else if (luaChon == "Tên sách")
                sach = db.Saches.Where(p => p.TenSach.Contains(txtTimKiem.Text)).ToList();
            else if (luaChon == "Tác giả")
                sach = db.Saches.Where(p => p.TacGia.TenTG.Contains(txtTimKiem.Text)).ToList();
            else if (luaChon == "Nhà xuất bản")
                sach = db.Saches.Where(p => p.NhaXuatBan.TenNXB.Contains(txtTimKiem.Text)).ToList();
            else if (luaChon == "Thể loại")
                sach = db.Saches.Where(p => p.TheLoai.TenTheLoai.Contains(txtTimKiem.Text)).ToList();

            // Hiển thị kết quả tìm kiếm
            dgvSach.DataSource = sach.Select(p => new
            {
                MaSach = "S" + p.ID,
                p.TenSach,
                p.TacGia.TenTG,
                p.NhaXuatBan.TenNXB,
                p.TheLoai.TenTheLoai,
                p.SoLuong,
                p.SoSachMuon
            }).ToList();

            // Hiển thị thông tin sách đầu tiên nếu có kết quả
            if (dgvSach.Rows.Count > 0)
            {
                int soLuongSachCon = int.Parse(dgvSach.Rows[0].Cells[5].Value.ToString()) - int.Parse(dgvSach.Rows[0].Cells[6].Value.ToString());
                
                txtMaSach.Text = dgvSach.Rows[0].Cells[0].Value.ToString();
                txtTenSach.Text = dgvSach.Rows[0].Cells[1].Value.ToString();
                txtTacGia.Text = dgvSach.Rows[0].Cells[2].Value.ToString();
                txtNXB.Text = dgvSach.Rows[0].Cells[3].Value.ToString();
                txtTheLoai.Text = dgvSach.Rows[0].Cells[4].Value.ToString();
                txtConSan.Text = soLuongSachCon.ToString();
            }
            else
            {
                // Xóa thông tin nếu không tìm thấy
                txtMaSach.Clear();
                txtTenSach.Clear();
                txtTacGia.Clear();
                txtNXB.Clear();
                txtTheLoai.Clear();
                txtConSan.Clear();
            }
        }

        /// <summary>
        /// Làm mới danh sách - Load lại toàn bộ dữ liệu từ database
        /// </summary>
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            loadDuLieu();
        }
    }
}
