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

namespace QuanLyThuVienApp
{
    public partial class frmQuanLyBanDoc : Form
    {
        public static int OTP;
        public static DateTime thoiGian;

        public frmQuanLyBanDoc()
        {
            InitializeComponent();
        }

        private void frmQuanLyBanDoc_Load(object sender, EventArgs e)
        {
            loadDuLieu();
            btnDangKy.Enabled = false;
        }

        private void loadDuLieu()
        {
            QLTVEntities db = new QLTVEntities();
            dgvBanDoc.DataSource = db.NguoiDungs.Where(p => p.QuyenHan == "user" && p.TrangThaiXacThuc == true && p.BiKhoa == false)
                .Select(p => new
                {
                    MaBanDoc = "BD" + p.ID,
                    p.HoTen,
                    p.Email,
                    p.NgayDangKi,
                    p.SoSachMuon
                }).ToList();

            if(dgvBanDoc.Rows.Count > 0)
            {
                txtID.Text = dgvBanDoc.Rows[0].Cells["MaBanDoc"].Value.ToString();
                txtSuaEmail.Text = dgvBanDoc.Rows[0].Cells["Email"].Value.ToString();
                txtSuaTen.Text = dgvBanDoc.Rows[0].Cells["HoTen"].Value.ToString();
            }
        }

