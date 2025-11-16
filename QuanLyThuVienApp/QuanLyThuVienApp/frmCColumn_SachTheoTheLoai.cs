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
    public partial class frmCColumn_SachTheoTheLoai : Form
    {
        public frmCColumn_SachTheoTheLoai()
        {
            InitializeComponent();

        }

        private void frmCColumn_SachTheoTheLoai_Load(object sender, EventArgs e)
        {
            if (this.MdiParent is frmMainUser)
            {
                btnInBaoCao.Hide();
            }
            loadChart();
        }

        private void loadChart()
        {
            QLTVEntities db = new QLTVEntities();

            var sachTheoTheLoai = db.Saches.GroupBy(p => p.MaTheLoai)
                .Select(p => new
                {
                    TheLoai = p.FirstOrDefault().TheLoai.TenTheLoai,
                    SoSach = p.Sum(s => s.SoLuong)
                }).ToList();

            Series series = new Series { Name = "SoLuongSach", ChartType = SeriesChartType.Column};

            foreach (var item in sachTheoTheLoai)
                series.Points.AddXY(item.TheLoai, item.SoSach);

            columnChart.Series.Add(series);

            // Điều chỉnh trục X
            columnChart.ChartAreas[0].AxisX.Title = "Thể loại sách";
            columnChart.ChartAreas[0].AxisX.Interval = 1;
            columnChart.ChartAreas[0].AxisX.LabelStyle.Angle = -40;

            // Điều chỉnh trục Y
            columnChart.ChartAreas[0].AxisY.Title = "Số lượng sách";
            //columnChart.ChartAreas[0].AxisY.LabelStyle.Format = "0.0";
            columnChart.ChartAreas[0].RecalculateAxesScale();    

            // Ẩn lưới biểu đồ
            columnChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            columnChart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            // Màu cột
            series.Color = Color.FromArgb(100, 149, 237);
            series["PointWidth"] = "0.45";

            // Hiện giá trị từng cột
            series.IsValueShownAsLabel = true;
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            frmCPie_SachTheoTheLoai frm = new frmCPie_SachTheoTheLoai();
            frm.MdiParent = this.MdiParent;
            this.Close();
            frm.Show();
        }

        private void btnInBaoCao_Click(object sender, EventArgs e)
        {
            frmReportSLSachTheoTheLoai frm = new frmReportSLSachTheoTheLoai();
            frm.ShowDialog();
        }

    }
}
