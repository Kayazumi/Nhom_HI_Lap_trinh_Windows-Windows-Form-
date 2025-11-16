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
    /// Form hiển thị báo cáo tỷ lệ sách theo thể loại (phần trăm)
    /// </summary>
    public partial class frmReportTiLeSachTheoTheLoai : Form
    {
        /// <summary>
        /// Khởi tạo form báo cáo tỷ lệ sách theo thể loại
        /// </summary>
        public frmReportTiLeSachTheoTheLoai()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện Load form - Load dữ liệu và hiển thị báo cáo
        /// Tính tỷ lệ phần trăm số lượng sách của mỗi thể loại
        /// </summary>
        private void frmReportTiLeSachTheoTheLoai_Load(object sender, EventArgs e)
        {
            QLTVEntities db = new QLTVEntities();

            // Lấy thông tin người in (Admin)
            NguoiDung nguoiIn = db.NguoiDungs.Where(p => p.ID == frmMainAdmin.ID).FirstOrDefault();
            if (nguoiIn == null) return;

            // Thiết lập tham số cho báo cáo
            ReportParameter[] para = new ReportParameter[1];
            para[0] = new ReportParameter("NguoiIn", nguoiIn.HoTen);

            reportViewer1.LocalReport.SetParameters(para);

            // Tính tổng số lượng sách
            int tongSoLuongSach = 0;
            if (db.Saches.Any()) tongSoLuongSach = db.Saches.Sum(p => p.SoLuong ?? 0);

            if (tongSoLuongSach == 0) return;

            // Nhóm sách theo thể loại và tính tỷ lệ phần trăm
            List<ThongTinSachTheoTheLoai> data = db.Saches
                .GroupBy(p => p.MaTheLoai)
                .Select(g => new ThongTinSachTheoTheLoai
                {
                    TheLoai = g.FirstOrDefault().TheLoai.TenTheLoai,
                    // Tính tỷ lệ phần trăm (làm tròn 2 chữ số thập phân)
                    TiLe = (float)Math.Round((float)(g.Sum(p => p.SoLuong ?? 0)) / tongSoLuongSach * 100, 2)
                })
                .ToList();

            // Thêm dữ liệu vào báo cáo
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetTiLeSachTheoTheLoai", data));

            this.reportViewer1.RefreshReport();
        }
    }
}
