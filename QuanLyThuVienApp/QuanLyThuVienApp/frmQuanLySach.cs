using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace QuanLyThuVienApp
{
    /// <summary>
    /// Form quản lý sách trong thư viện
    /// Cho phép Admin thêm, sửa, xóa sách và tìm kiếm sách theo nhiều tiêu chí
    /// </summary>
    public partial class frmQuanLySach : Form
    {
        /// <summary>
        /// Constructor của form quản lý sách
        /// </summary>
        public frmQuanLySach()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện Load form - được gọi khi form được hiển thị
        /// Tải dữ liệu sách và mặc định chọn chế độ Sửa/Xóa
        /// </summary>
        /// <param name="sender">Form này</param>
        /// <param name="e">Thông tin sự kiện Load</param>
        private void frmQuanLySach_Load(object sender, EventArgs e)
        {
            loadDuLieu(); // Tải dữ liệu sách từ database
            radioSuaXoa.Checked = true; // Mặc định chọn chế độ Sửa/Xóa
        }

        /// <summary>
        /// Hàm tải dữ liệu sách từ database và hiển thị lên DataGridView
        /// Đồng thời load danh sách Tác giả, Nhà xuất bản, Thể loại vào ComboBox
        /// Nếu đang ở chế độ Sửa/Xóa, tự động điền thông tin sách đầu tiên vào form
        /// </summary>
        private void loadDuLieu()
        {
            // Tạo kết nối đến database
            QLTVEntities db = new QLTVEntities();
            
            // Load danh sách sách vào DataGridView
            // Select: tạo đối tượng anonymous với các thuộc tính cần hiển thị
            // "S" + p.ID: format mã sách thành "S1", "S2", ...
            dgvSach.DataSource = db.Saches.Select(p => new {
                MaSach = "S" + p.ID,              // Mã sách (format: S + ID)
                p.TenSach,                        // Tên sách
                p.TacGia.TenTG,                   // Tên tác giả (navigation property)
                p.NhaXuatBan.TenNXB,              // Tên nhà xuất bản (navigation property)
                p.TheLoai.TenTheLoai,             // Tên thể loại (navigation property)
                p.SoLuong,                        // Tổng số lượng sách
                p.SoSachMuon,                     // Số sách đang được mượn
            }).ToList(); // Chuyển sang List để binding

            // Load danh sách Tác giả vào ComboBox
            // Prepend: thêm một item rỗng ở đầu danh sách (MaTG = -1, TenTG = "")
            cbTacGia.DataSource = db.TacGias.ToList().Prepend(new TacGia {MaTG = -1, TenTG = "" }).ToList();
            cbTacGia.DisplayMember = "TenTG";  // Hiển thị tên tác giả
            cbTacGia.ValueMember = "MaTG";     // Giá trị là mã tác giả

            // Load danh sách Nhà xuất bản vào ComboBox
            cbNXB.DataSource = db.NhaXuatBans.ToList().Prepend(new NhaXuatBan { MaNXB = -1, TenNXB = "" }).ToList();
            cbNXB.DisplayMember = "TenNXB";
            cbNXB.ValueMember = "MaNXB";

            // Load danh sách Thể loại vào ComboBox
            cbTheLoai.DataSource = db.TheLoais.ToList().Prepend(new TheLoai { MaTheLoai = -1, TenTheLoai = "" }).ToList();
            cbTheLoai.DisplayMember = "TenTheLoai";
            cbTheLoai.ValueMember = "MaTheLoai";

            // Nếu đang ở chế độ Thêm, không cần điền thông tin
            if (radioThem.Checked) return;

            // Nếu có dữ liệu trong DataGridView và đang ở chế độ Sửa/Xóa
            // Tự động điền thông tin sách đầu tiên vào các ô input
            if (dgvSach.Rows.Count > 0)
            {
                // Lấy thông tin từ dòng đầu tiên của DataGridView
                string tenTacGia = dgvSach.Rows[0].Cells[2].Value.ToString();  // Cột 2: Tên tác giả
                string tenNXB = dgvSach.Rows[0].Cells[3].Value.ToString();     // Cột 3: Tên NXB
                string tenTheLoai = dgvSach.Rows[0].Cells[4].Value.ToString(); // Cột 4: Tên thể loại

                // Tìm đối tượng Tác giả, NXB, Thể loại từ database dựa trên tên
                TacGia tacGia = db.TacGias.Where(p => p.TenTG == tenTacGia).FirstOrDefault();
                NhaXuatBan nxb = db.NhaXuatBans.Where(p => p.TenNXB == tenNXB).FirstOrDefault();
                TheLoai theLoai = db.TheLoais.Where(p => p.TenTheLoai == tenTheLoai).FirstOrDefault();

                // Điền thông tin vào các ô input
                txtMaSach.Text = dgvSach.Rows[0].Cells[0].Value.ToString();      // Mã sách
                txtTenSach.Text = dgvSach.Rows[0].Cells[1].Value.ToString();     // Tên sách
                txtSoLuong.Text = dgvSach.Rows[0].Cells[5].Value.ToString();     // Số lượng
                txtDangMuon.Text = dgvSach.Rows[0].Cells[6].Value.ToString();   // Số sách đang mượn
                
                // Chọn giá trị trong ComboBox
                cbTacGia.SelectedValue = tacGia.MaTG;
                cbNXB.SelectedValue = nxb.MaNXB;
                cbTheLoai.SelectedValue = theLoai.MaTheLoai;
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi người dùng click vào một ô trong DataGridView
        /// Nếu đang ở chế độ Sửa/Xóa, tự động điền thông tin sách được chọn vào form
        /// </summary>
        /// <param name="sender">DataGridView sách</param>
        /// <param name="e">Thông tin sự kiện click (chứa RowIndex, ColumnIndex)</param>
        private void dgvSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Nếu click vào header (RowIndex = -1), không làm gì
            if (e.RowIndex == -1) return;

            // Nếu đang ở chế độ Thêm, không cần điền thông tin
            if (radioThem.Checked) return;

            // Tạo kết nối database
            QLTVEntities db = new QLTVEntities();

            // Lấy thông tin từ dòng được click
            string tenTacGia = dgvSach.Rows[e.RowIndex].Cells[2].Value.ToString();  // Cột 2: Tên tác giả
            string tenNXB = dgvSach.Rows[e.RowIndex].Cells[3].Value.ToString();     // Cột 3: Tên NXB
            string tenTheLoai = dgvSach.Rows[e.RowIndex].Cells[4].Value.ToString(); // Cột 4: Tên thể loại

            // Tìm đối tượng từ database dựa trên tên
            TacGia tacGia = db.TacGias.Where(p => p.TenTG == tenTacGia).FirstOrDefault();
            NhaXuatBan nxb = db.NhaXuatBans.Where(p => p.TenNXB == tenNXB).FirstOrDefault();
            TheLoai theLoai = db.TheLoais.Where(p => p.TenTheLoai == tenTheLoai).FirstOrDefault();

            // Điền thông tin vào các ô input
            txtMaSach.Text = dgvSach.Rows[e.RowIndex].Cells[0].Value.ToString();      // Mã sách
            txtTenSach.Text = dgvSach.Rows[e.RowIndex].Cells[1].Value.ToString();     // Tên sách
            txtSoLuong.Text = dgvSach.Rows[e.RowIndex].Cells[5].Value.ToString();     // Số lượng
            txtDangMuon.Text = dgvSach.Rows[e.RowIndex].Cells[6].Value.ToString();   // Số sách đang mượn
            
            // Chọn giá trị trong ComboBox
            cbTacGia.SelectedValue = tacGia.MaTG;
            cbNXB.SelectedValue = nxb.MaNXB;
            cbTheLoai.SelectedValue = theLoai.MaTheLoai;
        }

        /// <summary>
        /// Xử lý sự kiện tìm kiếm sách theo nhiều tiêu chí
        /// Hỗ trợ tìm kiếm theo: Mã sách, Tên sách, Tác giả, Nhà xuất bản, Thể loại
        /// </summary>
        /// <param name="sender">Nút Tìm kiếm</param>
        /// <param name="e">Thông tin sự kiện Click</param>
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // Lấy tiêu chí tìm kiếm từ ComboBox
            string luaChon = cbTimKiem.Text;
            if (luaChon == "") return; // Nếu chưa chọn tiêu chí, dừng

            // Tạo kết nối database
            QLTVEntities db = new QLTVEntities();
            List<Sach> sach = new List<Sach>();

            // Tìm kiếm theo tiêu chí đã chọn
            // Contains: tìm kiếm chuỗi con (không phân biệt hoa thường)
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

            // Hiển thị kết quả tìm kiếm lên DataGridView
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

            // Nếu đang ở chế độ Thêm, không cần điền thông tin
            if (radioThem.Checked) return;

            // Nếu có kết quả, điền thông tin sách đầu tiên vào form
            if (dgvSach.Rows.Count > 0)
            {
                string tenTacGia = dgvSach.Rows[0].Cells[2].Value.ToString();
                string tenNXB = dgvSach.Rows[0].Cells[3].Value.ToString();
                string tenTheLoai = dgvSach.Rows[0].Cells[4].Value.ToString();

                TacGia tacGia = db.TacGias.Where(p => p.TenTG == tenTacGia).FirstOrDefault();
                NhaXuatBan nxb = db.NhaXuatBans.Where(p => p.TenNXB == tenNXB).FirstOrDefault();
                TheLoai theLoai = db.TheLoais.Where(p => p.TenTheLoai == tenTheLoai).FirstOrDefault();

                txtMaSach.Text = dgvSach.Rows[0].Cells[0].Value.ToString();
                txtTenSach.Text = dgvSach.Rows[0].Cells[1].Value.ToString();
                txtSoLuong.Text = dgvSach.Rows[0].Cells[5].Value.ToString();
                txtDangMuon.Text = dgvSach.Rows[0].Cells[6].Value.ToString();
                cbTacGia.SelectedValue = tacGia.MaTG;
                cbNXB.SelectedValue = nxb.MaNXB;
                cbTheLoai.SelectedValue = theLoai.MaTheLoai;
            }
            else
            {
                // Nếu không có kết quả, xóa các ô input
                txtMaSach.Clear();
                txtTenSach.Clear();
                txtSoLuong.Clear();
                txtDangMuon.Clear();
                cbTacGia.SelectedIndex = 0;
                cbNXB.SelectedIndex = 0;
                cbTheLoai.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút Làm mới
        /// Tải lại toàn bộ dữ liệu sách từ database
        /// </summary>
        /// <param name="sender">Nút Làm mới</param>
        /// <param name="e">Thông tin sự kiện Click</param>
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            loadDuLieu(); // Tải lại dữ liệu
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút Thêm sách
        /// Validate thông tin, kiểm tra Tác giả/NXB/Thể loại tồn tại, tạo sách mới và cập nhật số lượng sách của Tác giả/NXB/Thể loại
        /// </summary>
        /// <param name="sender">Nút Thêm</param>
        /// <param name="e">Thông tin sự kiện Click</param>
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Xác nhận với người dùng
            DialogResult result = MessageBox.Show(
                "Bạn có muốn thêm sách mới không?",
                "Thông báo!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No) return; // Người dùng chọn No, dừng xử lý

            // Lấy thông tin từ ComboBox
            string tenTG = cbTacGia.Text.ToString();
            string tenNXB = cbNXB.Text.ToString();
            string tenTheLoai = cbTheLoai.Text.ToString();

            // Kiểm tra đầy đủ thông tin
            if (txtTenSach.Text == "" || tenTG == "" || tenNXB == "" || tenTheLoai == "" || txtSoLuong.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate số lượng: phải là số nguyên dương
            if (!int.TryParse(txtSoLuong.Text, out int val) || val <= 0)
            {
                MessageBox.Show("Số lượng không hợp lệ!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoLuong.Focus();
                return;
            }

            // Tạo kết nối database
            QLTVEntities db = new QLTVEntities();

            // Kiểm tra Tác giả có tồn tại không
            TacGia tacGia = db.TacGias.Where(p => p.TenTG == tenTG).FirstOrDefault();
            if (tacGia == null)
            {
                MessageBox.Show("Tác giả không tồn tại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbTacGia.Focus();
                return;
            }

            // Kiểm tra Nhà xuất bản có tồn tại không
            NhaXuatBan nhaXuatBan = db.NhaXuatBans.Where(p => p.TenNXB == tenNXB).FirstOrDefault();
            if (nhaXuatBan == null)
            {
                MessageBox.Show("Nhà xuất bản không tồn tại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbNXB.Focus();
                return;
            }

            // Kiểm tra Thể loại có tồn tại không
            TheLoai theLoai = db.TheLoais.Where(p => p.TenTheLoai == tenTheLoai).FirstOrDefault();
            if (theLoai == null)
            {
                MessageBox.Show("Thể loại không tồn tại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbTheLoai.Focus();
                return;
            }            

            // Tạo đối tượng sách mới
            Sach sach = new Sach();
            sach.TenSach = txtTenSach.Text;                                    // Tên sách
            sach.MaTG = int.Parse(cbTacGia.SelectedValue.ToString());         // Mã tác giả
            sach.MaNXB = int.Parse(cbNXB.SelectedValue.ToString());           // Mã nhà xuất bản
            sach.MaTheLoai = int.Parse(cbTheLoai.SelectedValue.ToString());   // Mã thể loại
            sach.SoLuong = int.Parse(txtSoLuong.Text);                        // Số lượng
            sach.SoSachMuon = 0;                                              // Số sách đang mượn = 0 (sách mới)

            // Cập nhật số lượng sách của Tác giả, NXB, Thể loại (tăng lên 1)
            tacGia.SoMaSach += 1;
            nhaXuatBan.SoMaSach += 1;
            theLoai.SoMaSach += 1;

            // Thêm sách vào database
            db.Saches.Add(sach);
            db.SaveChanges(); // Lưu thay đổi
            
            // Tải lại dữ liệu để hiển thị sách mới
            loadDuLieu();
            
            // Xóa các ô input
            txtTenSach.Clear();
            txtSoLuong.Clear();
            
            // Thông báo thành công
            MessageBox.Show("Thêm sách thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaSach.Text == "") return;

            DialogResult result = MessageBox.Show(
                "Bạn có muốn sửa thông tin sách không?",
                "Thông báo!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No) return;

            string tenTG = cbTacGia.Text.ToString();
            string tenNXB = cbNXB.Text.ToString();
            string tenTheLoai = cbTheLoai.Text.ToString();

            if (txtTenSach.Text == "" || tenTG == "" || tenNXB == "" || tenTheLoai == "" || txtSoLuong.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(!int.TryParse(txtSoLuong.Text, out int val))
            {
                MessageBox.Show("Số lượng không hợp lệ!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoLuong.Focus();
                return;
            }
            else if (val < 1 || val < int.Parse(txtDangMuon.Text))
            {
                MessageBox.Show("Số lượng không hợp lệ!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoLuong.Focus();
                return;
            }

            QLTVEntities db = new QLTVEntities();

            TacGia tacGiaMoi = db.TacGias.Where(p => p.TenTG == tenTG).FirstOrDefault();
            if (tacGiaMoi == null)
            {
                MessageBox.Show("Tác giả không tồn tại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbTacGia.Focus();
                return;
            }

            NhaXuatBan nhaXuatBanMoi = db.NhaXuatBans.Where(p => p.TenNXB == tenNXB).FirstOrDefault();
            if (nhaXuatBanMoi == null)
            {
                MessageBox.Show("Nhà xuất bản không tồn tại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbNXB.Focus();
                return;
            }

            TheLoai theLoaiMoi = db.TheLoais.Where(p => p.TenTheLoai == tenTheLoai).FirstOrDefault();
            if (theLoaiMoi == null)
            {
                MessageBox.Show("Thể loại không tồn tại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbTheLoai.Focus();
                return;
            }

            int maSach = int.Parse(txtMaSach.Text.Substring(1));
            Sach sach = db.Saches.Where(p => p.ID == maSach).FirstOrDefault();

            if (theLoaiMoi.MaTheLoai != sach.MaTheLoai)
            {
                theLoaiMoi.SoMaSach += 1;
                TheLoai theLoaiCu = db.TheLoais.Where(p => p.MaTheLoai == sach.MaTheLoai).FirstOrDefault();
                theLoaiCu.SoMaSach -= 1;
            }

            if (tacGiaMoi.MaTG != sach.MaTG)
            {
                tacGiaMoi.SoMaSach += 1;
                TacGia tacGiaCu = db.TacGias.Where(p => p.MaTG == sach.MaTG).FirstOrDefault();
                tacGiaCu.SoMaSach -= 1;
            }

            if (nhaXuatBanMoi.MaNXB != sach.MaNXB)
            {
                nhaXuatBanMoi.SoMaSach += 1;
                NhaXuatBan nhaXuatBanCu = db.NhaXuatBans.Where(p => p.MaNXB == sach.MaNXB).FirstOrDefault();
                nhaXuatBanCu.SoMaSach -= 1;
            }

            sach.TenSach = txtTenSach.Text;
            sach.MaTG = int.Parse(cbTacGia.SelectedValue.ToString());
            sach.MaNXB = int.Parse(cbNXB.SelectedValue.ToString());
            sach.MaTheLoai = int.Parse(cbTheLoai.SelectedValue.ToString());
            sach.SoLuong = int.Parse(txtSoLuong.Text);

            db.SaveChanges();
            loadDuLieu();
            MessageBox.Show("Sửa sách thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaSach.Text == "") return;

            DialogResult result = MessageBox.Show(
                "Bạn có muốn xóa sách này không?",
                "Thông báo!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No) return;

            QLTVEntities db = new QLTVEntities();

            ChiTietPhieuMuon chiTietPhieuMuon = db.ChiTietPhieuMuons.Where(p=>"S" + p.IDSach == txtMaSach.Text).FirstOrDefault();
            if (chiTietPhieuMuon != null)
            {
                MessageBox.Show("Không thể xóa sách đã phát sinh dữ liệu!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Sach sach = db.Saches.Where(p => "S" + p.ID == txtMaSach.Text).FirstOrDefault();

            TacGia tacGia = db.TacGias.Where(p=>p.MaTG == sach.MaTG).FirstOrDefault();
            NhaXuatBan nhaXuatBan = db.NhaXuatBans.Where(p=> p.MaNXB == sach.MaNXB).FirstOrDefault();
            TheLoai theLoai = db.TheLoais.Where(p => p.MaTheLoai == sach.MaTheLoai).FirstOrDefault();

            tacGia.SoMaSach -= 1;
            nhaXuatBan.SoMaSach -= 1;
            theLoai.SoMaSach -= 1;

            db.Saches.Remove(sach);
            db.SaveChanges();
            loadDuLieu();
            MessageBox.Show("Xóa sách thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void radioThem_CheckedChanged(object sender, EventArgs e)
        {
            btnSua.Hide();
            btnXoa.Hide();
            txtMaSach.Hide();
            txtDangMuon.Hide();
            lbMaSach.Hide();
            lbDangMuon.Hide();
            btnThem.Show();

            txtMaSach.Clear();
            txtTenSach.Clear();
            txtSoLuong.Clear();
            txtDangMuon.Clear();
            cbTacGia.SelectedIndex = 0;
            cbNXB.SelectedIndex = 0;
            cbTheLoai.SelectedIndex = 0;
        }

        private void radioSuaXoa_CheckedChanged(object sender, EventArgs e)
        {
            btnSua.Show();
            btnXoa.Show();
            txtMaSach.Show();
            txtDangMuon.Show();
            lbMaSach.Show();
            lbDangMuon.Show();
            btnThem.Hide();

            QLTVEntities db = new QLTVEntities();
            if (dgvSach.Rows.Count > 0)
            {
                string tenTacGia = dgvSach.Rows[0].Cells[2].Value.ToString();
                string tenNXB = dgvSach.Rows[0].Cells[3].Value.ToString();
                string tenTheLoai = dgvSach.Rows[0].Cells[4].Value.ToString();

                TacGia tacGia = db.TacGias.Where(p => p.TenTG == tenTacGia).FirstOrDefault();
                NhaXuatBan nxb = db.NhaXuatBans.Where(p => p.TenNXB == tenNXB).FirstOrDefault();
                TheLoai theLoai = db.TheLoais.Where(p => p.TenTheLoai == tenTheLoai).FirstOrDefault();

                txtMaSach.Text = dgvSach.Rows[0].Cells[0].Value.ToString();
                txtTenSach.Text = dgvSach.Rows[0].Cells[1].Value.ToString();
                txtSoLuong.Text = dgvSach.Rows[0].Cells[5].Value.ToString();
                txtDangMuon.Text = dgvSach.Rows[0].Cells[6].Value.ToString();
                cbTacGia.SelectedValue = tacGia.MaTG;
                cbNXB.SelectedValue = nxb.MaNXB;
                cbTheLoai.SelectedValue = theLoai.MaTheLoai;
            }
        }

        private void btnThemTG_Click(object sender, EventArgs e)
        {
            frmTacGia frm = new frmTacGia();
            frm.ShowDialog();
            loadDuLieu();
        }

        private void btnThemNXB_Click(object sender, EventArgs e)
        {
            frmNXB frm = new frmNXB();
            frm.ShowDialog();
            loadDuLieu();
        }

        private void btnThemTheLoai_Click(object sender, EventArgs e)
        {
            frmTheLoai frm = new frmTheLoai();
            frm.ShowDialog();
            loadDuLieu();
        }
    }
}
