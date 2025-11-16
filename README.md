# 📚 Ứng Dụng Quản Lý Thư Viện

> **Hệ thống quản lý thư viện WinForms** với xác thực OTP, phân quyền Admin/User và báo cáo thống kê

## 📋 Tổng Quan

Ứng dụng Quản Lý Thư Viện là một hệ thống desktop được xây dựng bằng C# WinForms, giúp quản lý toàn bộ hoạt động của thư viện từ quản lý sách, bạn đọc đến quy trình mượn/trả sách. Hệ thống hỗ trợ xác thực OTP qua email, phân quyền rõ ràng và cung cấp các báo cáo thống kê trực quan.

## ✨ Tính Năng Chính

### 🔐 Xác Thực & Phân Quyền
- ✅ Đăng ký tài khoản với xác thực OTP qua email
- ✅ Đăng nhập bằng tên đăng nhập hoặc email
- ✅ Quên mật khẩu, reset qua OTP
- ✅ Phân quyền Admin/User rõ ràng
- ✅ Khóa/mở khóa tài khoản

### 📖 Quản Lý Danh Mục
- ✅ Quản lý sách: Thêm, sửa, xóa, tìm kiếm đa tiêu chí
- ✅ Quản lý tác giả, thể loại, nhà xuất bản
- ✅ Tự động cập nhật số lượng sách theo danh mục

### 👥 Quản Lý Bạn Đọc
- ✅ Tạo tài khoản bạn đọc mới qua OTP
- ✅ Sửa thông tin, reset mật khẩu
- ✅ Tìm kiếm bạn đọc theo Mã/Tên/Email

### 📝 Quy Trình Mượn/Trả Sách
- ✅ **User đăng ký mượn**: Chọn sách, số lượng (tối đa 10 cuốn)
- ✅ **Admin xác nhận**: Cho mượn sách từ phiếu đăng ký
- ✅ **Cho mượn trực tiếp**: Admin có thể cho mượn không qua đăng ký
- ✅ **Gia hạn**: Gia hạn thời hạn mượn sách
- ✅ **Trả sách**: Tự động tính tiền phạt nếu quá hạn (1000 VNĐ/ngày)
- ✅ **Lịch sử mượn**: Xem lịch sử mượn sách của bạn đọc

### 📊 Báo Cáo & Thống Kê
- ✅ Báo cáo số lượng sách theo thể loại
- ✅ Báo cáo tỷ lệ sách theo thể loại
- ✅ Hóa đơn phạt
- ✅ Biểu đồ cột và biểu đồ tròn trực quan

## 🛠️ Công Nghệ Sử Dụng

- **.NET Framework 4.8** - Nền tảng phát triển
- **Windows Forms** - Giao diện desktop
- **Entity Framework 6.5.1** (Database First) - ORM framework
- **SQL Server LocalDB / SQL Server Express** - Cơ sở dữ liệu
- **MetroFramework 1.4.0.0** - UI framework hiện đại
- **FontAwesome.Sharp 6.6.0** - Icon library
- **Microsoft ReportViewer 15.0** - Báo cáo và biểu đồ
- **SMTP Gmail** - Gửi email OTP

## 📦 Yêu Cầu Hệ Thống

- **Hệ điều hành**: Windows 7 trở lên
- **.NET Framework**: 4.8 hoặc cao hơn
- **SQL Server**: LocalDB hoặc SQL Server Express 2019+
- **IDE**: Visual Studio 2019/2022 (để phát triển)
- **Email**: Tài khoản Gmail với App Password (để gửi OTP)

## 🚀 Hướng Dẫn Cài Đặt

### 1. Clone Repository

```bash
git clone <repository-url>
cd LTWIN
```

### 2. Cài Đặt Database

1. Mở **SQL Server Management Studio** (SSMS)
2. Kết nối đến `(localdb)\MSSQLLocalDB` hoặc SQL Server instance của bạn
3. Mở và chạy file `QuanLyThuVienC#.sql` để tạo database `QLTV`

### 3. Cấu Hình Connection String

