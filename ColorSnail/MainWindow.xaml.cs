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
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing.Imaging;
using Microsoft.Win32;

namespace ColorSnail
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string GETCOLOR = "Please click to fetch color...";
        private static string WAITING = "Welcome to Color Snail alpha 0.1";
        private static string DONEGETTING = "Copy color code by clicking on the squares";

        private static int MSECANIME = 200;

        AboutWindow about = new AboutWindow();

        System.Windows.Media.Animation.DoubleAnimation enterAnimation = new System.Windows.Media.Animation.DoubleAnimation()
        {
            From = 0,
            To = 30,
            Duration = new System.Windows.Duration(TimeSpan.FromMilliseconds(MSECANIME)),
            AutoReverse = false
        };

        System.Windows.Media.Animation.Storyboard enterSB = new System.Windows.Media.Animation.Storyboard();

        System.Windows.Media.Animation.DoubleAnimation removeAnimation = new System.Windows.Media.Animation.DoubleAnimation()
        {
            From = 30,
            To = 0,
            Duration = new System.Windows.Duration(TimeSpan.FromMilliseconds(MSECANIME)),
            AutoReverse = false
        };

        System.Windows.Media.Animation.Storyboard removeSB = new System.Windows.Media.Animation.Storyboard();


        public MainWindow()
        {
            InitializeComponent();
            Mouse.AddMouseMoveHandler(this, mouse_move);
            print(WAITING);
            about.Visibility = Visibility.Hidden;

            initAnimations();
        }


        public void mouse_move(object sender, MouseEventArgs e)
        {

        }

        public Color getColor(double x, double y)
        {
            var bmp = new System.Drawing.Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            var graphics = System.Drawing.Graphics.FromImage(bmp);
            System.Drawing.Size size = new System.Drawing.Size(1, 1);
            graphics.CopyFromScreen((int)x, (int)y, 0, 0, size);
            var p = bmp.GetPixel(0, 0);
            Color c = Color.FromArgb(p.A, p.R, p.G, p.B);
            return c;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Mouse.AddMouseDownHandler(this, mouseDownWhenSelectingColor);
            this.Deactivated += holdWindowFocus;
            this.CaptureMouse();
            print(GETCOLOR);
        }

        private void mouseDownWhenSelectingColor(object sender, MouseButtonEventArgs args)
        {
            if (args.ChangedButton == MouseButton.Left)
            {
                var position = PointToScreen(Mouse.GetPosition(this));
                var c = getColor(position.X,position.Y);
                addColorItem(c);
                print(DONEGETTING); 
            }
            else
            {
                print("Exit selecting mode");
            }
            this.Focus();
            Mouse.RemoveMouseDownHandler(this, mouseDownWhenSelectingColor);
            this.Deactivated -= holdWindowFocus;
            this.ReleaseMouseCapture();
            OutBorder.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));
        }

        private void addColorItem(Color c)
        {
            var colorItem = new ColorItem(this);
            colorItem.Argb = c;
            spColor.Children.Insert(0, colorItem);
            System.Windows.Media.Animation.Storyboard.SetTarget(enterAnimation, colorItem);
            enterSB.Begin(this);
        }

        private void holdWindowFocus(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Activate();
        }

        private void print(string s)
        {
            tbStatus.Text = s;
        }

        private void btnSelectColor_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.AddMouseDownHandler(this, mouseDownWhenSelectingColor);
            this.CaptureMouse();
            OutBorder.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFAA3333"));
            print(GETCOLOR);
        }

        private void Close_Button_Mouse_up(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2) {
                about.Visibility = Visibility.Visible;   
            }
            try {
                DragMove();
            } catch (InvalidOperationException ea)
            {
                // this exception is catched because dragmove method seems to be falsely detecting the mouse event when mouse is up at the end of dragging
                // further solution is being looked for
            }
        }

        private void initAnimations()
        {
            enterSB.Children.Add(enterAnimation);
            removeSB.Children.Add(removeAnimation);
            System.Windows.Media.Animation.Storyboard.SetTargetProperty(removeAnimation, new PropertyPath(ColorItem.HeightProperty));
            System.Windows.Media.Animation.Storyboard.SetTargetProperty(enterAnimation, new PropertyPath(ColorItem.HeightProperty));

        }


        async internal void removeItem(ColorItem colorItem)
        {
            System.Windows.Media.Animation.Storyboard.SetTarget(removeAnimation, colorItem);
            removeSB.Begin(this);
            await Task.Delay(MSECANIME);
            spColor.Children.Remove(colorItem);
        }

        private void CloseButton_MouseEnter(object sender, MouseEventArgs e)
        {
            wrapPanelAroundCloseButton.Background = Brushes.DarkGray;
        }

        private void CloseButton_MouseLeave(object sender, MouseEventArgs e)
        {
            wrapPanelAroundCloseButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333337"));
        }

        internal static void logError(string str)
        {
            //TODO
        }

        internal void prompt(string str)
        {
            tbStatus.Text = str;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == true)
            {
                saveFile(sfd.FileName);
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            clearColors();

            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                loadFile(ofd.FileName);
            }
        }

        private void clearColors()
        {
            if (spColor.Children.Count != 0)
            {
                bool save = (System.Windows.MessageBox.Show("Do you want to save the current work?", "aler", MessageBoxButton.YesNo) == MessageBoxResult.Yes);
                if (save)
                {
                    btnSave_Click(null, null);
                }
            }
        }

        private void loadFile(string filename)
        {
            System.IO.Stream stream ;
            try
            {
                stream = new System.IO.FileStream(filename, System.IO.FileMode.Open);
            }
            catch (Exception)
            {
                prompt("Error reading the file");
                return;
            }

            var colors = ColorParser.parseStream(stream);
            stream.Close();

            if (colors.Count != 0)
            {
                foreach (var c in colors)
                {
                    addColorItem(c);
                }
            } 
            else
            {
                prompt("No valid color stored in file");
            }
        }

        private void saveFile(string filename)
        {
            var colorItems = new ColorItem[spColor.Children.Count];
            if (colorItems.Length == 0)
            {
                prompt("Error! No Color to be saved");
                return;
            }

            int i = 0;
            foreach (var child in spColor.Children)
            {
                var ci = (ColorItem)(child);
                colorItems[i] = ci;
                i++;
            }

            string[] document = ColorParser.generateDocument(colorItems);

            var writer = new System.IO.StreamWriter(filename);
            foreach (var s in document)
            {
                writer.WriteLine(s);
            }
            writer.Flush();
            writer.Close();
        }
    }
}
