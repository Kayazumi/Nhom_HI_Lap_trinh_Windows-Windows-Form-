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
    /// Form hiển thị báo cáo số lượng sách theo thể loại
    /// </summary>
    public partial class frmReportSLSachTheoTheLoai : Form
    {
        /// <summary>
        /// Khởi tạo form báo cáo số lượng sách theo thể loại
        /// </summary>
        public frmReportSLSachTheoTheLoai()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện Load form - Load dữ liệu và hiển thị báo cáo
        /// </summary>
        private void frmReportSLSachTheoTheLoai_Load(object sender, EventArgs e)
        {
            QLTVEntities db = new QLTVEntities();
            // Lấy thông tin người in (Admin)
            NguoiDung nguoiIn = db.NguoiDungs.Where(p=>p.ID == frmMainAdmin.ID).FirstOrDefault();
            if (nguoiIn == null) return;

            // Thiết lập tham số cho báo cáo
            ReportParameter[] para = new ReportParameter[1];
            para[0] = new ReportParameter("NguoiIn", nguoiIn.HoTen);

            reportViewer1.LocalReport.SetParameters(para);

            // Nhóm sách theo thể loại và tính tổng số lượng
            List<ThongTinSachTheoTheLoai> data = db.Saches
                .GroupBy(p => p.MaTheLoai)
                .Select(g => new ThongTinSachTheoTheLoai {
                    TheLoai = g.FirstOrDefault().TheLoai.TenTheLoai,
                    SoLuongSach = g.Sum(p => p.SoLuong ?? 0)
                })
                .ToList();

            // Thêm dữ liệu vào báo cáo
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetSLSachTheoTheLoai", data));

            this.reportViewer1.RefreshReport();
        }
    }
}
