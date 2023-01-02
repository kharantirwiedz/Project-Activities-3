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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SnakeWPF.Pages
{
    public partial class Info_Page : Page
    {
        MainWindow Main_Window = System.Windows.Application.Current.MainWindow as MainWindow;
        public static string Resources_Directory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/SnakeResources";
        public Info_Page()
        {
            InitializeComponent();
        }

        private void Info_Page_Loaded(object sender, RoutedEventArgs e)
        {
            Main_Window.KeyDown += Info_Page_KeyDown;
            Info_Page_Records_ListView.ItemsSource = Main_Window.Records_List;
            Alexander.Source = new BitmapImage(new Uri(Resources_Directory + "/am.png"));
            Fedor.Source = new BitmapImage(new Uri(Resources_Directory + "/fg.png"));
            Yaroslav.Source = new BitmapImage(new Uri(Resources_Directory + "/yl.png"));
            Daniel.Source = new BitmapImage(new Uri(Resources_Directory + "/do.png"));
            Narma.Source = new BitmapImage(new Uri(Resources_Directory + "/nb.png"));
        }

        private void Info_Page_Return_Clicked(object sender, RoutedEventArgs e)
        {
            Main_Window.Click_Sound();
            Main_Window.KeyDown -= Info_Page_KeyDown;
            Main_Window.MainFrame.Navigate(new Uri("./Pages/MainMenu_Page.xaml", UriKind.Relative));
        }

        private void Info_Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Main_Window.Click_Sound();
                Main_Window.KeyDown -= Info_Page_KeyDown;
                Main_Window.MainFrame.Navigate(new Uri("./Pages/MainMenu_Page.xaml", UriKind.Relative));
            }
        }
    }
}
