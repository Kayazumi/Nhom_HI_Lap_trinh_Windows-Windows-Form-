using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLyThuVienApp
{
    /// <summary>
    /// Form hiển thị biểu đồ cột số lượng sách theo thể loại
    /// Cho phép Admin in báo cáo, User chỉ xem
    /// </summary>
    public partial class frmCColumn_SachTheoTheLoai : Form
    {
        /// <summary>
        /// Khởi tạo form biểu đồ cột
        /// </summary>
        public frmCColumn_SachTheoTheLoai()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Sự kiện Load form - Ẩn nút in báo cáo nếu là User, sau đó load biểu đồ
        /// </summary>
        private void frmCColumn_SachTheoTheLoai_Load(object sender, EventArgs e)
        {
            // Ẩn nút in báo cáo nếu là User
            if (this.MdiParent is frmMainUser)
            {
                btnInBaoCao.Hide();
            }
            loadChart();
        }

        /// <summary>
        /// Load và hiển thị biểu đồ cột số lượng sách theo thể loại
        /// </summary>
        private void loadChart()
        {
            QLTVEntities db = new QLTVEntities();

            // Nhóm sách theo thể loại và tính tổng số lượng
            var sachTheoTheLoai = db.Saches.GroupBy(p => p.MaTheLoai)
                .Select(p => new
                {
                    TheLoai = p.FirstOrDefault().TheLoai.TenTheLoai,
                    SoSach = p.Sum(s => s.SoLuong)
                }).ToList();

            // Tạo series cho biểu đồ cột
            Series series = new Series { Name = "SoLuongSach", ChartType = SeriesChartType.Column};

            // Thêm dữ liệu vào biểu đồ
            foreach (var item in sachTheoTheLoai)
                series.Points.AddXY(item.TheLoai, item.SoSach);

            columnChart.Series.Add(series);

            // Điều chỉnh trục X
            columnChart.ChartAreas[0].AxisX.Title = "Thể loại sách";
            columnChart.ChartAreas[0].AxisX.Interval = 1;
            columnChart.ChartAreas[0].AxisX.LabelStyle.Angle = -40; // Xoay nhãn -40 độ

            // Điều chỉnh trục Y
            columnChart.ChartAreas[0].AxisY.Title = "Số lượng sách";
            columnChart.ChartAreas[0].RecalculateAxesScale();    

            // Ẩn lưới biểu đồ
            columnChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            columnChart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            // Màu cột
            series.Color = Color.FromArgb(100, 149, 237);
            series["PointWidth"] = "0.45"; // Độ rộng cột

            // Hiện giá trị từng cột
            series.IsValueShownAsLabel = true;
        }

        /// <summary>
        /// Chuyển sang biểu đồ tròn (Pie chart)
        /// </summary>
        private void btnChange_Click(object sender, EventArgs e)
        {
            frmCPie_SachTheoTheLoai frm = new frmCPie_SachTheoTheLoai();
            frm.MdiParent = this.MdiParent;
            this.Close();
            frm.Show();
        }

        /// <summary>
        /// Mở form in báo cáo số lượng sách theo thể loại (chỉ Admin)
        /// </summary>
        private void btnInBaoCao_Click(object sender, EventArgs e)
        {
            frmReportSLSachTheoTheLoai frm = new frmReportSLSachTheoTheLoai();
            frm.ShowDialog();
        }

    }
}
