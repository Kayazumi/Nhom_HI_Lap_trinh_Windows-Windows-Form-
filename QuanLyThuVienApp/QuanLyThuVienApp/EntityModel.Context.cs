using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Configuration;

namespace QuanLyThuVienApp
{
    public partial class QLTVEntities : DataContext
    {
        private static readonly MappingSource MappingSource = new AttributeMappingSource();

        public QLTVEntities()
            : base(ConfigurationManager.ConnectionStrings["QLTVEntities"].ConnectionString, MappingSource)
        {
        }

        public QLTVEntities(string connection)
            : base(connection, MappingSource)
        {
        }

        public QLTVEntities(System.Data.IDbConnection connection)
            : base(connection, MappingSource)
        {
        }

        public Table<ChiTietPhieuMuon> ChiTietPhieuMuons => GetTable<ChiTietPhieuMuon>();

        public Table<NguoiDung> NguoiDungs => GetTable<NguoiDung>();

        public Table<NhaXuatBan> NhaXuatBans => GetTable<NhaXuatBan>();

        public Table<PhieuMuon> PhieuMuons => GetTable<PhieuMuon>();

        public Table<Sach> Saches => GetTable<Sach>();

        public Table<TacGia> TacGias => GetTable<TacGia>();

        public Table<TheLoai> TheLoais => GetTable<TheLoai>();

        public void SaveChanges()
        {
            SubmitChanges();
        }
    }
}