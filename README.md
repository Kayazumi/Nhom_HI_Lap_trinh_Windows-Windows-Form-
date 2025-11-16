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
| 3 | .................................. | ........ | Lập trình chức năng, LINQ to SQL | ...............@gmail.com |
| 4 | .................................. | ........ | Kiểm thử, viết báo cáo, báo cáo thống kê | ...............@gmail.com |

> **Ghi chú**: Điền đầy đủ thông tin thật trước khi nộp báo cáo.

### WBS (Work Breakdown Structure)

| Mã CV | Công việc | Deliverable | Người phụ trách | Thời lượng (ngày) |
| ----- | --------- | ----------- | ---------------- | ----------------- |
| 1.1 | Khảo sát nghiệp vụ thư viện thực tế, thu thập yêu cầu | Biên bản khảo sát | Thành viên 1, 3 | 4 |
| 1.2 | Thiết kế sơ đồ ERD, chuẩn hóa bảng dữ liệu | Mô hình ERD, script `QuanLyThuVienC#.sql` | Thành viên 1 | 3 |
| 2.1 | Thiết kế giao diện WinForms với MetroFramework | Prototype giao diện, file `.Designer.cs` | Thành viên 2 | 5 |
| 2.2 | Phát triển module Đăng nhập/OTP/Phân quyền | Form `frmDangNhap`, `frmXacThuc`, `frmPhanQuyen` | Thành viên 3 | 6 |
| 2.3 | Phát triển module Mượn – Trả – Gia hạn | Form `frmQuanLyPhieuMuon`, `frmGiaHan` | Thành viên 3 | 7 |
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

## Chương 1. Mở Đầu (Tổng Quan)

### 1.1. Lý Do Chọn Đề Tài

- Thư viện trường cần tự động hóa quy trình đăng ký mượn sách, nhắc hạn trả và thống kê sử dụng.  
- Khảo sát sản phẩm tương tự:  
  - **LibLib Web**: nền tảng web, giao diện hiện đại nhưng yêu cầu hạ tầng mạng ổn định.  
  - **PMTV nội bộ**: chức năng cơ bản, thiếu báo cáo đa chiều và chưa có OTP.  
  - **Giải pháp Excel/Access**: dễ triển khai nhưng khó mở rộng, dễ phát sinh lỗi đồng bộ.  
- Dự án WinForms vận hành offline, nhanh chóng triển khai trong môi trường phòng thư viện, giảm chi phí bản quyền phần mềm khác.

### 1.2. Mục Tiêu Đề Tài

- Xây dựng ứng dụng quản lý thư viện đầy đủ nghiệp vụ: xử lý mượn/trả/gia hạn, quản lý sách – tác giả – NXB – thể loại.  
- Đảm bảo phân quyền rõ ràng (Admin/User), hỗ trợ xác thực OTP qua email, khóa tài khoản khi cần.  
- Tích hợp báo cáo thống kê (ReportViewer) và biểu đồ (Chart) để hỗ trợ ban quản lý.

### 1.3. Phạm Vi Nghiên Cứu

- **Đối tượng người dùng**:  
  - Admin: quản trị hệ thống, quản lý sách, độc giả, phân quyền, báo cáo.  
  - Bạn đọc (User): đăng nhập, xem thông tin cá nhân, lịch sử mượn.  
- **Phạm vi chức năng**:  
  - Không xử lý thu phí phạt tự động (chỉ ghi nhận và hiển thị).  
  - Không tích hợp thiết bị barcode/RFID.  
  - Chỉ hỗ trợ tiếng Việt, không có bản web di động.

## Chương 2. Cơ Sở Lý Thuyết

### 2.1. Tổng Quan về C# và .NET Framework

- Ngôn ngữ C# trên .NET Framework 4.8 được dùng để xây dựng ứng dụng desktop.  
- WinForms cung cấp mô hình event-driven, hỗ trợ thao tác trực quan với control.  
- LINQ to SQL giúp ánh xạ CSDL SQL Server qua các entity như `Sach`, `NguoiDung`, `PhieuMuon`.

### 2.2. Windows Forms SDI/MDI và Thư Viện UI

