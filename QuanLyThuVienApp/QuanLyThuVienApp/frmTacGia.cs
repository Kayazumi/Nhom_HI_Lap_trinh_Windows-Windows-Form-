using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace QuanLyThuVienApp
{
    public partial class frmTacGia : MetroFramework.Forms.MetroForm
    {
        public frmTacGia()
        {
            InitializeComponent();
        }

        private void frmTacGia_Load(object sender, EventArgs e)
        {
            loadDuLieu();
        }

        private void loadDuLieu()
        {
            QLTVEntities db = new QLTVEntities();
            dgvTacGia.DataSource = db.TacGias.Select(p => new
            {
                MaTG = "TG" + p.MaTG,
                p.TenTG,
                p.SoMaSach,
                p.MoTa
            }).ToList();

            if(dgvTacGia.Rows.Count > 0 ) 
            {
                txtMa.Text = dgvTacGia.Rows[0].Cells["MaTG"].Value.ToString();
                txtTen.Text = dgvTacGia.Rows[0].Cells["TenTG"].Value.ToString();
                txtMoTa.Text = dgvTacGia.Rows[0].Cells["MoTa"].Value.ToString();
            }
        }

        private void dgvTacGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            txtMa.Text = dgvTacGia.Rows[e.RowIndex].Cells["MaTG"].Value.ToString();
            txtTen.Text = dgvTacGia.Rows[e.RowIndex].Cells["TenTG"].Value.ToString();
            txtMoTa.Text = dgvTacGia.Rows[e.RowIndex].Cells["MoTa"].Value.ToString();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            QLTVEntities db = new QLTVEntities();
            List<TacGia> tacGias = new List<TacGia>();

            if (cbTim.Text == "Mã tác giả")
            {
                tacGias = db.TacGias.Where(p => ("TG" + p.MaTG).Contains(txtTim.Text)).ToList();
            }
            else if (cbTim.Text == "Tên tác giả")
            {
                tacGias = db.TacGias.Where(p => p.TenTG.Contains(txtTim.Text)).ToList();

            }
            else return;

            dgvTacGia.DataSource = tacGias.Select(p => new
            {
                MaTG = "TG" + p.MaTG,
                p.TenTG,
                p.SoMaSach,
                p.MoTa
            }).ToList();

            if (dgvTacGia.Rows.Count > 0)
            {
                txtMa.Text = dgvTacGia.Rows[0].Cells["MaTG"].Value.ToString();
                txtTen.Text = dgvTacGia.Rows[0].Cells["TenTG"].Value.ToString();
                txtMoTa.Text = dgvTacGia.Rows[0].Cells["MoTa"].Value.ToString();
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
                "Bạn có muốn thêm tác giả mới không?",
                "Thông báo!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No) return;

            if (txtTenTG.Text == "" || txtMoTaTG.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TacGia tacGia = new TacGia();
            tacGia.TenTG = txtTenTG.Text;
            tacGia.MoTa = txtMoTaTG.Text;
            tacGia.SoMaSach = 0;

            QLTVEntities db = new QLTVEntities();
            db.TacGias.Add(tacGia);
            db.SaveChanges();
            loadDuLieu();
            txtTenTG.Clear();
            txtMoTaTG.Clear();
            MessageBox.Show("Thêm tác giả thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có muốn sửa thông tin tác giả này không?",
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
            int maTG = int.Parse(txtMa.Text.Substring(2));
            TacGia tacGia = db.TacGias.Where(p=>p.MaTG == maTG).FirstOrDefault();

            tacGia.TenTG = txtTen.Text;
            tacGia.MoTa = txtMoTa.Text;

            db.SaveChanges();
            loadDuLieu();
            MessageBox.Show("Sửa tác giả thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có muốn xóa tác giả này không?",
                "Thông báo!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No) return;

            QLTVEntities db = new QLTVEntities();
            int maTG = int.Parse(txtMa.Text.Substring(2));
            TacGia tacGia = db.TacGias.Where(p=>p.MaTG == maTG).FirstOrDefault();

            if(tacGia.SoMaSach != 0) 
            {
                MessageBox.Show("Tác giả đang có sách trong thư viện!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            db.TacGias.Remove(tacGia);
            db.SaveChanges();
            loadDuLieu();
            MessageBox.Show("Xóa tác giả thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
