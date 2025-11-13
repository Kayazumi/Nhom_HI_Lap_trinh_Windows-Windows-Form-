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
    public partial class frmNXB : MetroFramework.Forms.MetroForm
    {
        public frmNXB()
        {
            InitializeComponent();
        }

        private void frmNXB_Load(object sender, EventArgs e)
        {
            loadDuLieu();
        }

        private void loadDuLieu()
        {
            QLTVEntities db = new QLTVEntities();
            dgvNXB.DataSource = db.NhaXuatBans.Select(p => new
            {
                MaNXB = "NXB" + p.MaNXB,
                p.TenNXB,
                p.SoMaSach,
                p.MoTa
            }).ToList();

            if (dgvNXB.Rows.Count > 0)
            {
                txtMa.Text = dgvNXB.Rows[0].Cells["MaNXB"].Value.ToString();
                txtTen.Text = dgvNXB.Rows[0].Cells["TenNXB"].Value.ToString();
                txtMoTa.Text = dgvNXB.Rows[0].Cells["MoTa"].Value.ToString();
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            QLTVEntities db = new QLTVEntities();
            List<NhaXuatBan> nxbs = new List<NhaXuatBan>();

            if (cbTim.Text == "Mã NXB")
            {
                nxbs = db.NhaXuatBans.Where(p=> ("NXB" + p.MaNXB).Contains(txtTim.Text)).ToList();
            }
            else if (cbTim.Text == "Tên NXB")
            {
                nxbs = db.NhaXuatBans.Where(p => p.TenNXB.Contains(txtTim.Text)).ToList();

            }
            else return;

            dgvNXB.DataSource = nxbs.Select(p => new
            {
                MaNXB = "NXB" + p.MaNXB,
                p.TenNXB,
                p.SoMaSach,
                p.MoTa
            }).ToList();

            if (dgvNXB.Rows.Count > 0)
            {
                txtMa.Text = dgvNXB.Rows[0].Cells["MaNXB"].Value.ToString();
                txtTen.Text = dgvNXB.Rows[0].Cells["TenNXB"].Value.ToString();
                txtMoTa.Text = dgvNXB.Rows[0].Cells["MoTa"].Value.ToString();
            }
            else
            {
                txtMa.Clear();
                txtTen.Clear();
                txtMoTa.Clear();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            loadDuLieu();
        }

        private void dgvNXB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            txtMa.Text = dgvNXB.Rows[e.RowIndex].Cells["MaNXB"].Value.ToString();
            txtTen.Text = dgvNXB.Rows[e.RowIndex].Cells["TenNXB"].Value.ToString();
            txtMoTa.Text = dgvNXB.Rows[e.RowIndex].Cells["MoTa"].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
               "Bạn có muốn thêm nhà xuất bản mới không?",
               "Thông báo!",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question
           );

            if (result == DialogResult.No) return;

            if (txtTenNXB.Text == "" || txtMoTaNXB.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            NhaXuatBan nxb = new NhaXuatBan();
            nxb.TenNXB = txtTenNXB.Text;
            nxb.MoTa = txtMoTaNXB.Text;
            nxb.SoMaSach = 0;

            QLTVEntities db = new QLTVEntities();
            db.NhaXuatBans.Add(nxb);
            db.SaveChanges();
            loadDuLieu();
            txtTenNXB.Clear();
            txtMoTaNXB.Clear();
            MessageBox.Show("Thêm nhà xuất bản thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
               "Bạn có muốn sửa thông tin nhà xuất bản này không?",
               "Thông báo!",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question
           );

            if (result == DialogResult.No) return;

            if (txtTen.Text == "" || txtMoTa.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            QLTVEntities db = new QLTVEntities();
            int maNXB = int.Parse(txtMa.Text.Substring(3));
            NhaXuatBan nxb = db.NhaXuatBans.Where(p => p.MaNXB == maNXB).FirstOrDefault();

            nxb.TenNXB = txtTen.Text;
            nxb.MoTa = txtMoTa.Text;

            db.SaveChanges();
            loadDuLieu();
            MessageBox.Show("Sửa nhà xuất bản thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
               "Bạn có muốn xóa nhà xuất bản này không?",
               "Thông báo!",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question
           );

            if (result == DialogResult.No) return;

            QLTVEntities db = new QLTVEntities();
            int maNXB = int.Parse(txtMa.Text.Substring(3));
            NhaXuatBan nxb = db.NhaXuatBans.Where(p => p.MaNXB == maNXB).FirstOrDefault();

            if (nxb.SoMaSach != 0)
            {
                MessageBox.Show("Nhà xuất bản đang có sách trong thư viện!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            db.NhaXuatBans.Remove(nxb);
            db.SaveChanges();
            loadDuLieu();
            MessageBox.Show("Xóa nhà xuất bản thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
