namespace QuanLyThuVienApp
{
    partial class frmMainAdmin
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainAdmin));
            this.btnQLPhieuMuon = new FontAwesome.Sharp.IconButton();
            this.btnQLSach = new FontAwesome.Sharp.IconButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnQLBanDoc = new FontAwesome.Sharp.IconButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tslbTimer = new System.Windows.Forms.ToolStripLabel();
            this.tslbThongTin = new System.Windows.Forms.ToolStripLabel();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.btnInfor = new FontAwesome.Sharp.IconButton();
            this.btnTroGiup = new FontAwesome.Sharp.IconButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDangXuat = new FontAwesome.Sharp.IconButton();
            this.btnCaNhan = new FontAwesome.Sharp.IconButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnPhanQuyen = new FontAwesome.Sharp.IconButton();
            this.btnThongKe = new FontAwesome.Sharp.IconButton();
            this.btnSach = new FontAwesome.Sharp.IconButton();
            this.panel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnQLPhieuMuon
            // 
            this.btnQLPhieuMuon.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnQLPhieuMuon.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnQLPhieuMuon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQLPhieuMuon.IconChar = FontAwesome.Sharp.IconChar.Receipt;
            this.btnQLPhieuMuon.IconColor = System.Drawing.Color.Black;
            this.btnQLPhieuMuon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnQLPhieuMuon.IconSize = 45;
            this.btnQLPhieuMuon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQLPhieuMuon.Location = new System.Drawing.Point(3, 426);
            this.btnQLPhieuMuon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnQLPhieuMuon.Name = "btnQLPhieuMuon";
            this.btnQLPhieuMuon.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnQLPhieuMuon.Size = new System.Drawing.Size(169, 82);
            this.btnQLPhieuMuon.TabIndex = 3;
            this.btnQLPhieuMuon.Text = "            Quản lý\r\n            phiếu";
            this.btnQLPhieuMuon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQLPhieuMuon.UseVisualStyleBackColor = false;
            this.btnQLPhieuMuon.Click += new System.EventHandler(this.btnQLPhieuMuon_Click);
            // 
            // btnQLSach
            // 
            this.btnQLSach.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnQLSach.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnQLSach.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQLSach.IconChar = FontAwesome.Sharp.IconChar.Bootstrap;
            this.btnQLSach.IconColor = System.Drawing.Color.Black;
            this.btnQLSach.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnQLSach.IconSize = 45;
            this.btnQLSach.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQLSach.Location = new System.Drawing.Point(0, 0);
            this.btnQLSach.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnQLSach.Name = "btnQLSach";
            this.btnQLSach.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnQLSach.Size = new System.Drawing.Size(169, 82);
            this.btnQLSach.TabIndex = 1;
            this.btnQLSach.Text = "            Quản lý\r\n            sách";
            this.btnQLSach.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQLSach.UseVisualStyleBackColor = false;
            this.btnQLSach.Click += new System.EventHandler(this.btnQLSach_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.AliceBlue;
            this.panel2.Controls.Add(this.btnSach);
            this.panel2.Controls.Add(this.btnThongKe);
            this.panel2.Controls.Add(this.btnPhanQuyen);
            this.panel2.Controls.Add(this.btnQLPhieuMuon);
            this.panel2.Controls.Add(this.btnQLBanDoc);
            this.panel2.Controls.Add(this.btnQLSach);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(11, 79);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(174, 680);
            this.panel2.TabIndex = 8;
            // 
            // btnQLBanDoc
            // 
            this.btnQLBanDoc.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnQLBanDoc.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnQLBanDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQLBanDoc.IconChar = FontAwesome.Sharp.IconChar.UsersBetweenLines;
            this.btnQLBanDoc.IconColor = System.Drawing.Color.Black;
            this.btnQLBanDoc.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnQLBanDoc.IconSize = 45;
            this.btnQLBanDoc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQLBanDoc.Location = new System.Drawing.Point(0, 82);
            this.btnQLBanDoc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnQLBanDoc.Name = "btnQLBanDoc";
            this.btnQLBanDoc.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnQLBanDoc.Size = new System.Drawing.Size(169, 82);
            this.btnQLBanDoc.TabIndex = 2;
            this.btnQLBanDoc.Text = "            Quản lý\r\n            bạn đọc";
            this.btnQLBanDoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQLBanDoc.UseVisualStyleBackColor = false;
            this.btnQLBanDoc.Click += new System.EventHandler(this.btnQLBanDoc_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslbTimer,
            this.tslbThongTin});
            this.toolStrip1.Location = new System.Drawing.Point(11, 759);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1398, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tslbTimer
            // 
            this.tslbTimer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslbTimer.Name = "tslbTimer";
            this.tslbTimer.Size = new System.Drawing.Size(111, 22);
            this.tslbTimer.Text = "toolStripLabel1";
            // 
            // tslbThongTin
            // 
            this.tslbThongTin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslbThongTin.Name = "tslbThongTin";
            this.tslbThongTin.Size = new System.Drawing.Size(111, 22);
            this.tslbThongTin.Text = "toolStripLabel1";
            // 
            // metroPanel1
            // 
            this.metroPanel1.BackColor = System.Drawing.Color.White;
            this.metroPanel1.Controls.Add(this.btnInfor);
            this.metroPanel1.Controls.Add(this.btnTroGiup);
            this.metroPanel1.Controls.Add(this.label1);
            this.metroPanel1.Controls.Add(this.btnDangXuat);
            this.metroPanel1.Controls.Add(this.btnCaNhan);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(11, 37);
            this.metroPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(1398, 42);
            this.metroPanel1.TabIndex = 5;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 11;
            // 
            // btnInfor
            // 
            this.btnInfor.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnInfor.IconChar = FontAwesome.Sharp.IconChar.Info;
            this.btnInfor.IconColor = System.Drawing.Color.Black;
            this.btnInfor.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnInfor.IconSize = 20;
            this.btnInfor.Location = new System.Drawing.Point(45, 0);
            this.btnInfor.Margin = new System.Windows.Forms.Padding(51, 50, 51, 50);
            this.btnInfor.Name = "btnInfor";
            this.btnInfor.Padding = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.btnInfor.Size = new System.Drawing.Size(37, 34);
            this.btnInfor.TabIndex = 6;
            this.btnInfor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnInfor.UseVisualStyleBackColor = true;
            this.btnInfor.Click += new System.EventHandler(this.btnInfor_Click);
            // 
            // btnTroGiup
            // 
            this.btnTroGiup.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnTroGiup.IconChar = FontAwesome.Sharp.IconChar.CircleQuestion;
            this.btnTroGiup.IconColor = System.Drawing.Color.Black;
            this.btnTroGiup.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnTroGiup.IconSize = 25;
            this.btnTroGiup.Location = new System.Drawing.Point(0, 0);
            this.btnTroGiup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTroGiup.Name = "btnTroGiup";
            this.btnTroGiup.Size = new System.Drawing.Size(37, 34);
            this.btnTroGiup.TabIndex = 5;
            this.btnTroGiup.UseVisualStyleBackColor = true;
            this.btnTroGiup.Click += new System.EventHandler(this.btnTroGiup_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SkyBlue;
            this.label1.Location = new System.Drawing.Point(568, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "QUẢN LÝ THƯ VIỆN";
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDangXuat.BackColor = System.Drawing.SystemColors.Info;
            this.btnDangXuat.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnDangXuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangXuat.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnDangXuat.IconChar = FontAwesome.Sharp.IconChar.RightFromBracket;
            this.btnDangXuat.IconColor = System.Drawing.Color.IndianRed;
            this.btnDangXuat.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDangXuat.IconSize = 25;
            this.btnDangXuat.Location = new System.Drawing.Point(1360, 0);
            this.btnDangXuat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new System.Drawing.Size(37, 34);
            this.btnDangXuat.TabIndex = 4;
            this.btnDangXuat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDangXuat.UseVisualStyleBackColor = false;
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);
            // 
            // btnCaNhan
            // 
            this.btnCaNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCaNhan.BackColor = System.Drawing.SystemColors.Info;
            this.btnCaNhan.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCaNhan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCaNhan.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnCaNhan.IconChar = FontAwesome.Sharp.IconChar.User;
            this.btnCaNhan.IconColor = System.Drawing.Color.IndianRed;
            this.btnCaNhan.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCaNhan.IconSize = 24;
            this.btnCaNhan.Location = new System.Drawing.Point(1311, 0);
            this.btnCaNhan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCaNhan.Name = "btnCaNhan";
            this.btnCaNhan.Size = new System.Drawing.Size(37, 34);
            this.btnCaNhan.TabIndex = 3;
            this.btnCaNhan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCaNhan.UseVisualStyleBackColor = false;
            this.btnCaNhan.Click += new System.EventHandler(this.btnCaNhan_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnPhanQuyen
            // 
            this.btnPhanQuyen.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnPhanQuyen.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnPhanQuyen.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPhanQuyen.IconChar = FontAwesome.Sharp.IconChar.PersonArrowUpFromLine;
            this.btnPhanQuyen.IconColor = System.Drawing.Color.Black;
            this.btnPhanQuyen.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPhanQuyen.IconSize = 40;
            this.btnPhanQuyen.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPhanQuyen.Location = new System.Drawing.Point(0, 168);
            this.btnPhanQuyen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPhanQuyen.Name = "btnPhanQuyen";
            this.btnPhanQuyen.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.btnPhanQuyen.Size = new System.Drawing.Size(169, 82);
            this.btnPhanQuyen.TabIndex = 2;
            this.btnPhanQuyen.Text = "Phân quyền";
            this.btnPhanQuyen.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPhanQuyen.UseVisualStyleBackColor = false;
            this.btnPhanQuyen.Click += new System.EventHandler(this.btnPhanQuyen_Click);
            // 
            // btnThongKe
            // 
            this.btnThongKe.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnThongKe.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnThongKe.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongKe.IconChar = FontAwesome.Sharp.IconChar.ChartSimple;
            this.btnThongKe.IconColor = System.Drawing.Color.Black;
            this.btnThongKe.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnThongKe.IconSize = 40;
            this.btnThongKe.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnThongKe.Location = new System.Drawing.Point(3, 340);
            this.btnThongKe.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.btnThongKe.Size = new System.Drawing.Size(169, 82);
            this.btnThongKe.TabIndex = 1;
            this.btnThongKe.Text = "Thống kê";
            this.btnThongKe.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnThongKe.UseVisualStyleBackColor = false;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
            // 
            // btnSach
            // 
            this.btnSach.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnSach.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSach.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSach.IconChar = FontAwesome.Sharp.IconChar.Book;
            this.btnSach.IconColor = System.Drawing.Color.Black;
            this.btnSach.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSach.IconSize = 40;
            this.btnSach.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSach.Location = new System.Drawing.Point(3, 254);
            this.btnSach.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSach.Name = "btnSach";
            this.btnSach.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.btnSach.Size = new System.Drawing.Size(169, 82);
            this.btnSach.TabIndex = 0;
            this.btnSach.Text = "Thông tin sách";
            this.btnSach.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSach.UseVisualStyleBackColor = false;
            this.btnSach.Click += new System.EventHandler(this.btnSach_Click);
            // 
            // frmMainAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1420, 794);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.metroPanel1);
            this.DisplayHeader = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "frmMainAdmin";
            this.Padding = new System.Windows.Forms.Padding(11, 37, 11, 10);
            this.Resizable = false;
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.TransparencyKey = System.Drawing.Color.Empty;
            this.Load += new System.EventHandler(this.frmMainAdmin_Load);
            this.panel2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FontAwesome.Sharp.IconButton btnQLPhieuMuon;
        private FontAwesome.Sharp.IconButton btnQLSach;
        private System.Windows.Forms.Panel panel2;
        private FontAwesome.Sharp.IconButton btnQLBanDoc;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel tslbTimer;
        private System.Windows.Forms.ToolStripLabel tslbThongTin;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private FontAwesome.Sharp.IconButton btnInfor;
        private FontAwesome.Sharp.IconButton btnTroGiup;
        private FontAwesome.Sharp.IconButton btnDangXuat;
        private FontAwesome.Sharp.IconButton btnCaNhan;
        private FontAwesome.Sharp.IconButton btnPhanQuyen;
        private FontAwesome.Sharp.IconButton btnThongKe;
        private FontAwesome.Sharp.IconButton btnSach;
    }
}