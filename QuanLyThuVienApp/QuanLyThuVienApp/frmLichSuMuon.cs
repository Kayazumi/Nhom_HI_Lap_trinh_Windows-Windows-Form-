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
    public partial class frmLichSuMuon : Form
    {
        public frmLichSuMuon()
        {
            InitializeComponent();
        }

        private void frmLichSuMuon_Load(object sender, EventArgs e)
        {
            loadDuLieu();
        }

        private void loadDuLieu()
        {
            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p=>p.ID == frmMainUser.ID).FirstOrDefault();

            dgvPhieuMuon.DataSource = db.PhieuMuons.Where(p => p.IDBanDoc == nguoiDung.ID)
                .Select(p => new
                {
                    p.MaPhieu,
                    p.NgayDangKyMuon,
                    TrangThai = (p.TrangThai == 0) ? "Đăng ký mượn" :
                                (p.TrangThai == 2) ? "Đã trả" :
                                (DbFunctions.TruncateTime(DateTime.Now) > DbFunctions.TruncateTime(p.HanTra)) ? "Quá hạn" : "Đang mượn",
                    p.NgayMuon,
                    p.HanTra,
                    p.NgayTra
                }).ToList();

            loadChiTietPhieu();
        }

        private void loadChiTietPhieu()
        {
            QLTVEntities db = new QLTVEntities();

            if (dgvPhieuMuon.Rows.Count > 0)
            {
                int maPhieu = int.Parse(dgvPhieuMuon.Rows[0].Cells["MaPhieu"].Value.ToString());
                string trangThai = dgvPhieuMuon.Rows[0].Cells["TrangThai"].Value.ToString();

                dgvChiTiet.DataSource = db.ChiTietPhieuMuons.Where(p => p.MaPhieu == maPhieu)
                    .Select(p => new {
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

        private void dgvPhieuMuon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            QLTVEntities db = new QLTVEntities();
            int maPhieu = int.Parse(dgvPhieuMuon.Rows[e.RowIndex].Cells["MaPhieu"].Value.ToString());
            string trangThai = dgvPhieuMuon.Rows[e.RowIndex].Cells["TrangThai"].Value.ToString();

            dgvChiTiet.DataSource = db.ChiTietPhieuMuons.Where(p => p.MaPhieu == maPhieu)
                .Select(p => new { 
                    MaPhieu = maPhieu,
                    MaSach = "S" + p.IDSach,
                    p.Sach.TenSach,
                    p.SoLuong
                }).ToList();

            if(trangThai == "Đăng ký mượn") btnXoa.Enabled = true;
            else btnXoa.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
               "Bạn có muốn xóa phiếu đăng ký không?",
               "Thông báo!",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question
           );

            if (result == DialogResult.No) return;

            int maPhieu = int.Parse(dgvChiTiet.Rows[0].Cells["MaPhieu2"].Value.ToString());
            QLTVEntities db = new QLTVEntities();

            int tongSach = 0;

            // cập nhật lại số lượng sách
            foreach (DataGridViewRow row in dgvChiTiet.Rows)
            {
                string maSach = row.Cells["MaSach"].Value.ToString();
                int soLuong = int.Parse(row.Cells["SoLuong"].Value.ToString());
                tongSach += soLuong;

                Sach sach = db.Saches.Where(p => ("S" + p.ID.ToString()) == maSach).FirstOrDefault();
                sach.SoSachMuon -= soLuong;
            }

            // cập nhật số sách đang mượn của user
            NguoiDung nguoiDung = db.NguoiDungs.Where(p=>p.ID == frmMainUser.ID).FirstOrDefault();
            nguoiDung.SoSachMuon -= tongSach;

            // xóa phiếu mượn và phiếu chi tiết
            PhieuMuon phieuMuon = db.PhieuMuons.Where(p => p.MaPhieu == maPhieu).FirstOrDefault();
            List<ChiTietPhieuMuon> chiTietPhieuMuon = db.ChiTietPhieuMuons.Where(p => p.MaPhieu == maPhieu).ToList();
            db.PhieuMuons.Remove(phieuMuon);
            db.ChiTietPhieuMuons.RemoveRange(chiTietPhieuMuon);

            // lưu vào database
            db.SaveChanges();
            loadDuLieu();
            if (dgvPhieuMuon.Rows.Count == 0)
            {
                dgvChiTiet.DataSource = null;
                dgvChiTiet.Rows.Clear();
            }
            MessageBox.Show("Xóa phiếu đăng ký thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cbLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLoc.Text == "Tất cả")
            {
                loadDuLieu();
                return;
            }

            QLTVEntities db = new QLTVEntities();
            List<PhieuMuon> phieuMuons = new List<PhieuMuon>();

            if(cbLoc.Text == "Đăng ký mượn")
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
                               p.NgayTra
                           }).ToList();

            loadChiTietPhieu();
        }

        private void dgvPhieuMuon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvPhieuMuon.Rows[e.RowIndex].Cells["TrangThai"].Value.ToString() == "Quá hạn")
                e.CellStyle.ForeColor = Color.Red;
        }
    }
}
