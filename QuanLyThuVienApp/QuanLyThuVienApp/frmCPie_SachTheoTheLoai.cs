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
    /// Form hiển thị biểu đồ tròn tỷ lệ sách theo thể loại
    /// Cho phép Admin in báo cáo, User chỉ xem
    /// </summary>
    public partial class frmCPie_SachTheoTheLoai : Form
    {
        /// <summary>
        /// Khởi tạo form biểu đồ tròn
        /// </summary>
        public frmCPie_SachTheoTheLoai()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện Load form - Ẩn nút in báo cáo nếu là User, sau đó load biểu đồ
        /// </summary>
        private void frmCPie_SachTheoTheLoai_Load(object sender, EventArgs e)
        {
            // Ẩn nút in báo cáo nếu là User
            if (this.MdiParent is frmMainUser)
            {
                btnInBaoCao.Hide();
            }
            loadChart();
        }

        /// <summary>
        /// Load và hiển thị biểu đồ tròn tỷ lệ sách theo thể loại
        /// </summary>
        private void loadChart()
        {
            QLTVEntities db = new QLTVEntities();

            // Nhóm sách theo thể loại và tính tổng số lượng
            var data = db.Saches.GroupBy(p => p.MaTheLoai)
                .Select(p => new
                {
                    TheLoai = p.FirstOrDefault().TheLoai.TenTheLoai,
                    SoLuong = p.Sum(s => s.SoLuong)
                }).ToList();

            // Tạo series cho biểu đồ tròn
            Series series = new Series { Name = "TiLeTheLoai", ChartType = SeriesChartType.Pie };
            
            // Thêm dữ liệu vào biểu đồ
            foreach(var item in data)
                series.Points.AddXY(item.TheLoai, item.SoLuong);

            chartTiLeTheLoai.Series.Clear();
            chartTiLeTheLoai.Series.Add(series);

            // Hiển thị phần trăm trên biểu đồ
            series.IsValueShownAsLabel = true;
            series.Label = "#PERCENT"; // Hiển thị phần trăm

            series.LegendText = "#AXISLABEL"; // Hiển thị tên thể loại trong legend
        }

        /// <summary>
        /// Chuyển sang biểu đồ cột (Column chart)
        /// </summary>
        private void btnChange_Click(object sender, EventArgs e)
        {
            frmCColumn_SachTheoTheLoai frm = new frmCColumn_SachTheoTheLoai();
            frm.MdiParent = this.MdiParent;
            this.Close();
            frm.Show();
        }

        /// <summary>
        /// Mở form in báo cáo tỷ lệ sách theo thể loại (chỉ Admin)
        /// </summary>
        private void btnInBaoCao_Click(object sender, EventArgs e)
        {
            frmReportTiLeSachTheoTheLoai frm = new frmReportTiLeSachTheoTheLoai();
            frm.ShowDialog();
        }
    }
}