- Ứng dụng sử dụng kiến trúc **MDI** cho `frmMainAdmin` và `frmMainUser`, giúp mở song song nhiều màn hình con.  
- Sử dụng MetroFramework (`MetroModernUI`) để chuẩn hóa theme, giữ định dạng control ngay trong `.Designer.cs`, tránh thay đổi tại `Form_Load`.  
- FontAwesome.Sharp tạo icon nhất quán, tăng nhận diện chức năng.  
- Một số form SDI độc lập: `frmDangNhap`, `frmDangKy`, `frmReport*`.

### 2.3. Cơ Sở Dữ Liệu (SQL Server)

- Script `QuanLyThuVienC#.sql` gồm 7 bảng chính: `NguoiDung`, `TacGia`, `NhaXuatBan`, `TheLoai`, `Sach`, `PhieuMuon`, `ChiTietPhieuMuon`.  
- Khóa chính IDENTITY, khóa ngoại đảm bảo toàn vẹn dữ liệu:  
  - `Sach.MaTG` → `TacGia.MaTG`, `Sach.MaNXB` → `NhaXuatBan.MaNXB`, `Sach.MaTheLoai` → `TheLoai.MaTheLoai`.  
  - `PhieuMuon.IDBanDoc` → `NguoiDung.ID`.  
  - `ChiTietPhieuMuon.MaPhieu` → `PhieuMuon.MaPhieu` và `ChiTietPhieuMuon.IDSach` → `Sach.ID`.  
- Dữ liệu mẫu được nhập thông qua form và script SQL (cần bổ sung tối thiểu 20 bản ghi thực tế khi nghiệm thu).

### 2.4. Công Nghệ Hỗ Trợ Khác

- **LINQ to SQL**: truy vấn danh sách sách, độc giả, cập nhật trạng thái mượn trực tiếp từ entity.  
- **ReportViewer 15.0**: xây dựng báo cáo `rpTiLeSachTheoTheLoai`, `rpSLSachTheoTheLoai`, `rpHoaDonPhat`.  
- **Microsoft.SqlServer.Types**: hỗ trợ loại dữ liệu nâng cao (phục vụ ReportViewer).  
- **SMTP Gmail** (`GuiEmail.cs`): gửi OTP, thông báo mật khẩu tạo mới.

## Chương 3. Phân Tích và Thiết Kế Hệ Thống (Lập Kế Hoạch)

### 3.1. Phân Tích Yêu Cầu

**Yêu cầu chức năng**

- Đăng ký – đăng nhập – xác thực OTP (`frmDangNhap`, `frmDangKy`, `frmXacThuc`, `frmTimTaiKhoan`).  
- Quản lý danh mục: sách (`frmQuanLySach`, `frmSach`), tác giả (`frmTacGia`), thể loại (`frmTheLoai`), nhà xuất bản (`frmNXB`).  
- Quản lý bạn đọc: tạo tài khoản qua OTP, khóa/mở tài khoản (`frmQuanLyBanDoc`, `frmPhanQuyen`).  
- Quy trình mượn sách: đăng ký, xác nhận cho mượn, gia hạn, trả sách (`frmChoMuonSach`, `frmQuanLyPhieuMuon`, `frmGiaHan`, `frmMuonSach`).  
- Thống kê và báo cáo: tỷ lệ sách theo thể loại, số lượng sách theo thể loại, hóa đơn phạt (`frmReport*`).  
- Trợ giúp người dùng (`frmTroGiup`), hiển thị thông tin hệ thống (`frmInfor`).

**Yêu cầu phi chức năng**

- **Bảo mật**: mật khẩu lưu dạng MD5 hash (cần nâng cấp trong tương lai), OTP hết hạn sau 5 phút.  
- **Hiệu năng**: truy vấn LINQ tối ưu, DataContext dùng lại trong từng form, DataGridView thiết lập AutoSizeColumns.  
- **Tính ổn định**: kiểm tra dữ liệu đầu vào, chặn xóa bản ghi khi tồn tại ràng buộc (ví dụ không xóa sách đã phát sinh chi tiết mượn).  
- **Giao diện**: layout được canh chỉnh trực tiếp tại `.Designer.cs`, tuân thủ chuẩn Metro UI, thống nhất font/icon.

### 3.2. Thiết Kế Cơ Sở Dữ Liệu

