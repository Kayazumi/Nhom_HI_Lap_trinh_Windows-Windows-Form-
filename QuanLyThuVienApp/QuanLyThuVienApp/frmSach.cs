using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVienApp
{
    public partial class frmSach : Form
    {
        public frmSach()
        {
            InitializeComponent();
        }

        private void frmSach_Load(object sender, EventArgs e)
        {
            loadDuLieu();
        }

        private void loadDuLieu()
        {
            QLTVEntities db = new QLTVEntities();
            dgvSach.DataSource = db.Saches.Select(p => new {
                MaSach = "S" + p.ID, p.TenSach, p.TacGia.TenTG, p.NhaXuatBan.TenNXB, p.TheLoai.TenTheLoai, p.SoLuong, p.SoSachMuon
            }).ToList();

            if (dgvSach.Rows.Count > 0)
            {
                int soLuongSachCon = int.Parse(dgvSach.Rows[0].Cells[5].Value.ToString()) - int.Parse(dgvSach.Rows[0].Cells[6].Value.ToString());

                txtMaSach.Text = dgvSach.Rows[0].Cells[0].Value.ToString();
                txtTenSach.Text = dgvSach.Rows[0].Cells[1].Value.ToString();
                txtTacGia.Text = dgvSach.Rows[0].Cells[2].Value.ToString();
                txtNXB.Text = dgvSach.Rows[0].Cells[3].Value.ToString();
                txtTheLoai.Text = dgvSach.Rows[0].Cells[4].Value.ToString();
                txtConSan.Text = soLuongSachCon.ToString();
            }
        }
    
        private void dgvSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                int row = e.RowIndex;
                int soLuongSachCon = int.Parse(dgvSach.Rows[row].Cells[5].Value.ToString()) - int.Parse(dgvSach.Rows[row].Cells[6].Value.ToString());

                txtMaSach.Text = dgvSach.Rows[row].Cells[0].Value.ToString();
                txtTenSach.Text = dgvSach.Rows[row].Cells[1].Value.ToString();
                txtTacGia.Text = dgvSach.Rows[row].Cells[2].Value.ToString();
                txtNXB.Text = dgvSach.Rows[row].Cells[3].Value.ToString();
                txtTheLoai.Text = dgvSach.Rows[row].Cells[4].Value.ToString();
                txtConSan.Text = soLuongSachCon.ToString();
            }
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

            if (dgvSach.Rows.Count > 0)
            {
                int soLuongSachCon = int.Parse(dgvSach.Rows[0].Cells[5].Value.ToString()) - int.Parse(dgvSach.Rows[0].Cells[6].Value.ToString());
                
                txtMaSach.Text = dgvSach.Rows[0].Cells[0].Value.ToString();
                txtTenSach.Text = dgvSach.Rows[0].Cells[1].Value.ToString();
                txtTacGia.Text = dgvSach.Rows[0].Cells[2].Value.ToString();
                txtNXB.Text = dgvSach.Rows[0].Cells[3].Value.ToString();
                txtTheLoai.Text = dgvSach.Rows[0].Cells[4].Value.ToString();
                txtConSan.Text = soLuongSachCon.ToString();
            }
            else
            {
                txtMaSach.Clear();
                txtTenSach.Clear();
                txtTacGia.Clear();
                txtNXB.Clear();
                txtTheLoai.Clear();
                txtConSan.Clear();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            loadDuLieu();
        }
    }
}
