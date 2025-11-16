using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVienApp
{
    /// <summary>
    /// Form đăng nhập - Form đầu tiên hiển thị khi mở ứng dụng
    /// Cho phép người dùng đăng nhập vào hệ thống bằng tên đăng nhập/email và mật khẩu
    /// </summary>
    public partial class frmDangNhap : MetroFramework.Forms.MetroForm
    {
        /// <summary>
        /// Constructor của form đăng nhập
        /// InitializeComponent() khởi tạo tất cả các controls được thiết kế trong Designer
        /// </summary>
        public frmDangNhap()
        {
            InitializeComponent();
        }
       
        /// <summary>
        /// Sự kiện Load form - được gọi khi form được hiển thị lần đầu
        /// Có thể dùng để khởi tạo dữ liệu ban đầu cho form
        /// </summary>
        /// <param name="sender">Đối tượng gây ra sự kiện (form này)</param>
        /// <param name="e">Thông tin về sự kiện Load</param>
        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            // Hiện tại không có code khởi tạo nào
            // Có thể thêm code để focus vào ô nhập tên đăng nhập, load cấu hình, etc.
        }

        /// <summary>
        /// Xử lý sự kiện khi người dùng nhấn nút Thoát
        /// Đóng form đăng nhập và kết thúc ứng dụng
        /// </summary>
        /// <param name="sender">Nút Thoát</param>
        /// <param name="e">Thông tin sự kiện Click</param>
        private void btnThoat_Click(object sender, EventArgs e)
        {
            // Đóng form này
            // Vì đây là form chính (Application.Run), đóng form này sẽ kết thúc ứng dụng
            this.Close();
        }

        /// <summary>
        /// Xử lý sự kiện khi người dùng nhấn nút Đăng nhập
        /// Thực hiện xác thực tài khoản và mật khẩu
        /// </summary>
        /// <param name="sender">Nút Đăng nhập</param>
        /// <param name="e">Thông tin sự kiện Click</param>
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            // Kiểm tra người dùng đã nhập đầy đủ thông tin chưa
            // txtTenDangNhap.Text == "" kiểm tra ô tên đăng nhập có rỗng không
            // txtMatKhau.Text == "" kiểm tra ô mật khẩu có rỗng không
            if(txtTenDangNhap.Text=="" || txtMatKhau.Text == "")
            {
                // Hiển thị thông báo lỗi yêu cầu nhập đầy đủ thông tin
                // MessageBoxButtons.OK: chỉ có nút OK
                // MessageBoxIcon.Error: hiển thị icon lỗi
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Dừng xử lý, không tiếp tục
                return;
            }

            // Tạo kết nối đến database thông qua Entity Framework
            // QLTVEntities là DbContext được tạo từ Entity Framework
            QLTVEntities db = new QLTVEntities();
            
            // Tìm người dùng trong database
            // Where(): lọc các bản ghi thỏa mãn điều kiện
            // p.TenDangNhap == txtTenDangNhap.Text: tìm theo tên đăng nhập
            // p.Email == txtTenDangNhap.Text: HOẶC tìm theo email (cho phép đăng nhập bằng email)
            // SingleOrDefault(): trả về 1 bản ghi nếu tìm thấy, null nếu không tìm thấy
            // Nếu có nhiều hơn 1 bản ghi sẽ throw exception
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.TenDangNhap == txtTenDangNhap.Text || p.Email == txtTenDangNhap.Text)
                .SingleOrDefault();

            // Kiểm tra xem có tìm thấy người dùng không
            if(nguoiDung != null)
            {
                // Tạo đối tượng MD5 để mã hóa mật khẩu
                // MD5 là thuật toán hash một chiều (không thể giải mã ngược)
                MD5 mD5 = MD5.Create();
                
                // Chuyển mật khẩu người dùng nhập vào thành mảng byte
                // Encoding.ASCII: sử dụng bảng mã ASCII để chuyển đổi
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(txtMatKhau.Text);
                
                // Mã hóa mật khẩu thành hash MD5
                // ComputeHash() trả về mảng byte 16 phần tử (128 bit)
                byte[] hashBytes = mD5.ComputeHash(inputBytes);
                
                // So sánh mật khẩu đã hash với mật khẩu lưu trong database
                // SequenceEqual(): so sánh từng phần tử trong 2 mảng byte
                // Nếu giống nhau => mật khẩu đúng
                if (hashBytes.SequenceEqual(nguoiDung.MatKhau))
                {
                    // Kiểm tra trạng thái xác thực email của người dùng
                    // TrangThaiXacThuc == null: chưa xác thực (tài khoản mới đăng ký)
                    // TrangThaiXacThuc == false: đã xác thực nhưng bị vô hiệu hóa
                    // Nếu chưa xác thực hoặc bị vô hiệu hóa, yêu cầu xác thực lại
                    if (nguoiDung.TrangThaiXacThuc == null || nguoiDung.TrangThaiXacThuc == false)
                    {
                        // Tạo mã OTP ngẫu nhiên 6 chữ số
                        // Random(): tạo số ngẫu nhiên
                        // Next(100000, 999999): tạo số từ 100000 đến 999999 (6 chữ số)
                        // ToString(): chuyển số thành chuỗi
                        Random random = new Random();
                        string OTP = random.Next(100000, 999999).ToString();
                        
                        // Gửi mã OTP đến email của người dùng
                        GuiEmail.guiEmail(nguoiDung.Email, "Mã xác thực của bạn là " + OTP);
                        
                        // Lưu mã OTP vào database
                        nguoiDung.MaOTP = OTP;
                        
                        // Lưu thời gian nhận OTP để kiểm tra hết hạn (thường là 5 phút)
                        nguoiDung.ThoiGianNhanOTP = DateTime.Now;
                        
                        // Lưu thay đổi vào database
                        db.SaveChanges();
                        
                        // Mở form xác thực OTP
                        // Truyền ID của người dùng để form xác thực biết xác thực cho ai
                        frmXacThuc frmXacThuc = new frmXacThuc(nguoiDung.ID);
                        
                        // Ẩn form đăng nhập
                        this.Hide();
                        
                        // Hiển thị form xác thực dạng dialog (chặn form đăng nhập)
                        // ShowDialog() sẽ chờ đến khi form xác thực đóng
                        frmXacThuc.ShowDialog();
                        
                        // Sau khi form xác thực đóng, hiển thị lại form đăng nhập
                        this.Show();
                        
                        // Dừng xử lý, không tiếp tục đăng nhập
                        return;
                    }

                    // Nếu đã xác thực, ẩn form đăng nhập
                    this.Hide();

                    // Kiểm tra quyền hạn của người dùng để mở form tương ứng
                    // "user": người dùng thông thường
                    if(nguoiDung.QuyenHan == "user")
                    {
                        // Tạo và hiển thị form chính cho người dùng
                        // Truyền: ID, Họ tên, và trạng thái bị khóa
                        frmMainUser frm = new frmMainUser(nguoiDung.ID, nguoiDung.HoTen, nguoiDung.BiKhoa);
                        frm.ShowDialog();
                    }
                    else
                    {
                        // "admin": quản trị viên
                        // Tạo và hiển thị form chính cho admin
                        frmMainAdmin frm = new frmMainAdmin(nguoiDung.ID, nguoiDung.HoTen, nguoiDung.BiKhoa);
                        frm.ShowDialog();
                    }
                    
                    // Đóng form đăng nhập sau khi đã mở form chính
                    this.Close();
                    return;
                }
            }

            // Nếu không tìm thấy người dùng hoặc mật khẩu sai
            // Hiển thị thông báo lỗi
            MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        /// <summary>
        /// Xử lý sự kiện khi người dùng click vào link "Đăng ký tài khoản"
        /// Mở form đăng ký để người dùng có thể tạo tài khoản mới
        /// </summary>
        /// <param name="sender">LinkLabel "Đăng ký tài khoản"</param>
        /// <param name="e">Thông tin sự kiện LinkClicked</param>
        private void linkDangKyTK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Tạo form đăng ký mới
            frmDangKy frm = new frmDangKy();
            
            // Ẩn form đăng nhập
            this.Hide();
            
            // Hiển thị form đăng ký dạng dialog
            // ShowDialog() chặn form đăng nhập cho đến khi form đăng ký đóng
            frm.ShowDialog();
            
            // Sau khi form đăng ký đóng, hiển thị lại form đăng nhập
            this.Show();
        }

        /// <summary>
        /// Xử lý sự kiện khi người dùng click vào link "Quên mật khẩu"
        /// Mở form tìm tài khoản để người dùng có thể đặt lại mật khẩu
        /// </summary>
        /// <param name="sender">LinkLabel "Quên mật khẩu"</param>
        /// <param name="e">Thông tin sự kiện LinkClicked</param>
        private void linkQuenMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Tạo form tìm tài khoản mới
            frmTimTaiKhoan frm = new frmTimTaiKhoan();
            
            // Ẩn form đăng nhập
            this.Hide();
            
            // Hiển thị form tìm tài khoản dạng dialog
            frm.ShowDialog();
            
            // Sau khi form tìm tài khoản đóng, hiển thị lại form đăng nhập
            this.Show();
        }
    }
}
