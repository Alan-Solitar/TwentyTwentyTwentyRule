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

        IOResourceManager ioManager;
        private List<string> images;
        private List<string> strings;
        private ObservableCollection<int> intervalValues;
        private ObservableCollection<int> restValues;
        public int nCountDown { get; set; }
        System.Windows.Threading.DispatcherTimer dispatcherTimer;



        public MainWindow()
        {
            InitializeComponent();
            ioManager = new IOResourceManager();
            strings = ioManager.getStringsFromFile();
            populateImageList();
            init();
        }
        public void populateImageList()
        {
            var executingPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            images = Directory.GetFiles(@".\Images", "*.jpg").ToList();
            images = images.Concat(Directory.GetFiles(@".\Images", "*.png")).ToList();
            foreach (string Im in images)
            {
                Console.WriteLine(Im);
            }


        }

        public void Notify()
        {
            var executingPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            Notification notification = new TwentyTwentyTwentyRule.Notification();
            notification.main_image.Source = new BitmapImage(new Uri(executingPath + images[0]));
            notification.setLabel(strings[0]);
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
        public void init()
        {
            initComboBoxes();
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            changeText();
        }

        public void changeText()
        {
            if (this.nCountDown != 0)
            {
                this.tBox.Text = nCountDown.ToString();
            }
            else
            {
                this.tBox.Text = "Something Happened";
            }
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            var executingPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            Notification notification = new TwentyTwentyTwentyRule.Notification();
            BitmapImage img = new BitmapImage(new Uri(executingPath + images[rand.Next(images.Count)]));
            notification.main_image.Source = img;
            Console.WriteLine(img.PixelHeight + " " + img.PixelWidth);
            notification.setImage(img.PixelWidth, img.PixelHeight);
            notification.CenterWindowOnScreen();
            notification.setLabel(strings[0]);
            notification.ShowDialog();

        }


    }
}

    