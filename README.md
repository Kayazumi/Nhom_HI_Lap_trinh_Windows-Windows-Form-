# 📚 Ứng Dụng Quản Lý Thư Viện

> Hệ thống quản lý thư viện desktop được xây dựng bằng C# WinForms, hỗ trợ quản lý mượn/trả sách, bạn đọc, và báo cáo thống kê với xác thực OTP qua email.

## 📋 Tổng Quan

Ứng dụng Quản Lý Thư Viện là một hệ thống quản lý thư viện nội bộ được phát triển bằng **Windows Forms** và **.NET Framework 4.8**. Hệ thống hỗ trợ quản lý toàn diện các hoạt động của thư viện bao gồm quản lý sách, bạn đọc, mượn/trả sách, và báo cáo thống kê.

### ✨ Tính Năng Chính

#### 🔐 Xác Thực & Phân Quyền
- Đăng ký tài khoản với xác thực OTP qua email
- Đăng nhập bằng tên đăng nhập hoặc email
- Phân quyền Admin/User rõ ràng
- Quên mật khẩu và reset qua OTP
- Khóa/mở khóa tài khoản

#### 📖 Quản Lý Danh Mục
- **Quản lý Sách**: Thêm, sửa, xóa, tìm kiếm đa tiêu chí
- **Quản lý Tác giả**: CRUD đầy đủ, tự động cập nhật số lượng sách
- **Quản lý Thể loại**: CRUD đầy đủ, tự động cập nhật số lượng sách
- **Quản lý Nhà xuất bản**: CRUD đầy đủ, tự động cập nhật số lượng sách

#### 👥 Quản Lý Bạn Đọc
- Tạo tài khoản bạn đọc mới với mật khẩu tự động
- Sửa thông tin bạn đọc
- Reset mật khẩu và gửi qua email
- Tìm kiếm bạn đọc theo Mã/Tên/Email

#### 📝 Quy Trình Mượn/Trả Sách
- **Đăng ký mượn**: Bạn đọc đăng ký mượn sách (tối đa 10 cuốn)
- **Xác nhận cho mượn**: Admin xác nhận và cho mượn sách
- **Cho mượn trực tiếp**: Admin có thể cho mượn sách không qua đăng ký
- **Gia hạn**: Gia hạn thời hạn mượn sách
- **Trả sách**: Xử lý trả sách và tính tiền phạt tự động (1000 VNĐ/ngày quá hạn)
- **Lịch sử mượn**: Xem lịch sử mượn sách của bạn đọc

#### 📊 Báo Cáo & Thống Kê
- Báo cáo số lượng sách theo thể loại
- Báo cáo tỷ lệ sách theo thể loại
- Hóa đơn phạt
- Biểu đồ cột và biểu đồ tròn trực quan
- Export báo cáo ra PDF, Excel, Word

## 🛠️ Công Nghệ Sử Dụng

### Framework & Ngôn Ngữ
- **.NET Framework 4.8**
- **C#** (Windows Forms)
- **Entity Framework 6.5.1** (Database First)

### Thư Viện UI
- **MetroFramework 1.4.0.0** - Giao diện Metro hiện đại
- **FontAwesome.Sharp 6.6.0** - Icon vector đẹp mắt

### Báo Cáo & Biểu Đồ
- **Microsoft ReportViewer 15.0** - Hiển thị báo cáo
- **System.Windows.Forms.DataVisualization.Charting** - Vẽ biểu đồ

### Cơ Sở Dữ Liệu
- **SQL Server** (LocalDB hoặc SQL Server Express/Standard)
- **Database**: `QLTV`

### Email
- **SMTP Gmail** - Gửi OTP và thông báo qua email

## 📦 Yêu Cầu Hệ Thống

### Phần Mềm
- **Windows 7** trở lên (64-bit khuyến nghị)
- **.NET Framework 4.8** (tự động cài khi cài đặt ứng dụng)
- **SQL Server LocalDB** hoặc **SQL Server Express/Standard**
- **Visual Studio 2022** (để phát triển)

### Phần Cứng
- RAM: Tối thiểu 2GB (4GB khuyến nghị)
- Ổ cứng: 500MB dung lượng trống
- Màn hình: Độ phân giải tối thiểu 1024x768

## 🚀 Cài Đặt & Chạy

