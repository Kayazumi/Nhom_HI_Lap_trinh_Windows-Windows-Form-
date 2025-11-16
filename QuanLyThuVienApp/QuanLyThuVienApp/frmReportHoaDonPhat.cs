using Microsoft.Reporting.WinForms;
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
    /// Form hiển thị báo cáo hóa đơn phạt khi bạn đọc trả sách quá hạn
    /// </summary>
    public partial class frmReportHoaDonPhat : Form
    {
        /// <summary>
        /// ID của người bị phạt
        /// </summary>
        private int ID_NguoiBiPhat;
        
        /// <summary>
        /// Số ngày quá hạn
        /// </summary>
        private int soNgay;
        
        /// <summary>
        /// Hạn trả sách
        /// </summary>
        private string hanTra;

        /// <summary>
        /// Khởi tạo form báo cáo hóa đơn phạt (không có tham số)
        /// </summary>
        public frmReportHoaDonPhat()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Khởi tạo form báo cáo hóa đơn phạt với thông tin người bị phạt
        /// </summary>
        /// <param name="_ID_NguoiBiPhat">ID của người bị phạt</param>
        /// <param name="_hanTra">Hạn trả sách</param>
        /// <param name="_soNgay">Số ngày quá hạn</param>
        public frmReportHoaDonPhat(int _ID_NguoiBiPhat, string _hanTra, int _soNgay)
        {
            ID_NguoiBiPhat = _ID_NguoiBiPhat;
            soNgay = _soNgay;
            hanTra = _hanTra;
            InitializeComponent();
        }


        /// <summary>
        /// Sự kiện Load form - Load dữ liệu và hiển thị báo cáo
        /// </summary>
        private void frmReportHoaDonPhat_Load(object sender, EventArgs e)
        {
            QLTVEntities db = new QLTVEntities();

            // Lấy thông tin người bị phạt
            NguoiDung nguoiBiPhat = db.NguoiDungs.Where(p => p.ID == ID_NguoiBiPhat).FirstOrDefault();
            if (nguoiBiPhat == null) return;

            // Lấy thông tin người in (Admin)
            NguoiDung nguoiIn = db.NguoiDungs.Where(p => p.ID == frmMainAdmin.ID).FirstOrDefault();
            if (nguoiIn == null) return;

            // Thiết lập các tham số cho báo cáo
            ReportParameter[] para = new ReportParameter[5];
            para[0] = new ReportParameter("NguoiIn", nguoiIn.HoTen);
            para[1] = new ReportParameter("NguoiBiPhat", nguoiBiPhat.HoTen);
            para[2] = new ReportParameter("HanTra", hanTra);
            para[3] = new ReportParameter("Email", nguoiBiPhat.Email);
            para[4] = new ReportParameter("SoNgay", soNgay.ToString());

            reportViewer1.LocalReport.SetParameters(para);
            this.reportViewer1.RefreshReport();
        }
    }
}
