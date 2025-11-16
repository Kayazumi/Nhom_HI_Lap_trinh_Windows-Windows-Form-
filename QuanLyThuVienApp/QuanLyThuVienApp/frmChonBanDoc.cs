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
    /// Form chọn bạn đọc để cho mượn sách
    /// Cho phép Admin chọn bạn đọc từ danh sách để mở form cho mượn sách
    /// </summary>
    public partial class frmChonBanDoc : MetroFramework.Forms.MetroForm
    {
        /// <summary>
        /// Biến cờ để xác định có đóng form sau khi cho mượn sách không
        /// </summary>
        public static bool thoat = false;

        /// <summary>
        /// Khởi tạo form chọn bạn đọc
        /// </summary>
        public frmChonBanDoc()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện Load form - Load danh sách bạn đọc
        /// </summary>
        private void frmChonBanDoc_Load(object sender, EventArgs e)
        {
            loadDuLieu();
        }

        /// <summary>
        /// Load danh sách bạn đọc (user đã xác thực, không bị khóa)
        /// Hiển thị số lượng sách có thể mượn (10 - SoSachMuon)
        /// </summary>
        private void loadDuLieu()
        {
            QLTVEntities db  = new QLTVEntities();
            // Load danh sách bạn đọc đã xác thực và không bị khóa
            dgvBanDoc.DataSource = db.NguoiDungs.Where(p => p.TrangThaiXacThuc == true && p.QuyenHan == "user" && p.BiKhoa == false)
                .Select(p => new
                {
                    MaBanDoc = "BD" + p.ID,
                    TenBanDoc = p.HoTen,
                    p.Email,
                    CoTheMuon = 10 - p.SoSachMuon // Số lượng sách còn có thể mượn
                }).ToList();

            // Hiển thị thông tin bạn đọc đầu tiên
            if (dgvBanDoc.Rows.Count > 0)
            {
                txtID.Text = dgvBanDoc.Rows[0].Cells["MaBanDoc"].Value.ToString();
                txtTen.Text = dgvBanDoc.Rows[0].Cells["TenBanDoc"].Value.ToString();
                txtEmail.Text = dgvBanDoc.Rows[0].Cells["Email"].Value.ToString();
            }
        }

        /// <summary>
        /// Đóng form
        /// </summary>
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Mở form cho mượn sách với bạn đọc được chọn
        /// Lấy ID từ textbox (bỏ prefix "BD")
        /// </summary>
        private void btnMuonSach_Click(object sender, EventArgs e)
        {
            thoat = false;
            if (txtID.Text == "") return;
            // Lấy ID từ textbox (bỏ prefix "BD")
            frmChoMuonSach frm = new frmChoMuonSach(int.Parse(txtID.Text.Substring(2)));
            frm.ShowDialog();
            // Đóng form nếu đã cho mượn sách thành công
            if (thoat) this.Close();
        }

        /// <summary>
        /// Tìm kiếm bạn đọc theo tiêu chí: Mã bạn đọc, Tên bạn đọc, Email
        /// </summary>
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string luaChon = cbTimKiem.Text;
            if (luaChon == "") return;

            QLTVEntities db = new QLTVEntities();
            List<NguoiDung> nguoiDungs = new List<NguoiDung>();

            // Tìm kiếm theo các tiêu chí khác nhau
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

            // Hiển thị kết quả tìm kiếm
            dgvBanDoc.DataSource = nguoiDungs.Select(p => new
            {
                MaBanDoc = "BD" + p.ID,
                TenBanDoc = p.HoTen,
                p.Email,
                CoTheMuon = 10 - p.SoSachMuon
            }).ToList();

            // Hiển thị thông tin bạn đọc đầu tiên nếu có kết quả
            if (dgvBanDoc.Rows.Count > 0)
            {
                txtID.Text = dgvBanDoc.Rows[0].Cells["MaBanDoc"].Value.ToString();
                txtTen.Text = dgvBanDoc.Rows[0].Cells["TenBanDoc"].Value.ToString();
                txtEmail.Text = dgvBanDoc.Rows[0].Cells["Email"].Value.ToString();
            }
            else
            {
                // Xóa thông tin nếu không tìm thấy
                txtID.Clear();
                txtEmail.Clear();
                txtTen.Clear();
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
        /// Sự kiện click vào cell của DataGridView - Hiển thị thông tin bạn đọc được chọn
        /// </summary>
        private void dgvBanDoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Bỏ qua nếu click vào header
            if (e.RowIndex == -1) return;

            // Hiển thị thông tin bạn đọc được chọn
            txtID.Text = dgvBanDoc.Rows[e.RowIndex].Cells["MaBanDoc"].Value.ToString();
            txtTen.Text = dgvBanDoc.Rows[e.RowIndex].Cells["TenBanDoc"].Value.ToString();
            txtEmail.Text = dgvBanDoc.Rows[e.RowIndex].Cells["Email"].Value.ToString();
        }
    }
}