### 1. Clone Repository
```bash
git clone <repository-url>
cd LTWIN
```

### 2. Cài Đặt Database

1. Mở **SQL Server Management Studio** (SSMS)
2. Kết nối đến SQL Server instance:
   - LocalDB: `(localdb)\MSSQLLocalDB`
   - Hoặc SQL Server: `localhost\SQLEXPRESS` (hoặc tên server của bạn)
3. Chạy script `QuanLyThuVienC#.sql` để tạo database `QLTV`

### 3. Cấu Hình Connection String

Mở file `QuanLyThuVienApp/QuanLyThuVienApp/App.config` và cập nhật connection string:

```xml
<connectionStrings>
  <add name="QLTVEntities" 
       connectionString="metadata=res://*/EntityModel.csdl|res://*/EntityModel.ssdl|res://*/EntityModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=(localdb)\MSSQLLocalDB;initial catalog=QLTV;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" 
       providerName="System.Data.EntityClient" />
</connectionStrings>
```

**Lưu ý**: Thay `(localdb)\MSSQLLocalDB` bằng tên server SQL Server của bạn nếu không dùng LocalDB.

### 4. Cấu Hình Email (OTP)

Mở file `QuanLyThuVienApp/QuanLyThuVienApp/GuiEmail.cs` và cập nhật:

```csharp
private static string taiKhoan = "your-email@gmail.com";
private static string matKhau = "your-app-password"; // App Password, không phải mật khẩu đăng nhập
```

