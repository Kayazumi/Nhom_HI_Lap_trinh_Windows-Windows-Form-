using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVienApp
{
    /// <summary>
    /// Form hiển thị lịch sử mượn sách của người dùng
    /// Cho phép xem các phiếu mượn, tính tiền phạt, và hủy phiếu đăng ký
    /// </summary>
    public partial class frmLichSuMuon : Form
    {
        /// <summary>
        /// Khởi tạo form lịch sử mượn sách
        /// </summary>
        public frmLichSuMuon()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện Load form - Load dữ liệu khi form được mở
        /// </summary>
        private void frmLichSuMuon_Load(object sender, EventArgs e)
        {
            loadDuLieu();
        }

        /// <summary>
        /// Tính tiền phạt dựa trên số ngày quá hạn
        /// </summary>
        /// <param name="hanTra">Ngày hạn trả sách</param>
        /// <returns>Số tiền phạt (VNĐ). Mỗi ngày quá hạn = 1000 VNĐ</returns>
        private int TinhTienPhat(DateTime? hanTra)
        {
            if (hanTra == null) return 0;

            int soNgayTre = (DateTime.Now.Date - hanTra.Value.Date).Days;
            if (soNgayTre <= 0) return 0;

            int tienPhatMotNgay = 1000; // số tiền phạt mỗi ngày
            return soNgayTre * tienPhatMotNgay;
        }

        /// <summary>
        /// Load danh sách phiếu mượn của người dùng hiện tại
        /// Hiển thị thông tin phiếu mượn, trạng thái, và tiền phạt
        /// </summary>
        private void loadDuLieu()
        {
            QLTVEntities db = new QLTVEntities();
            // Lấy thông tin người dùng hiện tại
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == frmMainUser.ID).FirstOrDefault();

            // Lấy danh sách tất cả phiếu mượn của người dùng
            var phieuMuonList = db.PhieuMuons.Where(p => p.IDBanDoc == nguoiDung.ID).ToList();

            // Hiển thị danh sách phiếu mượn với trạng thái và tiền phạt
            dgvPhieuMuon.DataSource = phieuMuonList
                .Select(p => new
                {
                    p.MaPhieu,
                    p.NgayDangKyMuon,
                    // Xác định trạng thái: 0 = Đăng ký mượn, 2 = Đã trả, 1 = Đang mượn hoặc Quá hạn
                    TrangThai = (p.TrangThai == 0) ? "Đăng ký mượn" :
                                (p.TrangThai == 2) ? "Đã trả" :
                                (DateTime.Now.Date > p.HanTra.Value.Date) ? "Quá hạn" : "Đang mượn",
                    p.NgayMuon,
                    p.HanTra,
                    p.NgayTra,
                    TienPhat = TinhTienPhat(p.HanTra) // Tính tiền phạt nếu quá hạn
                }).ToList();

            loadChiTietPhieu();
        }

        /// <summary>
        /// Load chi tiết sách trong phiếu mượn được chọn
        /// Hiển thị danh sách sách và số lượng mượn
        /// </summary>
        private void loadChiTietPhieu()
        {
            QLTVEntities db = new QLTVEntities();

            if (dgvPhieuMuon.Rows.Count > 0)
            {
                int maPhieu = int.Parse(dgvPhieuMuon.Rows[0].Cells["MaPhieu"].Value.ToString());
                string trangThai = dgvPhieuMuon.Rows[0].Cells["TrangThai"].Value.ToString();

                dgvChiTiet.DataSource = db.ChiTietPhieuMuons.Where(p => p.MaPhieu == maPhieu)
                    .Select(p => new
                    {
                        MaPhieu = maPhieu,
                        MaSach = "S" + p.IDSach,
                        p.Sach.TenSach,
                        p.SoLuong
                    }).ToList();

                if (trangThai == "Đăng ký mượn") btnXoa.Enabled = true;
                else btnXoa.Enabled = false;
            }
            else
            {
                // Danh sách trống
                dgvChiTiet.DataSource = db.ChiTietPhieuMuons.Where(p => p.MaPhieu == -100)
                    .Select(p => new
                    {
                        MaPhieu = -1,
                        MaSach = "S" + p.IDSach,
                        p.Sach.TenSach,
                        p.SoLuong,
                    }).ToList();
            }
        }

        /// <summary>
        /// Sự kiện khi click vào một phiếu mượn trong DataGridView
        /// Load chi tiết sách của phiếu mượn được chọn
        /// </summary>
        private void dgvPhieuMuon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return; // Không phải click vào row dữ liệu

            QLTVEntities db = new QLTVEntities();
            // Lấy mã phiếu và trạng thái từ row được chọn
            int maPhieu = int.Parse(dgvPhieuMuon.Rows[e.RowIndex].Cells["MaPhieu"].Value.ToString());
            string trangThai = dgvPhieuMuon.Rows[e.RowIndex].Cells["TrangThai"].Value.ToString();

            // Load chi tiết sách trong phiếu mượn
            dgvChiTiet.DataSource = db.ChiTietPhieuMuons.Where(p => p.MaPhieu == maPhieu)
                .Select(p => new
                {
                    MaPhieu = maPhieu,
                    MaSach = "S" + p.IDSach,
                    p.Sach.TenSach,
                    p.SoLuong
                }).ToList();

            // Chỉ cho phép xóa nếu phiếu ở trạng thái "Đăng ký mượn" (chưa được xác nhận)
            if (trangThai == "Đăng ký mượn") btnXoa.Enabled = true;
            else btnXoa.Enabled = false;
        }

        /// <summary>
        /// Xử lý xóa phiếu đăng ký mượn
        /// Chỉ cho phép xóa phiếu ở trạng thái "Đăng ký mượn" (TrangThai = 0)
        /// Cập nhật lại số lượng sách và số sách đang mượn của người dùng
        /// </summary>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Xác nhận với người dùng trước khi xóa
            DialogResult result = MessageBox.Show(
               "Bạn có muốn xóa phiếu đăng ký không?",
               "Thông báo!",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question
           );

            if (result == DialogResult.No) return;

            // Lấy mã phiếu từ chi tiết
            int maPhieu = int.Parse(dgvChiTiet.Rows[0].Cells["MaPhieu"].Value.ToString());
            QLTVEntities db = new QLTVEntities();

            int tongSach = 0; // Tổng số sách trong phiếu

            // Cập nhật lại số lượng sách (giảm SoSachMuon)
            foreach (DataGridViewRow row in dgvChiTiet.Rows)
            {
                string maSach = row.Cells["MaSach"].Value.ToString();
                int soLuong = int.Parse(row.Cells["SoLuong"].Value.ToString());
                tongSach += soLuong;

                Sach sach = db.Saches.Where(p => ("S" + p.ID.ToString()) == maSach).FirstOrDefault();
                sach.SoSachMuon -= soLuong;
            }

            // Cập nhật số sách đang mượn của người dùng (giảm số lượng)
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == frmMainUser.ID).FirstOrDefault();
            nguoiDung.SoSachMuon -= tongSach;

            // Xóa phiếu mượn và tất cả chi tiết phiếu mượn
            PhieuMuon phieuMuon = db.PhieuMuons.Where(p => p.MaPhieu == maPhieu).FirstOrDefault();
            List<ChiTietPhieuMuon> chiTietPhieuMuon = db.ChiTietPhieuMuons.Where(p => p.MaPhieu == maPhieu).ToList();
            db.PhieuMuons.Remove(phieuMuon);
            db.ChiTietPhieuMuons.RemoveRange(chiTietPhieuMuon);

            // Lưu thay đổi vào database
            db.SaveChanges();
            loadDuLieu(); // Load lại dữ liệu
            
            // Nếu không còn phiếu nào, xóa dữ liệu chi tiết
            if (dgvPhieuMuon.Rows.Count == 0)
            {
                dgvChiTiet.DataSource = null;
                dgvChiTiet.Rows.Clear();
            }
            MessageBox.Show("Xóa phiếu đăng ký thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Lọc danh sách phiếu mượn theo trạng thái
        /// Các trạng thái: Tất cả, Đăng ký mượn, Đã trả, Quá hạn, Đang mượn
        /// </summary>
        private void cbLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLoc.Text == "Tất cả")
            {
                loadDuLieu();
                return;
            }

            QLTVEntities db = new QLTVEntities();
            List<PhieuMuon> phieuMuons = new List<PhieuMuon>();

            if (cbLoc.Text == "Đăng ký mượn")
                phieuMuons = db.PhieuMuons.Where(p => p.TrangThai == 0).ToList();
            else if (cbLoc.Text == "Đã trả")
                phieuMuons = db.PhieuMuons.Where(p => p.TrangThai == 2).ToList();
            else if (cbLoc.Text == "Quá hạn")
                phieuMuons = db.PhieuMuons.Where(p => p.TrangThai == 1
                && DbFunctions.TruncateTime(DateTime.Now) > DbFunctions.TruncateTime(p.HanTra)).ToList();
            else
                phieuMuons = db.PhieuMuons.Where(p => p.TrangThai == 1
                && DbFunctions.TruncateTime(DateTime.Now) <= DbFunctions.TruncateTime(p.HanTra)).ToList();

            dgvPhieuMuon.DataSource = phieuMuons
                           .Select(p => new
                           {
                               p.MaPhieu,
                               p.NgayDangKyMuon,
                               TrangThai = (p.TrangThai == 0) ? "Đăng ký mượn" :
                                (p.TrangThai == 2) ? "Đã trả" :
                                DateTime.Now.Date > p.HanTra.Value.Date ? "Quá hạn" : "Đang mượn",
                               p.NgayMuon,
                               p.HanTra,
                               p.NgayTra,
                               TienPhat = TinhTienPhat(p.HanTra)
                           }).ToList();

            loadChiTietPhieu();
        }

        /// <summary>
        /// Định dạng màu sắc cho các cell trong DataGridView
        /// Hiển thị màu đỏ cho các phiếu quá hạn
        /// </summary>
        private void dgvPhieuMuon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Nếu phiếu quá hạn, hiển thị màu đỏ
            if (dgvPhieuMuon.Rows[e.RowIndex].Cells["TrangThai"].Value.ToString() == "Quá hạn")
                e.CellStyle.ForeColor = Color.Red;
        }
    }
}