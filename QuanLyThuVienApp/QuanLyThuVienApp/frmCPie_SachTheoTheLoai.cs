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
    public partial class frmCPie_SachTheoTheLoai : Form
    {
        public frmCPie_SachTheoTheLoai()
        {
            InitializeComponent();
        }

        private void frmCPie_SachTheoTheLoai_Load(object sender, EventArgs e)
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

            var data = db.Saches.GroupBy(p => p.MaTheLoai)
                .Select(p => new
                {
                    TheLoai = p.FirstOrDefault().TheLoai.TenTheLoai,
                    SoLuong = p.Sum(s => s.SoLuong)
                }).ToList();

            // Thêm dữ liệu
            Series series = new Series { Name = "TiLeTheLoai", ChartType = SeriesChartType.Pie };
            
            foreach(var item in data)
                series.Points.AddXY(item.TheLoai, item.SoLuong);

            chartTiLeTheLoai.Series.Clear();
            chartTiLeTheLoai.Series.Add(series);

            // Hiển thị phần trăm
            series.IsValueShownAsLabel = true;
            series.Label = "#PERCENT";

            series.LegendText = "#AXISLABEL";
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            frmCColumn_SachTheoTheLoai frm = new frmCColumn_SachTheoTheLoai();
            frm.MdiParent = this.MdiParent;
            this.Close();
            frm.Show();
        }

        private void btnInBaoCao_Click(object sender, EventArgs e)
        {
            frmReportTiLeSachTheoTheLoai frm = new frmReportTiLeSachTheoTheLoai();
            frm.ShowDialog();
        }
    }
}
