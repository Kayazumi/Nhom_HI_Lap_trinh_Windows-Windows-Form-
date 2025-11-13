using MetroFramework.Controls;
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
    public partial class frmTheLoai : MetroFramework.Forms.MetroForm
    {
        public frmTheLoai()
        {
            InitializeComponent();
        }

        private void frmTheLoai_Load(object sender, EventArgs e)
        {
            loadDuLieu();
        }

        private void loadDuLieu()
        {
            QLTVEntities db = new QLTVEntities();
            dgvTheLoai.DataSource = db.TheLoais.Select(p => new
            {
                MaTheLoai = "TL" + p.MaTheLoai,
                p.TenTheLoai,
                p.SoMaSach,
                p.MoTa
            }).ToList();

            if (dgvTheLoai.Rows.Count > 0)
            {
                txtMa.Text = dgvTheLoai.Rows[0].Cells["MaTheLoai"].Value.ToString();
                txtTen.Text = dgvTheLoai.Rows[0].Cells["TenTheLoai"].Value.ToString();
                txtMoTa.Text = dgvTheLoai.Rows[0].Cells["MoTa"].Value.ToString();
            }
        }

        private void dgvTheLoai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            txtMa.Text = dgvTheLoai.Rows[e.RowIndex].Cells["MaTheLoai"].Value.ToString();
            txtTen.Text = dgvTheLoai.Rows[e.RowIndex].Cells["TenTheLoai"].Value.ToString();
            txtMoTa.Text = dgvTheLoai.Rows[e.RowIndex].Cells["MoTa"].Value.ToString();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            QLTVEntities db = new QLTVEntities();
            List<TheLoai> theLoais = new List<TheLoai>();

            if (cbTim.Text == "Mã thể loại")
            {
                theLoais = db.TheLoais.Where(p => ("TL" + p.MaTheLoai).Contains(txtTim.Text)).ToList();
            }
            else if (cbTim.Text == "Tên thể loại")
            {
                theLoais = db.TheLoais.Where(p => p.TenTheLoai.Contains(txtTim.Text)).ToList();
            }
            else return;

            dgvTheLoai.DataSource = theLoais.Select(p => new
            {
                MaTheLoai = "TL" + p.MaTheLoai,
                p.TenTheLoai,
                p.SoMaSach,
                p.MoTa
            }).ToList();

            if (dgvTheLoai.Rows.Count > 0)
            {
                txtMa.Text = dgvTheLoai.Rows[0].Cells["MaTheLoai"].Value.ToString();
                txtTen.Text = dgvTheLoai.Rows[0].Cells["TenTheLoai"].Value.ToString();
                txtMoTa.Text = dgvTheLoai.Rows[0].Cells["MoTa"].Value.ToString();
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có muốn thêm thể loại mới không?",
                "Thông báo!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No) return;

            if (txtTenTL.Text == "" || txtMoTaTL.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TheLoai theLoai = new TheLoai();
            theLoai.TenTheLoai = txtTenTL.Text;
            theLoai.MoTa = txtMoTaTL.Text;
            theLoai.SoMaSach = 0;

            QLTVEntities db = new QLTVEntities();
            db.TheLoais.Add(theLoai);
            db.SaveChanges();
            loadDuLieu();
            txtTenTL.Clear();
            txtMoTaTL.Clear();
            MessageBox.Show("Thêm thể loại thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
               "Bạn có muốn sửa thông tin thể loại này không?",
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
            int maTL = int.Parse(txtMa.Text.Substring(2));
            TheLoai theLoai = db.TheLoais.Where(p => p.MaTheLoai == maTL).FirstOrDefault();

            theLoai.TenTheLoai = txtTen.Text;
            theLoai.MoTa = txtMoTa.Text;

            db.SaveChanges();
            loadDuLieu();
            MessageBox.Show("Sửa thể loại thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
               "Bạn có muốn xóa thể loại này không?",
               "Thông báo!",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question
           );

            if (result == DialogResult.No) return;
            QLTVEntities db = new QLTVEntities();
            int maTL = int.Parse(txtMa.Text.Substring(2));
            TheLoai theLoai = db.TheLoais.Where(p => p.MaTheLoai == maTL).FirstOrDefault();

            if (theLoai.SoMaSach != 0)
            {
                MessageBox.Show("Thể loại này đang có sách trong thư viện!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            db.TheLoais.Remove(theLoai);
            db.SaveChanges();
            loadDuLieu();
            MessageBox.Show("Xóa thể loại thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
