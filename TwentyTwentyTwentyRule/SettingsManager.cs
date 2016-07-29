using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyTwentyTwentyRule
{
    public class SettingsManager
    {
        public int GetUserRestInterval()
        {
            return Properties.Settings.Default.user_rest_interval;
        }
        public int GetUserCountdownInterval()
        {
            return Properties.Settings.Default.user_countdown_interval;
        }
        public void SetUserRestInterval(int restInterval)
        {
            Properties.Settings.Default.user_rest_interval =restInterval;
            Properties.Settings.Default.Save();
        }
 
        public void SetUserCountdownInterval(int countdownInterval)
        {
             Properties.Settings.Default.user_countdown_interval = countdownInterval;
            Properties.Settings.Default.Save();
        }
       
        


    }
}
