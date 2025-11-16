using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVienApp
{
    /// <summary>
    /// Form quản lý phiếu mượn sách
    /// Cho phép Admin xem, xử lý các phiếu mượn: đăng ký, đang mượn, đã trả
    /// </summary>
    public partial class frmQuanLyPhieuMuon : Form
    {
        /// <summary>
        /// Cờ kiểm tra xem có gia hạn thành công không
        /// Được set bởi form frmGiaHan khi gia hạn thành công
        /// </summary>
        public static bool giaHan = false;
        
        /// <summary>
        /// Constructor của form quản lý phiếu mượn
        /// </summary>
        public frmQuanLyPhieuMuon()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện Load form - được gọi khi form được hiển thị
        /// Mặc định hiển thị danh sách phiếu đăng ký mượn
        /// </summary>
        /// <param name="sender">Form này</param>
        /// <param name="e">Thông tin sự kiện Load</param>
        private void frmQuanLyPhieuMuon_Load(object sender, EventArgs e)
        {
            radioPhieuDangKy.Checked = true; // Mặc định chọn tab "Phiếu đăng ký"
        }
        
        /// <summary>
        /// Trạng thái phiếu mượn:
        /// 0: đăng ký mượn (chờ đến thư viện lấy sách)
        /// 1: đang mượn/quá hạn (đã lấy sách, đang trong thời gian mượn hoặc quá hạn)
        /// 2: đã trả (đã trả sách về thư viện)
        /// </summary>

        /// <summary>
        /// Hiển thị danh sách phiếu đăng ký mượn (TrangThai = 0)
        /// Ẩn/hiện các nút chức năng phù hợp với trạng thái phiếu đăng ký
        /// </summary>
        /// <param name="phieuMuons">Danh sách tất cả phiếu mượn từ database</param>
        private void optionPhieuDangKy(List<PhieuMuon> phieuMuons)
        {
            // Ẩn các nút không dùng cho phiếu đăng ký
            btnHoaDonPhat.Hide();  // Chưa có phạt (chưa mượn)
            btnTraSach.Hide();      // Chưa mượn nên chưa trả được
            btnGiaHan.Hide();       // Chưa mượn nên chưa gia hạn được
            
            // Hiện các nút dùng cho phiếu đăng ký
            btnMuonMoi.Show();      // Cho phép tạo phiếu mượn mới
            btnHuyPhieu.Show();     // Cho phép hủy phiếu đăng ký
            btnChoMuon.Show();      // Cho phép xác nhận cho mượn

            // Ẩn label tiền phạt (chưa có phạt)
            lbTienPhat1.Hide();
            lbTienPhat2.Hide();

            // Lọc và hiển thị chỉ các phiếu có TrangThai = 0 (đăng ký mượn)
            dgvPhieuMuon.DataSource = phieuMuons.Where(p => p.TrangThai == 0)
                .Select(p => new
                {
                    p.MaPhieu,                              // Mã phiếu
                    IDBanDoc = "DB" + p.IDBanDoc,          // Mã bạn đọc (format: DB + ID)
                    TenBanDoc = p.NguoiDung.HoTen,         // Tên bạn đọc (navigation property)
                    p.NgayDangKyMuon                       // Ngày đăng ký mượn
                }).ToList();

            // Đặt tên header cho các cột
            dgvPhieuMuon.Columns["MaPhieu"].HeaderText = "Mã phiếu";
            dgvPhieuMuon.Columns["IDBanDoc"].HeaderText = "Mã bạn đọc";
            dgvPhieuMuon.Columns["TenBanDoc"].HeaderText = "Tên bạn đọc";
            dgvPhieuMuon.Columns["NgayDangKyMuon"].HeaderText = "Ngày đăng ký";
            
            // Format ngày tháng: dd/MM/yyyy
            dgvPhieuMuon.Columns["NgayDangKyMuon"].DefaultCellStyle.Format = "dd/MM/yyyy";

            // Tự động điều chỉnh độ rộng cột
            dgvPhieuMuon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Load chi tiết phiếu đầu tiên (nếu có)
            loadChiTietPhieu();
        }

        /// <summary>
        /// Hiển thị danh sách phiếu đang mượn (TrangThai = 1)
        /// Tính tiền phạt nếu quá hạn (mỗi ngày quá hạn = 1000 VNĐ)
        /// </summary>
        /// <param name="phieuMuons">Danh sách tất cả phiếu mượn từ database</param>
        private void optionPhieuMuon(List<PhieuMuon> phieuMuons)
        {
            // Hiện các nút dùng cho phiếu đang mượn
            btnHoaDonPhat.Show();  // Xem hóa đơn phạt
            btnTraSach.Show();      // Trả sách
            btnGiaHan.Show();       // Gia hạn thời gian mượn
            
            // Ẩn các nút không dùng
            btnMuonMoi.Hide();      // Không tạo phiếu mới ở đây
            btnHuyPhieu.Hide();     // Không hủy được (đã mượn rồi)
            btnChoMuon.Hide();      // Không cho mượn được (đã mượn rồi)

            // Hiện label tiền phạt
            lbTienPhat1.Show();
            lbTienPhat2.Show();

            // Lọc và hiển thị chỉ các phiếu có TrangThai = 1 (đang mượn)
            dgvPhieuMuon.DataSource = phieuMuons.Where(p => p.TrangThai == 1)
                .Select(p => new
                {
                    p.MaPhieu,                              // Mã phiếu
                    IDBanDoc = "DB" + p.IDBanDoc,          // Mã bạn đọc
                    TenBanDoc = p.NguoiDung.HoTen,         // Tên bạn đọc
                    p.NgayMuon,                            // Ngày mượn
                    p.HanTra,                              // Hạn trả
                }).ToList();

            // Đặt tên header cho các cột
            dgvPhieuMuon.Columns["MaPhieu"].HeaderText = "Mã phiếu";
            dgvPhieuMuon.Columns["IDBanDoc"].HeaderText = "Mã bạn đọc";
            dgvPhieuMuon.Columns["TenBanDoc"].HeaderText = "Tên bạn đọc";
            dgvPhieuMuon.Columns["NgayMuon"].HeaderText = "Ngày mượn";
            dgvPhieuMuon.Columns["HanTra"].HeaderText = "Hạn trả";
            
            // Format ngày tháng
            dgvPhieuMuon.Columns["NgayMuon"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvPhieuMuon.Columns["HanTra"].DefaultCellStyle.Format = "dd/MM/yyyy";

            // Tính tiền phạt cho phiếu đầu tiên (nếu có)
            if (dgvPhieuMuon.Rows.Count > 0)
            {
                DateTime hanTra = (DateTime)dgvPhieuMuon.Rows[0].Cells["HanTra"].Value;
                // Tính số ngày quá hạn (số ngày từ hạn trả đến hiện tại)
                int soNgay = (DateTime.Now.Date - hanTra.Date).Days;
                // Nếu quá hạn (soNgay > 0), tính tiền phạt: số ngày * 1000 VNĐ
                if (soNgay > 0) 
                    lbTienPhat2.Text = soNgay.ToString() + "000 VNĐ";
                else 
                    lbTienPhat2.Text = "0 VNĐ"; // Chưa quá hạn
            }
            else 
                lbTienPhat2.Text = "0 VNĐ"; // Không có phiếu

            // Load chi tiết phiếu đầu tiên
            loadChiTietPhieu();
        }

        private void optionPhieuTra(List<PhieuMuon> phieuMuons)
        {
            btnHoaDonPhat.Hide();
            btnTraSach.Hide();
            btnGiaHan.Hide();
            btnMuonMoi.Hide();
            btnHuyPhieu.Hide();
            btnChoMuon.Hide();

            lbTienPhat1.Hide();
            lbTienPhat2.Hide();
                

            dgvPhieuMuon.DataSource = phieuMuons.Where(p => p.TrangThai == 2)
                .Select(p => new
                {
                    MaPhieu = p.MaPhieu,
                    IDBanDoc = "DB" + p.IDBanDoc,
                    TenBanDoc = p.NguoiDung.HoTen,
                    p.NgayMuon,
                    p.NgayTra
                }).ToList();

            dgvPhieuMuon.Columns["MaPhieu"].HeaderText = "Mã phiếu";
            dgvPhieuMuon.Columns["IDBanDoc"].HeaderText = "Mã bạn đọc";
            dgvPhieuMuon.Columns["TenBanDoc"].HeaderText = "Tên bạn đọc";
            dgvPhieuMuon.Columns["NgayMuon"].HeaderText = "Ngày mượn";
            dgvPhieuMuon.Columns["NgayTra"].HeaderText = "Ngày trả";
            dgvPhieuMuon.Columns["NgayMuon"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvPhieuMuon.Columns["NgayTra"].DefaultCellStyle.Format = "dd/MM/yyyy";

            loadChiTietPhieu();
        }
        private void loadChiTietPhieu()
        {
            QLTVEntities db = new QLTVEntities();

            if (dgvPhieuMuon.Rows.Count > 0)
            {
                int maPhieu = int.Parse(dgvPhieuMuon.Rows[0].Cells["MaPhieu"].Value.ToString());

                dgvChiTiet.DataSource = db.ChiTietPhieuMuons.Where(p => p.MaPhieu == maPhieu)
                    .Select(p => new
                    {
                        p.MaPhieu,
                        MaSach = "S" + p.IDSach,
                        p.Sach.TenSach,
                        p.SoLuong,
                        p.PhieuMuon.IDBanDoc,
                        p.PhieuMuon.HanTra
                    }).ToList();
            }
            else
            {
                // Danh sách trống
                dgvChiTiet.DataSource = db.ChiTietPhieuMuons.Where(p => p.MaPhieu == -100)
                    .Select(p => new
                    {
                        p.MaPhieu,
                        MaSach = "S" + p.IDSach,
                        p.Sach.TenSach,
                        p.SoLuong,
                        p.PhieuMuon.IDBanDoc,
                        p.PhieuMuon.HanTra
                    }).ToList();
            }   
        }

        private void radioPhieuMuon_CheckedChanged(object sender, EventArgs e)
        {
            QLTVEntities db = new QLTVEntities();
            optionPhieuMuon(db.PhieuMuons.ToList());
            
        }

        private void radioPhieuTra_CheckedChanged(object sender, EventArgs e)
        {
            QLTVEntities db = new QLTVEntities();
            optionPhieuTra(db.PhieuMuons.ToList());
        }

        private void radioPhieuDangKy_CheckedChanged(object sender, EventArgs e)
        {
            QLTVEntities db = new QLTVEntities();
            optionPhieuDangKy(db.PhieuMuons.ToList());
        }

        private void dgvPhieuMuon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            QLTVEntities db = new QLTVEntities();
            int maPhieu = int.Parse(dgvPhieuMuon.Rows[e.RowIndex].Cells["MaPhieu"].Value.ToString());

            dgvChiTiet.DataSource = db.ChiTietPhieuMuons.Where(p => p.MaPhieu == maPhieu)
                .Select(p => new {
                    MaPhieu = maPhieu,
                    MaSach = "S" + p.IDSach,
                    p.Sach.TenSach,
                    p.SoLuong,
                    p.PhieuMuon.IDBanDoc,
                    p.PhieuMuon.HanTra
                }).ToList();

            if (radioPhieuMuon.Checked)
            {
                DateTime hanTra = (DateTime)dgvPhieuMuon.Rows[e.RowIndex].Cells["HanTra"].Value;
                int soNgay = (DateTime.Now.Date - hanTra.Date).Days;
                if (soNgay > 0) lbTienPhat2.Text = soNgay.ToString() + "000 VNĐ";
                else lbTienPhat2.Text = "0 VNĐ";
            }
        }

        private void dgvPhieuMuon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvPhieuMuon.Columns[e.ColumnIndex].Name == "HanTra")
            {
                DateTime hanTra = (DateTime)dgvPhieuMuon.Rows[e.RowIndex].Cells["HanTra"].Value;
                if (DateTime.Now.Date > hanTra.Date)
                    dgvPhieuMuon.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            QLTVEntities db = new QLTVEntities();
            List<PhieuMuon> phieuMuon = new List<PhieuMuon>();

            if (cbTimKiem.Text == "Mã phiếu")
            {
                phieuMuon = db.PhieuMuons.Where(p => p.MaPhieu.ToString().Contains(txtTimKiem.Text)).ToList();
                if (radioPhieuDangKy.Checked)
                    optionPhieuDangKy(phieuMuon);
                else if (radioPhieuMuon.Checked)
                    optionPhieuMuon(phieuMuon);
                else optionPhieuTra(phieuMuon);
            }
            else if (cbTimKiem.Text == "Mã bạn đọc")
            {
                phieuMuon = db.PhieuMuons.Where(p => ("DB" + p.IDBanDoc.ToString()).Contains(txtTimKiem.Text)).ToList();
                if (radioPhieuDangKy.Checked)
                    optionPhieuDangKy(phieuMuon);
                else if (radioPhieuMuon.Checked)
                    optionPhieuMuon(phieuMuon);
                else optionPhieuTra(phieuMuon);
            }
            else if (cbTimKiem.Text == "Tên bạn đọc")
            {
                phieuMuon = db.PhieuMuons.Where(p => p.NguoiDung.HoTen.Contains(txtTimKiem.Text)).ToList();
                if (radioPhieuDangKy.Checked)
                    optionPhieuDangKy(phieuMuon);
                else if (radioPhieuMuon.Checked)
                    optionPhieuMuon(phieuMuon);
                else optionPhieuTra(phieuMuon);
            }
            else return;
            loadChiTietPhieu();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            if (radioPhieuDangKy.Checked)
            {
                radioPhieuDangKy.Checked = false;
                radioPhieuDangKy.Checked = true;
            }
            else if (radioPhieuMuon.Checked)
            {
                radioPhieuMuon.Checked = false;
                radioPhieuMuon.Checked = true;
            }
            else {
                radioPhieuTra.Checked = false;
                radioPhieuTra.Checked = true;
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút Trả sách
        /// Cập nhật số lượng sách, số sách mượn của bạn đọc, và trạng thái phiếu
        /// </summary>
        /// <param name="sender">Nút Trả sách</param>
        /// <param name="e">Thông tin sự kiện Click</param>
        private void btnTraSach_Click(object sender, EventArgs e)
        {
            // Kiểm tra có chi tiết phiếu không
            if (dgvChiTiet.Rows.Count == 0) return;

            // Xác nhận với người dùng về tiền phạt
            DialogResult result = MessageBox.Show(
                "Xác nhận đã thanh toán " + lbTienPhat2.Text + " tiền phạt!", 
                "Thông báo!",                  
                MessageBoxButtons.YesNo,              
                MessageBoxIcon.Question               
            );

            if (result == DialogResult.No) return; // Người dùng chọn No, dừng xử lý

            // Tạo kết nối database
            QLTVEntities db = new QLTVEntities();
            int tongSach = 0; // Tổng số sách trong phiếu
            
            // Duyệt qua từng sách trong chi tiết phiếu
            foreach(DataGridViewRow row in dgvChiTiet.Rows)
            {
                // Lấy ID sách (bỏ ký tự "S" ở đầu)
                int idSach = int.Parse(row.Cells["MaSach"].Value.ToString().Substring(1));
                // Lấy số lượng sách
                int soLuong = int.Parse(row.Cells["SoLuong"].Value.ToString());
                tongSach += soLuong; // Cộng vào tổng
                
                // Tìm sách trong database
                Sach sach = db.Saches.Where(p=>p.ID == idSach).FirstOrDefault();
                // Giảm số sách đang mượn (trả sách về)
                sach.SoSachMuon -= soLuong;
            }

            // Lấy mã phiếu và ID bạn đọc từ chi tiết phiếu
            int maPhieu = int.Parse(dgvChiTiet.Rows[0].Cells["MaPhieu2"].Value.ToString());
            int IDBanDoc = int.Parse(dgvChiTiet.Rows[0].Cells["IDBanDoc"].Value.ToString());

            // Tìm bạn đọc và giảm số sách đang mượn
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == IDBanDoc).FirstOrDefault();
            nguoiDung.SoSachMuon -= tongSach;

            // Tìm phiếu mượn và cập nhật trạng thái
            PhieuMuon phieuMuon = db.PhieuMuons.Where(p=>p.MaPhieu == maPhieu).FirstOrDefault();
            phieuMuon.TrangThai = 2;              // Đánh dấu đã trả
            phieuMuon.NgayTra = DateTime.Now;     // Lưu ngày trả

            // Lưu tất cả thay đổi vào database
            db.SaveChanges();
            
            // Làm mới danh sách (tải lại dữ liệu)
            btnLamMoi.PerformClick();

            // Thông báo thành công
            MessageBox.Show("Trả sách thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGiaHan_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.Rows.Count == 0) return;

            giaHan = false;
            int maPhieu = int.Parse(dgvChiTiet.Rows[0].Cells["MaPhieu2"].Value.ToString());
            frmGiaHan frm = new frmGiaHan(maPhieu);

            frm.ShowDialog();
            if (giaHan) btnLamMoi.PerformClick();
        }

        private void btnHuyPhieu_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.Rows.Count == 0) return;

            DialogResult result = MessageBox.Show(
                "Bạn có muốn hủy phiếu đăng ký mượn sách này không?",
                "Thông báo!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No) return;

            QLTVEntities db = new QLTVEntities();
            int tongSach = 0;
            foreach (DataGridViewRow row in dgvChiTiet.Rows)
            {
                int idSach = int.Parse(row.Cells["MaSach"].Value.ToString().Substring(1));
                int soLuong = int.Parse(row.Cells["SoLuong"].Value.ToString());
                tongSach += soLuong;
                Sach sach = db.Saches.Where(p => p.ID == idSach).FirstOrDefault();
                sach.SoSachMuon -= soLuong;
            }

            int maPhieu = int.Parse(dgvChiTiet.Rows[0].Cells["MaPhieu2"].Value.ToString());
            int IDBanDoc = int.Parse(dgvChiTiet.Rows[0].Cells["IDBanDoc"].Value.ToString());

            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == IDBanDoc).FirstOrDefault();
            nguoiDung.SoSachMuon -= tongSach;

            List<ChiTietPhieuMuon> chiTietPhieuMuons = db.ChiTietPhieuMuons.Where(p => p.MaPhieu == maPhieu).ToList();
            db.ChiTietPhieuMuons.RemoveRange(chiTietPhieuMuons);

            PhieuMuon phieuMuon = db.PhieuMuons.Where(p => p.MaPhieu == maPhieu).FirstOrDefault();
            db.PhieuMuons.Remove(phieuMuon);

            db.SaveChanges();
            btnLamMoi.PerformClick();

            MessageBox.Show("Hủy phiếu đăng ký thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút Cho mượn
        /// Chuyển phiếu từ trạng thái "Đăng ký" (0) sang "Đang mượn" (1)
        /// Set ngày mượn và hạn trả (14 ngày kể từ ngày mượn)
        /// </summary>
        /// <param name="sender">Nút Cho mượn</param>
        /// <param name="e">Thông tin sự kiện Click</param>
        private void btnChoMuon_Click(object sender, EventArgs e)
        {
            // Kiểm tra có chi tiết phiếu không
            if (dgvChiTiet.Rows.Count == 0) return;

            // Xác nhận với người dùng
            DialogResult result = MessageBox.Show(
                "Bạn có muốn xác nhận cho mượn không?",
                "Thông báo!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No) return; // Người dùng chọn No, dừng xử lý

            // Lấy mã phiếu từ chi tiết
            int maPhieu = int.Parse(dgvChiTiet.Rows[0].Cells["MaPhieu2"].Value.ToString());
            
            // Tạo kết nối database
            QLTVEntities db = new QLTVEntities();
            PhieuMuon phieuMuon = db.PhieuMuons.Where(p => p.MaPhieu == maPhieu).FirstOrDefault();

            // Cập nhật trạng thái và thời gian
            phieuMuon.TrangThai = 1;                          // Chuyển sang "Đang mượn"
            phieuMuon.NgayMuon = DateTime.Now;                 // Ngày mượn = hôm nay
            phieuMuon.HanTra = DateTime.Now.AddDays(14);      // Hạn trả = 14 ngày sau

            // Lưu thay đổi
            db.SaveChanges();
            
            // Làm mới danh sách
            btnLamMoi.PerformClick();

            // Thông báo thành công
            MessageBox.Show("Cho mượn thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMuonMoi_Click(object sender, EventArgs e)
        {
            frmChonBanDoc frm = new frmChonBanDoc();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void btnHoaDonPhat_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.Rows.Count == 0) return;

            DateTime hanTra = (DateTime)dgvChiTiet.Rows[0].Cells["HanTra"].Value;
            int soNgay = (DateTime.Now.Date - hanTra.Date).Days;
            int id = int.Parse(dgvChiTiet.Rows[0].Cells["IDBanDoc"].Value.ToString());
            string strHanTra = hanTra.ToString("dd/MM/yyyy");

            if (soNgay <= 0) soNgay = 0;

            frmReportHoaDonPhat frm = new frmReportHoaDonPhat(id, strHanTra, soNgay);
            frm.Owner = this;
            frm.ShowDialog();
        }
    }
}
