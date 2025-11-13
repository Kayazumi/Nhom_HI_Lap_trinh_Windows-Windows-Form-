using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace QuanLyThuVienApp
{
    [Table(Name = "dbo.ChiTietPhieuMuon")]
    public partial class ChiTietPhieuMuon : LinqEntityBase
    {
        private int _MaChiTiet;
        private int? _MaPhieu;
        private int? _IDSach;
        private int? _SoLuong;

        private EntityRef<Sach> _Sach;
        private EntityRef<PhieuMuon> _PhieuMuon;

        [Column(Storage = nameof(_MaChiTiet), DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, IsPrimaryKey = true)]
        public int MaChiTiet
        {
            get => _MaChiTiet;
            set
            {
                if (_MaChiTiet != value)
                {
                    SendPropertyChanging();
                    _MaChiTiet = value;
                    SendPropertyChanged(nameof(MaChiTiet));
                }
            }
        }

        [Column(Storage = nameof(_MaPhieu), DbType = "Int")]
        public int? MaPhieu
        {
            get => _MaPhieu;
            set
            {
                if (_MaPhieu != value)
                {
                    if (_PhieuMuon.HasLoadedOrAssignedValue)
                    {
                        throw new ForeignKeyReferenceAlreadyHasValueException();
                    }

                    SendPropertyChanging();
                    _MaPhieu = value;
                    SendPropertyChanged(nameof(MaPhieu));
                }
            }
        }

        [Column(Storage = nameof(_IDSach), DbType = "Int")]
        public int? IDSach
        {
            get => _IDSach;
            set
            {
                if (_IDSach != value)
                {
                    if (_Sach.HasLoadedOrAssignedValue)
                    {
                        throw new ForeignKeyReferenceAlreadyHasValueException();
                    }

                    SendPropertyChanging();
                    _IDSach = value;
                    SendPropertyChanged(nameof(IDSach));
                }
            }
        }

        [Column(Storage = nameof(_SoLuong), DbType = "Int")]
        public int? SoLuong
        {
            get => _SoLuong;
            set
            {
                if (_SoLuong != value)
                {
                    SendPropertyChanging();
                    _SoLuong = value;
                    SendPropertyChanged(nameof(SoLuong));
                }
            }
        }

        [Association(Storage = nameof(_Sach), OtherKey = nameof(global::QuanLyThuVienApp.Sach.ID), ThisKey = nameof(IDSach), IsForeignKey = true)]
        public Sach Sach
        {
            get => _Sach.Entity;
            set
            {
                var previousValue = _Sach.Entity;

                if ((previousValue != value) || !_Sach.HasLoadedOrAssignedValue)
                {
                    SendPropertyChanging();
                    if (previousValue != null)
                    {
                        _Sach.Entity = null;
                        previousValue.ChiTietPhieuMuons.Remove(this);
                    }

                    _Sach.Entity = value;
                    if (value != null)
                    {
                        value.ChiTietPhieuMuons.Add(this);
                        _IDSach = value.ID;
                    }
                    else
                    {
                        _IDSach = null;
                    }

                    SendPropertyChanged(nameof(Sach));
                }
            }
        }

        [Association(Storage = nameof(_PhieuMuon), OtherKey = nameof(global::QuanLyThuVienApp.PhieuMuon.MaPhieu), ThisKey = nameof(MaPhieu), IsForeignKey = true)]
        public PhieuMuon PhieuMuon
        {
            get => _PhieuMuon.Entity;
            set
            {
                var previousValue = _PhieuMuon.Entity;

                if ((previousValue != value) || !_PhieuMuon.HasLoadedOrAssignedValue)
                {
                    SendPropertyChanging();
                    if (previousValue != null)
                    {
                        _PhieuMuon.Entity = null;
                        previousValue.ChiTietPhieuMuons.Remove(this);
                    }

                    _PhieuMuon.Entity = value;
                    if (value != null)
                    {
                        value.ChiTietPhieuMuons.Add(this);
                        _MaPhieu = value.MaPhieu;
                    }
                    else
                    {
                        _MaPhieu = null;
                    }

                    SendPropertyChanged(nameof(PhieuMuon));
                }
            }
        }
    }
}
