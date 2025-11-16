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
    public partial class frmChonBanDoc : MetroFramework.Forms.MetroForm
    {
        public static bool thoat = false;

        public frmChonBanDoc()
        {
            InitializeComponent();
        }

        private void frmChonBanDoc_Load(object sender, EventArgs e)
        {
            loadDuLieu();
        }

        private void loadDuLieu()
        {
            QLTVEntities db  = new QLTVEntities();
            dgvBanDoc.DataSource = db.NguoiDungs.Where(p => p.TrangThaiXacThuc == true && p.QuyenHan == "user" && p.BiKhoa == false)
                .Select(p => new
                {
                    MaBanDoc = "BD" + p.ID,
                    TenBanDoc = p.HoTen,
                    p.Email,
                    CoTheMuon = 10 - p.SoSachMuon
                }).ToList();

            if (dgvBanDoc.Rows.Count > 0)
            {
                txtID.Text = dgvBanDoc.Rows[0].Cells["MaBanDoc"].Value.ToString();
                txtTen.Text = dgvBanDoc.Rows[0].Cells["TenBanDoc"].Value.ToString();
                txtEmail.Text = dgvBanDoc.Rows[0].Cells["Email"].Value.ToString();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMuonSach_Click(object sender, EventArgs e)
        {
            thoat = false;
            if (txtID.Text == "") return;
            frmChoMuonSach frm = new frmChoMuonSach(int.Parse(txtID.Text.Substring(2)));
            frm.ShowDialog();
            if (thoat) this.Close();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string luaChon = cbTimKiem.Text;
            if (luaChon == "") return;

            QLTVEntities db = new QLTVEntities();
            List<NguoiDung> nguoiDungs = new List<NguoiDung>();

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

            dgvBanDoc.DataSource = nguoiDungs.Select(p => new
            {
                MaBanDoc = "BD" + p.ID,
                TenBanDoc = p.HoTen,
                p.Email,
                CoTheMuon = 10 - p.SoSachMuon
            }).ToList();

            if (dgvBanDoc.Rows.Count > 0)
            {
                txtID.Text = dgvBanDoc.Rows[0].Cells["MaBanDoc"].Value.ToString();
                txtTen.Text = dgvBanDoc.Rows[0].Cells["TenBanDoc"].Value.ToString();
                txtEmail.Text = dgvBanDoc.Rows[0].Cells["Email"].Value.ToString();
            }
            else
            {
                txtID.Clear();
                txtEmail.Clear();
                txtTen.Clear();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            loadDuLieu();
        }

        private void dgvBanDoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            txtID.Text = dgvBanDoc.Rows[e.RowIndex].Cells["MaBanDoc"].Value.ToString();
            txtTen.Text = dgvBanDoc.Rows[e.RowIndex].Cells["TenBanDoc"].Value.ToString();
            txtEmail.Text = dgvBanDoc.Rows[e.RowIndex].Cells["Email"].Value.ToString();
        }
    }
}
