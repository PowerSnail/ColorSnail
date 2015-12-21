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

namespace ColorSnail
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string GETCOLOR = "Please click to fetch color...";
        private string WAITING = "Welcome to Color Snail alpha 0.1";
        private string DONEGETTING = "Copy color code by clicking on the squares";

        AboutWindow about = new AboutWindow();


        public MainWindow()
        {
            InitializeComponent();
            Mouse.AddMouseMoveHandler(this, mouse_move);
            print(WAITING);
            about.Visibility = Visibility.Hidden;
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
            Mouse.AddMouseDownHandler(this, mouseDown);
            this.CaptureMouse();
            print(GETCOLOR);
        }

        private void mouseDown(object sender, MouseButtonEventArgs args)
        {
            this.CaptureMouse();
            var position = PointToScreen(Mouse.GetPosition(this));
            var c = getColor(position.X,position.Y);
            var colorItem = new ColorItem();
            colorItem.Argb = c;
            spColor.Children.Insert(0, colorItem);
            this.Focus();
            Mouse.RemoveMouseDownHandler(this, mouseDown);
            this.ReleaseMouseCapture();
            print(DONEGETTING);
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Activate();
        }

        private void print(string s)
        {
            tbStatus.Text = s;
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.AddMouseDownHandler(this, mouseDown);
            this.CaptureMouse();
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

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
