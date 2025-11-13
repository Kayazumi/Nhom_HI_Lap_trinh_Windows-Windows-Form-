namespace QuanLyThuVienApp
{
    partial class frmQuanLyPhieuMuon
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnTimKiem = new FontAwesome.Sharp.IconButton();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvPhieuMuon = new System.Windows.Forms.DataGridView();
            this.btnLamMoi = new FontAwesome.Sharp.IconButton();
            this.cbTimKiem = new System.Windows.Forms.ComboBox();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioPhieuDangKy = new System.Windows.Forms.RadioButton();
            this.groupPhieuTra = new System.Windows.Forms.GroupBox();
            this.radioPhieuTra = new System.Windows.Forms.RadioButton();
            this.radioPhieuMuon = new System.Windows.Forms.RadioButton();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.MaPhieu2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaSach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenSach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDBanDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HanTra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.btnChoMuon = new System.Windows.Forms.Button();
            this.btnHuyPhieu = new System.Windows.Forms.Button();
            this.btnMuonMoi = new System.Windows.Forms.Button();
            this.btnGiaHan = new System.Windows.Forms.Button();
            this.btnTraSach = new System.Windows.Forms.Button();
            this.lbTienPhat1 = new System.Windows.Forms.Label();
            this.lbTienPhat2 = new System.Windows.Forms.Label();
            this.btnHoaDonPhat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuMuon)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupPhieuTra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnTimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnTimKiem.IconColor = System.Drawing.Color.Black;
            this.btnTimKiem.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnTimKiem.IconSize = 19;
            this.btnTimKiem.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnTimKiem.Location = new System.Drawing.Point(787, 221);
            this.btnTimKiem.Margin = new System.Windows.Forms.Padding(2);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(86, 25);
            this.btnTimKiem.TabIndex = 33;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(676, 145);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 17);
            this.label1.TabIndex = 30;
            this.label1.Text = "Tìm kiếm theo";
            // 
            // dgvPhieuMuon
            // 
            this.dgvPhieuMuon.AllowUserToAddRows = false;
            this.dgvPhieuMuon.AllowUserToDeleteRows = false;
            this.dgvPhieuMuon.AllowUserToResizeRows = false;
            this.dgvPhieuMuon.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPhieuMuon.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvPhieuMuon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPhieuMuon.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvPhieuMuon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPhieuMuon.Location = new System.Drawing.Point(0, 0);
            this.dgvPhieuMuon.Margin = new System.Windows.Forms.Padding(2);
            this.dgvPhieuMuon.MultiSelect = false;
            this.dgvPhieuMuon.Name = "dgvPhieuMuon";
            this.dgvPhieuMuon.ReadOnly = true;
            this.dgvPhieuMuon.RowHeadersVisible = false;
            this.dgvPhieuMuon.RowHeadersWidth = 51;
            this.dgvPhieuMuon.RowTemplate.Height = 24;
            this.dgvPhieuMuon.Size = new System.Drawing.Size(915, 196);
            this.dgvPhieuMuon.TabIndex = 1;
            this.dgvPhieuMuon.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPhieuMuon_CellClick);
            this.dgvPhieuMuon.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvPhieuMuon_CellFormatting);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnLamMoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.IconChar = FontAwesome.Sharp.IconChar.ArrowRotateBackward;
            this.btnLamMoi.IconColor = System.Drawing.Color.Black;
            this.btnLamMoi.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnLamMoi.IconSize = 19;
            this.btnLamMoi.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnLamMoi.Location = new System.Drawing.Point(689, 221);
            this.btnLamMoi.Margin = new System.Windows.Forms.Padding(2);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(86, 25);
            this.btnLamMoi.TabIndex = 32;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // cbTimKiem
            // 
            this.cbTimKiem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTimKiem.FormattingEnabled = true;
            this.cbTimKiem.Items.AddRange(new object[] {
            "Mã phiếu",
            "Mã bạn đọc",
            "Tên bạn đọc"});
            this.cbTimKiem.Location = new System.Drawing.Point(777, 142);
            this.cbTimKiem.Margin = new System.Windows.Forms.Padding(2);
            this.cbTimKiem.Name = "cbTimKiem";
            this.cbTimKiem.Size = new System.Drawing.Size(108, 25);
            this.cbTimKiem.TabIndex = 31;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimKiem.Location = new System.Drawing.Point(679, 184);
            this.txtTimKiem.Margin = new System.Windows.Forms.Padding(2);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(206, 23);
            this.txtTimKiem.TabIndex = 29;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvPhieuMuon);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 279);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(915, 196);
            this.panel1.TabIndex = 28;
            // 
            // radioPhieuDangKy
            // 
            this.radioPhieuDangKy.AutoSize = true;
            this.radioPhieuDangKy.Location = new System.Drawing.Point(22, 24);
            this.radioPhieuDangKy.Margin = new System.Windows.Forms.Padding(2);
            this.radioPhieuDangKy.Name = "radioPhieuDangKy";
            this.radioPhieuDangKy.Size = new System.Drawing.Size(78, 21);
            this.radioPhieuDangKy.TabIndex = 34;
            this.radioPhieuDangKy.TabStop = true;
            this.radioPhieuDangKy.Text = "Đăng ký";
            this.radioPhieuDangKy.UseVisualStyleBackColor = true;
            this.radioPhieuDangKy.CheckedChanged += new System.EventHandler(this.radioPhieuDangKy_CheckedChanged);
            // 
            // groupPhieuTra
            // 
            this.groupPhieuTra.Controls.Add(this.radioPhieuTra);
            this.groupPhieuTra.Controls.Add(this.radioPhieuMuon);
            this.groupPhieuTra.Controls.Add(this.radioPhieuDangKy);
            this.groupPhieuTra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPhieuTra.Location = new System.Drawing.Point(582, 32);
            this.groupPhieuTra.Margin = new System.Windows.Forms.Padding(2);
            this.groupPhieuTra.Name = "groupPhieuTra";
            this.groupPhieuTra.Padding = new System.Windows.Forms.Padding(2);
            this.groupPhieuTra.Size = new System.Drawing.Size(303, 60);
            this.groupPhieuTra.TabIndex = 35;
            this.groupPhieuTra.TabStop = false;
            this.groupPhieuTra.Text = "Danh sách phiếu";
            // 
            // radioPhieuTra
            // 
            this.radioPhieuTra.AutoSize = true;
            this.radioPhieuTra.Location = new System.Drawing.Point(219, 24);
            this.radioPhieuTra.Margin = new System.Windows.Forms.Padding(2);
            this.radioPhieuTra.Name = "radioPhieuTra";
            this.radioPhieuTra.Size = new System.Drawing.Size(65, 21);
            this.radioPhieuTra.TabIndex = 34;
            this.radioPhieuTra.TabStop = true;
            this.radioPhieuTra.Text = "Đã trả";
            this.radioPhieuTra.UseVisualStyleBackColor = true;
            this.radioPhieuTra.CheckedChanged += new System.EventHandler(this.radioPhieuTra_CheckedChanged);
            // 
            // radioPhieuMuon
            // 
            this.radioPhieuMuon.AutoSize = true;
            this.radioPhieuMuon.Location = new System.Drawing.Point(111, 24);
            this.radioPhieuMuon.Margin = new System.Windows.Forms.Padding(2);
            this.radioPhieuMuon.Name = "radioPhieuMuon";
            this.radioPhieuMuon.Size = new System.Drawing.Size(99, 21);
            this.radioPhieuMuon.TabIndex = 34;
            this.radioPhieuMuon.TabStop = true;
            this.radioPhieuMuon.Text = "Đang mượn";
            this.radioPhieuMuon.UseVisualStyleBackColor = true;
            this.radioPhieuMuon.CheckedChanged += new System.EventHandler(this.radioPhieuMuon_CheckedChanged);
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AllowUserToDeleteRows = false;
            this.dgvChiTiet.AllowUserToResizeRows = false;
            this.dgvChiTiet.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvChiTiet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaPhieu2,
            this.MaSach,
            this.TenSach,
            this.SoLuong,
            this.IDBanDoc,
            this.HanTra});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvChiTiet.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvChiTiet.Location = new System.Drawing.Point(33, 32);
            this.dgvChiTiet.Margin = new System.Windows.Forms.Padding(2);
            this.dgvChiTiet.MultiSelect = false;
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.ReadOnly = true;
            this.dgvChiTiet.RowHeadersVisible = false;
            this.dgvChiTiet.RowHeadersWidth = 51;
            this.dgvChiTiet.RowTemplate.Height = 24;
            this.dgvChiTiet.Size = new System.Drawing.Size(484, 177);
            this.dgvChiTiet.TabIndex = 37;
            // 
            // MaPhieu2
            // 
            this.MaPhieu2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MaPhieu2.DataPropertyName = "MaPhieu";
            this.MaPhieu2.HeaderText = "Mã phiếu";
            this.MaPhieu2.MinimumWidth = 6;
            this.MaPhieu2.Name = "MaPhieu2";
            this.MaPhieu2.ReadOnly = true;
            this.MaPhieu2.Width = 87;
            // 
            // MaSach
            // 
            this.MaSach.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MaSach.DataPropertyName = "MaSach";
            this.MaSach.HeaderText = "Mã sách";
            this.MaSach.MinimumWidth = 6;
            this.MaSach.Name = "MaSach";
            this.MaSach.ReadOnly = true;
            this.MaSach.Width = 83;
            // 
            // TenSach
            // 
            this.TenSach.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TenSach.DataPropertyName = "TenSach";
            this.TenSach.HeaderText = "Tên sách";
            this.TenSach.MinimumWidth = 6;
            this.TenSach.Name = "TenSach";
            this.TenSach.ReadOnly = true;
            // 
            // SoLuong
            // 
            this.SoLuong.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SoLuong.DataPropertyName = "SoLuong";
            this.SoLuong.HeaderText = "Số lượng";
            this.SoLuong.MinimumWidth = 6;
            this.SoLuong.Name = "SoLuong";
            this.SoLuong.ReadOnly = true;
            this.SoLuong.Width = 85;
            // 
            // IDBanDoc
            // 
            this.IDBanDoc.DataPropertyName = "IDBanDoc";
            this.IDBanDoc.HeaderText = "ID bạn đọc";
            this.IDBanDoc.MinimumWidth = 6;
            this.IDBanDoc.Name = "IDBanDoc";
            this.IDBanDoc.ReadOnly = true;
            this.IDBanDoc.Visible = false;
            this.IDBanDoc.Width = 125;
            // 
            // HanTra
            // 
            this.HanTra.DataPropertyName = "HanTra";
            this.HanTra.HeaderText = "Hạn trả";
            this.HanTra.Name = "HanTra";
            this.HanTra.ReadOnly = true;
            this.HanTra.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(227, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 17);
            this.label2.TabIndex = 38;
            this.label2.Text = "Chi tiết phiếu";
            // 
            // btnChoMuon
            // 
            this.btnChoMuon.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnChoMuon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChoMuon.Location = new System.Drawing.Point(337, 217);
            this.btnChoMuon.Margin = new System.Windows.Forms.Padding(2);
            this.btnChoMuon.Name = "btnChoMuon";
            this.btnChoMuon.Size = new System.Drawing.Size(106, 25);
            this.btnChoMuon.TabIndex = 39;
            this.btnChoMuon.Text = "Cho mượn";
            this.btnChoMuon.UseVisualStyleBackColor = false;
            this.btnChoMuon.Click += new System.EventHandler(this.btnChoMuon_Click);
            // 
            // btnHuyPhieu
            // 
            this.btnHuyPhieu.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnHuyPhieu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuyPhieu.Location = new System.Drawing.Point(224, 217);
            this.btnHuyPhieu.Margin = new System.Windows.Forms.Padding(2);
            this.btnHuyPhieu.Name = "btnHuyPhieu";
            this.btnHuyPhieu.Size = new System.Drawing.Size(106, 25);
            this.btnHuyPhieu.TabIndex = 39;
            this.btnHuyPhieu.Text = "Hủy phiếu";
            this.btnHuyPhieu.UseVisualStyleBackColor = false;
            this.btnHuyPhieu.Click += new System.EventHandler(this.btnHuyPhieu_Click);
            // 
            // btnMuonMoi
            // 
            this.btnMuonMoi.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnMuonMoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMuonMoi.Location = new System.Drawing.Point(111, 217);
            this.btnMuonMoi.Margin = new System.Windows.Forms.Padding(2);
            this.btnMuonMoi.Name = "btnMuonMoi";
            this.btnMuonMoi.Size = new System.Drawing.Size(106, 25);
            this.btnMuonMoi.TabIndex = 40;
            this.btnMuonMoi.Text = "Mượn mới";
            this.btnMuonMoi.UseVisualStyleBackColor = false;
            this.btnMuonMoi.Click += new System.EventHandler(this.btnMuonMoi_Click);
            // 
            // btnGiaHan
            // 
            this.btnGiaHan.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnGiaHan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGiaHan.Location = new System.Drawing.Point(224, 217);
            this.btnGiaHan.Margin = new System.Windows.Forms.Padding(2);
            this.btnGiaHan.Name = "btnGiaHan";
            this.btnGiaHan.Size = new System.Drawing.Size(106, 25);
            this.btnGiaHan.TabIndex = 41;
            this.btnGiaHan.Text = "Gia hạn";
            this.btnGiaHan.UseVisualStyleBackColor = false;
            this.btnGiaHan.Click += new System.EventHandler(this.btnGiaHan_Click);
            // 
            // btnTraSach
            // 
            this.btnTraSach.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnTraSach.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTraSach.Location = new System.Drawing.Point(337, 217);
            this.btnTraSach.Margin = new System.Windows.Forms.Padding(2);
            this.btnTraSach.Name = "btnTraSach";
            this.btnTraSach.Size = new System.Drawing.Size(106, 25);
            this.btnTraSach.TabIndex = 41;
            this.btnTraSach.Text = "Trả sách";
            this.btnTraSach.UseVisualStyleBackColor = false;
            this.btnTraSach.Click += new System.EventHandler(this.btnTraSach_Click);
            // 
            // lbTienPhat1
            // 
            this.lbTienPhat1.AutoSize = true;
            this.lbTienPhat1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTienPhat1.ForeColor = System.Drawing.Color.Red;
            this.lbTienPhat1.Location = new System.Drawing.Point(214, 247);
            this.lbTienPhat1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTienPhat1.Name = "lbTienPhat1";
            this.lbTienPhat1.Size = new System.Drawing.Size(76, 17);
            this.lbTienPhat1.TabIndex = 42;
            this.lbTienPhat1.Text = "Tiền phạt: ";
            // 
            // lbTienPhat2
            // 
            this.lbTienPhat2.AutoSize = true;
            this.lbTienPhat2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTienPhat2.ForeColor = System.Drawing.Color.Red;
            this.lbTienPhat2.Location = new System.Drawing.Point(284, 247);
            this.lbTienPhat2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTienPhat2.Name = "lbTienPhat2";
            this.lbTienPhat2.Size = new System.Drawing.Size(49, 17);
            this.lbTienPhat2.TabIndex = 42;
            this.lbTienPhat2.Text = "0 VNĐ";
            // 
            // btnHoaDonPhat
            // 
            this.btnHoaDonPhat.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnHoaDonPhat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHoaDonPhat.Location = new System.Drawing.Point(111, 217);
            this.btnHoaDonPhat.Margin = new System.Windows.Forms.Padding(2);
            this.btnHoaDonPhat.Name = "btnHoaDonPhat";
            this.btnHoaDonPhat.Size = new System.Drawing.Size(106, 25);
            this.btnHoaDonPhat.TabIndex = 41;
            this.btnHoaDonPhat.Text = "Hóa đơn phạt";
            this.btnHoaDonPhat.UseVisualStyleBackColor = false;
            this.btnHoaDonPhat.Click += new System.EventHandler(this.btnHoaDonPhat_Click);
            // 
            // frmQuanLyPhieuMuon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(915, 475);
            this.Controls.Add(this.lbTienPhat2);
            this.Controls.Add(this.lbTienPhat1);
            this.Controls.Add(this.btnTraSach);
            this.Controls.Add(this.btnHoaDonPhat);
            this.Controls.Add(this.btnGiaHan);
            this.Controls.Add(this.btnMuonMoi);
            this.Controls.Add(this.btnHuyPhieu);
            this.Controls.Add(this.btnChoMuon);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvChiTiet);
            this.Controls.Add(this.groupPhieuTra);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.cbTimKiem);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmQuanLyPhieuMuon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmQuanLyPhieuMuon";
            this.Load += new System.EventHandler(this.frmQuanLyPhieuMuon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuMuon)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupPhieuTra.ResumeLayout(false);
            this.groupPhieuTra.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FontAwesome.Sharp.IconButton btnTimKiem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvPhieuMuon;
        private FontAwesome.Sharp.IconButton btnLamMoi;
        private System.Windows.Forms.ComboBox cbTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioPhieuDangKy;
        private System.Windows.Forms.GroupBox groupPhieuTra;
        private System.Windows.Forms.RadioButton radioPhieuTra;
        private System.Windows.Forms.RadioButton radioPhieuMuon;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnChoMuon;
        private System.Windows.Forms.Button btnHuyPhieu;
        private System.Windows.Forms.Button btnMuonMoi;
        private System.Windows.Forms.Button btnGiaHan;
        private System.Windows.Forms.Button btnTraSach;
        private System.Windows.Forms.Label lbTienPhat1;
        private System.Windows.Forms.Label lbTienPhat2;
        private System.Windows.Forms.Button btnHoaDonPhat;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaPhieu2;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaSach;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenSach;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDBanDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn HanTra;
    }
}