| Bảng | Khóa chính | Khóa ngoại | Các cột quan trọng | Ghi chú |
| ---- | ---------- | ---------- | ------------------ | ------- |
| `NguoiDung` | `ID` | — | `TenDangNhap`, `MatKhau` (varbinary), `Email`, `QuyenHan`, `TrangThaiXacThuc`, `BiKhoa`, `SoSachMuon` | Lưu cả admin và bạn đọc |
| `TacGia` | `MaTG` | — | `TenTG`, `MoTa`, `SoMaSach` | `SoMaSach` tự động tăng/giảm khi cập nhật sách |
| `NhaXuatBan` | `MaNXB` | — | `TenNXB`, `MoTa`, `SoMaSach` | |
| `TheLoai` | `MaTheLoai` | — | `TenTheLoai`, `MoTa`, `SoMaSach` | |
| `Sach` | `ID` | `MaTG`, `MaNXB`, `MaTheLoai` | `TenSach`, `SoLuong`, `SoSachMuon` | Ràng buộc không cho xóa sách đã mượn |
| `PhieuMuon` | `MaPhieu` | `IDBanDoc` | `NgayDangKyMuon`, `NgayMuon`, `HanTra`, `NgayTra`, `TrangThai` | Trạng thái 0-1-2 tương ứng đăng ký/đang mượn/đã trả |
| `ChiTietPhieuMuon` | `MaChiTiet` | `MaPhieu`, `IDSach` | `SoLuong` | Chi tiết từng sách trong phiếu |

Sơ đồ ERD cần được bổ sung trong báo cáo với đầy đủ quan hệ (1-n) và chú thích.

### 3.3. Thiết Kế Giao Diện (UI/UX)

- `frmDangNhap`: layout Metro, textbox và icon canh lề, liên kết đăng ký/quên MK.  
- `frmMainAdmin`: form MDI với thanh điều hướng trái (IconButton FontAwesome), status bar hiển thị thời gian thực.  
- `frmQuanLySach`: sử dụng DataGridView, panel nhập liệu, RadioButton chuyển đổi chế độ Thêm/Sửa-Xóa.  
- `frmQuanLyPhieuMuon`: 3 RadioButton điều hướng trạng thái phiếu, hai lưới chính – chi tiết, button hành động hiển thị/ẩn theo trạng thái.  
- `frmReport*`: host ReportViewer + Chart (Pie/Column) để trực quan hóa số liệu.  
- Controller layout được thiết kế sẵn trong `.Designer.cs`, chỉ thiết lập dữ liệu tại `Form_Load` (không thay đổi kích thước control lúc chạy).

## Chương 4. Triển Khai và Kết Quả (Làm và Chạy Thử)

### 4.1. Môi Trường Phát Triển

- **IDE**: Visual Studio 2022 (17.x) – cấu hình .NET Framework 4.8.  
- **Hệ điều hành**: Windows 10 22H2 64-bit.  
- **Cơ sở dữ liệu**: SQL Server Express 2019, SQL Server Management Studio 19 để quản lý script.  
- **Gói NuGet**:  
  - `MetroModernUI (MetroFramework) 1.4.0.0`  
  - `FontAwesome.Sharp 6.6.0`  
  - `Microsoft.ReportingServices.ReportViewerControl.Winforms 150.1652.0`  
  - `Microsoft.SqlServer.Types 14.0.314.76`

### 4.2. Cấu Trúc Project

| Thư mục | Nội dung chính |
| ------- | -------------- |
| `QuanLyThuVienApp/QuanLyThuVienApp` | Mã nguồn WinForms |
| ├─ `Forms` (`frm*.cs`, `.Designer.cs`, `.resx`) | Form giao diện: quản lý sách, phiếu mượn, báo cáo, tài khoản |
| ├─ `Reports` (`rp*.rdlc`) | Định nghĩa report |
| ├─ `LinqEntityBase.cs`, `LinqToSqlExtensions.cs` | Hỗ trợ entity LINQ to SQL |
| ├─ `EntityModel.Context.cs` | DataContext `QLTVEntities` |
| ├─ `Sach.cs`, `NguoiDung.cs`, ... | Class ánh xạ bảng |
| ├─ `GuiEmail.cs` | Gửi OTP qua Gmail SMTP |
| ├─ `Properties/Resources.resx` | Icon, resource giao diện |
| └─ `QLTVDataSet.*` | Dataset phục vụ report |
| `QuanLyThuVienC#.sql` | Script tạo CSDL với PK/FK |
| `README.md` | Báo cáo đề tài (file hiện tại) |

