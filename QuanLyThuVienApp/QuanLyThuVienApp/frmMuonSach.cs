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
    /// Form đăng ký mượn sách cho User
    /// Cho phép User chọn sách, chỉnh sửa số lượng và đăng ký mượn (TrangThai = 0)
    /// </summary>
    public partial class frmMuonSach : Form
    {
        /// <summary>
        /// Constructor của form mượn sách
        /// </summary>
        public frmMuonSach()
        {
            InitializeComponent();
        }
     
        /// <summary>
        /// Sự kiện Load form - được gọi khi form được hiển thị
        /// Thêm nút vào DataGridView và tải danh sách sách
        /// </summary>
        /// <param name="sender">Form này</param>
        /// <param name="e">Thông tin sự kiện Load</param>
        private void frmMuonSach_Load(object sender, EventArgs e)
        {
            themNutDGV(); // Thêm nút "Đăng ký" và "Xóa" vào DataGridView
            loadDuLieu(); // Tải danh sách sách
        }
        
        /// <summary>
        /// Thêm các nút vào DataGridView
        /// - Nút "Đăng ký" vào dgvSach (để thêm sách vào danh sách mượn)
        /// - Nút "Xóa" vào dgvSachMuon (để xóa sách khỏi danh sách mượn)
        /// </summary>
        private void themNutDGV()
        {
            // Tạo nút đăng ký cho DataGridView danh sách sách
            DataGridViewButtonColumn nutDangKy = new DataGridViewButtonColumn();
            nutDangKy.HeaderText = "";
            nutDangKy.Text = "Đăng ký";
            nutDangKy.Name = "btnDangKy";
            nutDangKy.Width = 78;
            nutDangKy.UseColumnTextForButtonValue = true;

            // Tạo nút xóa cho DataGridView danh sách sách mượn
            DataGridViewButtonColumn nutXoa = new DataGridViewButtonColumn();
            nutXoa.HeaderText = "";
            nutXoa.Text = "Xóa";
            nutXoa.Name = "btnXoa";
            nutXoa.Width = 45;
            nutXoa.UseColumnTextForButtonValue = true;

            // Thêm nút vào DataGridView
            dgvSach.Columns.Add(nutDangKy); // Thêm nút "Đăng ký" vào danh sách sách
            dgvSachMuon.Columns.Add(nutXoa); // Thêm nút "Xóa" vào danh sách sách mượn
        }

        /// <summary>
        /// Tải danh sách sách từ database
        /// Hiển thị tất cả sách với thông tin: Mã, Tên, Tác giả, NXB, Thể loại, Số lượng còn sẵn
        /// </summary>
        private void loadDuLieu()
        {
            QLTVEntities db = new QLTVEntities();
            // Load tất cả sách với thông tin liên quan
            dgvSach.DataSource = db.Saches.Select(p => new
            {
                MaSach = "S" + p.ID,              // Mã sách (format: S + ID)
                p.TenSach,                        // Tên sách
                p.TacGia.TenTG,                   // Tên tác giả (navigation property)
                p.NhaXuatBan.TenNXB,              // Tên nhà xuất bản (navigation property)
                p.TheLoai.TenTheLoai,             // Tên thể loại (navigation property)
                CoSan = p.SoLuong - p.SoSachMuon  // Số lượng còn sẵn = Tổng - Đang mượn
            }).ToList();
        }

        /// <summary>
        /// Xử lý sự kiện khi click vào DataGridView danh sách sách
        /// Khi click nút "Đăng ký", thêm sách vào danh sách mượn
        /// Nếu sách đã có trong danh sách, tăng số lượng lên 1
        /// </summary>
        /// <param name="sender">DataGridView danh sách sách</param>
        /// <param name="e">Thông tin sự kiện CellClick</param>
        private void dgvSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra click vào nút "Đăng ký" và có chọn dòng hợp lệ
            if(e.RowIndex != -1 && dgvSach.Columns[e.ColumnIndex].Name.ToString() == "btnDangKy")
            {
                // Kiểm tra sách còn sẵn không
                if(int.Parse(dgvSach.Rows[e.RowIndex].Cells["CoSan"].Value.ToString()) == 0)
                {
                    MessageBox.Show("Đã hết sách!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lấy thông tin sách
                string maSach = dgvSach.Rows[e.RowIndex].Cells["MaSach"].Value.ToString();
                string tenSach = dgvSach.Rows[e.RowIndex].Cells["TenSach"].Value.ToString();

                // Kiểm tra sách đã có trong danh sách mượn chưa
                foreach (DataGridViewRow row in dgvSachMuon.Rows)
                {
                    if (row.Cells["MaSach2"].Value.ToString() == maSach)
                    {
                        // Sách đã có, tăng số lượng lên 1
                        int soLuong = int.Parse(row.Cells["SoLuong2"].Value.ToString());
                        row.Cells["SoLuong2"].Value = soLuong + 1;
                        return;
                    }
                }

                // Sách chưa có, thêm mới với số lượng = 1
                dgvSachMuon.Rows.Add(maSach, tenSach, 1);
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi click vào DataGridView danh sách sách mượn
        /// - Click vào cột số lượng (cột 2): Cho phép chỉnh sửa số lượng
        /// - Click vào nút "Xóa" (cột 3): Xóa sách khỏi danh sách mượn
        /// </summary>
        /// <param name="sender">DataGridView danh sách sách mượn</param>
        /// <param name="e">Thông tin sự kiện CellClick</param>
        private void dgvSachMuon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex == -1) return; // Click vào header, bỏ qua

            if (e.ColumnIndex == 2)
                // Click vào cột số lượng, cho phép chỉnh sửa
                dgvSachMuon.BeginEdit(true);
            else if (e.ColumnIndex == 3)
                // Click vào nút "Xóa", xóa dòng khỏi danh sách
                dgvSachMuon.Rows.RemoveAt(e.RowIndex);
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút Tìm kiếm
        /// Tìm kiếm sách theo Mã/Tên/Tác giả/NXB/Thể loại
        /// </summary>
        /// <param name="sender">Nút Tìm kiếm</param>
        /// <param name="e">Thông tin sự kiện Click</param>
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string luaChon = cbTimKiem.Text; // Lấy tiêu chí tìm kiếm
            if (luaChon == "") return; // Chưa chọn tiêu chí, dừng

            QLTVEntities db = new QLTVEntities();
            List<Sach> sach = new List<Sach>();

            // Tìm kiếm theo tiêu chí đã chọn
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
                CoSan = p.SoLuong - p.SoSachMuon
            }).ToList();
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút Làm mới
        /// Tải lại danh sách sách từ database (bỏ bộ lọc tìm kiếm)
        /// </summary>
        /// <param name="sender">Nút Làm mới</param>
        /// <param name="e">Thông tin sự kiện Click</param>
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            loadDuLieu(); // Tải lại dữ liệu
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút Xóa hết
        /// Xóa tất cả sách khỏi danh sách mượn
        /// </summary>
        /// <param name="sender">Nút Xóa hết</param>
        /// <param name="e">Thông tin sự kiện Click</param>
        private void btnXoaHet_Click(object sender, EventArgs e)
        {
            dgvSachMuon.Rows.Clear(); // Xóa tất cả dòng
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút Đăng ký mượn
        /// Tạo phiếu đăng ký mượn (TrangThai = 0), lưu chi tiết và cập nhật số lượng sách
        /// Kiểm tra giới hạn mượn: tối đa 10 cuốn sách
        /// </summary>
        /// <param name="sender">Nút Đăng ký mượn</param>
        /// <param name="e">Thông tin sự kiện Click</param>
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            // Kiểm tra có sách trong danh sách mượn không
            if (dgvSachMuon.Rows.Count == 0) return;

            // Xác nhận với người dùng
            DialogResult result = MessageBox.Show(
               "Bạn có muốn đăng ký mượn sách không?",
               "Thông báo!",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question
           );

            if (result == DialogResult.No) return; // Người dùng chọn No, dừng xử lý

            QLTVEntities db = new QLTVEntities();
            // Lấy thông tin bạn đọc hiện tại
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == frmMainUser.ID).SingleOrDefault();
            int soLuongMuon = 0; // Tổng số sách muốn mượn

            // Tính tổng số sách muốn mượn
            foreach (DataGridViewRow row in dgvSachMuon.Rows)
                soLuongMuon += int.Parse(row.Cells["SoLuong2"].Value.ToString());

            // Kiểm tra giới hạn mượn: tối đa 10 cuốn
            if(soLuongMuon + nguoiDung.SoSachMuon > 10)
            {
                MessageBox.Show("Quá giới hạn sách có thể mượn! (" + soLuongMuon + "/" + (10 - nguoiDung.SoSachMuon.Value) + ")", "Thông báo!"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            // Tạo phiếu mượn mới
            PhieuMuon phieuMuon = new PhieuMuon();
            phieuMuon.IDBanDoc = nguoiDung.ID;                    // ID bạn đọc
            phieuMuon.NgayDangKyMuon = DateTime.Now;              // Ngày đăng ký = hôm nay
            // Trạng thái phiếu:
            // 0: chờ đến thư viện lấy sách (đăng ký mượn)
            // 1: đang mượn
            // 2: đã trả
            // -1: quá hạn
            phieuMuon.TrangThai = 0;  // Trạng thái = 0 (đăng ký mượn)
            db.PhieuMuons.Add(phieuMuon);

            // Lưu chi tiết phiếu mượn và cập nhật số lượng sách
            foreach(DataGridViewRow row in dgvSachMuon.Rows)
            {
                // Tạo chi tiết phiếu mượn
                ChiTietPhieuMuon chiTiet = new ChiTietPhieuMuon();
                chiTiet.MaPhieu = phieuMuon.MaPhieu; // Mã phiếu vừa tạo
                chiTiet.IDSach = int.Parse(row.Cells["MaSach2"].Value.ToString().Substring(1)); // ID sách (bỏ "S")
                chiTiet.SoLuong = int.Parse(row.Cells["SoLuong2"].Value.ToString()); // Số lượng
                db.ChiTietPhieuMuons.Add(chiTiet);

                // Cập nhật số sách đang mượn của sách
                Sach sach = db.Saches.Where(p => p.ID == chiTiet.IDSach).SingleOrDefault();
                sach.SoSachMuon += chiTiet.SoLuong;
            }

            // Cập nhật số sách đang mượn của bạn đọc
            nguoiDung.SoSachMuon += soLuongMuon;
            db.SaveChanges(); // Lưu tất cả thay đổi

            // Tạm tắt event CellValidating để clear dgv (tránh lỗi khi xóa dòng)
            dgvSachMuon.CellValidating -= dgvSachMuon_CellValidating;
            dgvSachMuon.Rows.Clear(); // Xóa danh sách sách mượn
            dgvSachMuon.CellValidating += dgvSachMuon_CellValidating; // Bật lại event

            loadDuLieu(); // Tải lại danh sách sách
            MessageBox.Show("Đăng ký mượn thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Xử lý sự kiện khi validate số lượng sách trong DataGridView danh sách sách mượn
        /// Kiểm tra số lượng nhập vào có hợp lệ và không vượt quá số lượng có sẵn
        /// </summary>
        /// <param name="sender">DataGridView danh sách sách mượn</param>
        /// <param name="e">Thông tin sự kiện CellValidating</param>
        private void dgvSachMuon_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex != 2) return; // Chỉ validate cột số lượng (cột 2)
            
            string soLuong = e.FormattedValue.ToString();
            // Kiểm tra số lượng là số nguyên dương
            if (int.TryParse(soLuong, out int result) && result > 0)
            {
                QLTVEntities db = new QLTVEntities();
                // Lấy ID sách (bỏ "S" ở đầu)
                int maSach = int.Parse(dgvSachMuon.Rows[e.RowIndex].Cells["MaSach2"].Value.ToString().Substring(1));
                Sach sach = db.Saches.Where(p => p.ID == maSach).SingleOrDefault();
                
                // Kiểm tra số lượng không vượt quá số lượng có sẵn
                if (sach != null && (sach.SoLuong - sach.SoSachMuon) < result)
                {
                    MessageBox.Show("Không đủ số lượng sách!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true; // Hủy thay đổi
                }
            }
            else
            {
                // Số lượng không hợp lệ
                MessageBox.Show("Số lượng không hợp lệ!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true; // Hủy thay đổi
            }
        }
    }
}
