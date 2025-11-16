namespace QuanLyThuVienApp
{
    partial class frmCColumn_SachTheoTheLoai
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.columnChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnChange = new FontAwesome.Sharp.IconButton();
            this.btnInBaoCao = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.columnChart)).BeginInit();
            this.SuspendLayout();
            // 
            // columnChart
            // 
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.Name = "ChartArea1";
            this.columnChart.ChartAreas.Add(chartArea1);
            this.columnChart.Location = new System.Drawing.Point(11, 97);
            this.columnChart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.columnChart.Name = "columnChart";
            series1.ChartArea = "ChartArea1";
            series1.CustomProperties = "PointWidth=0.5";
            series1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series1.IsValueShownAsLabel = true;
            series1.IsXValueIndexed = true;
            series1.Name = "Series1";
            series1.ShadowColor = System.Drawing.Color.Black;
            this.columnChart.Series.Add(series1);
            this.columnChart.Size = new System.Drawing.Size(1121, 481);
            this.columnChart.TabIndex = 0;
            this.columnChart.Text = "chart1";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "Title1";
            title1.Text = "Số lượng sách theo từng thể loại";
            this.columnChart.Titles.Add(title1);
            // 
            // btnChange
            // 
            this.btnChange.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnChange.IconChar = FontAwesome.Sharp.IconChar.Repeat;
            this.btnChange.IconColor = System.Drawing.Color.Gray;
            this.btnChange.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnChange.IconSize = 25;
            this.btnChange.Location = new System.Drawing.Point(1159, 258);
            this.btnChange.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnChange.Name = "btnChange";
            this.btnChange.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.btnChange.Size = new System.Drawing.Size(47, 43);
            this.btnChange.TabIndex = 1;
            this.btnChange.UseVisualStyleBackColor = false;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnInBaoCao
            // 
            this.btnInBaoCao.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnInBaoCao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInBaoCao.Location = new System.Drawing.Point(15, 14);
            this.btnInBaoCao.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnInBaoCao.Name = "btnInBaoCao";
            this.btnInBaoCao.Size = new System.Drawing.Size(151, 31);
            this.btnInBaoCao.TabIndex = 2;
            this.btnInBaoCao.Text = "In báo cáo";
            this.btnInBaoCao.UseVisualStyleBackColor = false;
            this.btnInBaoCao.Click += new System.EventHandler(this.btnInBaoCao_Click);
            // 
            // frmCColumn_SachTheoTheLoai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1220, 585);
            this.ControlBox = false;
            this.Controls.Add(this.btnInBaoCao);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.columnChart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCColumn_SachTheoTheLoai";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmThongKe";
            this.Load += new System.EventHandler(this.frmCColumn_SachTheoTheLoai_Load);
            ((System.ComponentModel.ISupportInitialize)(this.columnChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart columnChart;
        private FontAwesome.Sharp.IconButton btnChange;
        private System.Windows.Forms.Button btnInBaoCao;
    }
}