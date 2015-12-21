using System;
using System.Collections.Generic;
using System.IO;
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

namespace ColorSnail
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();

        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }
    }
}
