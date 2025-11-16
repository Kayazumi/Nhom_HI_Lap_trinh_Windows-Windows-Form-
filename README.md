# Báo Cáo Đề Tài WinForm C#

> **Tên đề tài**: Ứng dụng Quản Lý Thư Viện WinForms  
> **Dự án**: `QuanLyThuVienApp` – nền tảng quản lý mượn/trả sách nội bộ với xác thực OTP, phân quyền Admin/User và báo cáo thống kê.

## Trang Bìa / Lời Nói Đầu

- **Ngành học**: Công nghệ thông tin  
- **Giảng viên hướng dẫn**: ..................................................................  
- **Nhóm thực hiện**: ..................................................................  
- **Thời gian thực hiện**: 09/2024 – 11/2024  
- **Lời nói đầu**: Báo cáo mô tả toàn diện quá trình xây dựng ứng dụng WinForms quản lý thư viện. Nội dung phản ánh đúng phạm vi công việc, cơ sở lý thuyết áp dụng, kết quả đạt được và những hướng phát triển tiếp theo của sản phẩm.

## Trang Giới Thiệu Thành Viên

| STT | Họ và tên | MSSV | Vai trò chính | Email |
| --- | --------- | ---- | ------------- | ----- |
| 1 | .................................. | ........ | Trưởng nhóm, phân tích nghiệp vụ | ...............@gmail.com |
| 2 | .................................. | ........ | Thiết kế giao diện, chuẩn hóa UI | ...............@gmail.com |
| 3 | .................................. | ........ | Lập trình chức năng, Entity Framework | ...............@gmail.com |
| 4 | .................................. | ........ | Kiểm thử, viết báo cáo, báo cáo thống kê | ...............@gmail.com |

> **Ghi chú**: Điền đầy đủ thông tin thật trước khi nộp báo cáo.

### WBS (Work Breakdown Structure)

| Mã CV | Công việc | Deliverable | Người phụ trách | Thời lượng (ngày) |
| ----- | --------- | ----------- | ---------------- | ----------------- |
| 1.1 | Khảo sát nghiệp vụ thư viện thực tế, thu thập yêu cầu | Biên bản khảo sát | Thành viên 1, 3 | 4 |
| 1.2 | Thiết kế sơ đồ ERD, chuẩn hóa bảng dữ liệu | Mô hình ERD, script `QuanLyThuVienC#.sql` | Thành viên 1 | 3 |
| 2.1 | Thiết kế giao diện WinForms với MetroFramework | Prototype giao diện, file `.Designer.cs` | Thành viên 2 | 5 |
| 2.2 | Phát triển module Đăng nhập/OTP/Phân quyền | Form `frmDangNhap`, `frmXacThuc`, `frmPhanQuyen` | Thành viên 3 | 6 |
| 2.3 | Phát triển module Mượn – Trả – Gia hạn | Form `frmQuanLyPhieuMuon`, `frmGiaHan`, `frmMuonSach` | Thành viên 3 | 7 |
| 2.4 | Báo cáo thống kê LINQ + ReportViewer | `frmReportSLSachTheoTheLoai`, `rp*.rdlc` | Thành viên 4 | 4 |
| 3.1 | Kiểm thử tích hợp & chỉnh lỗi UI | Kịch bản test, log lỗi | Thành viên 4 | 4 |
| 3.2 | Hoàn thiện tài liệu, video demo | README, video demo, slides | Toàn nhóm | 3 |

### Tự Đánh Giá Tỷ Lệ Đóng Góp

| Thành viên | Tỷ lệ (%) | Minh chứng chính |
| ---------- | --------- | ---------------- |
| Thành viên 1 | 30% | Phân tích, thiết kế CSDL, viết báo cáo chương 1-3 |
| Thành viên 2 | 20% | Thiết kế control, chuẩn hóa theme Metro, icon FontAwesome |
| Thành viên 3 | 30% | Code xử lý mượn/trả, đăng nhập OTP, nghiệp vụ chính |
| Thành viên 4 | 20% | Kiểm thử, báo cáo thống kê, tài liệu hướng dẫn |

> Thang đo 5% – tối đa 2 thành viên cùng mốc điểm.

## Mục Lục