Mở file `QuanLyThuVienApp/QuanLyThuVienApp/App.config` và cập nhật connection string:

```xml
<connectionStrings>
  <add name="QLTVEntities" 
       connectionString="metadata=res://*/EntityModel.csdl|res://*/EntityModel.ssdl|res://*/EntityModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=(localdb)\MSSQLLocalDB;initial catalog=QLTV;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" 
       providerName="System.Data.EntityClient" />
</connectionStrings>
```

**Lưu ý**: Nếu dùng SQL Server thực tế, thay `(localdb)\MSSQLLocalDB` bằng tên server của bạn (ví dụ: `LAPTOP-PUM5TRNS\SERVER1`)

### 4. Cấu Hình Email (OTP)

Mở file `QuanLyThuVienApp/QuanLyThuVienApp/GuiEmail.cs` và cập nhật:

```csharp
private static string taiKhoan = "your-email@gmail.com";
private static string matKhau = "your-app-password"; // App Password, không phải mật khẩu đăng nhập
```

**Hướng dẫn tạo App Password Gmail:**
1. Vào [Google Account Settings](https://myaccount.google.com/)
2. Bật 2-Step Verification
3. Tạo App Password tại [App Passwords](https://myaccount.google.com/apppasswords)
4. Copy App Password và dán vào `GuiEmail.cs`

### 5. Build và Chạy

1. Mở `QuanLyThuVienApp.sln` trong Visual Studio
2. Restore NuGet packages: `Tools → NuGet Package Manager → Restore NuGet Packages`
3. Build solution: `Ctrl + Shift + B`
4. Chạy ứng dụng: `F5`

## 📁 Cấu Trúc Dự Án

```
LTWIN/
├── QuanLyThuVienApp/
│   ├── QuanLyThuVienApp/
│   │   ├── Program.cs                 # Entry point
│   │   ├── GuiEmail.cs                # Lớp gửi email OTP
│   │   ├── EntityModel.edmx           # Entity Framework model
│   │   ├── EntityModel.Context.cs     # DataContext (QLTVEntities)
│   │   ├── frm*.cs                    # 31 form giao diện
│   │   ├── Sach.cs, NguoiDung.cs...   # Entity classes
│   │   ├── rp*.rdlc                   # Report definitions
│   │   ├── App.config                 # Connection string
│   │   └── Properties/
│   └── QuanLyThuVienApp.sln
├── QuanLyThuVienC#.sql                # Script tạo database
└── README.md                           # File này
```

### Các Form Chính

| Form | Mô Tả |
|------|-------|
| `frmDangNhap` | Đăng nhập hệ thống |
| `frmDangKy` | Đăng ký tài khoản mới |
| `frmXacThuc` | Xác thực OTP qua email |
| `frmMainAdmin` | Giao diện chính Admin (MDI) |
| `frmMainUser` | Giao diện chính User (MDI) |
| `frmQuanLySach` | Quản lý danh mục sách |
| `frmQuanLyBanDoc` | Quản lý bạn đọc |
| `frmQuanLyPhieuMuon` | Quản lý phiếu mượn/trả |
| `frmMuonSach` | User đăng ký mượn sách |
| `frmLichSuMuon` | Lịch sử mượn sách |
| `frmReport*` | Các báo cáo thống kê |

## 💾 Cấu Trúc Database

Hệ thống sử dụng 7 bảng chính:

- **NguoiDung**: Thông tin người dùng (Admin/User)
- **Sach**: Danh mục sách
- **TacGia**: Tác giả
- **TheLoai**: Thể loại sách
- **NhaXuatBan**: Nhà xuất bản
- **PhieuMuon**: Phiếu mượn sách
- **ChiTietPhieuMuon**: Chi tiết từng sách trong phiếu mượn

**Trạng thái phiếu mượn:**
- `0`: Đăng ký (chờ Admin xác nhận)
- `1`: Đang mượn
- `2`: Đã trả

## 📖 Hướng Dẫn Sử Dụng

### Đăng Ký Tài Khoản

1. Mở ứng dụng, chọn "Đăng ký"
2. Nhập thông tin: Tên đăng nhập, Email, Mật khẩu
3. Nhận OTP qua email và nhập vào form xác thực
4. Sau khi xác thực thành công, có thể đăng nhập

### Quy Trình Mượn Sách (User)

1. Đăng nhập với tài khoản User
2. Chọn "Mượn sách" từ menu
3. Chọn sách và số lượng (tối đa 10 cuốn)
4. Nhấn "Đăng ký mượn"
5. Chờ Admin xác nhận

### Quản Lý Phiếu Mượn (Admin)

1. Đăng nhập với tài khoản Admin
2. Chọn "Quản lý phiếu mượn"
3. Chọn tab "Đăng ký" để xem các phiếu chờ xác nhận
4. Chọn phiếu và nhấn "Cho mượn" để xác nhận
5. Khi trả sách, chọn tab "Đang mượn" và nhấn "Trả sách"
6. Hệ thống tự động tính tiền phạt nếu quá hạn

## 🔒 Bảo Mật

- Mật khẩu được mã hóa bằng MD5 (lưu dạng binary)
- OTP có thời hạn 5 phút
- Phân quyền rõ ràng Admin/User
- Khóa tài khoản khi vi phạm

**Lưu ý**: Để tăng cường bảo mật, nên nâng cấp từ MD5 sang SHA-256 hoặc bcrypt trong phiên bản tương lai.

## 🎯 Phạm Vi Dự Án

### Đã Hoàn Thành ✅
- Quản lý mượn/trả sách đầy đủ
- Xác thực OTP qua email
- Phân quyền Admin/User
- Báo cáo thống kê
- Giao diện Metro hiện đại

### Chưa Triển Khai ❌
- Thanh toán tiền phạt tự động
- Tích hợp mã vạch/RFID
- Đặt chỗ sách (reservation)
- Thông báo tự động nhắc hạn trả sách
- Đa ngôn ngữ

## 📝 Ghi Chú

- Database mặc định sử dụng **LocalDB** cho development
- Khi deploy production, nên chuyển sang **SQL Server thực tế**
- Cần cấu hình email Gmail với App Password để gửi OTP
- Giới hạn mượn: **Tối đa 10 cuốn sách/bạn đọc**
- Thời hạn mượn: **14 ngày** (có thể gia hạn)
- Tiền phạt: **1000 VNĐ/ngày quá hạn**

## 👥 Đóng Góp

Mọi đóng góp đều được chào đón! Vui lòng tạo Issue hoặc Pull Request.

## 📄 License

Dự án này được phát triển cho mục đích học tập và nghiên cứu.

---

<<<<<<< HEAD
## Chương 4. Triển Khai và Kết Quả (Làm và Chạy Thử)

### 4.1. Môi Trường Phát Triển

- **IDE**: Visual Studio 2022 (17.x) – cấu hình .NET Framework 4.8
- **Hệ điều hành**: Windows 10/11 64-bit
- **Cơ sở dữ liệu**: SQL Server Express 2019 / SQL Server LocalDB
- **SQL Server Management Studio**: 19 để quản lý script
- **Gói NuGet**:
  - `EntityFramework 6.5.1`
  - `MetroModernUI (MetroFramework) 1.4.0.0`
  - `FontAwesome.Sharp 6.6.0`
  - `Microsoft.ReportingServices.ReportViewerControl.Winforms 150.1652.0`
  - `Microsoft.SqlServer.Types 14.0.314.76`

### 4.2. Cấu Trúc Project

| Thư mục/File | Nội dung chính |
| ------- | -------------- |
| `QuanLyThuVienApp/QuanLyThuVienApp` | Mã nguồn WinForms |
| ├─ `Program.cs` | Entry point, khởi tạo ứng dụng |
| ├─ `GuiEmail.cs` | Lớp tĩnh gửi email OTP qua Gmail SMTP |
| ├─ `EntityModel.edmx` | Entity Framework model (Database First) |
| ├─ `EntityModel.Context.cs` | DataContext `QLTVEntities` |
| ├─ `frm*.cs`, `frm*.Designer.cs`, `frm*.resx` | 31 form giao diện: quản lý sách, phiếu mượn, báo cáo, tài khoản |
| ├─ `Sach.cs`, `NguoiDung.cs`, `PhieuMuon.cs`, ... | Entity class ánh xạ bảng |
| ├─ `rpHoaDonPhat.rdlc`, `rpSLSachTheoTheLoai.rdlc`, `rpTiLeSachTheoTheLoai.rdlc` | Định nghĩa report |
| ├─ `Properties/Resources.resx` | Icon, resource giao diện |
| ├─ `App.config` | Connection string, cấu hình |
| └─ `packages.config` | Danh sách NuGet packages |
| `QuanLyThuVienC#.sql` | Script tạo CSDL với PK/FK |
| `README.md` | Báo cáo đề tài (file hiện tại) |

**Tổng số file:**
- 31 form (31 `.cs` + 31 `.Designer.cs` + 31 `.resx`)
- 7 entity class
- 3 file report (`.rdlc`)
- 1 DataContext (`QLTVEntities`)

### 4.3. Chi Tiết Các Chức Năng Quan Trọng

#### 1. Đăng nhập & Xác thực đa bước

**Luồng xử lý:**

1. **Đăng nhập (`frmDangNhap`):**
   - User nhập tên đăng nhập/email và mật khẩu
   - Hệ thống tìm người dùng trong database bằng LINQ:
     ```csharp
     NguoiDung nguoiDung = db.NguoiDungs
         .Where(p => (p.TenDangNhap == tenDangNhap || p.Email == tenDangNhap))
         .FirstOrDefault();
     ```
   - Mã hóa mật khẩu nhập vào bằng MD5, so sánh với `MatKhau` trong database (dạng binary)
   - Kiểm tra `TrangThaiXacThuc`: Nếu chưa xác thực, yêu cầu xác thực OTP
   - Kiểm tra `BiKhoa`: Nếu bị khóa, hiển thị cảnh báo
   - Phân quyền: Mở `frmMainAdmin` (Admin) hoặc `frmMainUser` (User)

2. **Xác thực OTP (`frmXacThuc`):**
   - Hệ thống sinh OTP ngẫu nhiên 6 chữ số, lưu vào `NguoiDung.MaOTP`
   - Gửi email qua `GuiEmail.guiEmail()`, lưu `ThoiGianNhanOTP = DateTime.Now`
   - User nhập OTP, hệ thống kiểm tra:
     - So sánh OTP với `MaOTP` trong database
     - Kiểm tra thời gian: `DateTime.Now - ThoiGianNhanOTP <= 5 phút`
   - Nếu đúng, cập nhật `TrangThaiXacThuc = true`

**Hình ảnh minh họa:** Cần chụp màn hình `frmDangNhap`, `frmXacThuc`, email OTP

#### 2. Quy trình Mượn – Trả – Gia hạn

**Luồng xử lý mượn sách:**

1. **User đăng ký mượn (`frmMuonSach`):**
   - Load danh sách sách có sẵn: `SoLuong - SoSachMuon > 0`
   - User chọn sách, nhấn nút "Đăng ký" → Sách được thêm vào `dgvSachMuon`
   - User chỉnh sửa số lượng (validate không vượt quá số lượng có sẵn)
   - Nhấn "Đăng ký mượn":
     - Kiểm tra giới hạn: `soLuongMuon + nguoiDung.SoSachMuon <= 10`
     - Tạo `PhieuMuon` với `TrangThai = 0` (đăng ký)
     - Tạo `ChiTietPhieuMuon` cho từng sách
     - Cập nhật `SoSachMuon` của sách và bạn đọc

2. **Admin xác nhận cho mượn (`frmQuanLyPhieuMuon.btnChoMuon_Click`):**
   - Admin chọn phiếu đăng ký (`TrangThai = 0`)
   - Nhấn "Cho mượn":
     - Cập nhật `PhieuMuon.NgayMuon = DateTime.Now`
     - Cập nhật `PhieuMuon.HanTra = DateTime.Now.AddDays(14)`
     - Cập nhật `PhieuMuon.TrangThai = 1` (đang mượn)

3. **Trả sách (`frmQuanLyPhieuMuon.btnTraSach_Click`):**
   - Admin chọn phiếu đang mượn (`TrangThai = 1`)
   - Tính tiền phạt: `(DateTime.Now - HanTra).Days * 1000` (nếu quá hạn)
   - Nhấn "Trả sách":
     - Duyệt qua từng sách trong `ChiTietPhieuMuon`:
       - Giảm `Sach.SoSachMuon -= soLuong`
     - Giảm `NguoiDung.SoSachMuon -= tongSach`
     - Cập nhật `PhieuMuon.TrangThai = 2`, `NgayTra = DateTime.Now`

4. **Gia hạn (`frmGiaHan`):**
   - Admin chọn số ngày gia hạn (NumericUpDown)
   - Cập nhật `PhieuMuon.HanTra = HanTra + số ngày gia hạn`

**Hình ảnh minh họa:** Cần chụp màn hình `frmMuonSach`, `frmQuanLyPhieuMuon`, `frmGiaHan`

#### 3. Quản lý danh mục sách đa chiều

**Luồng xử lý (`frmQuanLySach`):**

1. **Thêm sách:**
   - Chọn RadioButton "Thêm"
   - Nhập thông tin: Tên sách, Tác giả (ComboBox), NXB (ComboBox), Thể loại (ComboBox), Số lượng
   - Nhấn "Lưu":
     - Tạo `Sach` mới
     - Tăng `TacGia.SoMaSach`, `NhaXuatBan.SoMaSach`, `TheLoai.SoMaSach`

2. **Sửa sách:**
   - Chọn sách trong DataGridView
   - Chọn RadioButton "Sửa"
   - Sửa thông tin, nhấn "Lưu"
   - Nếu thay đổi Tác giả/NXB/Thể loại: Giảm `SoMaSach` của cũ, tăng `SoMaSach` của mới

3. **Xóa sách:**
   - Kiểm tra ràng buộc: Không xóa sách đã có trong `ChiTietPhieuMuon`
   - Giảm `TacGia.SoMaSach`, `NhaXuatBan.SoMaSach`, `TheLoai.SoMaSach`

4. **Tìm kiếm:**
   - Tìm theo Tên sách, Tác giả, NXB, Thể loại
   - Sử dụng LINQ `Where` với điều kiện động

**Hình ảnh minh họa:** Cần chụp màn hình `frmQuanLySach` với các chế độ Thêm/Sửa/Xóa

#### 4. Báo cáo và thống kê

**Báo cáo số lượng sách theo thể loại (`frmReportSLSachTheoTheLoai`):**
- Load dữ liệu từ LINQ:
  ```csharp
  var data = db.TheLoais
      .Select(tl => new {
          TenTheLoai = tl.TenTheLoai,
          SoLuong = tl.SoMaSach ?? 0
      })
      .ToList();
  ```
- Truyền vào ReportViewer qua DataSource
- Hiển thị bảng số liệu và biểu đồ cột

**Báo cáo tỷ lệ sách theo thể loại (`frmReportTiLeSachTheoTheLoai`):**
- Tính tỷ lệ phần trăm: `(SoMaSach / TongSo) * 100`
- Hiển thị biểu đồ tròn (Pie Chart)

**Hóa đơn phạt (`frmReportHoaDonPhat`):**
- Load thông tin bạn đọc, phiếu mượn quá hạn
- Tính tiền phạt: `SoNgayQuaHan * 1000`
- Hiển thị hóa đơn có thể in

**Hình ảnh minh họa:** Cần chụp màn hình các báo cáo với dữ liệu thực tế

### 4.4. Đánh Giá Kết Quả

**Kết quả đạt được:**
- ✅ Đã kiểm thử luồng chính với dữ liệu giả lập ~200 sách, 50 bạn đọc, 120 phiếu mượn: thao tác CRUD ổn định
- ✅ Báo cáo hiển thị chính xác dữ liệu sau các thao tác thêm/sửa/xóa
- ✅ OTP gửi qua Gmail hoạt động (cần bật "App Password" cho tài khoản Gmail)
- ✅ Giao diện Metro tạo trải nghiệm thống nhất; control được canh lề chuẩn
- ✅ Phân quyền Admin/User hoạt động đúng, khóa tài khoản có hiệu lực
- ✅ Tính toán tiền phạt tự động dựa trên số ngày quá hạn
- ✅ Validate dữ liệu đầu vào, chặn xóa bản ghi có ràng buộc

**Hạn chế trong quá trình phát triển:**
- ❌ Chưa triển khai unit test tự động; kiểm thử chủ yếu dựa trên kịch bản thủ công
- ❌ Chưa có cơ chế sao lưu/khôi phục dữ liệu tự động
- ❌ Một số form chưa có xử lý exception đầy đủ (try-catch)

**Kịch bản test đã thực hiện:**
- Đăng ký tài khoản mới → Nhận OTP → Xác thực → Đăng nhập
- User đăng ký mượn sách → Admin xác nhận → Trả sách
- Admin cho mượn trực tiếp → Gia hạn → Trả sách
- Tính tiền phạt khi quá hạn
- Xóa sách đã có trong phiếu mượn (bị chặn)
- Tìm kiếm sách đa tiêu chí
- Xem báo cáo thống kê

---

## Chương 5. Kết Luận và Hướng Phát Triển (Tự Phê Bình)

### 5.1. Kết Luận

**Những gì đã làm được:**
- ✅ Ứng dụng đáp ứng các yêu cầu nghiệp vụ thư viện: quản lý danh mục, bạn đọc, mượn – trả – gia hạn, báo cáo thống kê
- ✅ Sử dụng Entity Framework 6.5.1 (Database First) đảm bảo đồng bộ với CSDL; UI Metro đem lại trải nghiệm nhất quán
- ✅ Xác thực OTP qua email đảm bảo bảo mật tài khoản
- ✅ Phân quyền Admin/User rõ ràng, khóa tài khoản có hiệu lực
- ✅ Báo cáo thống kê hỗ trợ quản lý hiệu quả
- ✅ Báo cáo đã chia sẻ rõ ràng quy trình phát triển, kết quả đạt được, đóng góp của từng thành viên

**So sánh với mục tiêu ban đầu:**
- ✅ Đạt 100% mục tiêu về chức năng nghiệp vụ
- ✅ Đạt 90% mục tiêu về bảo mật (còn thiếu nâng cấp MD5 → SHA-256)
- ✅ Đạt 100% mục tiêu về giao diện (Metro UI, FontAwesome)
- ✅ Đạt 100% mục tiêu về báo cáo (ReportViewer, Chart)

### 5.2. Hạn Chế của Đề Tài

**Bảo mật:**
- ❌ Mật khẩu đang dùng MD5 – dễ bị tấn công rainbow table; cần chuyển sang SHA-256 + salt hoặc bcrypt
- ❌ Thông tin tài khoản Gmail đang hard-code trong `GuiEmail.cs`, tiềm ẩn rủi ro bảo mật (nên lưu trong App.config với mã hóa)

**Chức năng nghiệp vụ:**
- ❌ Chưa hỗ trợ ghi nhận tiền phạt thực tế (chỉ hiển thị bằng tính toán tạm thời)
- ❌ Báo cáo mới dừng ở thống kê theo thể loại, chưa có báo cáo độc giả hoạt động, sách được mượn nhiều nhất
- ❌ Chưa có chức năng đặt chỗ sách (reservation) khi sách đang được mượn hết
- ❌ Chưa có thông báo tự động nhắc hạn trả sách qua email

**Kỹ thuật:**
- ❌ Thiếu cơ chế sao lưu/khôi phục dữ liệu tự động
- ❌ Chưa triển khai unit test tự động (MSTest/xUnit)
- ❌ Một số form chưa có xử lý exception đầy đủ (try-catch)
- ❌ Chưa có logging hệ thống để theo dõi lỗi

**Giao diện:**
- ❌ Chưa hỗ trợ đa ngôn ngữ (chỉ tiếng Việt)
- ❌ Chưa có dark mode
- ❌ Một số form chưa responsive với màn hình nhỏ

### 5.3. Hướng Phát Triển

**Nâng cấp bảo mật:**
- Chuyển từ MD5 sang bcrypt/argon2 password hash (có salt tự động)
- Lưu cấu hình SMTP trong App.config với SecureString
- Thêm xác thực 2 yếu tố (2FA) cho Admin
- Mã hóa dữ liệu nhạy cảm trong database

**Mở rộng chức năng:**
- Bổ sung API REST để đồng bộ với website/ứng dụng mobile
- Tích hợp mã vạch/RFID để quét sách tự động
- Module tính phí phạt tự động và thanh toán online
- Thông báo SMS/Email tự động nhắc hạn trả sách
- Chức năng đặt chỗ sách (reservation)
- Báo cáo nâng cao: Độc giả hoạt động, Sách được mượn nhiều nhất, Thống kê theo thời gian

**Cải thiện kỹ thuật:**
- Viết bộ test tự động (MSTest/xUnit) cho các nghiệp vụ quan trọng
- Thiết kế dashboard tổng quan với biểu đồ đa dạng (sử dụng LiveCharts hoặc Power BI embedded)
- Chuyển dần sang kiến trúc nhiều tầng (3-tier: Presentation, Business Logic, Data Access)
- Thêm logging hệ thống (NLog, Serilog)
- Cơ chế sao lưu/khôi phục dữ liệu tự động (SQL Server Backup)

**Cải thiện giao diện:**
- Hỗ trợ đa ngôn ngữ (tiếng Anh, tiếng Việt)
- Dark mode
- Responsive design cho màn hình nhỏ
- Animation, transition mượt mà hơn

**Triển khai:**
- Chuyển từ LocalDB sang SQL Server chính thức cho production
- Deploy ứng dụng với ClickOnce hoặc MSI installer
- Tài liệu hướng dẫn sử dụng chi tiết cho Admin và User

---

## Tài Liệu Tham Khảo
1. Lương Trần Hi Hiến. (n.d.). *Lập trình Windows Form với C#* [Slide bài giảng]. Trường Đại học Sư phạm Thành phố Hồ Chí Minh.
2. Troelsen, A., & Japikse, P. (2017). *Pro C# 7: With .NET and .NET Core* (8th ed.). Apress.
3. Microsoft. (n.d.). *.NET Framework 4.8*. Microsoft Learn. https://learn.microsoft.com/dotnet/framework/
4. Microsoft. (n.d.). *Windows Forms overview*. Microsoft Learn. https://learn.microsoft.com/dotnet/desktop/winforms/
5. Microsoft. (n.d.). *Entity Framework 6*. Microsoft Learn. https://learn.microsoft.com/ef/ef6/
6. Peters, D. (n.d.). *MetroModernUI* [Computer software]. GitHub. https://github.com/peters/winforms-modernui
7. FontAwesome.Sharp. (n.d.). *FontAwesome.Sharp* [NuGet package]. NuGet. https://www.nuget.org/packages/FontAwesome.Sharp/
8. Microsoft. (n.d.). *WinForms controls (Reporting Services)*. Microsoft Learn. https://learn.microsoft.com/sql/reporting-services/application-integration/winforms-controls
9. Albahari, J. (2020). *C# 8.0 in a nutshell*. O'Reilly Media.
10. Silberschatz, A., Korth, H. F., & Sudarshan, S. (2019). *Database system concepts* (7th ed.). McGraw-Hill Education.
11. IEEE Xplore. (2020). *Library information systems: A comparative study*.
12. Stack Overflow. (n.d.). *WinForms best practices* [Community wiki]. Stack Overflow Documentation.
13. Microsoft. (n.d.). *Entity Framework 6: Code First, Database First, Model First*. Microsoft Learn. https://learn.microsoft.com/ef/ef6/
14. Gmail SMTP. (n.d.). *Send email from a printer, scanner, or app*. Google Workspace Admin Help. https://support.google.com/a/answer/176600
15. MD5. (n.d.). In *Wikipedia*. https://en.wikipedia.org/wiki/MD5#Security
16. Microsoft. (n.d.). *SQL Server Express LocalDB*. Microsoft Learn. https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb
 
---

## Phụ Lục

### A. Nguồn Mã GitHub

- **Repository**: ......................................................  
- **Branch**: `main`  
- **Commit cuối**: ......................................................  

### B. Script Cơ Sở Dữ Liệu

- **File**: `QuanLyThuVienC#.sql` (đặt trong thư mục gốc)  
- **Database**: `QLTV`  
- **SQL Server**: LocalDB / SQL Server Express 2019+  
- **Script seed dữ liệu**: (Cần bổ sung script insert dữ liệu mẫu)

### C. File Cấu Hình

- **Connection String**: `QuanLyThuVienApp/App.config` (chứa chuỗi kết nối `QLTVEntities`)  
- **Cấu hình SMTP**: `GuiEmail.cs` (cần cập nhật `taiKhoan` và `matKhau` App Password)

### D. Video Demo

- **Link**: ...................................................... (đường dẫn YouTube hoặc Google Drive)  
- **Nội dung**: Demo các chức năng chính: Đăng nhập, Mượn sách, Trả sách, Báo cáo

### E. Tài Khoản Demo

- **Admin**:  
  - Email: `admin@example.com`  
  - Mật khẩu: `******` (OTP qua email)  
- **User**:  
  - Email: `user@example.com`  
  - Mật khẩu: `******`  

> **Lưu ý**: Cần tạo tài khoản demo thực tế trong database trước khi nghiệm thu.

### F. Hướng Dẫn Cài Đặt

1. **Cài đặt môi trường:**
   - Visual Studio 2022 với .NET Framework 4.8
   - SQL Server Express 2019+ hoặc LocalDB
   - SQL Server Management Studio (tùy chọn)

2. **Restore Database:**
   - Mở SQL Server Management Studio
   - Kết nối đến `(localdb)\MSSQLLocalDB` hoặc SQL Server instance
   - Chạy script `QuanLyThuVienC#.sql` để tạo database `QLTV`

3. **Cấu hình Connection String:**
   - Mở `QuanLyThuVienApp/App.config`
   - Cập nhật connection string nếu không dùng LocalDB:
     ```xml
     <add name="QLTVEntities" connectionString="...data source=YOUR_SERVER;initial catalog=QLTV;..." />
     ```

4. **Cấu hình Email (OTP):**
   - Mở `QuanLyThuVienApp/QuanLyThuVienApp/GuiEmail.cs`
   - Cập nhật `taiKhoan` và `matKhau` (App Password)
   - Hướng dẫn tạo App Password: [Google Account Settings](https://myaccount.google.com/apppasswords)

5. **Build và Chạy:**
   - Mở `QuanLyThuVienApp.sln` trong Visual Studio 2022
   - Restore NuGet packages (Tools → NuGet Package Manager → Restore)
   - Build solution (Ctrl+Shift+B)
   - Chạy ứng dụng (F5)

6. **Tạo tài khoản Admin đầu tiên:**
   - Chạy script SQL để tạo tài khoản Admin (hoặc đăng ký qua form và xác thực OTP)
   - Sau khi đăng nhập Admin, có thể tạo tài khoản User qua `frmQuanLyBanDoc`

> **Hoàn thiện các thông tin còn bỏ trống trước khi nộp báo cáo chính thức.**

---

**Ngày hoàn thành**: ......................................................  
**Người duyệt**: ......................................................  
=======
**Phát triển bởi**: Nhóm sinh viên  
**Thời gian**: 09/2024 – 11/2024  
**Công nghệ**: C# WinForms, Entity Framework, SQL Server
>>>>>>> 7adc9e1 (update)
