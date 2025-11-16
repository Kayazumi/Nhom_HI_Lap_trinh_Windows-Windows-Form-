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
    public partial class frmReportSLSachTheoTheLoai : Form
    {
        public frmReportSLSachTheoTheLoai()
        {
            InitializeComponent();
        }

        private void frmReportSLSachTheoTheLoai_Load(object sender, EventArgs e)
        {
            QLTVEntities db = new QLTVEntities();
            NguoiDung nguoiIn = db.NguoiDungs.Where(p=>p.ID == frmMainAdmin.ID).FirstOrDefault();
            if (nguoiIn == null) return;

            ReportParameter[] para = new ReportParameter[1];
            para[0] = new ReportParameter("NguoiIn", nguoiIn.HoTen);

            reportViewer1.LocalReport.SetParameters(para);

            List<ThongTinSachTheoTheLoai> data = db.Saches
                .GroupBy(p => p.MaTheLoai)
                .Select(g => new ThongTinSachTheoTheLoai {
                    TheLoai = g.FirstOrDefault().TheLoai.TenTheLoai,
                    SoLuongSach = g.Sum(p => p.SoLuong ?? 0)
                })
                .ToList();

            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetSLSachTheoTheLoai", data));

            this.reportViewer1.RefreshReport();
        }
    }
}