        private bool isEmail(string inputEmail)
        {
            inputEmail = inputEmail ?? string.Empty;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút Gửi mã OTP
        /// Gửi mã OTP 6 chữ số qua email để xác thực đăng ký bạn đọc mới
        /// </summary>
        /// <param name="sender">Nút Gửi mã</param>
        /// <param name="e">Thông tin sự kiện Click</param>
        private void btnGuiMa_Click(object sender, EventArgs e)
        {
            // Kiểm tra email đã nhập chưa
            if(txtEmail.Text == "")
            {
                MessageBox.Show("Vui lòng nhập email!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra định dạng email
            if (!isEmail(txtEmail.Text))
            {
                MessageBox.Show("Email không hợp lệ!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            QLTVEntities db = new QLTVEntities();
            // Kiểm tra email đã tồn tại chưa
            NguoiDung nguoiDung = db.NguoiDungs.Where(p=>p.Email == txtEmail.Text).FirstOrDefault();

            if (nguoiDung != null)
            {
                // Nếu email đã được xác thực (đã sử dụng), báo lỗi
                if (nguoiDung.TrangThaiXacThuc == true)
                {
                    MessageBox.Show("Email đã được sử dụng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    // Nếu email chưa xác thực (đăng ký dở dang), xóa để đăng ký lại
                    db.NguoiDungs.Remove(nguoiDung);
                    db.SaveChanges();
                }
            }

            // Tạo mã OTP ngẫu nhiên 6 chữ số (100000-999999)
            Random random = new Random();
            OTP = random.Next(100000, 999999);
            thoiGian = DateTime.Now; // Lưu thời gian gửi OTP

            // Gửi email chứa mã OTP
            GuiEmail.guiEmail(txtEmail.Text, "Mã xác thực  của bạn là " + OTP);
            
            // Khóa ô email và kích hoạt nút Đăng ký
            txtEmail.ReadOnly = true;
            btnDangKy.Enabled = true;

            MessageBox.Show("Vui lòng kiểm tra email!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            txtEmail.Text = "";
            txtTen.Text = "";
            txtMa.Text = "";

            txtEmail.ReadOnly = false;
            btnDangKy.Enabled = false;
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút Đăng ký
        /// Tạo tài khoản bạn đọc mới sau khi xác thực OTP thành công
        /// Mật khẩu được tạo ngẫu nhiên 6 chữ số và gửi qua email
        /// </summary>
        /// <param name="sender">Nút Đăng ký</param>
        /// <param name="e">Thông tin sự kiện Click</param>
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            // Xác nhận với người dùng
            DialogResult result = MessageBox.Show(
                "Bạn có muốn đăng ký tài khoản mới không?",
                "Thông báo!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No) return; // Người dùng chọn No, dừng xử lý

            // Kiểm tra đầy đủ thông tin
            if (txtEmail.Text == "" || txtTen.Text == "" || txtMa.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            QLTVEntities db = new QLTVEntities();
            // Xóa tài khoản chưa xác thực nếu có (đăng ký dở dang)
            NguoiDung nd = db.NguoiDungs.Where(p => p.Email == txtEmail.Text).FirstOrDefault();

            if(nd != null)
            {
                db.NguoiDungs.Remove(nd);
                db.SaveChanges();
            }

            // Kiểm tra mã OTP có đúng không
            if (txtMa.Text != OTP.ToString())
            {
                MessageBox.Show("Mã xác thực không đúng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra mã OTP có hết hạn không (quá 5 phút)
            if((DateTime.Now - thoiGian).TotalMinutes > 5)
            {
                MessageBox.Show("Mã xác thực đã hết hạn!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tạo mật khẩu ngẫu nhiên 6 chữ số
            Random random = new Random();
            string matKhau = random.Next(100000, 999999).ToString();

            // Mã hóa mật khẩu bằng MD5
            MD5 mD5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(matKhau);
            byte[] hashBytes = mD5.ComputeHash(inputBytes);

            // Tạo đối tượng bạn đọc mới
            NguoiDung nguoiDung = new NguoiDung();
            nguoiDung.Email = txtEmail.Text;                    // Email
            nguoiDung.TrangThaiXacThuc = true;                  // Đã xác thực
            nguoiDung.BiKhoa = false;                          // Chưa bị khóa
            nguoiDung.HoTen = txtTen.Text;                      // Họ tên
            nguoiDung.QuyenHan = "user";                       // Quyền bạn đọc
            nguoiDung.NgayDangKi = DateTime.Now;                // Ngày đăng ký
            nguoiDung.SoSachMuon = 0;                          // Số sách đang mượn = 0
            nguoiDung.MaOTP = txtMa.Text;                       // Lưu mã OTP đã dùng
            nguoiDung.ThoiGianNhanOTP = thoiGian;              // Thời gian nhận OTP
            nguoiDung.MatKhau = hashBytes;                      // Mật khẩu đã mã hóa MD5
            nguoiDung.TenDangNhap = txtEmail.Text;             // Tên đăng nhập = email

            // Thêm vào database
            db.NguoiDungs.Add(nguoiDung);
            db.SaveChanges();
            
            // Gửi mật khẩu qua email
            GuiEmail.guiEmail(txtEmail.Text, "Mật khẩu đăng nhập của bạn là " + matKhau);

            // Xóa các ô input
            txtEmail.Clear();
            txtMa.Clear();
            txtTen.Clear();
            
            // Tải lại danh sách bạn đọc
            loadDuLieu();

            // Vô hiệu hóa nút Đăng ký và mở khóa ô email
            btnDangKy.Enabled = false;
            txtEmail.ReadOnly = false;
            
            // Thông báo thành công
            MessageBox.Show("Tạo tài khoản bạn đọc thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            loadDuLieu();
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút Tìm kiếm
        /// Tìm kiếm bạn đọc theo Mã/Tên/Email
        /// </summary>
        /// <param name="sender">Nút Tìm kiếm</param>
        /// <param name="e">Thông tin sự kiện Click</param>
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string luaChon = cbTimKiem.Text; // Lấy tiêu chí tìm kiếm
            if (luaChon == "") return; // Chưa chọn tiêu chí, dừng

            QLTVEntities db = new QLTVEntities();
            List<NguoiDung> nguoiDungs = new List<NguoiDung>();

            // Tìm kiếm theo tiêu chí đã chọn
            if (luaChon == "Mã bạn đọc")
                // Tìm theo mã (format: BD + ID)
                nguoiDungs = db.NguoiDungs.Where(p => p.QuyenHan == "user" && p.TrangThaiXacThuc == true
                && p.BiKhoa == false && ("BD" + p.ID.ToString()).Contains(txtTimKiem.Text)).ToList();
            else if (luaChon == "Tên bạn đọc")
                // Tìm theo tên (contains)
                nguoiDungs = db.NguoiDungs.Where(p => p.QuyenHan == "user" && p.TrangThaiXacThuc == true
                && p.BiKhoa == false && p.HoTen.Contains(txtTimKiem.Text)).ToList();
            else if (luaChon == "Email")
                // Tìm theo email (contains)
                nguoiDungs = db.NguoiDungs.Where(p => p.QuyenHan == "user" && p.TrangThaiXacThuc == true
                && p.BiKhoa == false && p.Email.Contains(txtTimKiem.Text)).ToList();
            else return; // Tiêu chí không hợp lệ

            // Hiển thị kết quả tìm kiếm
            dgvBanDoc.DataSource = nguoiDungs.Select(p => new
            {
                MaBanDoc = "BD" + p.ID,
                p.HoTen,
                p.Email,
                p.NgayDangKi,
                p.SoSachMuon
            }).ToList();

            // Hiển thị thông tin bạn đọc đầu tiên (nếu có)
            if (dgvBanDoc.Rows.Count > 0)
            {
                txtID.Text = dgvBanDoc.Rows[0].Cells["MaBanDoc"].Value.ToString();
                txtSuaEmail.Text = dgvBanDoc.Rows[0].Cells["Email"].Value.ToString();
                txtSuaTen.Text = dgvBanDoc.Rows[0].Cells["HoTen"].Value.ToString();
            }
            else
            {
                // Không tìm thấy, xóa các ô input
                txtID.Clear();
                txtSuaEmail.Clear();
                txtSuaTen.Clear();
            }

        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút Sửa Email
        /// Đổi email của bạn đọc, yêu cầu xác thực OTP qua email mới
        /// </summary>
        /// <param name="sender">Nút Sửa Email</param>
        /// <param name="e">Thông tin sự kiện Click</param>
        private void btnSuaEmail_Click(object sender, EventArgs e)
        {
            // Kiểm tra có bạn đọc được chọn không
            if (txtID.Text == "") return;

            // Xác nhận với người dùng
            DialogResult result = MessageBox.Show(
                "Bạn có muốn thay đổi email cho tài khoản này không?",
                "Thông báo!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No) return; // Người dùng chọn No, dừng xử lý

            // Kiểm tra email mới đã nhập chưa
            if (txtSuaEmail.Text == "")
            {
                MessageBox.Show("Vui lòng nhập email mới!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra định dạng email
            if (!isEmail(txtSuaEmail.Text))
            {
                MessageBox.Show("Email không hợp lệ!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            QLTVEntities db = new QLTVEntities();
            // Kiểm tra email mới đã được sử dụng chưa
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.Email == txtSuaEmail.Text).FirstOrDefault();

            if(nguoiDung != null)
            {
                // Nếu email đã được xác thực (đã sử dụng), báo lỗi
                if(nguoiDung.TrangThaiXacThuc == true)
                {
                    MessageBox.Show("Email đã được sử dụng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    // Nếu email chưa xác thực (đăng ký dở dang), xóa để dùng lại
                    db.NguoiDungs.Remove(nguoiDung);
                    db.SaveChanges();
                }
            }

            // Lấy ID bạn đọc từ mã (bỏ "BD" ở đầu)
            int id = int.Parse(txtID.Text.Substring(2));
            nguoiDung = db.NguoiDungs.Where(p => p.ID == id).FirstOrDefault();

            // Kiểm tra email mới có khác email cũ không
            if (txtSuaEmail.Text == nguoiDung.Email)
            {
                MessageBox.Show("Cần nhập email mới!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tạo mã OTP mới
            Random random = new Random();
            string OTP = random.Next(100000, 999999).ToString();

            // Gửi OTP qua email mới
            GuiEmail.guiEmail(txtSuaEmail.Text, "Mã xác thực của bạn là " + OTP);

            // Lưu OTP và thời gian vào database
            nguoiDung.MaOTP = OTP;
            nguoiDung.ThoiGianNhanOTP = DateTime.Now;
            db.SaveChanges();

            // Mở form xác thực OTP
            // Nếu xác thực thành công, cập nhật email mới
            frmXacThuc frm = new frmXacThuc(nguoiDung.ID, xacNhan =>
            {
                if (xacNhan) // Xác thực thành công
                {
                    nguoiDung.Email = txtSuaEmail.Text; // Cập nhật email mới
                    db.SaveChanges();
                    loadDuLieu(); // Tải lại danh sách
                }
            });
            frm.ShowDialog();
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút Sửa Tên
        /// Cập nhật họ tên của bạn đọc
        /// </summary>
        /// <param name="sender">Nút Sửa Tên</param>
        /// <param name="e">Thông tin sự kiện Click</param>
        private void btnSuaTen_Click(object sender, EventArgs e)
        {
            // Kiểm tra có bạn đọc được chọn không
            if (txtID.Text == "") return;

            // Xác nhận với người dùng
            DialogResult result = MessageBox.Show(
                "Bạn có muốn sửa tên người dùng cho tài khoản này không?",
                "Thông báo!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No) return; // Người dùng chọn No, dừng xử lý

            // Kiểm tra họ tên đã nhập chưa
            if (txtSuaTen.Text == "")
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            QLTVEntities db = new QLTVEntities();
            // Lấy ID bạn đọc từ mã (bỏ "BD" ở đầu)
            int id = int.Parse(txtID.Text.Substring(2));

            // Tìm bạn đọc trong database
            NguoiDung nguoiDung = db.NguoiDungs.Where(p=>p.ID == id).FirstOrDefault();

            // Cập nhật họ tên mới
            nguoiDung.HoTen = txtSuaTen.Text;
            db.SaveChanges();
            
            // Tải lại danh sách
            loadDuLieu();
            
            // Thông báo thành công
            MessageBox.Show("Cập nhật tên thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút Reset Mật khẩu
        /// Tạo mật khẩu mới ngẫu nhiên 6 chữ số và gửi qua email
        /// </summary>
        /// <param name="sender">Nút Reset Mật khẩu</param>
        /// <param name="e">Thông tin sự kiện Click</param>
        private void btnResetMK_Click(object sender, EventArgs e)
        {
            // Kiểm tra có bạn đọc được chọn không
            if (txtID.Text == "") return;

            // Xác nhận với người dùng
            DialogResult result = MessageBox.Show(
                "Bạn có muốn đặt lại mật khẩu cho tài khoản này không?",
                "Thông báo!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No) return; // Người dùng chọn No, dừng xử lý

            // Tạo mật khẩu mới ngẫu nhiên 6 chữ số
            Random random = new Random();
            string matKhau = random.Next(100000, 999999).ToString();

            QLTVEntities db = new QLTVEntities();
            // Lấy ID bạn đọc từ mã (bỏ "BD" ở đầu)
            int id = int.Parse(txtID.Text.Substring(2));

            // Tìm bạn đọc trong database
            NguoiDung nguoiDung = db.NguoiDungs.Where(p => p.ID == id).FirstOrDefault();

            // Mã hóa mật khẩu mới bằng MD5
            MD5 mD5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(matKhau);
            byte[] hashBytes = mD5.ComputeHash(inputBytes);

            // Cập nhật mật khẩu mới (đã mã hóa)
            nguoiDung.MatKhau = hashBytes;
            db.SaveChanges();

            // Gửi mật khẩu mới qua email
            GuiEmail.guiEmail(nguoiDung.Email, "Mật khẩu mới của bạn là " + matKhau);

            // Thông báo thành công
            MessageBox.Show("Mật khẩu mới sẽ được gửi về email đăng ký!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Xử lý sự kiện khi click vào một dòng trong DataGridView danh sách bạn đọc
        /// Hiển thị thông tin bạn đọc được chọn vào các ô input để chỉnh sửa
        /// </summary>
        /// <param name="sender">DataGridView danh sách bạn đọc</param>
        /// <param name="e">Thông tin sự kiện CellClick</param>
        private void dgvBanDoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return; // Click vào header, bỏ qua

            // Nếu có dữ liệu, hiển thị thông tin bạn đọc được chọn
            if (dgvBanDoc.Rows.Count > 0)
            {
                txtID.Text = dgvBanDoc.Rows[e.RowIndex].Cells["MaBanDoc"].Value.ToString();
                txtSuaEmail.Text = dgvBanDoc.Rows[e.RowIndex].Cells["Email"].Value.ToString();
                txtSuaTen.Text = dgvBanDoc.Rows[e.RowIndex].Cells["HoTen"].Value.ToString();
            }
            else
            {
                // Không có dữ liệu, xóa các ô input
                txtID.Clear();
                txtEmail.Clear();
                txtSuaTen.Clear();
            }
        }
    }
}
