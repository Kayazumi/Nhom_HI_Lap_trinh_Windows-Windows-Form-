using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace QuanLyThuVienApp
{
    /// <summary>
    /// Form đăng ký tài khoản mới
    /// Cho phép người dùng tạo tài khoản mới với email, tên đăng nhập, mật khẩu và họ tên
    /// Sau khi đăng ký, hệ thống sẽ gửi mã OTP để xác thực email
    /// </summary>
    public partial class frmDangKy : MetroFramework.Forms.MetroForm
    {
        /// <summary>
        /// Constructor của form đăng ký
        /// Khởi tạo các controls trong form
        /// </summary>
        public frmDangKy()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện Load form - được gọi khi form được hiển thị
        /// </summary>
        /// <param name="sender">Form này</param>
        /// <param name="e">Thông tin sự kiện Load</param>
        private void frmDangKy_Load(object sender, EventArgs e)
        {
            // Không có code khởi tạo
        }

        /// <summary>
        /// Xử lý sự kiện khi người dùng nhấn nút Thoát
        /// Đóng form đăng ký và quay lại form đăng nhập
        /// </summary>
        /// <param name="sender">Nút Thoát</param>
        /// <param name="e">Thông tin sự kiện Click</param>
        private void btnThoat_Click(object sender, EventArgs e)
        {
            // Đóng form này
            this.Close();   
        }

        /// <summary>
        /// Hàm kiểm tra định dạng email có hợp lệ không
        /// Sử dụng Regular Expression để validate định dạng email
        /// </summary>
        /// <param name="inputEmail">Chuỗi email cần kiểm tra</param>
        /// <returns>true nếu email hợp lệ, false nếu không hợp lệ</returns>
        private bool isEmail(string inputEmail)
        {
            // Xử lý trường hợp inputEmail là null
            // ?? là null-coalescing operator: nếu inputEmail null thì dùng string.Empty
            inputEmail = inputEmail ?? string.Empty;
            
            // Regular Expression pattern để kiểm tra định dạng email
            // Pattern này kiểm tra:
            // - Phần trước @: chứa chữ cái, số, dấu gạch dưới, dấu gạch ngang, dấu chấm
            // - Phần sau @: có thể là IP address hoặc domain name
            // - Domain: có thể kết thúc bằng 2-4 chữ cái hoặc 1-3 chữ số
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            
            // Tạo đối tượng Regex với pattern trên
            Regex re = new Regex(strRegex);
            
            // Kiểm tra xem inputEmail có khớp với pattern không
            if (re.IsMatch(inputEmail))
                return (true);  // Email hợp lệ
            else
                return (false); // Email không hợp lệ
        }

        /// <summary>
        /// Xử lý sự kiện khi người dùng nhấn nút Đăng ký
        /// Thực hiện các bước: validate dữ liệu, kiểm tra trùng lặp, mã hóa mật khẩu, tạo OTP, lưu vào database
        /// </summary>
        /// <param name="sender">Nút Đăng ký</param>
        /// <param name="e">Thông tin sự kiện Click</param>
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            // BƯỚC 1: Kiểm tra người dùng đã nhập đầy đủ thông tin chưa
            // Kiểm tra từng trường: Email, Tên đăng nhập, Mật khẩu, Xác nhận mật khẩu, Họ tên
            if(txtEmail.Text=="" || txtTenDangNhap.Text=="" || txtMatKhau.Text=="" || txtMatKhau2.Text == "" || txtHoTen.Text == "")
            {
                // Hiển thị thông báo lỗi nếu thiếu thông tin
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Dừng xử lý
            }

            // BƯỚC 2: Kiểm tra định dạng email có hợp lệ không
            // Sử dụng hàm isEmail() đã định nghĩa ở trên
            if(!isEmail(txtEmail.Text)) 
            {
                // Hiển thị thông báo lỗi nếu email không hợp lệ
                MessageBox.Show("Email không hợp lệ!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Focus vào ô email để người dùng sửa
                txtEmail.Focus();
                return; // Dừng xử lý
            }

            // BƯỚC 3: Kiểm tra tên đăng nhập đã tồn tại chưa
            // Tạo kết nối đến database
            QLTVEntities db = new QLTVEntities();
            
            // Tìm người dùng có tên đăng nhập trùng với tên đăng nhập người dùng nhập
            // SingleOrDefault(): trả về 1 bản ghi hoặc null
            NguoiDung nguoiDung = db.NguoiDungs.Where(p=>p.TenDangNhap==txtTenDangNhap.Text).SingleOrDefault();

            // Nếu tìm thấy người dùng có tên đăng nhập trùng
            if(nguoiDung != null)
            {
                // Kiểm tra xem tài khoản đó đã được xác thực chưa
                if(nguoiDung.TrangThaiXacThuc == true)
                {
                    // Nếu đã xác thực => tên đăng nhập đã được sử dụng
                    MessageBox.Show("Tên đăng nhập đã tồn tại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenDangNhap.Focus(); // Focus vào ô tên đăng nhập
                    return; // Dừng xử lý
                }
                else
                {
                    // Nếu chưa xác thực (tài khoản đăng ký nhưng chưa xác thực email)
                    // Xóa tài khoản cũ để cho phép đăng ký lại với tên đăng nhập đó
                    db.NguoiDungs.Remove(nguoiDung);
                    db.SaveChanges(); // Lưu thay đổi
                }
            }

            // BƯỚC 4: Kiểm tra email đã được sử dụng chưa
            // Tìm người dùng có email trùng với email người dùng nhập
            nguoiDung = db.NguoiDungs.SingleOrDefault(p=>p.Email==txtEmail.Text);
            
            if (nguoiDung != null)
            {
                // Kiểm tra xem email đó đã được xác thực chưa
                if (nguoiDung.TrangThaiXacThuc == true)
                {
                    // Nếu đã xác thực => email đã được sử dụng
                    MessageBox.Show("Email đã được sử dụng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenDangNhap.Focus();
                    return; // Dừng xử lý
                }
                else
                {
                    // Nếu chưa xác thực => xóa tài khoản cũ để cho phép đăng ký lại
                    db.NguoiDungs.Remove(nguoiDung);
                    db.SaveChanges(); // Lưu thay đổi
                }
            }
            
            // BƯỚC 5: Kiểm tra độ dài mật khẩu (tối thiểu 6 ký tự)
            if(txtMatKhau.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu có tối thiểu 6 ký tự!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMatKhau.Focus(); // Focus vào ô mật khẩu
                return; // Dừng xử lý
            }

            // BƯỚC 6: Kiểm tra xác nhận mật khẩu có khớp với mật khẩu không
            if (txtMatKhau2.Text != txtMatKhau.Text)
            {
                MessageBox.Show("Xác nhận mật khẩu sai!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMatKhau2.Focus(); // Focus vào ô xác nhận mật khẩu
                return; // Dừng xử lý
            }

            // BƯỚC 7: Mã hóa mật khẩu bằng MD5
            // Tạo đối tượng MD5 để hash mật khẩu
            MD5 mD5 = MD5.Create();
            
            // Chuyển mật khẩu từ string sang mảng byte (ASCII encoding)
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(txtMatKhau.Text);
            
            // Hash mật khẩu thành mảng byte 16 phần tử
            byte[] hashBytes = mD5.ComputeHash(inputBytes);

            // BƯỚC 8: Tạo mã OTP ngẫu nhiên 6 chữ số
            Random rand = new Random();
            // Next(100000, 999999): tạo số từ 100000 đến 999999
            string OTP = rand.Next(100000, 999999).ToString();

            // BƯỚC 9: Tạo đối tượng NguoiDung mới và gán các thuộc tính
            NguoiDung nd = new NguoiDung();

            // Gán các thông tin từ form vào đối tượng
            nd.TenDangNhap = txtTenDangNhap.Text;  // Tên đăng nhập
            nd.HoTen = txtHoTen.Text;               // Họ tên
            nd.MatKhau = hashBytes;                 // Mật khẩu đã hash (mảng byte)
            nd.Email = txtEmail.Text;               // Email
            nd.MaOTP = OTP;                         // Mã OTP để xác thực
            
            // Thêm người dùng mới vào database
            db.NguoiDungs.Add(nd);
            
            // Lưu thay đổi vào database để lấy ID tự động
            db.SaveChanges();
            
            // BƯỚC 10: Gửi mã OTP đến email của người dùng
            GuiEmail.guiEmail(txtEmail.Text, "Mã xác thực của bạn là " + OTP);
            
            // Lưu thời gian nhận OTP để kiểm tra hết hạn (thường là 5 phút)
            nd.ThoiGianNhanOTP = DateTime.Now;
            
            // Lưu lại thay đổi (thời gian nhận OTP)
            db.SaveChanges();
            
            // BƯỚC 11: Mở form xác thực OTP
            // Truyền ID của người dùng vừa tạo để form xác thực biết xác thực cho ai
            frmXacThuc frm = new frmXacThuc(nd.ID);
            
            // Ẩn form đăng ký
            this.Hide();
            
            // Hiển thị form xác thực dạng dialog (chặn form đăng ký)
            frm.ShowDialog();
            
            // Đóng form đăng ký sau khi form xác thực đóng
            this.Close();
        }
    }
}
