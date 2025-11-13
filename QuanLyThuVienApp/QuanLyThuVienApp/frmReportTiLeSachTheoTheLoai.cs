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
    public partial class frmReportTiLeSachTheoTheLoai : Form
    {
        public frmReportTiLeSachTheoTheLoai()
        {
            InitializeComponent();
        }

        private void frmReportTiLeSachTheoTheLoai_Load(object sender, EventArgs e)
        {
            QLTVEntities db = new QLTVEntities();

            NguoiDung nguoiIn = db.NguoiDungs.Where(p => p.ID == frmMainAdmin.ID).FirstOrDefault();
            if (nguoiIn == null) return;

            ReportParameter[] para = new ReportParameter[1];
            para[0] = new ReportParameter("NguoiIn", nguoiIn.HoTen);

            reportViewer1.LocalReport.SetParameters(para);

            int tongSoLuongSach = 0;
            if (db.Saches.Any()) tongSoLuongSach = db.Saches.Sum(p => p.SoLuong ?? 0);

            if (tongSoLuongSach == 0) return;

            List<ThongTinSachTheoTheLoai> data = db.Saches
                .GroupBy(p => p.MaTheLoai)
                .Select(g => new ThongTinSachTheoTheLoai
                {
                    TheLoai = g.FirstOrDefault().TheLoai.TenTheLoai,
                    TiLe = (float)Math.Round((float)(g.Sum(p => p.SoLuong ?? 0)) / tongSoLuongSach * 100, 2)
                })
                .ToList();

            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetTiLeSachTheoTheLoai", data));

            this.reportViewer1.RefreshReport();
        }
    }
}
