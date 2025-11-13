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
    public partial class frmXacThuc : MetroFramework.Forms.MetroForm
    {
        private int ID;
        private bool kiemTra = false;
        private readonly Action<bool> callback;
        public frmXacThuc()
        {
            InitializeComponent();
        }

        public frmXacThuc(int _ID)
        {
            ID = _ID;
            InitializeComponent();
        }

        public frmXacThuc(int _ID, Action<bool> callback)
        {
            ID = _ID;
            kiemTra = true;
            InitializeComponent();
            this.callback = callback;
        }

        private void frmXacNhanOTP_Load(object sender, EventArgs e)
        {
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (kiemTra) callback(false);
            this.Close();
        }

        
        
        private void btnGuiLai_Click(object sender, EventArgs e)
        {
            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == ID).SingleOrDefault();

            Random random = new Random();
            string OTP = random.Next(100000, 999999).ToString();

            nguoiDung.MaOTP = OTP;
            GuiEmail.guiEmail(nguoiDung.Email, "Mã xác thực của bạn là " + OTP);
            nguoiDung.ThoiGianNhanOTP = DateTime.Now;
            db.SaveChanges();
        }

        private void btnXacThuc_Click(object sender, EventArgs e)
        {
            if (txtMaXacThuc.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã xác thực!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == ID).SingleOrDefault();

            if (nguoiDung == null) return;

            if (txtMaXacThuc.Text != nguoiDung.MaOTP)
            {
                MessageBox.Show("Mã xác thực không chính xác!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaXacThuc.Focus();
                return;
            }

            if ((DateTime.Now - nguoiDung.ThoiGianNhanOTP.Value).TotalMinutes > 5)
            {
                MessageBox.Show("Mã xác thực đã hết hạn!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            nguoiDung.TrangThaiXacThuc = true;
            nguoiDung.BiKhoa = false;
            nguoiDung.QuyenHan = "user";
            nguoiDung.SoSachMuon = 0;
            nguoiDung.NgayDangKi = DateTime.Now;
            db.SaveChanges();
            MessageBox.Show("Xác thực thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (kiemTra) callback(true);
            
            this.Close();
        }
    }
}
