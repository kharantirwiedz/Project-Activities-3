using System;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SnakeWPF.Pages
{
    public partial class GameSet_Page : Page
    {
        TextDecorationCollection Red_Underline = new TextDecorationCollection();

        MainWindow Main_Window = Application.Current.MainWindow as MainWindow;
        public GameSet_Page()
        {
            InitializeComponent();
        }

        private void GameSet_Page_Loaded(object sender, RoutedEventArgs e)
        {
            TextDecoration Underline = new TextDecoration();
            Underline.Pen = new Pen(Brushes.Red, 1);
            Underline.PenThicknessUnit = TextDecorationUnit.FontRecommended;
            Red_Underline.Add(Underline);

            GameSet_UserName_TextBox.Text = Main_Window.User_Name.ToString();
            GameSet_GridSize_TextBox.Text = Main_Window.Current_Game_Size.ToString();
            Main_Window.KeyDown += GameSet_Page_KeyDown;
        }

        private void GameSet_Page_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case (Key.Escape):
                    {
                        SaveChanges();
                        Main_Window.Click_Sound();
                        Main_Window.KeyDown -= GameSet_Page_KeyDown;
                        Main_Window.MainFrame.Navigate(new Uri("./Pages/MainMenu_Page.xaml", UriKind.Relative));
                        break;
                    }
                case (Key.Enter):
                    {
                        SaveChanges();
                        Main_Window.Click_Sound();
                        Main_Window.KeyDown -= GameSet_Page_KeyDown;
                        Main_Window.MainFrame.Navigate(new Uri("./Pages/Game_Page.xaml", UriKind.Relative));
                        break;
                    }
            }
        }

        private void GameSet_Page_Start_Clicked(object sender, RoutedEventArgs e)
        {
            SaveChanges();
            Main_Window.Click_Sound();
            Main_Window.KeyDown -= GameSet_Page_KeyDown;
            Main_Window.MainFrame.Navigate(new Uri("./Pages/Game_Page.xaml", UriKind.Relative));
        }

        private void GameSet_Page_Return_Clicked(object sender, RoutedEventArgs e)
        {
            SaveChanges();
            Main_Window.Click_Sound();
            Main_Window.KeyDown -= GameSet_Page_KeyDown;
            Main_Window.MainFrame.Navigate(new Uri("./Pages/MainMenu_Page.xaml", UriKind.Relative));
        }

        private void SaveChanges()
        {
            if (GameSet_UserName_TextBox.Text != "")
                Main_Window.User_Name = GameSet_UserName_TextBox.Text;

            int Entered_Size;
            if (int.TryParse(GameSet_GridSize_TextBox.Text, out Entered_Size))
            {
                if (Entered_Size > 1)
                    Main_Window.Current_Game_Size = Entered_Size;
            }
        }

        private void GameSet_GridSize_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int Entered_Size;
            if (int.TryParse(GameSet_GridSize_TextBox.Text, out Entered_Size))
            {
                GameSet_GridSize_TextBox.TextDecorations.Clear();
                if (Entered_Size <= 1)
                    GameSet_GridSize_TextBox.TextDecorations = Red_Underline;
            }
            else
                GameSet_GridSize_TextBox.TextDecorations = Red_Underline;
        }

    }
}
