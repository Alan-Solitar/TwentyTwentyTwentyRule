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

namespace TwentyTwentyTwentyRule
{
    /// <summary>
    /// Interaction logic for Notification.xaml
    /// This class represent a notification window which
    /// will render in response to a a time countdown.
    /// </summary>
    public partial class Notification : Window
    {
        public Notification()
        {
            InitializeComponent();
        }
        //
        public void setImage(int width, int height)
        {
   
            this.main_image.Width = width;
            this.main_image.Height = height;
            this.Width = width;
            this.Height = height;
            // MaxWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            // MaxHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.SizeToContent = SizeToContent.WidthAndHeight;

        }

        //Reposition the window in the center of the screen
        public void CenterWindowOnScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
     
        }

        /*Set position and content of label to be displayed on top of image
         * @param lbl_text message to be displayed to the user
        */
        public void setLabel(string lbl_text)
        {
            this.lbl_phrase.Width = this.main_image.Width / 2;
            this.lbl_phrase.Height = this.main_image.Height / 2;
            this.lbl_phrase.Content = lbl_text;
        }
    }
}

