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
    /// <summary>
    /// Form gia hạn thời gian mượn sách
    /// Cho phép Admin gia hạn thời gian trả sách cho bạn đọc
    /// </summary>
    public partial class frmGiaHan : MetroFramework.Forms.MetroForm
    {
        /// <summary>
        /// Mã phiếu mượn cần gia hạn
        /// </summary>
        public static int maPhieu;
        /// <summary>
        /// Form quản lý phiếu mượn (để cập nhật sau khi gia hạn)
        /// </summary>
        public static frmQuanLyPhieuMuon frm;
        
        /// <summary>
        /// Khởi tạo form gia hạn
        /// </summary>
        public frmGiaHan()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Khởi tạo form gia hạn với mã phiếu cụ thể
        /// </summary>
        /// <param name="_maPhieu">Mã phiếu mượn cần gia hạn</param>
        public frmGiaHan(int _maPhieu)
        {
            maPhieu = _maPhieu;
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện Load form
        /// </summary>
        private void frmGiaHan_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Đóng form
        /// </summary>
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Xử lý gia hạn thời gian mượn sách
        /// Cập nhật HanTra = HanTra hiện tại + số ngày gia hạn
        /// </summary>
        private void btnGiaHan_Click(object sender, EventArgs e)
        {
            // Lấy số ngày gia hạn từ NumericUpDown
            int days = int.Parse(numericUpDown1.Value.ToString());
            if(days == 0)
            {
                MessageBox.Show("Vui lòng chọn số ngày gia hạn!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            QLTVEntities db = new QLTVEntities();
            // Lấy phiếu mượn cần gia hạn
            PhieuMuon phieuMuon = db.PhieuMuons.Where(p=>p.MaPhieu == maPhieu).FirstOrDefault();

            // Cập nhật hạn trả = hạn trả hiện tại + số ngày gia hạn
            DateTime hanTra = phieuMuon.HanTra.Value; 
            phieuMuon.HanTra = hanTra.AddDays(days);

            // Lưu thay đổi vào database
            db.SaveChanges();
            MessageBox.Show("Gia hạn thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Đánh dấu đã gia hạn để form quản lý phiếu mượn reload dữ liệu
            frmQuanLyPhieuMuon.giaHan = true;
            this.Close();
        }
    }
}
