using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVienApp
{
    /// <summary>
    /// Lớp tĩnh GuiEmail chịu trách nhiệm gửi email trong hệ thống
    /// Sử dụng Gmail SMTP để gửi email OTP và thông báo
    /// </summary>
    public static class GuiEmail
    {
        // Tài khoản Gmail được sử dụng để gửi email
        // Đây là email của hệ thống thư viện
        public static string taiKhoan = "thuvienhcmue@gmail.com";
        
        // Mật khẩu ứng dụng (App Password) của Gmail
        // Không phải mật khẩu đăng nhập thông thường
        // Được tạo từ Google Account Settings > Security > 2-Step Verification > App passwords
        public static string matKhau = "vddc dvte zsfn gcwt";

        /// <summary>
        /// Hàm gửi email đến địa chỉ email được chỉ định
        /// </summary>
        /// <param name="emailTo">Địa chỉ email người nhận</param>
        /// <param name="content">Nội dung email cần gửi</param>
        /// <returns>true nếu gửi thành công, false nếu có lỗi</returns>
        public static bool guiEmail(string emailTo, string content)
        {
            // Tạo địa chỉ email người gửi
            // MailAddress(string address, string displayName)
            // address: địa chỉ email thực tế
            // displayName: tên hiển thị trong email (người nhận sẽ thấy tên này)
            var fromAddress = new MailAddress(taiKhoan, "Quan ly thu vien");
            
            // Tạo địa chỉ email người nhận
            // Sử dụng emailTo làm cả address và displayName
            var toAddress = new MailAddress(emailTo, emailTo);
            
            // Lấy mật khẩu ứng dụng để xác thực
            string fromPassword = matKhau;
            
            // Tiêu đề email mặc định
            string subject = "Thông báo hệ thống";
            
            // Nội dung email (thường là mã OTP hoặc thông báo)
            string body = content;

            // Cấu hình SMTP Client để kết nối với Gmail
            var smtp = new SmtpClient
            {
                // Máy chủ SMTP của Gmail
                Host = "smtp.gmail.com",
                
                // Cổng SMTP của Gmail (587 là cổng TLS/STARTTLS)
                Port = 587,
                
                // Bật SSL/TLS để mã hóa kết nối
                // Đảm bảo thông tin được mã hóa khi truyền qua mạng
                EnableSsl = true,
                
                // Phương thức gửi qua mạng (không phải file hoặc pickup directory)
                DeliveryMethod = SmtpDeliveryMethod.Network,
                
                // Không sử dụng credentials mặc định của Windows
                // Sẽ sử dụng credentials riêng được cung cấp bên dưới
                UseDefaultCredentials = false,
                
                // Xác thực với Gmail bằng email và mật khẩu ứng dụng
                // NetworkCredential(string userName, string password)
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            
            // Khối try-catch để xử lý lỗi khi gửi email
            try
            {
                // Sử dụng using để đảm bảo MailMessage được dispose đúng cách
                // MailMessage đại diện cho một email cần gửi
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    // Tiêu đề email
                    Subject = subject,
                    // Nội dung email
                    Body = body
                })
                {
                    // Gửi email thông qua SMTP client
                    // Hàm này sẽ kết nối đến Gmail, xác thực và gửi email
                    smtp.Send(message);
                }
                
                // Trả về true nếu gửi thành công
                return true;
            }
            catch (Exception ex)
            {
                // Nếu có lỗi xảy ra, hiển thị thông báo lỗi cho người dùng
                // ex.Message chứa thông tin chi tiết về lỗi
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Trả về false để báo gửi thất bại
                return false;
            }
        }
    }
}
