using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace QuanLyThuVienApp
{
    /// <summary>
    /// Lớp Program chứa điểm khởi đầu của ứng dụng
    /// Đây là entry point chính khi chạy ứng dụng Windows Forms
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Hàm Main - Điểm khởi đầu của ứng dụng
        /// [STAThread] đảm bảo thread chạy ở chế độ Single Threaded Apartment
        /// Đây là yêu cầu bắt buộc cho Windows Forms applications
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Bật visual styles cho Windows Forms (Windows XP style)
            // Cho phép ứng dụng sử dụng các style hiện đại của Windows
            Application.EnableVisualStyles();
            
            // Tắt text rendering mặc định để tương thích với .NET Framework cũ
            // false = sử dụng GDI+ thay vì GDI để render text
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Khởi chạy ứng dụng với form đăng nhập làm form đầu tiên
            // Application.Run() sẽ tạo message loop để xử lý các sự kiện Windows
            // Form đăng nhập sẽ là form đầu tiên hiển thị khi mở ứng dụng
            Application.Run(new frmDangNhap());
        }
    }
}