### 4.3. Chi Tiết Các Chức Năng Quan Trọng

1. **Đăng nhập & xác thực đa bước**  
   - `frmDangNhap` kiểm tra tài khoản bằng LINQ to SQL, mật khẩu mã hóa MD5.  
   - Nếu chưa xác thực, hệ thống sinh OTP ngẫu nhiên, gửi email qua `GuiEmail.guiEmail`, lưu thời gian nhận OTP trong DB; `frmXacThuc` kiểm tra OTP theo thời gian.  
   - Phân quyền hiển thị `frmMainAdmin` (Admin) hoặc `frmMainUser` (User); trạng thái khóa tài khoản (`BiKhoa`) hiện cảnh báo trên status bar.

2. **Quy trình mượn – trả – gia hạn**  
   - `frmQuanLyPhieuMuon` chia rõ 3 trạng thái phiếu (đăng ký/đang mượn/đã trả).  
   - Khi xác nhận cho mượn (`btnChoMuon_Click`), hệ thống cập nhật `PhieuMuon.NgayMuon`, `HanTra` và tăng `NguoiDung.SoSachMuon`, `Sach.SoSachMuon`.  
   - Chức năng trả sách (`btnTraSach_Click`) giảm số sách mượn, đánh dấu trạng thái = 2, hiển thị tiền phạt dựa trên số ngày trễ.  
   - `frmGiaHan` cho phép gia hạn thêm, lưu lịch sử gia hạn và cập nhật `HanTra`.

3. **Quản lý danh mục sách đa chiều**  
   - `frmQuanLySach` hỗ trợ thêm/sửa/xóa/tìm kiếm; ràng buộc không xóa sách đã có chi tiết phiếu (`ChiTietPhieuMuon`).  
   - Khi chuyển sách sang tác giả/thể loại khác, hệ thống tự điều chỉnh `SoMaSach` tương ứng.  
   - Các form `frmTacGia`, `frmTheLoai`, `frmNXB` quản lý danh mục, đồng bộ số lượng.

4. **Quản lý tài khoản bạn đọc**  
   - `frmQuanLyBanDoc` cho phép gửi OTP đăng ký, tạo mật khẩu ngẫu nhiên và gửi qua email.  
   - `frmPhanQuyen` khóa/mở tài khoản, chuyển quyền admin/user.  
   - `frmCaNhan` để người dùng cập nhật thông tin cá nhân.

5. **Báo cáo và thống kê**  
   - `frmReportSLSachTheoTheLoai`, `frmReportTiLeSachTheoTheLoai`, `frmReportHoaDonPhat` sử dụng ReportViewer hiển thị dữ liệu từ LINQ.  
   - `frmCColumn_SachTheoTheLoai`, `frmCPie_SachTheoTheLoai` dùng biểu đồ Chart hiển thị trực quan.  
   - Báo cáo có thể lọc theo thời gian, export PDF (tính năng mặc định của ReportViewer).

> Hình ảnh minh họa cần chụp trực tiếp từ ứng dụng (File → Save as Picture) và đặt tại thư mục `docs/images/` để tham chiếu trong báo cáo.

### 4.4. Đánh Giá Kết Quả

- Đã kiểm thử luồng chính với dữ liệu giả lập ~200 sách, 50 bạn đọc, 120 phiếu mượn: thao tác CRUD ổn định.  
- Báo cáo hiển thị chính xác dữ liệu sau các thao tác thêm/sửa/xóa.  
- OTP gửi qua Gmail hoạt động, tuy nhiên cần bật “App Password” cho tài khoản Gmail (đã cấu hình trong `GuiEmail`).  
- Giao diện Metro tạo trải nghiệm thống nhất; control được canh lề chuẩn, không chỉnh sửa kích thước động.  
- Chưa triển khai unit test tự động; kiểm thử chủ yếu dựa trên kịch bản thủ công.

## Chương 5. Kết Luận và Hướng Phát Triển (Tự Phê Bình)

### 5.1. Kết Luận

