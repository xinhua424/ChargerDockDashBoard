using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;


namespace ChargerDockDashBoard
{
    /// <summary>
    /// Interaction logic for ChargingCurveWindow.xaml
    /// </summary>
    public partial class ChargingCurveWindow : Window
    {
        private ObservableCollection<DataPoint> _serialData = new ObservableCollection<DataPoint>();
        public ChargingCurveProfile CurveProfile = new ChargingCurveProfile();
        public ChargingCurveWindow()
        {
            InitializeComponent();

            Binding binding = new Binding();
            binding.Source = CurveProfile;
            binding.Path = new PropertyPath("Slot");
            BindingOperations.SetBinding(this.tbSlot, TextBlock.TextProperty, binding);

            binding = new Binding();
            binding.Source = CurveProfile;
            binding.Path = new PropertyPath("SerialNumber");
            BindingOperations.SetBinding(this.tbSerialNumber, TextBlock.TextProperty, binding);

            binding = new Binding();
            binding.Source = CurveProfile;
            binding.Path = new PropertyPath("Energy");
            BindingOperations.SetBinding(this.tbEnergy, TextBlock.TextProperty, binding);

            binding = new Binding();
            binding.Source = CurveProfile;
            binding.Path = new PropertyPath("Elapsed");
            BindingOperations.SetBinding(this.tbElapsed, TextBlock.TextProperty, binding);
        }

        public class ChargingCurveProfile:INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            private int _slot;
            private string _serialNumber;
            private double _energy=0;
            private int _elapsed=0;

            public string SerialNumber
            {
                get => _serialNumber;
                set
                {
                    _serialNumber = value;
                    OnPropertyChanged("SerialNumber");
                }
            }

            public int Elapsed
            {
                get => _elapsed;
                set
                {
                    _elapsed = value;
                    OnPropertyChanged("Elapsed");
                }
            }

            public double Energy
            {
                get => _energy;
                set
                {
                    _energy = value;
                    OnPropertyChanged("Energy");
                }
            }

            public int Slot
            {
                get => _slot;
                set
                {
                    _slot = value;
                    OnPropertyChanged("Slot");
                }
            }

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ObservableCollection<DataPoint> SerialData
        {
            get => _serialData;
            set
            {
                _serialData = value;
                this.ChargingCurveChart.DataContext = _serialData;
            }
        }

        



    }
}
