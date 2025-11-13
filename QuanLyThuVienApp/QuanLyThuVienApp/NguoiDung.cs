using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace QuanLyThuVienApp
{
    [Table(Name = "dbo.NguoiDung")]
    public partial class NguoiDung : LinqEntityBase
    {
        private int _ID;
        private string _TenDangNhap;
        private byte[] _MatKhau;
        private string _Email;
        private DateTime? _NgayDangKi;
        private string _MaOTP;
        private DateTime? _ThoiGianNhanOTP;
        private bool? _TrangThaiXacThuc;
        private string _QuyenHan;
        private string _HoTen;
        private int? _SoSachMuon;
        private bool? _BiKhoa;
        private readonly EntitySet<PhieuMuon> _PhieuMuons;

        public NguoiDung()
        {
            _PhieuMuons = new EntitySet<PhieuMuon>(attach_PhieuMuon, detach_PhieuMuon);
        }

        [Column(Storage = nameof(_ID), DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, IsPrimaryKey = true)]
        public int ID
        {
            get => _ID;
            set
            {
                if (_ID != value)
                {
                    SendPropertyChanging();
                    _ID = value;
                    SendPropertyChanged(nameof(ID));
                }
            }
        }

        [Column(Storage = nameof(_TenDangNhap), DbType = "VarChar(50)")]
        public string TenDangNhap
        {
            get => _TenDangNhap;
            set
            {
                if (_TenDangNhap != value)
                {
                    SendPropertyChanging();
                    _TenDangNhap = value;
                    SendPropertyChanged(nameof(TenDangNhap));
                }
            }
        }

        [Column(Storage = nameof(_MatKhau), DbType = "VarBinary(MAX)")]
        public byte[] MatKhau
        {
            get => _MatKhau;
            set
            {
                if (_MatKhau != value)
                {
                    SendPropertyChanging();
                    _MatKhau = value;
                    SendPropertyChanged(nameof(MatKhau));
                }
            }
        }

        [Column(Storage = nameof(_Email), DbType = "VarChar(50)")]
        public string Email
        {
            get => _Email;
            set
            {
                if (_Email != value)
                {
                    SendPropertyChanging();
                    _Email = value;
                    SendPropertyChanged(nameof(Email));
                }
            }
        }

        [Column(Storage = nameof(_NgayDangKi), DbType = "DateTime")]
        public DateTime? NgayDangKi
        {
            get => _NgayDangKi;
            set
            {
                if (_NgayDangKi != value)
                {
                    SendPropertyChanging();
                    _NgayDangKi = value;
                    SendPropertyChanged(nameof(NgayDangKi));
                }
            }
        }

        [Column(Storage = nameof(_MaOTP), DbType = "VarChar(6)")]
        public string MaOTP
        {
            get => _MaOTP;
            set
            {
                if (_MaOTP != value)
                {
                    SendPropertyChanging();
                    _MaOTP = value;
                    SendPropertyChanged(nameof(MaOTP));
                }
            }
        }

        [Column(Storage = nameof(_ThoiGianNhanOTP), DbType = "DateTime")]
        public DateTime? ThoiGianNhanOTP
        {
            get => _ThoiGianNhanOTP;
            set
            {
                if (_ThoiGianNhanOTP != value)
                {
                    SendPropertyChanging();
                    _ThoiGianNhanOTP = value;
                    SendPropertyChanged(nameof(ThoiGianNhanOTP));
                }
            }
        }

        [Column(Storage = nameof(_TrangThaiXacThuc), DbType = "Bit")]
        public bool? TrangThaiXacThuc
        {
            get => _TrangThaiXacThuc;
            set
            {
                if (_TrangThaiXacThuc != value)
                {
                    SendPropertyChanging();
                    _TrangThaiXacThuc = value;
                    SendPropertyChanged(nameof(TrangThaiXacThuc));
                }
            }
        }

        [Column(Storage = nameof(_QuyenHan), DbType = "VarChar(5)")]
        public string QuyenHan
        {
            get => _QuyenHan;
            set
            {
                if (_QuyenHan != value)
                {
                    SendPropertyChanging();
                    _QuyenHan = value;
                    SendPropertyChanged(nameof(QuyenHan));
                }
            }
        }

        [Column(Storage = nameof(_HoTen), DbType = "NVarChar(50)")]
        public string HoTen
        {
            get => _HoTen;
            set
            {
                if (_HoTen != value)
                {
                    SendPropertyChanging();
                    _HoTen = value;
                    SendPropertyChanged(nameof(HoTen));
                }
            }
        }

        [Column(Storage = nameof(_SoSachMuon), DbType = "Int")]
        public int? SoSachMuon
        {
            get => _SoSachMuon;
            set
            {
                if (_SoSachMuon != value)
                {
                    SendPropertyChanging();
                    _SoSachMuon = value;
                    SendPropertyChanged(nameof(SoSachMuon));
                }
            }
        }

        [Column(Storage = nameof(_BiKhoa), DbType = "Bit")]
        public bool? BiKhoa
        {
            get => _BiKhoa;
            set
            {
                if (_BiKhoa != value)
                {
                    SendPropertyChanging();
                    _BiKhoa = value;
                    SendPropertyChanged(nameof(BiKhoa));
                }
            }
        }

        [Association(Storage = nameof(_PhieuMuons), OtherKey = nameof(PhieuMuon.IDBanDoc), ThisKey = nameof(ID))]
        public EntitySet<PhieuMuon> PhieuMuons
        {
            get => _PhieuMuons;
            set => _PhieuMuons.Assign(value);
        }

        private void attach_PhieuMuon(PhieuMuon entity)
        {
            SendPropertyChanging();
            entity.NguoiDung = this;
            SendPropertyChanged(nameof(PhieuMuons));
        }

        private void detach_PhieuMuon(PhieuMuon entity)
        {
            SendPropertyChanging();
            entity.NguoiDung = null;
            SendPropertyChanged(nameof(PhieuMuons));
        }
    }
}
