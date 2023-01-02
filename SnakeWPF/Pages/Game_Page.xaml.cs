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
    public partial class Game_Page : Page
    {
        SnakeLogic.GameInstance Game_Instance;
        MainWindow Main_Window = Application.Current.MainWindow as MainWindow;

        public Game_Page()
        {
            InitializeComponent();
        }

        private void Game_Page_Loaded(object sender, RoutedEventArgs e)
        {
            Main_Window.KeyDown += Game_Page_KeyDown;
            Game_Instance = new SnakeLogic.GameInstance(Game_Canvas, Main_Window.Current_Game_Size, Game_Page_Score, Game_Page_Level);
            this.Game_Page_Score.DataContext = Game_Instance.Score;
        }

        private void Game_Page_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case (Key.Escape):
                    {
                        Game_Instance.End("");
                        Main_Window.Click_Sound();
                        Main_Window.Music_Player.Open(new Uri(Main_Window.Music_Paths[0]));
                        Main_Window.Music_Player.Play();
                        Main_Window.KeyDown -= Game_Page_KeyDown;
                        Main_Window.MainFrame.Navigate(new Uri("./Pages/GameSet_Page.xaml", UriKind.Relative));
                        break;
                    }
                case (Key.Enter):
                    {
                        Main_Window.Music_Player.Stop();
                        Game_Instance = new SnakeLogic.GameInstance(Game_Canvas, Main_Window.Current_Game_Size, Game_Page_Score, Game_Page_Level);
                        break;
                    }
                case (Key.Up): case (Key.W):
                    {
                        Game_Instance.List_Directions.Add(SnakeLogic.Directions.Up);
                        break;
                    }
                case (Key.Left): case (Key.A):
                    {
                        Game_Instance.List_Directions.Add(SnakeLogic.Directions.Left);
                        break;
                    }
                case (Key.Down): case (Key.S):
                    {
                        Game_Instance.List_Directions.Add(SnakeLogic.Directions.Down);
                        break;
                    }
                case (Key.Right): case (Key.D):
                    {
                        Game_Instance.List_Directions.Add(SnakeLogic.Directions.Right);
                        break;
                    }
            }
        }

        private void Game_Page_Exit_Clicked(object sender, RoutedEventArgs e)
        {
            Game_Instance.End("");
            Main_Window.Click_Sound();
            Main_Window.Music_Player.Open(new Uri(Main_Window.Music_Paths[0]));
            Main_Window.Music_Player.Play();
            Main_Window.KeyDown -= Game_Page_KeyDown;
            Main_Window.MainFrame.Navigate(new Uri("./Pages/GameSet_Page.xaml", UriKind.Relative));
        }
    }
}
