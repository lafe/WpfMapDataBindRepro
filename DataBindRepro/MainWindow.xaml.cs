using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using JetBrains.Annotations;
using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;

namespace DataBindRepro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {


        public MainWindow()
        {
            InitializeComponent();
            Latitude = 51.718889;
            Longitude = 8.755278;
        }

        private double _latitude;
        public double Latitude
        {
            get => _latitude;
            set
            {
                if (value.Equals(_latitude)) return;
                _latitude = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CurrentPosition));
            }
        }

        private double _longitude;
        public double Longitude
        {
            get => _longitude;
            set
            {
                if (value.Equals(_longitude)) return;
                _longitude = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CurrentPosition));
            }
        }

        public Geopoint CurrentPosition
        {
            get
            {
                return new Geopoint(new BasicGeoposition()
                {
                    Latitude = Latitude,
                    Longitude = Longitude
                });
            }
            set
            {
                if (value.Position.Longitude.Equals(Longitude) && value.Position.Latitude.Equals(Latitude))
                {
                    return;
                }

                Longitude = value.Position.Longitude;
                Latitude = value.Position.Latitude;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
