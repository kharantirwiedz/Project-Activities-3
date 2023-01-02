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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SnakeWPF.Pages
{
    public partial class MainMenu_Page : Page
    {
        MainWindow Main_Window = (Application.Current.MainWindow as MainWindow);
        public MainMenu_Page()
        {
            InitializeComponent();
        }

        private void MainMenu_Page_Loaded(object sender, RoutedEventArgs e)
        {
            Main_Window.KeyDown += MainMenu_Page_KeyDown;
        }

        private void MainMenu_Page_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case (Key.Escape):
                    {
                        Main_Window.Click_Sound();
                        Application.Current.Shutdown();
                        break;
                    }
                case (Key.Enter):
                    {
                        Main_Window.Click_Sound();
                        Main_Window.KeyDown -= MainMenu_Page_KeyDown;
                        Main_Window.MainFrame.Navigate(new Uri("./Pages/GameSet_Page.xaml", UriKind.Relative));
                        break;
                    }
            }
        }

        private void MainMenu_Page_Start_Clicked(object sender, RoutedEventArgs e)
        {
            Main_Window.Click_Sound();
            Main_Window.KeyDown -= MainMenu_Page_KeyDown;
            Main_Window.MainFrame.Navigate(new Uri("./Pages/GameSet_Page.xaml", UriKind.Relative));
        }

        private void MainMenu_Page_Info_Clicked(object sender, RoutedEventArgs e)
        {
            Main_Window.Click_Sound();
            Main_Window.KeyDown -= MainMenu_Page_KeyDown;
            Main_Window.MainFrame.Navigate(new Uri("./Pages/Info_Page.xaml", UriKind.Relative));
        }

        private void MainMenu_Page_ExitGame_Clicked(object sender, RoutedEventArgs e)
        {
            Main_Window.Click_Sound();
            Application.Current.Shutdown();
        }
    }
}
