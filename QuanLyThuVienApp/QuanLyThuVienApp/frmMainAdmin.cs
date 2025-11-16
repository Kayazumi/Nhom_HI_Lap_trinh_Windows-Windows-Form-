using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVienApp
{
    public partial class frmMainAdmin : MetroFramework.Forms.MetroForm
    {
        public static int ID;
        public static string text;
        public frmMainAdmin()
        {
            InitializeComponent();
        }

        public frmMainAdmin(int _ID, string _tenNguoiDung, bool? _biKhoa)
        {
            ID = _ID;
            InitializeComponent();

            if (_biKhoa == true)
            {
                text = "Tài khoản của bạn đang bị khóa, vui lòng đến thư viện để được xử lý!";
                tslbThongTin.ForeColor = Color.Red;
            }
            else if (_biKhoa == false)
                text = "Chào mừng: " + _tenNguoiDung;

        }

        private void frmMainAdmin_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            frmInfor frm = new frmInfor();
            frm.MdiParent = this;
            frm.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tslbThongTin.Text = text;
            tslbTimer.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss  ");
        }

        private void btnInfor_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
                form.Close();
            frmInfor frm = new frmInfor();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnCaNhan_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
                form.Close();
            frmCaNhan frm = new frmCaNhan(ID);
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            frmDangNhap frm = new frmDangNhap();
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }

        private void btnSach_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
                form.Close();
            frmSach frm = new frmSach();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnQLSach_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
                form.Close();
            frmQuanLySach frm = new frmQuanLySach();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnQLBanDoc_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
                form.Close();
            frmQuanLyBanDoc frm = new frmQuanLyBanDoc();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnQLPhieuMuon_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
                form.Close();
            frmQuanLyPhieuMuon frm = new frmQuanLyPhieuMuon();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
                form.Close();
            frmCColumn_SachTheoTheLoai frm = new frmCColumn_SachTheoTheLoai();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnPhanQuyen_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
                form.Close();
            frmPhanQuyen frm = new frmPhanQuyen();
            frm.MdiParent = this;
            frm.Show();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
                form.Close();
            frmCaNhan frm = new frmCaNhan();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnTroGiup_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
                form.Close();
            frmTroGiup frm = new frmTroGiup();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
