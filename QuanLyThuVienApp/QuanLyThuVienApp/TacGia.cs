using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace QuanLyThuVienApp
{
    [Table(Name = "dbo.TacGia")]
    public partial class TacGia : LinqEntityBase
    {
        private int _MaTG;
        private string _TenTG;
        private string _MoTa;
        private int? _SoMaSach;
        private readonly EntitySet<Sach> _Saches;

        public TacGia()
        {
            _Saches = new EntitySet<Sach>(attach_Sach, detach_Sach);
        }

        [Column(Storage = nameof(_MaTG), DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, IsPrimaryKey = true)]
        public int MaTG
        {
            get => _MaTG;
            set
            {
                if (_MaTG != value)
                {
                    SendPropertyChanging();
                    _MaTG = value;
                    SendPropertyChanged(nameof(MaTG));
                }
            }
        }

        [Column(Storage = nameof(_TenTG), DbType = "NVarChar(50)")]
        public string TenTG
        {
            get => _TenTG;
            set
            {
                if (_TenTG != value)
                {
                    SendPropertyChanging();
                    _TenTG = value;
                    SendPropertyChanged(nameof(TenTG));
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

        [Association(Storage = nameof(_Saches), OtherKey = nameof(Sach.MaTG), ThisKey = nameof(MaTG))]
        public EntitySet<Sach> Saches
        {
            get => _Saches;
            set => _Saches.Assign(value);
        }

        private void attach_Sach(Sach entity)
        {
            SendPropertyChanging();
            entity.TacGia = this;
            SendPropertyChanged(nameof(Saches));
        }

        private void detach_Sach(Sach entity)
        {
            SendPropertyChanging();
            entity.TacGia = null;
            SendPropertyChanged(nameof(Saches));
        }
    }
}