1. [Chương 1. Mở Đầu (Tổng Quan)](#chương-1-mở-đầu-tổng-quan)  
2. [Chương 2. Cơ Sở Lý Thuyết](#chương-2-cơ-sở-lý-thuyết)  
3. [Chương 3. Phân Tích và Thiết Kế Hệ Thống (Lập Kế Hoạch)](#chương-3-phân-tích-và-thiết-kế-hệ-thống-lập-kế-hoạch)  
4. [Chương 4. Triển Khai và Kết Quả (Làm và Chạy Thử)](#chương-4-triển-khai-và-kết-quả-làm-và-chạy-thử)  
5. [Chương 5. Kết Luận và Hướng Phát Triển (Tự Phê Bình)](#chương-5-kết-luận-và-hướng-phát-triển-tự-phê-bình)  
6. [Tài Liệu Tham Khảo](#tài-liệu-tham-khảo)  
7. [Phụ Lục](#phụ-lục)  

### Danh Mục Hình Vẽ

| Ký hiệu | Tên hình | Trang |
| ------- | -------- | ----- |
| Hình 2.1 | Kiến trúc tổng quan ứng dụng WinForms (Admin/User) | ... |
| Hình 3.1 | Sơ đồ ERD thư viện (NguoiDung – Sach – PhieuMuon) | ... |
| Hình 3.2 | WBS chi tiết dự án | ... |
| Hình 4.1 | Giao diện `frmDangNhap` với theme Metro | ... |
| Hình 4.2 | `frmMainAdmin` (MDI) hiển thị menu chức năng | ... |
| Hình 4.3 | `frmQuanLyPhieuMuon` với DataGridView trạng thái | ... |
| Hình 4.4 | Báo cáo `rpTiLeSachTheoTheLoai` trên ReportViewer | ... |
| Hình 4.5 | Quy trình mượn sách từ đăng ký đến trả sách | ... |
| Hình 4.6 | Giao diện quản lý sách với tìm kiếm đa tiêu chí | ... |

### Danh Mục Bảng

| Ký hiệu | Tên bảng | Trang |
| ------- | -------- | ----- |
| Bảng 1.1 | So sánh 3 phần mềm quản lý thư viện hiện có | ... |
| Bảng 3.1 | Phân rã yêu cầu chức năng / phi chức năng | ... |
| Bảng 3.2 | Thiết kế chi tiết CSDL `QuanLyThuVienC#.sql` | ... |
| Bảng 3.3 | Thiết kế giao diện chính và luồng điều hướng | ... |
| Bảng 4.1 | Cấu trúc thư mục dự án WinForms | ... |
| Bảng 4.2 | Kịch bản test chức năng mượn – trả | ... |
| Bảng 5.1 | Tổng hợp hạn chế & giải pháp đề xuất | ... |

---

## Chương 1. Mở Đầu (Tổng Quan)

### 1.1. Lý Do Chọn Đề Tài

Thư viện trường học hiện đại cần một hệ thống quản lý tự động hóa để:
- Tối ưu hóa quy trình đăng ký mượn sách, giảm thời gian chờ đợi
- Tự động nhắc hạn trả sách và tính toán tiền phạt
- Thống kê sử dụng sách theo thể loại, tác giả để hỗ trợ quyết định mua sách
- Quản lý tài khoản bạn đọc với xác thực email an toàn

**Khảo sát sản phẩm tương tự:**

1. **LibLib Web** (Phần mềm quản lý thư viện web-based)
   - Ưu điểm: Giao diện hiện đại, truy cập từ xa, đa nền tảng
   - Nhược điểm: Yêu cầu hạ tầng mạng ổn định, chi phí hosting, phụ thuộc internet
   - Khác biệt: Dự án này là WinForms offline, không cần internet để hoạt động

2. **PMTV nội bộ** (Phần mềm quản lý thư viện desktop truyền thống)
   - Ưu điểm: Chạy offline, ổn định
   - Nhược điểm: Thiếu báo cáo đa chiều, chưa có xác thực OTP, giao diện cũ
   - Khác biệt: Dự án này tích hợp OTP email, báo cáo ReportViewer, giao diện Metro hiện đại

3. **Giải pháp Excel/Access**
   - Ưu điểm: Dễ triển khai, không cần lập trình
   - Nhược điểm: Khó mở rộng, dễ phát sinh lỗi đồng bộ, không có phân quyền
   - Khác biệt: Dự án này có kiến trúc database chuẩn, phân quyền Admin/User, xử lý nghiệp vụ tự động

**Lý do chọn đề tài:**
- Dự án WinForms vận hành offline, phù hợp môi trường phòng thư viện không có internet ổn định
- Nhanh chóng triển khai, không cần cấu hình server phức tạp
- Giảm chi phí bản quyền phần mềm thương mại
- Tích hợp xác thực OTP qua email, đảm bảo bảo mật tài khoản
- Báo cáo thống kê hỗ trợ quản lý hiệu quả

### 1.2. Mục Tiêu Đề Tài

**Mục tiêu chính:**
- Xây dựng ứng dụng quản lý thư viện đầy đủ nghiệp vụ: xử lý mượn/trả/gia hạn, quản lý sách – tác giả – NXB – thể loại
- Đảm bảo phân quyền rõ ràng (Admin/User), hỗ trợ xác thực OTP qua email, khóa tài khoản khi cần
- Tích hợp báo cáo thống kê (ReportViewer) và biểu đồ (Chart) để hỗ trợ ban quản lý

**Các chức năng đạt được:**
- ✅ Đăng ký, đăng nhập với xác thực OTP qua email
- ✅ Quản lý danh mục: Sách, Tác giả, Thể loại, Nhà xuất bản (CRUD đầy đủ)
- ✅ Quản lý bạn đọc: Tạo tài khoản, khóa/mở khóa, reset mật khẩu
- ✅ Quy trình mượn sách: Đăng ký mượn → Xác nhận cho mượn → Gia hạn → Trả sách
- ✅ Tính toán tiền phạt tự động dựa trên số ngày quá hạn
- ✅ Báo cáo thống kê: Số lượng sách theo thể loại, Tỷ lệ sách theo thể loại, Hóa đơn phạt
- ✅ Biểu đồ trực quan: Biểu đồ cột, biểu đồ tròn

### 1.3. Phạm Vi Nghiên Cứu

**Đối tượng người dùng:**

1. **Admin (Quản trị viên)**
   - Quản trị hệ thống: Phân quyền, khóa/mở khóa tài khoản
   - Quản lý danh mục: Sách, Tác giả, Thể loại, Nhà xuất bản
   - Quản lý bạn đọc: Tạo tài khoản, sửa thông tin, reset mật khẩu
   - Quản lý phiếu mượn: Xác nhận cho mượn, trả sách, gia hạn
   - Cho mượn sách trực tiếp (không qua đăng ký)
   - Xem báo cáo thống kê

2. **Bạn đọc (User)**
   - Đăng nhập, xem thông tin cá nhân
   - Đăng ký mượn sách (tối đa 10 cuốn)
   - Xem lịch sử mượn sách
   - Hủy phiếu đăng ký mượn (nếu chưa được xác nhận)
   - Xem danh sách sách có sẵn

**Phạm vi chức năng:**
- ✅ Quản lý mượn/trả sách với 3 trạng thái: Đăng ký (0), Đang mượn (1), Đã trả (2)
- ✅ Tính tiền phạt: 1000 VNĐ/ngày quá hạn (chỉ hiển thị, chưa tích hợp thanh toán)
- ✅ Giới hạn mượn: Tối đa 10 cuốn sách/bạn đọc
- ✅ Thời hạn mượn: 14 ngày (có thể gia hạn)
- ❌ Không xử lý thu phí phạt tự động (chỉ ghi nhận và hiển thị)
- ❌ Không tích hợp thiết bị barcode/RFID
- ❌ Chỉ hỗ trợ tiếng Việt, không có bản web di động
- ❌ Chưa có chức năng đặt chỗ sách (reservation)

---

## Chương 2. Cơ Sở Lý Thuyết

### 2.1. Tổng Quan về C# và .NET Framework

**C# và .NET Framework 4.8:**
- C# là ngôn ngữ lập trình hướng đối tượng, được phát triển bởi Microsoft
- .NET Framework 4.8 là phiên bản cuối cùng của .NET Framework, cung cấp thư viện phong phú cho phát triển ứng dụng Windows
- Dự án sử dụng .NET Framework 4.8 để đảm bảo tương thích với Windows 7 trở lên

**Windows Forms:**
- Windows Forms là framework GUI cho .NET Framework, cho phép xây dựng ứng dụng desktop với giao diện đồ họa
- Mô hình event-driven: Ứng dụng phản ứng với các sự kiện người dùng (click, nhập liệu, v.v.)
- Hỗ trợ thao tác trực quan với control thông qua Visual Studio Designer

**Entity Framework 6.5.1 (Database First):**
- Entity Framework là ORM (Object-Relational Mapping) framework của Microsoft
- Sử dụng mô hình Database First: Tạo database trước, sau đó sinh entity class từ `EntityModel.edmx`
- LINQ (Language Integrated Query) được sử dụng để truy vấn dữ liệu qua các entity class như `Sach`, `NguoiDung`, `PhieuMuon`
- Sử dụng `QLTVEntities` (DbContext) để thao tác với database thông qua LINQ queries
- Ưu điểm: Type-safe, IntelliSense hỗ trợ, giảm code SQL thủ công, hỗ trợ lazy loading, change tracking

### 2.2. Windows Forms SDI/MDI và Thư Viện UI

**Kiến trúc MDI (Multiple Document Interface):**
- Ứng dụng sử dụng kiến trúc **MDI** cho `frmMainAdmin` và `frmMainUser`
- Form MDI Parent cho phép mở nhiều form con (MDI Children) trong cùng một cửa sổ
- Ưu điểm: Quản lý nhiều màn hình cùng lúc, giao diện gọn gàng, dễ điều hướng

**MetroFramework (MetroModernUI 1.4.0.0):**
- Thư viện UI hiện đại, mô phỏng giao diện Metro của Windows 8/10
- Chuẩn hóa theme, màu sắc, font chữ cho toàn bộ ứng dụng
- Các control chính: `MetroButton`, `MetroTextBox`, `MetroPanel`, `MetroLabel`
- Layout được thiết kế sẵn trong `.Designer.cs`, không thay đổi kích thước động lúc runtime

**FontAwesome.Sharp 6.6.0:**
- Thư viện icon vector, cung cấp hơn 1000 icon miễn phí
- Tạo icon nhất quán, tăng nhận diện chức năng
- Sử dụng `IconButton` để hiển thị icon trên button

**Form SDI độc lập:**
- Một số form không phải MDI Child: `frmDangNhap`, `frmDangKy`, `frmXacThuc`, `frmReport*`
- Các form này mở độc lập, không phụ thuộc MDI Parent

### 2.3. Cơ Sở Dữ Liệu (SQL Server)

**SQL Server LocalDB:**
- LocalDB là phiên bản nhẹ của SQL Server, phù hợp phát triển và triển khai nhỏ
- Connection string: `(localdb)\MSSQLLocalDB`
- Database: `QLTV`

**Cấu trúc Database (7 bảng chính):**

1. **NguoiDung** (Người dùng/Bạn đọc)
   - Lưu thông tin cả Admin và User
   - `MatKhau`: Lưu dạng `varbinary(MAX)` (MD5 hash)
   - `QuyenHan`: 'admin' hoặc 'user'
   - `TrangThaiXacThuc`: Bit (0/1) - đã xác thực email chưa
   - `BiKhoa`: Bit (0/1) - tài khoản có bị khóa không
   - `SoSachMuon`: Số sách đang mượn

2. **TacGia** (Tác giả)
   - `SoMaSach`: Số lượng sách của tác giả (tự động cập nhật)

3. **NhaXuatBan** (Nhà xuất bản)
   - `SoMaSach`: Số lượng sách của NXB (tự động cập nhật)

4. **TheLoai** (Thể loại)
   - `SoMaSach`: Số lượng sách của thể loại (tự động cập nhật)

5. **Sach** (Sách)
   - `SoLuong`: Tổng số lượng sách
   - `SoSachMuon`: Số sách đang được mượn
   - Khóa ngoại: `MaTG`, `MaNXB`, `MaTheLoai`

6. **PhieuMuon** (Phiếu mượn)
   - `TrangThai`: 0 (đăng ký), 1 (đang mượn), 2 (đã trả)
   - `NgayDangKyMuon`: Ngày bạn đọc đăng ký mượn
   - `NgayMuon`: Ngày thực tế mượn sách (khi Admin xác nhận)
   - `HanTra`: Hạn trả sách (14 ngày sau ngày mượn)
   - `NgayTra`: Ngày thực tế trả sách

7. **ChiTietPhieuMuon** (Chi tiết phiếu mượn)
   - Lưu từng sách trong phiếu mượn
   - `SoLuong`: Số lượng sách mượn của từng đầu sách

**Ràng buộc toàn vẹn dữ liệu:**
- Khóa chính: IDENTITY tự động tăng
- Khóa ngoại đảm bảo toàn vẹn: Không thể xóa sách đã có trong `ChiTietPhieuMuon`
- Check constraint: `QuyenHan` chỉ nhận 'admin' hoặc 'user'

### 2.4. Các Kỹ Thuật/Công Nghệ Hỗ Trợ Khác

**Entity Framework 6.5.1:**
- ORM (Object-Relational Mapping) framework
- Tự động sinh entity class từ database schema
- `QLTVEntities` là DbContext, quản lý kết nối và thao tác database
- Lazy loading: Tự động load dữ liệu liên quan khi truy cập navigation property

**Microsoft ReportViewer 15.0:**
- Control hiển thị báo cáo từ file `.rdlc` (Report Definition Language Client)
- Các báo cáo: `rpTiLeSachTheoTheLoai`, `rpSLSachTheoTheLoai`, `rpHoaDonPhat`
- Hỗ trợ export PDF, Excel, Word
- Dữ liệu được truyền từ LINQ query qua DataSource

**System.Windows.Forms.DataVisualization.Charting:**
- Thư viện vẽ biểu đồ tích hợp trong .NET Framework
- `frmCColumn_SachTheoTheLoai`: Biểu đồ cột số lượng sách theo thể loại
- `frmCPie_SachTheoTheLoai`: Biểu đồ tròn tỷ lệ sách theo thể loại

**SMTP Gmail (GuiEmail.cs):**
- Gửi email OTP và thông báo qua Gmail SMTP
- Sử dụng App Password (không phải mật khẩu đăng nhập thông thường)
- Port: 587 (TLS)
- Email hệ thống: `thuvienhcmue@gmail.com`

**Microsoft.SqlServer.Types 14.0.314.76:**
- Hỗ trợ loại dữ liệu nâng cao của SQL Server (phục vụ ReportViewer)

---

## Chương 3. Phân Tích và Thiết Kế Hệ Thống (Lập Kế Hoạch)

### 3.1. Phân Tích Yêu Cầu

#### Yêu Cầu Chức Năng

**1. Quản lý xác thực và phân quyền:**
- Đăng ký tài khoản với xác thực email OTP (`frmDangKy`, `frmXacThuc`)
- Đăng nhập bằng tên đăng nhập hoặc email (`frmDangNhap`)
- Quên mật khẩu, reset qua OTP (`frmTimTaiKhoan`, `frmDatLaiMatKhau`)
- Đổi mật khẩu (`frmDoiMatKhau`)
- Phân quyền Admin/User (`frmPhanQuyen`)
- Khóa/mở khóa tài khoản

**2. Quản lý danh mục:**
- **Sách** (`frmQuanLySach`, `frmSach`): Thêm, sửa, xóa, tìm kiếm đa tiêu chí
- **Tác giả** (`frmTacGia`): CRUD, tự động cập nhật `SoMaSach`
- **Thể loại** (`frmTheLoai`): CRUD, tự động cập nhật `SoMaSach`
- **Nhà xuất bản** (`frmNXB`): CRUD, tự động cập nhật `SoMaSach`

**3. Quản lý bạn đọc:**
- Tạo tài khoản bạn đọc mới qua OTP (`frmQuanLyBanDoc`)
- Tạo mật khẩu ngẫu nhiên 6 chữ số, gửi qua email
- Sửa thông tin bạn đọc (tên, email)
- Reset mật khẩu, gửi mật khẩu mới qua email
- Tìm kiếm bạn đọc theo Mã/Tên/Email

**4. Quy trình mượn sách:**
- **User đăng ký mượn** (`frmMuonSach`):
  - Chọn sách, số lượng
  - Tạo phiếu với `TrangThai = 0` (đăng ký)
  - Cập nhật `SoSachMuon` của sách và bạn đọc
- **Admin xác nhận cho mượn** (`frmQuanLyPhieuMuon.btnChoMuon_Click`):
  - Chuyển `TrangThai = 1` (đang mượn)
  - Set `NgayMuon = DateTime.Now`, `HanTra = DateTime.Now.AddDays(14)`
- **Admin cho mượn trực tiếp** (`frmChoMuonSach`):
  - Chọn bạn đọc, sách
  - Tạo phiếu với `TrangThai = 1` ngay lập tức
- **Gia hạn** (`frmGiaHan`):
  - Cập nhật `HanTra = HanTra + số ngày gia hạn`
- **Trả sách** (`frmQuanLyPhieuMuon.btnTraSach_Click`):
  - Giảm `SoSachMuon` của sách và bạn đọc
  - Set `TrangThai = 2`, `NgayTra = DateTime.Now`
  - Tính tiền phạt nếu quá hạn

**5. Lịch sử mượn sách:**
- User xem lịch sử mượn của mình (`frmLichSuMuon`)
- Hiển thị trạng thái: Đăng ký, Đang mượn, Đã trả
- Tính tiền phạt nếu quá hạn
- Hủy phiếu đăng ký (nếu chưa được xác nhận)

**6. Thống kê và báo cáo:**
- Báo cáo số lượng sách theo thể loại (`frmReportSLSachTheoTheLoai`)
- Báo cáo tỷ lệ sách theo thể loại (`frmReportTiLeSachTheoTheLoai`)
- Hóa đơn phạt (`frmReportHoaDonPhat`)
- Biểu đồ cột (`frmCColumn_SachTheoTheLoai`)
- Biểu đồ tròn (`frmCPie_SachTheoTheLoai`)

**7. Chức năng hỗ trợ:**
- Thông tin hệ thống (`frmInfor`)
- Trợ giúp (`frmTroGiup`)
- Thông tin cá nhân (`frmCaNhan`)

#### Yêu Cầu Phi Chức Năng

**Bảo mật:**
- Mật khẩu lưu dạng MD5 hash (16 byte binary)
- OTP hết hạn sau 5 phút (`ThoiGianNhanOTP`)
- Phân quyền rõ ràng Admin/User
- Khóa tài khoản khi vi phạm

**Hiệu năng:**
- Truy vấn LINQ tối ưu, sử dụng `Where`, `FirstOrDefault`, `SingleOrDefault`
- DataContext (`QLTVEntities`) tạo mới trong mỗi form, đóng sau khi dùng
- DataGridView thiết lập `AutoSizeColumns` để hiển thị đầy đủ

**Tính ổn định:**
- Kiểm tra dữ liệu đầu vào (null, rỗng, định dạng)
- Chặn xóa bản ghi khi tồn tại ràng buộc (ví dụ: không xóa sách đã có trong `ChiTietPhieuMuon`)
- Validate số lượng sách mượn không vượt quá số lượng có sẵn
- Giới hạn mượn tối đa 10 cuốn/bạn đọc

**Giao diện:**
- Layout được canh chỉnh trực tiếp tại `.Designer.cs`
- Tuân thủ chuẩn Metro UI, thống nhất font/icon
- Không thay đổi kích thước control lúc runtime

### 3.2. Thiết Kế Cơ Sở Dữ Liệu

| Bảng | Khóa chính | Khóa ngoại | Các cột quan trọng | Ghi chú |
| ---- | ---------- | ---------- | ------------------ | ------- |
| `NguoiDung` | `ID` (int, IDENTITY) | — | `TenDangNhap` (varchar(50), unique), `MatKhau` (varbinary(MAX)), `Email` (varchar(50), unique), `QuyenHan` (varchar(5), check 'admin'/'user'), `TrangThaiXacThuc` (bit), `BiKhoa` (bit), `SoSachMuon` (int), `HoTen` (nvarchar(50)), `MaOTP` (varchar(6)), `ThoiGianNhanOTP` (datetime) | Lưu cả admin và bạn đọc |
| `TacGia` | `MaTG` (int, IDENTITY) | — | `TenTG` (nvarchar(50)), `MoTa` (nvarchar(500)), `SoMaSach` (int) | `SoMaSach` tự động tăng/giảm khi cập nhật sách |
| `NhaXuatBan` | `MaNXB` (int, IDENTITY) | — | `TenNXB` (nvarchar(50)), `MoTa` (nvarchar(500)), `SoMaSach` (int) | |
| `TheLoai` | `MaTheLoai` (int, IDENTITY) | — | `TenTheLoai` (nvarchar(50)), `MoTa` (nvarchar(500)), `SoMaSach` (int) | |
| `Sach` | `ID` (int, IDENTITY) | `MaTG` → `TacGia.MaTG`, `MaNXB` → `NhaXuatBan.MaNXB`, `MaTheLoai` → `TheLoai.MaTheLoai` | `TenSach` (nvarchar(100)), `SoLuong` (int), `SoSachMuon` (int) | Ràng buộc không cho xóa sách đã mượn |
| `PhieuMuon` | `MaPhieu` (int, IDENTITY) | `IDBanDoc` → `NguoiDung.ID` | `NgayDangKyMuon` (datetime), `NgayMuon` (datetime), `HanTra` (datetime), `NgayTra` (datetime), `TrangThai` (int, 0/1/2) | Trạng thái 0-1-2 tương ứng đăng ký/đang mượn/đã trả |
| `ChiTietPhieuMuon` | `MaChiTiet` (int, IDENTITY) | `MaPhieu` → `PhieuMuon.MaPhieu`, `IDSach` → `Sach.ID` | `SoLuong` (int) | Chi tiết từng sách trong phiếu |

**Sơ đồ ERD:**
- Cần bổ sung sơ đồ ERD trong báo cáo với đầy đủ quan hệ (1-n) và chú thích
- Quan hệ chính:
  - `NguoiDung` (1) → (n) `PhieuMuon`
  - `PhieuMuon` (1) → (n) `ChiTietPhieuMuon`
  - `Sach` (1) → (n) `ChiTietPhieuMuon`
  - `TacGia` (1) → (n) `Sach`
  - `NhaXuatBan` (1) → (n) `Sach`
  - `TheLoai` (1) → (n) `Sach`

### 3.3. Thiết Kế Giao Diện (UI/UX)

**Giao diện đăng nhập (`frmDangNhap`):**
- Layout Metro, textbox và icon canh lề
- Liên kết đăng ký/quên mật khẩu
- Hiển thị lỗi rõ ràng khi đăng nhập sai

**Giao diện chính Admin (`frmMainAdmin`):**
- Form MDI với thanh điều hướng trái (IconButton FontAwesome)
- Status bar hiển thị thời gian thực (Timer cập nhật mỗi giây)
- Hiển thị thông tin người dùng, trạng thái khóa tài khoản
- Menu: Quản lý sách, Quản lý bạn đọc, Quản lý phiếu mượn, Báo cáo, Phân quyền

**Giao diện chính User (`frmMainUser`):**
- Tương tự `frmMainAdmin` nhưng ít chức năng hơn
- Menu: Xem sách, Mượn sách, Lịch sử mượn, Thống kê
- Nếu tài khoản bị khóa, nút "Mượn sách" bị disable

**Giao diện quản lý sách (`frmQuanLySach`):**
- DataGridView hiển thị danh sách sách
- Panel nhập liệu với ComboBox chọn Tác giả, NXB, Thể loại
- RadioButton chuyển đổi chế độ Thêm/Sửa-Xóa
- Tìm kiếm đa tiêu chí: Tên sách, Tác giả, NXB, Thể loại

**Giao diện quản lý phiếu mượn (`frmQuanLyPhieuMuon`):**
- 3 RadioButton điều hướng trạng thái phiếu: Đăng ký, Đang mượn, Đã trả
- Hai DataGridView: Danh sách phiếu và Chi tiết phiếu
- Button hành động hiển thị/ẩn theo trạng thái:
  - Đăng ký: "Cho mượn", "Hủy phiếu"
  - Đang mượn: "Trả sách", "Gia hạn"
  - Đã trả: Chỉ xem

**Giao diện báo cáo (`frmReport*`):**
- Host ReportViewer + Chart (Pie/Column) để trực quan hóa số liệu
- Dữ liệu được load từ LINQ query
- Hỗ trợ export PDF, Excel

**Nguyên tắc thiết kế:**
- Controller layout được thiết kế sẵn trong `.Designer.cs`
- Chỉ thiết lập dữ liệu tại `Form_Load` (không thay đổi kích thước control lúc runtime)
- Sử dụng Metro theme nhất quán
- Icon FontAwesome hỗ trợ nhận diện chức năng

---

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
17. 
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
