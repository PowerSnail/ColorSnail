using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ColorSnail
{
    class ColorParser
    {

        public static List<Color> parseStream(Stream stream)
        {
            List<Color> parsedList = new List<Color>();

            StreamReader reader = new StreamReader(stream);

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim();
                if (line[0] == '/' && line[1] == '/') continue; // indicating comment


                int start = line.IndexOf('#');
                int split = line.IndexOf('|');
                if (start == -1 || split == -1)
                {
                    MainWindow.logError("Parser cannot find legitimate start or split character in this line: \n\t" + line);
                    MainWindow.logError("The line is ignored, and parsing continues.");
                }

                string[] colors = line.Substring(start + 1, 11).ToUpper().Split('|'); // for correct calculation of ascii id, all letters are converted to uppercase

                if (colors.Length != 4)
                {
                    MainWindow.logError("Parser cannot find right number of split character in this line: \n\t" + line);
                    MainWindow.logError("The line is ignored, and parsing continues.");
                }

                byte[] argb = new byte[4];

                try
                {
                    for (int i = 0; i < 4; i++)
                    {
                        argb[i] = Byte.Parse(colors[i], System.Globalization.NumberStyles.HexNumber);
                    }

                }
                catch (Exception)
                {
                    MainWindow.logError("Number is incorrect in this line: \n\t" + line);
                    MainWindow.logError("The line is ignored, and parsing continues.");
                    continue;
                }

                parsedList.Insert(0, new Color() { A = argb[0], R = argb[1], G = argb[2], B = argb[3] });
            }
            
            return parsedList;
        }

        public static string[] generateDocument(ColorItem[] items)
        {
            string[] strs = new string[items.Length];
            for (int i = 0; i < strs.Length; i++)
            {
                Color c = items[i].Argb;
                byte[] colors = new byte[]
                {
                    c.A,
                    c.R,
                    c.G,
                    c.B
                };
                strs[i] = "#" + BitConverter.ToString(colors).Replace("-", "|");

            }

            return strs;
        }
    }
}
