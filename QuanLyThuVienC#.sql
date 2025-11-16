create database QLTV
go 
use QLTV

create table NguoiDung(
	ID int primary key identity(1,1),
	TenDangNhap varchar(50) unique,
	MatKhau varbinary(MAX),
	Email varchar(50) unique,
	NgayDangKi datetime,
	MaOTP varchar(6),
	ThoiGianNhanOTP datetime,
	TrangThaiXacThuc bit default 0,
	QuyenHan varchar(5) default 'user' check(QuyenHan = 'admin' or QuyenHan = 'user'),
	HoTen nvarchar(50),
	SoSachMuon int default 0,
	BiKhoa bit default 0 
)

create table TacGia(
	MaTG int primary key identity(1,1),
	TenTG nvarchar(50),
	MoTa nvarchar(500),
	SoMaSach int
)

create table NhaXuatBan(
	MaNXB int primary key identity(1,1),
	TenNXB nvarchar(50),
	MoTa nvarchar(500),
	SoMaSach int
)

create table TheLoai(
	MaTheLoai int primary key identity(1,1),
	TenTheLoai nvarchar(50),
	MoTa nvarchar(500),
	SoMaSach int
)

create table Sach(
	ID int primary key identity(1,1),
	TenSach nvarchar(100),
	MaTG int foreign key (MaTG) references TacGia(MaTG),
	MaNXB int foreign key (MaNXB) references NhaXuatBan(MaNXB),
	MaTheLoai int foreign key (MaTheLoai) references TheLoai(MaTheLoai),
	SoLuong int default 1,
	SoSachMuon int default 0
)

create table PhieuMuon(
	MaPhieu int primary key identity(1,1),
	IDBanDoc int foreign key (IDBanDoc) references NguoiDung(ID),
	NgayDangKyMuon datetime,
	NgayMuon datetime,
	HanTra datetime,
	NgayTra datetime,
	TrangThai int default 0
)

--0: đăng ký mượn
--1: đang mượn/quá hạn
--2: đã trả

create table ChiTietPhieuMuon(
	MaChiTiet int primary key identity(1,1),
	MaPhieu int foreign key (MaPhieu) references PhieuMuon(MaPhieu),
	IDSach int foreign key (IDSach) references Sach(ID),
	SoLuong int
)


select * from TheLoai
select * from NhaXuatBan
select * from TacGia

select * from Sach
select * from NguoiDung
select * from PhieuMuon
select * from ChiTietPhieuMuon

