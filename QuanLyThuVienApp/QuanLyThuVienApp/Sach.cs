using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace QuanLyThuVienApp
{
    [Table(Name = "dbo.Sach")]
    public partial class Sach : LinqEntityBase
    {
        private int _ID;
        private string _TenSach;
        private int? _MaTG;
        private int? _MaNXB;
        private int? _MaTheLoai;
        private int? _SoLuong;
        private int? _SoSachMuon;

        private readonly EntitySet<ChiTietPhieuMuon> _ChiTietPhieuMuons;
        private EntityRef<NhaXuatBan> _NhaXuatBan;
        private EntityRef<TacGia> _TacGia;
        private EntityRef<TheLoai> _TheLoai;

        public Sach()
        {
            _ChiTietPhieuMuons = new EntitySet<ChiTietPhieuMuon>(attach_ChiTietPhieuMuon, detach_ChiTietPhieuMuon);
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

        [Column(Storage = nameof(_TenSach), DbType = "NVarChar(100)")]
        public string TenSach
        {
            get => _TenSach;
            set
            {
                if (_TenSach != value)
                {
                    SendPropertyChanging();
                    _TenSach = value;
                    SendPropertyChanged(nameof(TenSach));
                }
            }
        }

        [Column(Storage = nameof(_MaTG), DbType = "Int")]
        public int? MaTG
        {
            get => _MaTG;
            set
            {
                if (_MaTG != value)
                {
                    if (_TacGia.HasLoadedOrAssignedValue)
                    {
                        throw new ForeignKeyReferenceAlreadyHasValueException();
                    }

                    SendPropertyChanging();
                    _MaTG = value;
                    SendPropertyChanged(nameof(MaTG));
                }
            }
        }

        [Column(Storage = nameof(_MaNXB), DbType = "Int")]
        public int? MaNXB
        {
            get => _MaNXB;
            set
            {
                if (_MaNXB != value)
                {
                    if (_NhaXuatBan.HasLoadedOrAssignedValue)
                    {
                        throw new ForeignKeyReferenceAlreadyHasValueException();
                    }

                    SendPropertyChanging();
                    _MaNXB = value;
                    SendPropertyChanged(nameof(MaNXB));
                }
            }
        }

        [Column(Storage = nameof(_MaTheLoai), DbType = "Int")]
        public int? MaTheLoai
        {
            get => _MaTheLoai;
            set
            {
                if (_MaTheLoai != value)
                {
                    if (_TheLoai.HasLoadedOrAssignedValue)
                    {
                        throw new ForeignKeyReferenceAlreadyHasValueException();
                    }

                    SendPropertyChanging();
                    _MaTheLoai = value;
                    SendPropertyChanged(nameof(MaTheLoai));
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

        [Association(Storage = nameof(_ChiTietPhieuMuons), OtherKey = nameof(ChiTietPhieuMuon.IDSach), ThisKey = nameof(ID))]
        public EntitySet<ChiTietPhieuMuon> ChiTietPhieuMuons
        {
            get => _ChiTietPhieuMuons;
            set => _ChiTietPhieuMuons.Assign(value);
        }

        [Association(Storage = nameof(_NhaXuatBan), OtherKey = nameof(global::QuanLyThuVienApp.NhaXuatBan.MaNXB), ThisKey = nameof(MaNXB), IsForeignKey = true)]
        public NhaXuatBan NhaXuatBan
        {
            get => _NhaXuatBan.Entity;
            set
            {
                var previousValue = _NhaXuatBan.Entity;

                if ((previousValue != value) || !_NhaXuatBan.HasLoadedOrAssignedValue)
                {
                    SendPropertyChanging();
                    if (previousValue != null)
                    {
                        _NhaXuatBan.Entity = null;
                        previousValue.Saches.Remove(this);
                    }

                    _NhaXuatBan.Entity = value;
                    if (value != null)
                    {
                        value.Saches.Add(this);
                        _MaNXB = value.MaNXB;
                    }
                    else
                    {
                        _MaNXB = null;
                    }

                    SendPropertyChanged(nameof(NhaXuatBan));
                }
            }
        }

        [Association(Storage = nameof(_TacGia), OtherKey = nameof(global::QuanLyThuVienApp.TacGia.MaTG), ThisKey = nameof(MaTG), IsForeignKey = true)]
        public TacGia TacGia
        {
            get => _TacGia.Entity;
            set
            {
                var previousValue = _TacGia.Entity;

                if ((previousValue != value) || !_TacGia.HasLoadedOrAssignedValue)
                {
                    SendPropertyChanging();
                    if (previousValue != null)
                    {
                        _TacGia.Entity = null;
                        previousValue.Saches.Remove(this);
                    }

                    _TacGia.Entity = value;
                    if (value != null)
                    {
                        value.Saches.Add(this);
                        _MaTG = value.MaTG;
                    }
                    else
                    {
                        _MaTG = null;
                    }

                    SendPropertyChanged(nameof(TacGia));
                }
            }
        }

        [Association(Storage = nameof(_TheLoai), OtherKey = nameof(global::QuanLyThuVienApp.TheLoai.MaTheLoai), ThisKey = nameof(MaTheLoai), IsForeignKey = true)]
        public TheLoai TheLoai
        {
            get => _TheLoai.Entity;
            set
            {
                var previousValue = _TheLoai.Entity;

                if ((previousValue != value) || !_TheLoai.HasLoadedOrAssignedValue)
                {
                    SendPropertyChanging();
                    if (previousValue != null)
                    {
                        _TheLoai.Entity = null;
                        previousValue.Saches.Remove(this);
                    }

                    _TheLoai.Entity = value;
                    if (value != null)
                    {
                        value.Saches.Add(this);
                        _MaTheLoai = value.MaTheLoai;
                    }
                    else
                    {
                        _MaTheLoai = null;
                    }

                    SendPropertyChanged(nameof(TheLoai));
                }
            }
        }

        private void attach_ChiTietPhieuMuon(ChiTietPhieuMuon entity)
        {
            SendPropertyChanging();
            entity.Sach = this;
            SendPropertyChanged(nameof(ChiTietPhieuMuons));
        }

        private void detach_ChiTietPhieuMuon(ChiTietPhieuMuon entity)
        {
            SendPropertyChanging();
            entity.Sach = null;
            SendPropertyChanged(nameof(ChiTietPhieuMuons));
        }
    }
}
