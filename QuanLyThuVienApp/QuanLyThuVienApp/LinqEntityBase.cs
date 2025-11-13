using System.ComponentModel;

namespace QuanLyThuVienApp
{
    public abstract class LinqEntityBase : INotifyPropertyChanging, INotifyPropertyChanged
    {
        protected static readonly PropertyChangingEventArgs EmptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);

        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SendPropertyChanging()
        {
            PropertyChanging?.Invoke(this, EmptyChangingEventArgs);
        }

        protected void SendPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}