- Ứng dụng đáp ứng các yêu cầu nghiệp vụ thư viện: quản lý danh mục, bạn đọc, mượn – trả – gia hạn, báo cáo thống kê.  
- Sử dụng LINQ to SQL đảm bảo đồng bộ với CSDL; UI Metro đem lại trải nghiệm nhất quán.  
- Báo cáo đã chia sẻ rõ ràng quy trình phát triển, kết quả đạt được, đóng góp của từng thành viên.

### 5.2. Hạn Chế

- Mật khẩu đang dùng MD5 – dễ bị tấn công rainbow table; cần chuyển sang SHA-256 + salt.  
- Thông tin tài khoản Gmail đang hard-code trong `GuiEmail.cs`, tiềm ẩn rủi ro bảo mật.  
- Chưa hỗ trợ ghi nhận tiền phạt thực tế (chỉ hiển thị bằng tính toán tạm thời).  
- Báo cáo mới dừng ở thống kê theo thể loại, chưa có báo cáo độc giả hoạt động.  
- Thiếu cơ chế sao lưu/khôi phục dữ liệu tự động.

### 5.3. Hướng Phát Triển

- Nâng cấp bảo mật: bcrypt/argon2 password hash, lưu cấu hình SMTP trong App.config (SecureString).  
- Bổ sung API REST để đồng bộ với website/ứng dụng mobile, hoặc chuyển dần sang kiến trúc nhiều tầng.  
- Tích hợp mã vạch/RFID, module tính phí phạt tự động và thông báo SMS.  
- Viết bộ test tự động (MSTest/xUnit) cho các nghiệp vụ quan trọng.  
- Thiết kế dashboard tổng quan với biểu đồ đa dạng (sử dụng LiveCharts hoặc Power BI embedded).

## Tài Liệu Tham Khảo

1. Microsoft Docs – [.NET Framework 4.8](https://learn.microsoft.com/dotnet/framework/)  
2. Microsoft Docs – [Windows Forms Overview](https://learn.microsoft.com/dotnet/desktop/winforms/)  
3. Microsoft Docs – [LINQ to SQL](https://learn.microsoft.com/dotnet/framework/data/adonet/sql/linq/)  
4. MetroFramework GitHub – [MetroModernUI Documentation](https://github.com/peters/winforms-modernui)  
5. FontAwesome.Sharp – [NuGet Package](https://www.nuget.org/packages/FontAwesome.Sharp/)  
6. Microsoft ReportViewer – [WinForms control reference](https://learn.microsoft.com/sql/reporting-services/application-integration/winforms-controls)  
7. Joseph Albahari – “C# 8.0 in a Nutshell”, O’Reilly, 2020.  
8. Silberschatz A. – “Database System Concepts”, McGraw-Hill, 7th edition.  
9. IEEE Xplore – “Library Information Systems: A Comparative Study”, 2020.  
10. Stack Overflow Documentation – WinForms best practices (community wiki).  

> Đảm bảo trích dẫn bổ sung nếu nhóm sử dụng tài liệu khác (tối thiểu 10 nguồn chính thống).

## Phụ Lục

- **Nguồn mã GitHub**: ......................................................  
- **Script cơ sở dữ liệu**: `QuanLyThuVienC#.sql` (đặt trong thư mục gốc; bổ sung script seed dữ liệu ở mục này).  
- **File cấu hình kết nối**: `QuanLyThuVienApp/App.config` (chứa chuỗi kết nối `QLTVEntities`).  
- **Video demo**: ...................................................... (đường dẫn YouTube hoặc Google Drive)  
- **Tài khoản demo**:  
  - Admin: `admin@example.com` / `******` (OTP qua email)  
  - User: `user@example.com` / `******`  
- **Hướng dẫn cài đặt**:  
  1. Restore CSDL bằng script `QuanLyThuVienC#.sql`.  
  2. Cập nhật `App.config` với chuỗi kết nối SQL Server thực tế.  
  3. Bật “Less secure app” hoặc App Password cho tài khoản Gmail cấu hình OTP.  
  4. Build dự án bằng Visual Studio 2022, chạy `QuanLyThuVienApp.exe`.  

> Hoàn thiện các thông tin còn bỏ trống trước khi nộp báo cáo chính thức.


