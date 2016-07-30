using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
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


namespace TwentyTwentyTwentyRule
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Notification notification;
        IOResourceManager ioManager;
        SettingsManager sManager;
        private List<string> images;
        private List<string> strings;
        private ObservableCollection<int> intervalValues;
        private ObservableCollection<int> restValues;
        public UInt16 nCountDown { get; set; }
        public string restInterval { get; set; }
        public int restIntervalIndex;
        public int countdownIntervalIndex;
        public string countdownInterval { get; set; }
        System.Windows.Threading.DispatcherTimer dispatcherTimer;
        private ushort nRest;
        private bool bTimerOn;
        private DateTime start;
        private int elapsedTime;
        private bool bLockComputer;
        Random rand;

        public MainWindow()
        {
            InitializeComponent();
            ioManager = new IOResourceManager();
            sManager = new SettingsManager();
            restIntervalIndex = sManager.GetUserRestInterval();
            countdownIntervalIndex = sManager.GetUserCountdownInterval();
            strings = ioManager.GetStringsFromFile();
            images = ioManager.PopulateImageList();
            bTimerOn = false;
            rand = new Random();
            initComboBoxes();
           // cmbbox_interval.SelectedIndex=countdownIntervalIndex;
           // cmbbox_wait_time.SelectedIndex = restIntervalIndex;
            restInterval = cmbbox_wait_time.SelectedItem.ToString();
            countdownInterval = cmbbox_interval.SelectedItem.ToString();
            initTimer();
            bLockComputer = false;
        }

        //Create Notification and display to user  
        public void Notify()
        {

            var executingPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            notification = new TwentyTwentyTwentyRule.Notification();
            notification.main_image.Source = new BitmapImage(new Uri(executingPath + images[rand.Next(images.Count)]));
            notification.setLabel(strings[0]);
            notification.CenterWindowOnScreen();
            notification.ShowDialog();
        }
        public void initComboBoxes()
        {
            intervalValues = new ObservableCollection<int>();
            for (int i = 5; i <= 30; i += 5)
            {
                intervalValues.Add(i);
            }
            restValues = new ObservableCollection<int>();
            for (int i = 1; i <= 10; ++i)
            {
                restValues.Add(i);
            }
            this.cmbbox_interval.ItemsSource = intervalValues;
            this.cmbbox_wait_time.ItemsSource = restValues;
        }
        public void initTimer()
        {
            nCountDown = UInt16.Parse( countdownInterval);
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            //dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Interval = new TimeSpan(0, nCountDown, 0);
            start = DateTime.Now;
            
        }

        public void changeText()
        {
            
                //this.tBox.Text = (elapsedTime).ToString();
          
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (bLockComputer)
                System.Diagnostics.Process.Start(@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");
            else
            {

                Notify();
                if (notification.WindowState == WindowState.Minimized)
                {
                    notification.WindowState = WindowState.Normal;
                }

                notification.Activate();
                notification.Topmost = true;  // important
                notification.Topmost = false; // important
                notification.Focus();         // important
            }

        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            
            if(bTimerOn)
            {
                dispatcherTimer.Stop();
                btn1.Content = ("Start");
                bTimerOn = false;
                cmbbox_interval.IsEnabled = true;
                cmbbox_wait_time.IsEnabled = true;
            }
            else
            {
                dispatcherTimer.Start();
                btn1.Content = "Stop";
                dispatcherTimer.Start();
                bTimerOn = true;
                //disable combobox while 
                cmbbox_interval.IsEnabled = false;
                cmbbox_wait_time.IsEnabled = false;
            }
        }

        private void cmbbox_interval_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //makes sure it doesn't run the code on startup
            if (!IsLoaded)
                return;
            else
            {
                ComboBox cmbx = (sender as ComboBox);
                sManager.SetUserCountdownInterval(cmbx.SelectedIndex);
                nCountDown = UInt16.Parse(cmbx.SelectedItem.ToString());
               // dispatcherTimer.Interval =new TimeSpan(0, nCountDown, 1);
            }
        }

        private void cmbbox_wait_time_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmbx = (sender as ComboBox);
            sManager.SetUserRestInterval(cmbx.SelectedIndex);
            nRest = UInt16.Parse(cmbx.SelectedItem.ToString());
        }

        private void btn_lock_button_click(object sender, RoutedEventArgs e)
        {
            if (bLockComputer != true)
            {
                bLockComputer = true;
                btn_lock_button.ToolTip = "Disable Lock";

            }
            else
            {
                bLockComputer = false;
                btn_lock_button.ToolTip = "Enable Computer Lock";
            }
        }

       
    }
}

    