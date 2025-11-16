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
    public partial class frmReportHoaDonPhat : Form
    {
        private int ID_NguoiBiPhat, soNgay;
        private string hanTra;

        public frmReportHoaDonPhat()
        {
            InitializeComponent();
        }

        public frmReportHoaDonPhat(int _ID_NguoiBiPhat, string _hanTra, int _soNgay)
        {
            ID_NguoiBiPhat = _ID_NguoiBiPhat;
            soNgay = _soNgay;
            hanTra = _hanTra;
            InitializeComponent();
        }


        private void frmReportHoaDonPhat_Load(object sender, EventArgs e)
        {
            QLTVEntities db = new QLTVEntities();

            NguoiDung nguoiBiPhat = db.NguoiDungs.Where(p => p.ID == ID_NguoiBiPhat).FirstOrDefault();
            if (nguoiBiPhat == null) return;

            NguoiDung nguoiIn = db.NguoiDungs.Where(p => p.ID == frmMainAdmin.ID).FirstOrDefault();
            if (nguoiIn == null) return;

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
