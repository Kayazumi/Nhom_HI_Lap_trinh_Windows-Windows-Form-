using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace QuanLyThuVienApp
{
    [Table(Name = "dbo.NhaXuatBan")]
    public partial class NhaXuatBan : LinqEntityBase
    {
        private int _MaNXB;
        private string _TenNXB;
        private string _MoTa;
        private int? _SoMaSach;
        private readonly EntitySet<Sach> _Saches;

        public NhaXuatBan()
        {
            _Saches = new EntitySet<Sach>(attach_Sach, detach_Sach);
        }

        [Column(Storage = nameof(_MaNXB), DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, IsPrimaryKey = true)]
        public int MaNXB
        {
            get => _MaNXB;
            set
            {
                if (_MaNXB != value)
                {
                    SendPropertyChanging();
                    _MaNXB = value;
                    SendPropertyChanged(nameof(MaNXB));
                }
            }
        }

        [Column(Storage = nameof(_TenNXB), DbType = "NVarChar(50)")]
        public string TenNXB
        {
            get => _TenNXB;
            set
            {
                if (_TenNXB != value)
                {
                    SendPropertyChanging();
                    _TenNXB = value;
                    SendPropertyChanged(nameof(TenNXB));
                }
            }
        }

        [Column(Storage = nameof(_MoTa), DbType = "NVarChar(500)")]
        public string MoTa
        {
            get => _MoTa;
            set
            {
                if (_MoTa != value)
                {
                    SendPropertyChanging();
                    _MoTa = value;
                    SendPropertyChanged(nameof(MoTa));
                }
            }
        }

        [Column(Storage = nameof(_SoMaSach), DbType = "Int")]
        public int? SoMaSach
        {
            get => _SoMaSach;
            set
            {
                if (_SoMaSach != value)
                {
                    SendPropertyChanging();
                    _SoMaSach = value;
                    SendPropertyChanged(nameof(SoMaSach));
                }
            }
        }

        [Association(Storage = nameof(_Saches), OtherKey = nameof(Sach.MaNXB), ThisKey = nameof(MaNXB))]
        public EntitySet<Sach> Saches
        {
            get => _Saches;
            set => _Saches.Assign(value);
        }

        private void attach_Sach(Sach entity)
        {
            SendPropertyChanging();
            entity.NhaXuatBan = this;
            SendPropertyChanged(nameof(Saches));
        }

        private void detach_Sach(Sach entity)
        {
            SendPropertyChanging();
            entity.NhaXuatBan = null;
            SendPropertyChanged(nameof(Saches));
        }
    }
}
