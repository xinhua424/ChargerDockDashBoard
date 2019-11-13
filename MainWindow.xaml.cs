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
using System.Timers;

namespace ChargerDockDashBoard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int activeCellBatch=0;
        public ReceivedContent receivedContent=new ReceivedContent();

        System.Timers.Timer timerUIRefresh = new System.Timers.Timer();

        ChargerCell.ChargerCellProperty[] ChargerCellsProperty = new ChargerCell.ChargerCellProperty[400];

        private bool _rolling=true;

        private readonly int ROLLING_BATCH_SECONDS = 3; //Set the time for each batch in rolling.

        public MainWindow()
        {
            InitializeComponent();
            Binding binding = new Binding();
            binding.Source = receivedContent;
            binding.Path = new PropertyPath("Content");

            InitializeActiveCells();

            timerUIRefresh.Interval = 1000; // 1 seconds
            timerUIRefresh.Elapsed += new ElapsedEventHandler(this.OnTimer);
        }

        public bool Rolling
        {
            get => _rolling;
            set
            {
                _rolling = value;
            }
        }

        public void InitializeActiveCells()
        {
            for (int index = 0; index < ChargerCellsProperty.Length; index++)
            {
                ChargerCellsProperty[index] = new ChargerCell.ChargerCellProperty();
                ChargerCellsProperty[index].CellID = index + 1;
            }
            this.ActiveChargerCell0.chargerCellProperty = ChargerCellsProperty[0];
            this.ActiveChargerCell1.chargerCellProperty = ChargerCellsProperty[1];
            this.ActiveChargerCell2.chargerCellProperty = ChargerCellsProperty[2];
            this.ActiveChargerCell3.chargerCellProperty = ChargerCellsProperty[3];
            this.ActiveChargerCell4.chargerCellProperty = ChargerCellsProperty[4];
            this.ActiveChargerCell5.chargerCellProperty = ChargerCellsProperty[5];
            this.ActiveChargerCell6.chargerCellProperty = ChargerCellsProperty[6];
            this.ActiveChargerCell7.chargerCellProperty = ChargerCellsProperty[7];
            this.ActiveChargerCell8.chargerCellProperty = ChargerCellsProperty[8];
            this.ActiveChargerCell9.chargerCellProperty = ChargerCellsProperty[9];
            this.ActiveChargerCell10.chargerCellProperty = ChargerCellsProperty[10];
            this.ActiveChargerCell11.chargerCellProperty = ChargerCellsProperty[11];
            this.ActiveChargerCell12.chargerCellProperty = ChargerCellsProperty[12];
            this.ActiveChargerCell13.chargerCellProperty = ChargerCellsProperty[13];
            this.ActiveChargerCell14.chargerCellProperty = ChargerCellsProperty[14];
            this.ActiveChargerCell15.chargerCellProperty = ChargerCellsProperty[15];
            this.ActiveChargerCell16.chargerCellProperty = ChargerCellsProperty[16];
            this.ActiveChargerCell17.chargerCellProperty = ChargerCellsProperty[17];
            this.ActiveChargerCell18.chargerCellProperty = ChargerCellsProperty[18];
            this.ActiveChargerCell19.chargerCellProperty = ChargerCellsProperty[19];
        }

        public class ReceivedContent:INotifyPropertyChanged
        {
            private string _content="def";
            public event PropertyChangedEventHandler PropertyChanged;

            public string Content
            {
                get =>_content;
                set
                {
                    _content = value;
                }
            }

            public void Add(string s)
            {
                _content += s + Environment.NewLine;
                OnPropertyChanged("Content");
            }

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void btButton_Click(object sender, RoutedEventArgs e)
        {
            //ChargerCellsProperty[0].Status = ChargerCellStatus.Charging;
            //this.ActiveChargerCell0.chargerCellProperty = ChargerCellsProperty[0];
            timerUIRefresh.Start();
        }

        private void OnTimer(object sender,ElapsedEventArgs args)
        {
            if(Rolling)
            {
                activeCellBatch++;
                activeCellBatch %= (8* ROLLING_BATCH_SECONDS);
            }

            int batch = activeCellBatch / ROLLING_BATCH_SECONDS;

            this.ActiveChargerCell0.chargerCellProperty = ChargerCellsProperty[batch * 50 + 0];
            this.ActiveChargerCell1.chargerCellProperty = ChargerCellsProperty[batch * 50 + 1];
            this.ActiveChargerCell2.chargerCellProperty = ChargerCellsProperty[batch * 50 + 2];
            this.ActiveChargerCell3.chargerCellProperty = ChargerCellsProperty[batch * 50 + 3];
            this.ActiveChargerCell4.chargerCellProperty = ChargerCellsProperty[batch * 50 + 4];
            this.ActiveChargerCell5.chargerCellProperty = ChargerCellsProperty[batch * 50 + 5];
            this.ActiveChargerCell6.chargerCellProperty = ChargerCellsProperty[batch * 50 + 6];
            this.ActiveChargerCell7.chargerCellProperty = ChargerCellsProperty[batch * 50 + 7];
            this.ActiveChargerCell8.chargerCellProperty = ChargerCellsProperty[batch * 50 + 8];
            this.ActiveChargerCell9.chargerCellProperty = ChargerCellsProperty[batch * 50 + 9];
            this.ActiveChargerCell10.chargerCellProperty = ChargerCellsProperty[batch * 50 + 10];
            this.ActiveChargerCell11.chargerCellProperty = ChargerCellsProperty[batch * 50 + 11];
            this.ActiveChargerCell12.chargerCellProperty = ChargerCellsProperty[batch * 50 + 12];
            this.ActiveChargerCell13.chargerCellProperty = ChargerCellsProperty[batch * 50 + 13];
            this.ActiveChargerCell14.chargerCellProperty = ChargerCellsProperty[batch * 50 + 14];
            this.ActiveChargerCell15.chargerCellProperty = ChargerCellsProperty[batch * 50 + 15];
            this.ActiveChargerCell16.chargerCellProperty = ChargerCellsProperty[batch * 50 + 16];
            this.ActiveChargerCell17.chargerCellProperty = ChargerCellsProperty[batch * 50 + 17];
            this.ActiveChargerCell18.chargerCellProperty = ChargerCellsProperty[batch * 50 + 18];
            this.ActiveChargerCell19.chargerCellProperty = ChargerCellsProperty[batch * 50 + 19];
            
            for(int index=0;index<ChargerCellsProperty.Length;index++)
            {
                if(ChargerCellsProperty[index].Status==ChargerCellStatus.Charging)
                {
                    ChargerCellsProperty[index].TestTime = DateTime.Now.Subtract(ChargerCellsProperty[index].ChargingStartTime).ToString(@"hh\:mm\:ss");
                }
            }
        }

        private void btButton1_Click(object sender, RoutedEventArgs e)
        {
            ChargerCellsProperty[0].Status=ChargerCellStatus.Charging;
        }

        private void btButton2_Click(object sender, RoutedEventArgs e)
        {
            ChargerCellsProperty[0].Status = ChargerCellStatus.Error;
            ChargerCellsProperty[0].TipMessage = "Short circuit.";

        }
    }
}
