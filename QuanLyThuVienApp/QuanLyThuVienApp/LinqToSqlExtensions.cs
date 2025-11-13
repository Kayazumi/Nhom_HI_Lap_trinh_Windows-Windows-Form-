using System.Data.Linq;

namespace QuanLyThuVienApp
{
    public static class LinqToSqlExtensions
    {
        public static void Add<TEntity>(this Table<TEntity> table, TEntity entity) where TEntity : class
        {
            table.InsertOnSubmit(entity);
        }

        public static void Remove<TEntity>(this Table<TEntity> table, TEntity entity) where TEntity : class
        {
            table.DeleteOnSubmit(entity);
        }
    }
}


