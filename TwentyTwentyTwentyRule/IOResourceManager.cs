using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TwentyTwentyTwentyRule
{
    class IOResourceManager
    {
        string stringsFile = "strings.txt";
        string executingPath = "";
        List<string> strings;

        public IOResourceManager()
        {
            executingPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }
        public List<string> GetStringsFromFile()
        {
            StreamReader reader;
            string filepath = Path.Combine(executingPath, stringsFile);
            if (filepath != null)
            {
                reader = new StreamReader(filepath, System.Text.Encoding.GetEncoding(932));
                strings = new List<string>();
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    strings.Add(line);
                }
                foreach (string s in strings)
                {
                    Console.WriteLine(s);
                }
            }
            return strings;
        }

        /*
         *@return return a list of images 
        */
        public List<string> PopulateImageList()
        {
            var executingPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var images = Directory.GetFiles(@".\Images", "*.jpg").ToList();
            images = images.Concat(Directory.GetFiles(@".\Images", "*.png")).ToList();

            //debug
            foreach (string Im in images)
            {
                Console.WriteLine(Im);
            }

            return images;


        }

    }
}