**Hướng dẫn tạo App Password Gmail**:
1. Vào [Google Account Settings](https://myaccount.google.com/)
2. Bật **2-Step Verification**
3. Tạo **App Password** tại [App Passwords](https://myaccount.google.com/apppasswords)
4. Copy App Password và dán vào `matKhau`

### 5. Build & Chạy

1. Mở `QuanLyThuVienApp.sln` trong **Visual Studio 2022**
2. Restore NuGet packages:
   - Click phải vào Solution → **Restore NuGet Packages**
   - Hoặc: `Tools` → `NuGet Package Manager` → `Package Manager Console` → `Update-Package -reinstall`
3. Build Solution: `Ctrl + Shift + B`
4. Chạy ứng dụng: `F5`

### 6. Tạo Tài Khoản Admin Đầu Tiên

- Đăng ký tài khoản mới qua form đăng ký
- Xác thực OTP qua email
- Sau khi đăng nhập, vào **Phân quyền** để chuyển tài khoản thành Admin

## 📁 Cấu Trúc Dự Án

```
LTWIN/
├── QuanLyThuVienApp/
│   ├── QuanLyThuVienApp/
│   │   ├── Program.cs                 # Entry point
│   │   ├── GuiEmail.cs                 # Gửi email OTP
│   │   ├── EntityModel.edmx            # Entity Framework model
│   │   ├── EntityModel.Context.cs      # DataContext (QLTVEntities)
│   │   ├── frm*.cs                     # 31 form giao diện
│   │   ├── Sach.cs, NguoiDung.cs, ...  # Entity classes
│   │   ├── rp*.rdlc                    # Report definitions
│   │   ├── App.config                  # Connection string
│   │   └── Properties/                 # Resources, settings
│   └── QuanLyThuVienApp.sln           # Solution file
├── QuanLyThuVienC#.sql                # Database script
└── README.md                          # File này
```

### Các Form Chính

| Form | Chức Năng |
|------|-----------|
| `frmDangNhap` | Đăng nhập hệ thống |
| `frmDangKy` | Đăng ký tài khoản mới |
| `frmXacThuc` | Xác thực OTP qua email |
| `frmMainAdmin` | Giao diện chính Admin (MDI) |
| `frmMainUser` | Giao diện chính User (MDI) |
| `frmQuanLySach` | Quản lý sách (CRUD) |
| `frmQuanLyBanDoc` | Quản lý bạn đọc |
| `frmQuanLyPhieuMuon` | Quản lý phiếu mượn (cho mượn, trả sách) |
| `frmMuonSach` | Đăng ký mượn sách (User) |
| `frmLichSuMuon` | Lịch sử mượn sách (User) |
| `frmReport*` | Các báo cáo thống kê |
| `frmPhanQuyen` | Phân quyền Admin/User |

## 💻 Hướng Dẫn Sử Dụng

### Cho Admin

1. **Đăng nhập**: Sử dụng tài khoản Admin
2. **Quản lý sách**: 
   - Vào **Quản lý sách** → Thêm/Sửa/Xóa sách
   - Tìm kiếm sách theo nhiều tiêu chí
3. **Quản lý bạn đọc**:
   - Vào **Quản lý bạn đọc** → Tạo tài khoản mới
   - Reset mật khẩu, sửa thông tin
4. **Quản lý phiếu mượn**:
   - Xem danh sách phiếu đăng ký → **Cho mượn**
   - Xem danh sách phiếu đang mượn → **Trả sách** hoặc **Gia hạn**
5. **Báo cáo**: Xem các báo cáo thống kê và export ra PDF/Excel

### Cho Bạn Đọc (User)

1. **Đăng nhập**: Sử dụng tài khoản User
2. **Xem sách**: Xem danh sách sách có sẵn
3. **Mượn sách**: 
   - Vào **Mượn sách** → Chọn sách → Đăng ký mượn
   - Tối đa 10 cuốn sách
4. **Lịch sử mượn**: Xem lịch sử mượn sách và trạng thái
5. **Hủy phiếu**: Hủy phiếu đăng ký nếu chưa được xác nhận

## 🗄️ Cấu Trúc Database

Hệ thống sử dụng **7 bảng chính**:

1. **NguoiDung** - Người dùng/Bạn đọc (cả Admin và User)
2. **Sach** - Sách
3. **TacGia** - Tác giả
4. **TheLoai** - Thể loại
5. **NhaXuatBan** - Nhà xuất bản
6. **PhieuMuon** - Phiếu mượn
7. **ChiTietPhieuMuon** - Chi tiết phiếu mượn

**Quan hệ**:
- `NguoiDung` (1) → (n) `PhieuMuon`
- `PhieuMuon` (1) → (n) `ChiTietPhieuMuon`
- `Sach` (1) → (n) `ChiTietPhieuMuon`
- `TacGia`, `NhaXuatBan`, `TheLoai` (1) → (n) `Sach`

## 🔒 Bảo Mật

- Mật khẩu được mã hóa bằng **MD5** (lưu dạng binary)
- Xác thực **OTP qua email** (hết hạn sau 5 phút)
- Phân quyền **Admin/User** rõ ràng
- Khóa tài khoản khi vi phạm

> **Lưu ý**: Trong production, nên nâng cấp từ MD5 sang SHA-256 hoặc bcrypt để tăng cường bảo mật.

## 📝 Quy Trình Mượn/Trả Sách

```
1. User đăng ký mượn sách (Trạng thái: Đăng ký)
   ↓
2. Admin xác nhận cho mượn (Trạng thái: Đang mượn)
   ↓
3. User trả sách (Trạng thái: Đã trả)
   - Tính tiền phạt nếu quá hạn (1000 VNĐ/ngày)
```

**Quy tắc**:
- Thời hạn mượn: **14 ngày**
- Tối đa: **10 cuốn sách/bạn đọc**
- Có thể **gia hạn** thời hạn mượn

## 🐛 Xử Lý Lỗi Thường Gặp

### Lỗi kết nối database
- Kiểm tra SQL Server đã chạy chưa
- Kiểm tra connection string trong `App.config`
- Đảm bảo database `QLTV` đã được tạo

### Lỗi gửi email OTP
- Kiểm tra App Password Gmail đã đúng chưa
- Kiểm tra kết nối internet
- Kiểm tra firewall có chặn port 587 không

### Lỗi không tìm thấy DLL
- Restore NuGet packages
- Kiểm tra `packages.config`

## 📄 License

Dự án này được phát triển cho mục đích học tập và nghiên cứu.

## 👥 Đóng Góp

Mọi đóng góp đều được chào đón! Vui lòng:
1. Fork repository
2. Tạo feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to branch (`git push origin feature/AmazingFeature`)
5. Mở Pull Request

## 📞 Liên Hệ

- **Email**: thuvienhcmue@gmail.com
- **Repository**: [GitHub Link]

---

**Phát triển bởi**: Nhóm sinh viên Công nghệ thông tin  
**Thời gian**: 09/2024 – 11/2024  
**Phiên bản**: 1.0.0
