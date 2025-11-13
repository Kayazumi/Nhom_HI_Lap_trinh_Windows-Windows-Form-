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
    public partial class frmGiaHan : MetroFramework.Forms.MetroForm
    {
        public static int maPhieu;
        public static frmQuanLyPhieuMuon frm;
        public frmGiaHan()
        {
            InitializeComponent();
        }

        public frmGiaHan(int _maPhieu)
        {
            maPhieu = _maPhieu;
            InitializeComponent();
        }

        private void frmGiaHan_Load(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGiaHan_Click(object sender, EventArgs e)
        {
            int days = int.Parse(numericUpDown1.Value.ToString());
            if(days == 0)
            {
                MessageBox.Show("Vui lòng chọn số ngày gia hạn!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            QLTVEntities db = new QLTVEntities();
            PhieuMuon phieuMuon = db.PhieuMuons.Where(p=>p.MaPhieu == maPhieu).FirstOrDefault();

            DateTime hanTra = phieuMuon.HanTra.Value; 
            phieuMuon.HanTra = hanTra.AddDays(days);

            db.SaveChanges();
            MessageBox.Show("Gia hạn thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            frmQuanLyPhieuMuon.giaHan = true;
            this.Close();
        }
    }
}
