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
    /// Form trợ giúp - Hiển thị hướng dẫn sử dụng hệ thống
    /// Các chủ đề: Đăng ký mượn sách, Quy định phạt, Gia hạn, Quên mật khẩu, Cho mượn sách, Tạo tài khoản
    /// </summary>
    public partial class frmTroGiup : Form
    {
        /// <summary>
        /// Khởi tạo form trợ giúp
        /// </summary>
        public frmTroGiup()
        {
            InitializeComponent();
        }
     
        /// <summary>
        /// Sự kiện Load form
        /// </summary>
        private void frmTroGiup_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Hiển thị hướng dẫn đăng ký mượn sách
        /// </summary>
        private void label1_Click(object sender, EventArgs e)
        {
            // Đổi màu label được chọn
            label1.ForeColor = Color.Red;
            label2.ForeColor = Color.Black;
            label3.ForeColor = Color.Black;
            label4.ForeColor = Color.Black;
            label5.ForeColor = Color.Black;
            label6.ForeColor = Color.Black;

            // Hiển thị hướng dẫn
            richTextBox1.Text = "-> Nhấn vào Đăng ký mượn sách\n\n" +
                   "-> Bên phải cuốn sách bạn muốn mượn, chọn Đăng ký để thêm vào danh sách\n\n" +
                   "-> Trên Danh sách đăng ký mượn, nhấn vào Đăng ký để gửi yêu cầu\n\n" +
                   "-> Vui lòng đến thư viện để lấy sách trong vòng 5 ngày, sau 5 ngày không đến mượn sách chúng tôi sẽ hủy phiếu đăng ký";
        }

        /// <summary>
        /// Hiển thị quy định phạt khi quá hạn
        /// </summary>
        private void label2_Click(object sender, EventArgs e)
        {
            // Đổi màu label được chọn
            label1.ForeColor = Color.Black;
            label2.ForeColor = Color.Red;
            label3.ForeColor = Color.Black;
            label4.ForeColor = Color.Black;
            label5.ForeColor = Color.Black;
            label6.ForeColor = Color.Black;
            // Hiển thị quy định phạt
            richTextBox1.Text = "-> Nếu bạn quá hạn mượn sách, bạn sẽ chịu mức phạt tương ứng với số ngày quá hạn\n\n" +
                "-> Mỗi ngày quá hạn được tính là 1000 VNĐ\n\n" +
                "-> Nếu thư viện thấy có dấu hiệu vi phạm nghiêm trọng, chúng tôi sẽ tiến hành khóa tài khoản của bạn và ghi nhận vào hồ sơ";
        }

        /// <summary>
        /// Hiển thị hướng dẫn gia hạn thời gian mượn sách
        /// </summary>
        private void label3_Click(object sender, EventArgs e)
        {
            // Đổi màu label được chọn
            label1.ForeColor = Color.Black;
            label2.ForeColor = Color.Black;
            label3.ForeColor = Color.Red;
            label4.ForeColor = Color.Black;
            label5.ForeColor = Color.Black;
            label6.ForeColor = Color.Black;
            // Hiển thị hướng dẫn gia hạn
            richTextBox1.Text = "-> Hiện tại chúng tôi chưa cho phép gia hạn sách mượn trên ứng dụng\n\n" +
                "-> Nếu muốn gia hạn thời gian mượn sách, hãy đến thư viện để được hỗ trợ";
        }

        /// <summary>
        /// Hiển thị hướng dẫn khi quên mật khẩu
        /// </summary>
        private void label4_Click(object sender, EventArgs e)
        {
            // Đổi màu label được chọn
            label1.ForeColor = Color.Black;
            label2.ForeColor = Color.Black;
            label3.ForeColor = Color.Black;
            label4.ForeColor = Color.Red;
            label5.ForeColor = Color.Black;
            label6.ForeColor = Color.Black;
            // Hiển thị hướng dẫn
            richTextBox1.Text = "-> Bạn hãy đến thư viện để được hỗ trợ\n\n" +
                "-> Chúng tôi có thể Reset mật khẩu hoặc Liên kết email mới giúp bạn";
        }

        /// <summary>
        /// Hiển thị hướng dẫn cho Admin về cách cho mượn sách
        /// </summary>
        private void label5_Click(object sender, EventArgs e)
        {
            // Đổi màu label được chọn
            label1.ForeColor = Color.Black;
            label2.ForeColor = Color.Black;
            label3.ForeColor = Color.Black;
            label4.ForeColor = Color.Black;
            label5.ForeColor = Color.Red;
            label6.ForeColor = Color.Black;
            // Hiển thị hướng dẫn cho Admin
            richTextBox1.Text = "   (Dành cho nhân viên thư viện)\n\n" +
                "-> Chọn mục Quản lý phiếu\n\n" +
                "-> Trong phần Danh sách phiếu, chọn Đăng ký\n\n" +
                "-> Nhấn vào Mượn mới\n\n" +
                "-> Lựa chọn bạn đọc cần mượn sách, nếu chưa có tài khoản thì tạo tài khoản cho bạn đọc, nhấn Mượn sách\n\n" +
                "-> Lựa chọn sách bạn đọc cần mượn\n\n" +
                "-> Nhấn Cho mượn để hoàn tất cho mượn sách";
        }

        /// <summary>
        /// Hiển thị hướng dẫn cho Admin về cách tạo tài khoản bạn đọc
        /// </summary>
        private void label6_Click(object sender, EventArgs e)
        {
            // Đổi màu label được chọn
            label1.ForeColor = Color.Black;
            label2.ForeColor = Color.Black;
            label3.ForeColor = Color.Black;
            label4.ForeColor = Color.Black;
            label5.ForeColor = Color.Black;
            label6.ForeColor = Color.Red;
            // Hiển thị hướng dẫn cho Admin
            richTextBox1.Text = "   (Dành cho nhân viên thư viện)\n\n" +
                "-> Vào phần Quản lý bạn đọc\n\n" +
                "-> Điền thông tin trong mục Tạo tài khoản mới\n\n" +
                "-> Xác thực email bằng mã OTP, sau đó nhấn Đăng ký\n\n" +
                "-> Mật khẩu đăng nhập sẽ được tạo ngẫu nhiên và gửi về email đăng ký";
        }
    }
}
