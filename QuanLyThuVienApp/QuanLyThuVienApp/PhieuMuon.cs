using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace QuanLyThuVienApp
{
    [Table(Name = "dbo.PhieuMuon")]
    public partial class PhieuMuon : LinqEntityBase
    {
        private int _MaPhieu;
        private int? _IDBanDoc;
        private DateTime? _NgayDangKyMuon;
        private DateTime? _NgayMuon;
        private DateTime? _HanTra;
        private DateTime? _NgayTra;
        private int? _TrangThai;

        private readonly EntitySet<ChiTietPhieuMuon> _ChiTietPhieuMuons;
        private EntityRef<NguoiDung> _NguoiDung;

        public PhieuMuon()
        {
            _ChiTietPhieuMuons = new EntitySet<ChiTietPhieuMuon>(attach_ChiTietPhieuMuon, detach_ChiTietPhieuMuon);
        }

        [Column(Storage = nameof(_MaPhieu), DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, IsPrimaryKey = true)]
        public int MaPhieu
        {
            get => _MaPhieu;
            set
            {
                if (_MaPhieu != value)
                {
                    SendPropertyChanging();
                    _MaPhieu = value;
                    SendPropertyChanged(nameof(MaPhieu));
                }
            }
        }

        [Column(Storage = nameof(_IDBanDoc), DbType = "Int")]
        public int? IDBanDoc
        {
            get => _IDBanDoc;
            set
            {
                if (_IDBanDoc != value)
                {
                    if (_NguoiDung.HasLoadedOrAssignedValue)
                    {
                        throw new ForeignKeyReferenceAlreadyHasValueException();
                    }

                    SendPropertyChanging();
                    _IDBanDoc = value;
                    SendPropertyChanged(nameof(IDBanDoc));
                }
            }
        }

        [Column(Storage = nameof(_NgayDangKyMuon), DbType = "DateTime")]
        public DateTime? NgayDangKyMuon
        {
            get => _NgayDangKyMuon;
            set
            {
                if (_NgayDangKyMuon != value)
                {
                    SendPropertyChanging();
                    _NgayDangKyMuon = value;
                    SendPropertyChanged(nameof(NgayDangKyMuon));
                }
            }
        }

        [Column(Storage = nameof(_NgayMuon), DbType = "DateTime")]
        public DateTime? NgayMuon
        {
            get => _NgayMuon;
            set
            {
                if (_NgayMuon != value)
                {
                    SendPropertyChanging();
                    _NgayMuon = value;
                    SendPropertyChanged(nameof(NgayMuon));
                }
            }
        }

        [Column(Storage = nameof(_HanTra), DbType = "DateTime")]
        public DateTime? HanTra
        {
            get => _HanTra;
            set
            {
                if (_HanTra != value)
                {
                    SendPropertyChanging();
                    _HanTra = value;
                    SendPropertyChanged(nameof(HanTra));
                }
            }
        }

        [Column(Storage = nameof(_NgayTra), DbType = "DateTime")]
        public DateTime? NgayTra
        {
            get => _NgayTra;
            set
            {
                if (_NgayTra != value)
                {
                    SendPropertyChanging();
                    _NgayTra = value;
                    SendPropertyChanged(nameof(NgayTra));
                }
            }
        }

        [Column(Storage = nameof(_TrangThai), DbType = "Int")]
        public int? TrangThai
        {
            get => _TrangThai;
            set
            {
                if (_TrangThai != value)
                {
                    SendPropertyChanging();
                    _TrangThai = value;
                    SendPropertyChanged(nameof(TrangThai));
                }
            }
        }

        [Association(Storage = nameof(_ChiTietPhieuMuons), OtherKey = nameof(ChiTietPhieuMuon.MaPhieu), ThisKey = nameof(MaPhieu))]
        public EntitySet<ChiTietPhieuMuon> ChiTietPhieuMuons
        {
            get => _ChiTietPhieuMuons;
            set => _ChiTietPhieuMuons.Assign(value);
        }

        [Association(Storage = nameof(_NguoiDung), OtherKey = nameof(global::QuanLyThuVienApp.NguoiDung.ID), ThisKey = nameof(IDBanDoc), IsForeignKey = true)]
        public NguoiDung NguoiDung
        {
            get => _NguoiDung.Entity;
            set
            {
                var previousValue = _NguoiDung.Entity;

                if ((previousValue != value) || !_NguoiDung.HasLoadedOrAssignedValue)
                {
                    SendPropertyChanging();
                    if (previousValue != null)
                    {
                        _NguoiDung.Entity = null;
                        previousValue.PhieuMuons.Remove(this);
                    }

                    _NguoiDung.Entity = value;
                    if (value != null)
                    {
                        value.PhieuMuons.Add(this);
                        _IDBanDoc = value.ID;
                    }
                    else
                    {
                        _IDBanDoc = null;
                    }

                    SendPropertyChanged(nameof(NguoiDung));
                }
            }
        }

        private void attach_ChiTietPhieuMuon(ChiTietPhieuMuon entity)
        {
            SendPropertyChanging();
            entity.PhieuMuon = this;
            SendPropertyChanged(nameof(ChiTietPhieuMuons));
        }

        private void detach_ChiTietPhieuMuon(ChiTietPhieuMuon entity)
        {
            SendPropertyChanging();
            entity.PhieuMuon = null;
            SendPropertyChanged(nameof(ChiTietPhieuMuons));
        }
    }
}
