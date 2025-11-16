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
    public partial class frmQuanLySach : Form
    {
        public frmQuanLySach()
        {
            InitializeComponent();
        }

        private void frmQuanLySach_Load(object sender, EventArgs e)
        {
            loadDuLieu();
            radioSuaXoa.Checked = true;
        }

        private void loadDuLieu()
        {
            QLTVEntities db = new QLTVEntities();
            dgvSach.DataSource = db.Saches.Select(p => new {
                MaSach = "S" + p.ID,
                p.TenSach,
                p.TacGia.TenTG,
                p.NhaXuatBan.TenNXB,
                p.TheLoai.TenTheLoai,
                p.SoLuong,
                p.SoSachMuon,
            }).ToList();

            cbTacGia.DataSource = db.TacGias.ToList().Prepend(new TacGia {MaTG = -1, TenTG = "" }).ToList();
            cbTacGia.DisplayMember = "TenTG";
            cbTacGia.ValueMember = "MaTG";

            cbNXB.DataSource = db.NhaXuatBans.ToList().Prepend(new NhaXuatBan { MaNXB = -1, TenNXB = "" }).ToList();
            cbNXB.DisplayMember = "TenNXB";
            cbNXB.ValueMember = "MaNXB";

            cbTheLoai.DataSource = db.TheLoais.ToList().Prepend(new TheLoai { MaTheLoai = -1, TenTheLoai = "" }).ToList();
            cbTheLoai.DisplayMember = "TenTheLoai";
            cbTheLoai.ValueMember = "MaTheLoai";

            if (radioThem.Checked) return;

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

        private void dgvSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            if (radioThem.Checked) return;

            QLTVEntities db = new QLTVEntities();

            string tenTacGia = dgvSach.Rows[e.RowIndex].Cells[2].Value.ToString();
            string tenNXB = dgvSach.Rows[e.RowIndex].Cells[3].Value.ToString();
            string tenTheLoai = dgvSach.Rows[e.RowIndex].Cells[4].Value.ToString();

            TacGia tacGia = db.TacGias.Where(p => p.TenTG == tenTacGia).FirstOrDefault();
            NhaXuatBan nxb = db.NhaXuatBans.Where(p => p.TenNXB == tenNXB).FirstOrDefault();
            TheLoai theLoai = db.TheLoais.Where(p => p.TenTheLoai == tenTheLoai).FirstOrDefault();

            txtMaSach.Text = dgvSach.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTenSach.Text = dgvSach.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSoLuong.Text = dgvSach.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtDangMuon.Text = dgvSach.Rows[e.RowIndex].Cells[6].Value.ToString();
            cbTacGia.SelectedValue = tacGia.MaTG;
            cbNXB.SelectedValue = nxb.MaNXB;
            cbTheLoai.SelectedValue = theLoai.MaTheLoai;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string luaChon = cbTimKiem.Text;
            if (luaChon == "") return;

            QLTVEntities db = new QLTVEntities();
            List<Sach> sach = new List<Sach>();

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

            if (radioThem.Checked) return;

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
                txtMaSach.Clear();
                txtTenSach.Clear();
                txtSoLuong.Clear();
                txtDangMuon.Clear();
                cbTacGia.SelectedIndex = 0;
                cbNXB.SelectedIndex = 0;
                cbTheLoai.SelectedIndex = 0;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            loadDuLieu();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có muốn thêm sách mới không?",
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

            if (!int.TryParse(txtSoLuong.Text, out int val) || val <= 0)
            {
                MessageBox.Show("Số lượng không hợp lệ!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoLuong.Focus();
                return;
            }

            QLTVEntities db = new QLTVEntities();

            TacGia tacGia = db.TacGias.Where(p => p.TenTG == tenTG).FirstOrDefault();
            if (tacGia == null)
            {
                MessageBox.Show("Tác giả không tồn tại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbTacGia.Focus();
                return;
            }

            NhaXuatBan nhaXuatBan = db.NhaXuatBans.Where(p => p.TenNXB == tenNXB).FirstOrDefault();
            if (nhaXuatBan == null)
            {
                MessageBox.Show("Nhà xuất bản không tồn tại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbNXB.Focus();
                return;
            }

            TheLoai theLoai = db.TheLoais.Where(p => p.TenTheLoai == tenTheLoai).FirstOrDefault();
            if (theLoai == null)
            {
                MessageBox.Show("Thể loại không tồn tại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbTheLoai.Focus();
                return;
            }            

            Sach sach = new Sach();
            sach.TenSach = txtTenSach.Text;
            sach.MaTG = int.Parse(cbTacGia.SelectedValue.ToString());
            sach.MaNXB = int.Parse(cbNXB.SelectedValue.ToString());
            sach.MaTheLoai = int.Parse(cbTheLoai.SelectedValue.ToString());
            sach.SoLuong = int.Parse(txtSoLuong.Text);
            sach.SoSachMuon = 0;

            tacGia.SoMaSach += 1;
            nhaXuatBan.SoMaSach += 1;
            theLoai.SoMaSach += 1;

            db.Saches.Add(sach);
            db.SaveChanges();
            loadDuLieu();
            txtTenSach.Clear();
            txtSoLuong.Clear();
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
