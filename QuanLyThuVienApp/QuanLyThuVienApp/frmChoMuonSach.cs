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
    /// Form cho Admin cho mượn sách trực tiếp cho bạn đọc
    /// Khác với frmMuonSach: Tạo phiếu với TrangThai = 1 (đang mượn) ngay lập tức
    /// </summary>
    public partial class frmChoMuonSach : MetroFramework.Forms.MetroForm
    {
        /// <summary>
        /// Mã bạn đọc được chọn để cho mượn sách
        /// </summary>
        public static int maBanDoc;

        /// <summary>
        /// Khởi tạo form cho mượn sách
        /// </summary>
        public frmChoMuonSach()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Khởi tạo form cho mượn sách với mã bạn đọc cụ thể
        /// </summary>
        /// <param name="_maBanDoc">Mã bạn đọc cần cho mượn sách</param>
        public frmChoMuonSach(int _maBanDoc)
        {
            maBanDoc = _maBanDoc;
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện Load form - Khởi tạo nút và load dữ liệu
        /// </summary>
        private void frmChoMuonSach_Load(object sender, EventArgs e)
        {
            themNutDGV();
            loadDuLieu();
        }
        
        /// <summary>
        /// Thêm các nút vào DataGridView
        /// - Nút "Đăng ký" cho dgvSach để thêm sách vào danh sách mượn
        /// - Nút "Xóa" cho dgvSachMuon để xóa sách khỏi danh sách mượn
        /// </summary>
        private void themNutDGV()
        {
            // Tạo nút đăng ký
            DataGridViewButtonColumn nutDangKy = new DataGridViewButtonColumn();
            nutDangKy.HeaderText = "";
            nutDangKy.Text = "Đăng ký";
            nutDangKy.Name = "btnDangKy";
            nutDangKy.Width = 78;
            nutDangKy.UseColumnTextForButtonValue = true;

            // Tạo nút xóa
            DataGridViewButtonColumn nutXoa = new DataGridViewButtonColumn();
            nutXoa.HeaderText = "";
            nutXoa.Text = "Xóa";
            nutXoa.Name = "btnXoa";
            nutXoa.Width = 45;
            nutXoa.UseColumnTextForButtonValue = true;

            // Thêm nút vào DataGridView
            dgvSach.Columns.Add(nutDangKy);
            dgvSachMuon.Columns.Add(nutXoa);
        }

        /// <summary>
        /// Load dữ liệu sách và thông tin bạn đọc
        /// Hiển thị danh sách sách có sẵn và thông tin bạn đọc được chọn
        /// </summary>
        private void loadDuLieu()
        {
            QLTVEntities db = new QLTVEntities();
            // Load danh sách sách với thông tin tác giả, NXB, thể loại và số lượng còn sẵn
            dgvSach.DataSource = db.Saches.Select(p => new
            {
                MaSach = "S" + p.ID,
                p.TenSach,
                p.TacGia.TenTG,
                p.NhaXuatBan.TenNXB,
                p.TheLoai.TenTheLoai,
                CoSan = p.SoLuong - p.SoSachMuon // Số lượng sách còn sẵn
            }).ToList();

            // Load thông tin bạn đọc
            NguoiDung banDoc = db.NguoiDungs.Where(p => p.ID == maBanDoc).FirstOrDefault();
            txtMaBanDoc.Text = "BD" + banDoc.ID;
            txtTenBanDoc.Text = banDoc.HoTen.ToString();
            txtCoTheMuon.Text = (10 - int.Parse(banDoc.SoSachMuon.ToString())).ToString(); // Số sách còn có thể mượn (tối đa 10)
        }

        /// <summary>
        /// Sự kiện click vào DataGridView sách
        /// Xử lý khi click nút "Đăng ký" để thêm sách vào danh sách mượn
        /// </summary>
        private void dgvSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && dgvSach.Columns[e.ColumnIndex].Name.ToString() == "btnDangKy")
            {
                // Kiểm tra sách còn sẵn không
                if (int.Parse(dgvSach.Rows[e.RowIndex].Cells["CoSan"].Value.ToString()) == 0)
                {
                    MessageBox.Show("Đã hết sách!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string maSach = dgvSach.Rows[e.RowIndex].Cells["MaSach"].Value.ToString();
                string tenSach = dgvSach.Rows[e.RowIndex].Cells["TenSach"].Value.ToString();

                // Kiểm tra sách đã có trong danh sách mượn chưa
                foreach (DataGridViewRow row in dgvSachMuon.Rows)
                {
                    if (row.Cells["MaSach2"].Value.ToString() == maSach)
                    {
                        // Nếu đã có, tăng số lượng lên 1
                        int soLuong = int.Parse(row.Cells["SoLuong2"].Value.ToString());
                        row.Cells["SoLuong2"].Value = soLuong + 1;
                        return;
                    }
                }

                // Nếu chưa có, thêm mới vào danh sách mượn với số lượng = 1
                dgvSachMuon.Rows.Add(maSach, tenSach, 1);
            }
        }

        /// <summary>
        /// Sự kiện click vào DataGridView sách mượn
        /// - Click vào cột số lượng: Cho phép chỉnh sửa số lượng
        /// - Click vào nút Xóa: Xóa sách khỏi danh sách mượn
        /// </summary>
        private void dgvSachMuon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            if (e.ColumnIndex == 2) // Cột số lượng
                dgvSachMuon.BeginEdit(true);
            else if (e.ColumnIndex == 3) // Nút xóa
                dgvSachMuon.Rows.RemoveAt(e.RowIndex);
        }

        /// <summary>
        /// Tìm kiếm sách theo các tiêu chí: Mã sách, Tên sách, Tác giả, Nhà xuất bản, Thể loại
        /// </summary>
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string luaChon = cbTimKiem.Text;
            if (luaChon == "") return;

            QLTVEntities db = new QLTVEntities();
            List<Sach> sach = new List<Sach>();

            // Tìm kiếm theo tiêu chí được chọn
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
        /// Làm mới danh sách sách - Load lại tất cả sách
        /// </summary>
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            loadDuLieu();
        }

        /// <summary>
        /// Xóa tất cả sách khỏi danh sách mượn
        /// </summary>
        private void btnXoaHet_Click(object sender, EventArgs e)
        {
            dgvSachMuon.Rows.Clear();
        }

        /// <summary>
        /// Validate số lượng sách khi người dùng chỉnh sửa
        /// Kiểm tra số lượng không vượt quá số lượng sách còn sẵn
        /// </summary>
        private void dgvSachMuon_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex != 2) return; // Chỉ validate cột số lượng
            string soLuong = e.FormattedValue.ToString();
            if (int.TryParse(soLuong, out int result) && result > 0)
            {
                QLTVEntities db = new QLTVEntities();
                int maSach = int.Parse(dgvSachMuon.Rows[e.RowIndex].Cells["MaSach2"].Value.ToString().Substring(1));
                Sach sach = db.Saches.Where(p => p.ID == maSach).SingleOrDefault();
                // Kiểm tra số lượng không vượt quá số lượng còn sẵn
                if (sach != null && (sach.SoLuong - sach.SoSachMuon) < result)
                {
                    MessageBox.Show("Không đủ số lượng sách!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
            else
            {
                MessageBox.Show("Số lượng không hợp lệ!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
       
        /// <summary>
        /// Đóng form và đánh dấu đã thoát
        /// </summary>
        private void btnThoat_Click(object sender, EventArgs e)
        {
            frmChonBanDoc.thoat = true;
            this.Close();
        }
        
        /// <summary>
        /// Đóng form để chọn lại bạn đọc
        /// </summary>
        private void btnChonLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Xử lý cho mượn sách
        /// Tạo phiếu mượn với TrangThai = 1 (đang mượn) ngay lập tức
        /// Cập nhật số lượng sách và số sách đang mượn của bạn đọc
        /// </summary>
        private void btn_ChoMuon_Click(object sender, EventArgs e)
        {
            if (dgvSachMuon.Rows.Count == 0) return;

            // Xác nhận với Admin trước khi cho mượn
            DialogResult result = MessageBox.Show(
               "Bạn có muốn xác nhận cho mượn sách không?",
               "Thông báo!",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question
           );

            if (result == DialogResult.No) return;

            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == maBanDoc).SingleOrDefault();
            int soLuongMuon = 0;

            // Tính tổng số lượng sách mượn
            foreach (DataGridViewRow row in dgvSachMuon.Rows)
                soLuongMuon += int.Parse(row.Cells["SoLuong2"].Value.ToString());

            // Kiểm tra giới hạn mượn sách (tối đa 10 cuốn)
            if (soLuongMuon + nguoiDung.SoSachMuon > 10)
            {
                MessageBox.Show("Quá giới hạn sách có thể mượn! (" + soLuongMuon + "/" + (10 - nguoiDung.SoSachMuon.Value) + ")", "Thông báo!"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            /*
             Tạo phiếu mượn với TrangThai = 1 (đang mượn) ngay lập tức
             Khác với frmMuonSach: Không cần xác nhận, cho mượn trực tiếp
             */
            PhieuMuon phieuMuon = new PhieuMuon();

            phieuMuon.IDBanDoc = nguoiDung.ID;
            phieuMuon.NgayDangKyMuon = DateTime.Now;
            phieuMuon.NgayMuon = DateTime.Now; // Ngày mượn = ngày hiện tại
            phieuMuon.TrangThai = 1; // Đang mượn ngay lập tức
            phieuMuon.HanTra = DateTime.Now.AddDays(14); // Hạn trả = 14 ngày sau
            db.PhieuMuons.Add(phieuMuon);

            /*
             Lưu chi tiết phiếu mượn và cập nhật số lượng sách
             */
            foreach (DataGridViewRow row in dgvSachMuon.Rows)
            {
                ChiTietPhieuMuon chiTiet = new ChiTietPhieuMuon();
                chiTiet.MaPhieu = phieuMuon.MaPhieu;
                chiTiet.IDSach = int.Parse(row.Cells["MaSach2"].Value.ToString().Substring(1));
                chiTiet.SoLuong = int.Parse(row.Cells["SoLuong2"].Value.ToString());
                db.ChiTietPhieuMuons.Add(chiTiet);

                // Cập nhật số lượng sách đang mượn
                Sach sach = db.Saches.Where(p => p.ID == chiTiet.IDSach).SingleOrDefault();
                sach.SoSachMuon += chiTiet.SoLuong;
            }

            // Cập nhật số sách đang mượn của bạn đọc
            nguoiDung.SoSachMuon += soLuongMuon;
            db.SaveChanges();

            // Tạm tắt event CellValidating để clear dgv (tránh lỗi khi xóa)
            dgvSachMuon.CellValidating -= dgvSachMuon_CellValidating;
            dgvSachMuon.Rows.Clear();
            dgvSachMuon.CellValidating += dgvSachMuon_CellValidating;

            loadDuLieu(); // Load lại dữ liệu
            MessageBox.Show("Cho mượn thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
