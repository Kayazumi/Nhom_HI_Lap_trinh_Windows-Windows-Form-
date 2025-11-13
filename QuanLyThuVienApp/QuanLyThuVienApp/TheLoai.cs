using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace QuanLyThuVienApp
{
    [Table(Name = "dbo.TheLoai")]
    public partial class TheLoai : LinqEntityBase
    {
        private int _MaTheLoai;
        private string _TenTheLoai;
        private string _MoTa;
        private int? _SoMaSach;
        private readonly EntitySet<Sach> _Saches;

        public TheLoai()
        {
            _Saches = new EntitySet<Sach>(attach_Sach, detach_Sach);
        }

        [Column(Storage = nameof(_MaTheLoai), DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, IsPrimaryKey = true)]
        public int MaTheLoai
        {
            get => _MaTheLoai;
            set
            {
                if (_MaTheLoai != value)
                {
                    SendPropertyChanging();
                    _MaTheLoai = value;
                    SendPropertyChanged(nameof(MaTheLoai));
                }
            }
        }

        [Column(Storage = nameof(_TenTheLoai), DbType = "NVarChar(50)")]
        public string TenTheLoai
        {
            get => _TenTheLoai;
            set
            {
                if (_TenTheLoai != value)
                {
                    SendPropertyChanging();
                    _TenTheLoai = value;
                    SendPropertyChanged(nameof(TenTheLoai));
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

        [Association(Storage = nameof(_Saches), OtherKey = nameof(Sach.MaTheLoai), ThisKey = nameof(MaTheLoai))]
        public EntitySet<Sach> Saches
        {
            get => _Saches;
            set => _Saches.Assign(value);
        }

        private void attach_Sach(Sach entity)
        {
            SendPropertyChanging();
            entity.TheLoai = this;
            SendPropertyChanged(nameof(Saches));
        }

        private void detach_Sach(Sach entity)
        {
            SendPropertyChanging();
            entity.TheLoai = null;
            SendPropertyChanged(nameof(Saches));
        }
    }
}
