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
    /// Form xác thực OTP (One-Time Password)
    /// Cho phép người dùng nhập mã OTP được gửi đến email để xác thực tài khoản
    /// Có 3 constructor để hỗ trợ các trường hợp sử dụng khác nhau
    /// </summary>
    public partial class frmXacThuc : MetroFramework.Forms.MetroForm
    {
        // ID của người dùng cần xác thực
        private int ID;
        
        // Cờ kiểm tra xem form này có được gọi từ form khác với callback không
        // true: có callback, false: không có callback
        private bool kiemTra = false;
        
        // Callback function được gọi khi xác thực thành công hoặc thất bại
        // Action<bool>: delegate nhận 1 tham số bool (true = thành công, false = thất bại)
        private readonly Action<bool> callback;
        
        /// <summary>
        /// Constructor mặc định (không tham số)
        /// Thường không được sử dụng, chỉ để tương thích
        /// </summary>
        public frmXacThuc()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor với ID người dùng
        /// Được sử dụng khi đăng ký tài khoản mới hoặc đăng nhập lần đầu
        /// </summary>
        /// <param name="_ID">ID của người dùng cần xác thực</param>
        public frmXacThuc(int _ID)
        {
            ID = _ID; // Lưu ID vào biến private
            InitializeComponent(); // Khởi tạo controls
        }

        /// <summary>
        /// Constructor với ID và callback function
        /// Được sử dụng khi form này được gọi từ form khác và cần thông báo kết quả
        /// Ví dụ: khi đổi email, cần xác thực email mới
        /// </summary>
        /// <param name="_ID">ID của người dùng cần xác thực</param>
        /// <param name="callback">Hàm callback được gọi khi xác thực thành công hoặc thất bại</param>
        public frmXacThuc(int _ID, Action<bool> callback)
        {
            ID = _ID; // Lưu ID
            kiemTra = true; // Đánh dấu có callback
            InitializeComponent(); // Khởi tạo controls
            this.callback = callback; // Lưu callback function
        }

        /// <summary>
        /// Sự kiện Load form - được gọi khi form được hiển thị
        /// </summary>
        /// <param name="sender">Form này</param>
        /// <param name="e">Thông tin sự kiện Load</param>
        private void frmXacNhanOTP_Load(object sender, EventArgs e)
        {
            // Không có code khởi tạo
        }

        /// <summary>
        /// Xử lý sự kiện khi người dùng nhấn nút Thoát
        /// Nếu có callback, gọi callback với giá trị false (thất bại)
        /// </summary>
        /// <param name="sender">Nút Thoát</param>
        /// <param name="e">Thông tin sự kiện Click</param>
        private void btnThoat_Click(object sender, EventArgs e)
        {
            // Nếu có callback (form được gọi từ form khác)
            if (kiemTra) 
                callback(false); // Gọi callback với false = xác thực thất bại
            
            // Đóng form
            this.Close();
        }

        /// <summary>
        /// Xử lý sự kiện khi người dùng nhấn nút "Gửi lại" mã OTP
        /// Tạo mã OTP mới và gửi đến email của người dùng
        /// </summary>
        /// <param name="sender">Nút Gửi lại</param>
        /// <param name="e">Thông tin sự kiện Click</param>
        private void btnGuiLai_Click(object sender, EventArgs e)
        {
            // Tạo kết nối đến database
            QLTVEntities db = new QLTVEntities();
            
            // Tìm người dùng theo ID
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == ID).SingleOrDefault();

            // Tạo mã OTP mới ngẫu nhiên 6 chữ số
            Random random = new Random();
            // Next(100000, 999999): tạo số từ 100000 đến 999999
            string OTP = random.Next(100000, 999999).ToString();

            // Cập nhật mã OTP mới vào database
            nguoiDung.MaOTP = OTP;
            
            // Gửi mã OTP mới đến email của người dùng
            GuiEmail.guiEmail(nguoiDung.Email, "Mã xác thực của bạn là " + OTP);
            
            // Cập nhật thời gian nhận OTP (để tính thời gian hết hạn)
            nguoiDung.ThoiGianNhanOTP = DateTime.Now;
            
            // Lưu thay đổi vào database
            db.SaveChanges();
        }

        /// <summary>
        /// Xử lý sự kiện khi người dùng nhấn nút "Xác thực"
        /// Kiểm tra mã OTP người dùng nhập có đúng và còn hạn không
        /// Nếu đúng, cập nhật trạng thái xác thực và thông tin tài khoản
        /// </summary>
        /// <param name="sender">Nút Xác thực</param>
        /// <param name="e">Thông tin sự kiện Click</param>
        private void btnXacThuc_Click(object sender, EventArgs e)
        {
            // Kiểm tra người dùng đã nhập mã xác thực chưa
            if (txtMaXacThuc.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã xác thực!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Dừng xử lý
            }

            // Tạo kết nối đến database
            QLTVEntities db = new QLTVEntities();
            
            // Tìm người dùng theo ID
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == ID).SingleOrDefault();

            // Kiểm tra xem có tìm thấy người dùng không
            if (nguoiDung == null) 
                return; // Nếu không tìm thấy, dừng xử lý

            // Kiểm tra mã OTP người dùng nhập có khớp với mã OTP trong database không
            if (txtMaXacThuc.Text != nguoiDung.MaOTP)
            {
                // Nếu không khớp, hiển thị thông báo lỗi
                MessageBox.Show("Mã xác thực không chính xác!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaXacThuc.Focus(); // Focus vào ô nhập mã để người dùng nhập lại
                return; // Dừng xử lý
            }

            // Kiểm tra mã OTP có hết hạn không (thời gian hết hạn: 5 phút)
            // DateTime.Now: thời gian hiện tại
            // nguoiDung.ThoiGianNhanOTP.Value: thời gian nhận OTP
            // TotalMinutes: số phút chênh lệch giữa 2 thời điểm
            if ((DateTime.Now - nguoiDung.ThoiGianNhanOTP.Value).TotalMinutes > 5)
            {
                // Nếu quá 5 phút, mã OTP đã hết hạn
                MessageBox.Show("Mã xác thực đã hết hạn!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Dừng xử lý
            }
            
            // Nếu mã OTP đúng và còn hạn, cập nhật trạng thái tài khoản
            nguoiDung.TrangThaiXacThuc = true;  // Đánh dấu đã xác thực
            nguoiDung.BiKhoa = false;           // Đảm bảo tài khoản không bị khóa
            nguoiDung.QuyenHan = "user";        // Mặc định quyền là user (người dùng thông thường)
            nguoiDung.SoSachMuon = 0;           // Khởi tạo số sách đang mượn = 0
            nguoiDung.NgayDangKi = DateTime.Now; // Lưu ngày đăng ký (nếu chưa có)
            
            // Lưu thay đổi vào database
            db.SaveChanges();
            
            // Hiển thị thông báo thành công
            MessageBox.Show("Xác thực thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            // Nếu có callback (form được gọi từ form khác)
            if (kiemTra) 
                callback(true); // Gọi callback với true = xác thực thành công
            
            // Đóng form xác thực
            this.Close();
        }
    }
}
