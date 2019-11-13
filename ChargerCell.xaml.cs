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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ChargerDockDashBoard
{
    /// <summary>
    /// Interaction logic for ChargerCell.xaml
    /// </summary>
    public partial class ChargerCell : UserControl
    {
        private ChargerCellProperty _chargerCellProperty=new ChargerCellProperty();
        public ChargerCellProperty chargerCellProperty
        {
            get => _chargerCellProperty;
            set
            {
                _chargerCellProperty.CellID = value.CellID;
                _chargerCellProperty.CellColor = value.CellColor;
                _chargerCellProperty.Current = value.Current;
                _chargerCellProperty.DeviceSN = value.DeviceSN;
                _chargerCellProperty.Status = value.Status;
                _chargerCellProperty.TestTime = value.TestTime;
                _chargerCellProperty.Voltage = value.Voltage;
                _chargerCellProperty.TipMessage = value.TipMessage;
            }
        }

        public ChargerCell()
        {
            InitializeComponent();

            Binding binding = new Binding();
            binding.Source = chargerCellProperty;
            binding.Path = new PropertyPath("DeviceSN");
            BindingOperations.SetBinding(this.tbSerialNumber, TextBlock.TextProperty, binding);

            binding = new Binding();
            binding.Source = chargerCellProperty;
            binding.Path = new PropertyPath("TestTime");
            BindingOperations.SetBinding(this.tbElapsed, TextBlock.TextProperty, binding);

            binding = new Binding();
            binding.Source = chargerCellProperty;
            binding.Path = new PropertyPath("Voltage");
            BindingOperations.SetBinding(this.tbVoltage, TextBlock.TextProperty, binding);

            binding = new Binding();
            binding.Source = chargerCellProperty;
            binding.Path = new PropertyPath("Current");
            BindingOperations.SetBinding(this.tbCurrent, TextBlock.TextProperty, binding);

            binding = new Binding();
            binding.Source = chargerCellProperty;
            binding.Path = new PropertyPath("CellID");
            BindingOperations.SetBinding(this.tbCellId, TextBlock.TextProperty, binding);

            binding = new Binding();
            binding.Source = chargerCellProperty;
            binding.Path = new PropertyPath("CellColor");
            BindingOperations.SetBinding(this.cellColor, SolidColorBrush.ColorProperty, binding);

            binding = new Binding();
            binding.Source = chargerCellProperty;
            binding.Path = new PropertyPath("TipMessage");
            BindingOperations.SetBinding(this.ttCellStatus, TextBlock.TextProperty, binding);


        }

        public class ChargerCellProperty:INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            private string _deviceSN="xxxxxx";
            private TimeSpan _testTime=new TimeSpan(0,0,0);
            private double _voltage=0;
            private double _current=0;
            private ChargerCellStatus _status=ChargerCellStatus.Idle;
            private Color _cellColor=Colors.Blue;
            private int _cellID=1;
            public  DateTime ChargingStartTime=new DateTime(2000,1,1,0,0,0);
            private string _tipMessage = "Status";

            public ChargerCellProperty()
            {
                Status = ChargerCellStatus.Idle;
                
            }

            public string DeviceSN
            {
                get => _deviceSN;
                set
                {
                    _deviceSN = value;
                    OnPropertyChanged("DeviceSN");
                }
            }

            public string TestTime
            {
                get => _testTime.ToString("c");
                set
                {
                    _testTime = TimeSpan.Parse(value);
                    OnPropertyChanged("TestTime");
                }
            }

            public double Voltage
            {
                get => _voltage;
                set
                {
                    _voltage = Math.Round(value,3);
                    OnPropertyChanged("Voltage");
                }
            }

            public double Current
            {
                get => _current;
                set
                {
                    _current = Math.Round(value, 3);
                    OnPropertyChanged("Current");
                }
            }

            public int CellID
            {
                get => _cellID;
                set
                {
                    _cellID = value;
                    OnPropertyChanged("CellID");
                }
            }

            public string TipMessage
            {
                get => _tipMessage;
                set
                {
                    _tipMessage = value;
                    OnPropertyChanged("TipMessage");
                }
            }

            public Color CellColor
            {
                get => _cellColor;
                set
                {
                    _cellColor = value;
                    OnPropertyChanged("CellColor");
                }
            }

            public ChargerCellStatus Status
            {
                get => _status;
                set
                {
                    ChargerCellStatus previousStatus = _status;
                    _status = (ChargerCellStatus) Enum.Parse(typeof(ChargerCellStatus),value.ToString());
                    UpdateCellStatus();
                    if(previousStatus==ChargerCellStatus.Idle && _status==ChargerCellStatus.Charging)
                    {
                        this.ChargingStartTime = DateTime.Now;
                    }
                    
                }
            }

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            protected void UpdateCellStatus()
            {
                switch(_status)
                {
                    case ChargerCellStatus.Idle:
                        CellColor = Colors.Gray;
                        //CellColor = new SolidColorBrush(Colors.Gray) { Opacity = 0.5 };
                        TipMessage = "The slot is in idle.";
                        break;
                    case ChargerCellStatus.Charging:
                        CellColor = Colors.Blue;
                        //CellColor = new SolidColorBrush(Colors.Blue) { Opacity=0.5} ;
                        TipMessage = "The device is in charging.";
                        break;
                    case ChargerCellStatus.Error:
                        CellColor = Colors.Red;
                        //CellColor = new SolidColorBrush(Colors.Red) { Opacity=0.5} ;
                        TipMessage = "Error happened.";
                        break;
                    case ChargerCellStatus.Finished:
                        CellColor = Colors.Green;
                        //CellColor = new SolidColorBrush(Colors.Green) { Opacity=0.5} ;
                        TipMessage = "The device has been charged.";
                        break;
                    default:
                        break;
                }
            }

        }

        private void cellBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                ChargerCell cc = (ChargerCell)((FrameworkElement)sender).Parent;
                if (cc.chargerCellProperty.Status != ChargerCellStatus.Idle)
                {
                    //MessageBox.Show("Double click");
                    ChargingCurveWindow ccw = new ChargingCurveWindow();
                    ccw.Show();
                }
            }
        }
    }

    public enum ChargerCellStatus
    {
        Idle,
        Charging,
        Error,
        Finished,
    }
}
