using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Media;


namespace SnakeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary

    public partial class MainWindow : Window
    {
        public string Records_Path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Records.txt";
        public static string Resources_Directory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/SnakeResources";
        public string[] Music_Paths = new[] { Resources_Directory + "/Menu_Music.wav", Resources_Directory + "/Hello_World.wav" };
        public string[] Clicksounds_Paths = new[] { Resources_Directory + "/click1.wav", Resources_Directory + "/click2.wav",
                                                    Resources_Directory + "/click3.wav" };
        public string[] Eatsounds_Paths = new[] { Resources_Directory + "/eat1.wav", Resources_Directory + "/eat2.wav", Resources_Directory + "/eat3.wav",
                                                  Resources_Directory + "/eat4.wav", Resources_Directory + "/eat5.wav", Resources_Directory + "/eat6.wav",
                                                  Resources_Directory + "/eat7.wav"};
        public string[] Deathsounds_Paths = new[] { Resources_Directory + "/dead1.wav", Resources_Directory + "/dead2.wav",
                                                    Resources_Directory + "/dead3.wav", Resources_Directory + "/dead4.wav" };
        public string[] Walksounds_Paths = new[] { Resources_Directory + "/walk1.wav", Resources_Directory + "/walk2.wav",
                                                   Resources_Directory + "/walk3.wav", Resources_Directory + "/walk4.wav"};
        public ObservableCollection<string[]> Records_List = new ObservableCollection<string[]>();
        public string User_Name { get; set; }
        public MediaPlayer Music_Player { get; set; }
        public SoundPlayer Sound_Player { get; set; }
        public int Current_Game_Size { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Main_Window_Loaded(object sender, RoutedEventArgs e)
        {
            Music_Player = new MediaPlayer();
            Sound_Player = new SoundPlayer();
            Music_Player.MediaEnded += Music_Ended;
            Music_Player.Open(new Uri(Music_Paths[0]));
            Music_Player.Play();

            Records_Read();
            if (Records_List.Count > 0)
            {
                User_Name = Records_List[Records_List.Count - 1][0];
                Current_Game_Size = Convert.ToInt32(Records_List[Records_List.Count - 1][2]);
            }
            else
            {
                User_Name = Environment.UserName;
                Current_Game_Size = 5;
            }

            MainFrame.Source = new Uri("./Pages/MainMenu_Page.xaml", UriKind.Relative);
        }

        public void Click_Sound()
        {
            Sound_Player.SoundLocation = Clicksounds_Paths[(new Random()).Next(0, 3)];
            Sound_Player.Play();
        }

        private void Music_Ended(object sender, EventArgs e)
        {
            (sender as MediaPlayer).Position = TimeSpan.Zero;
            (sender as MediaPlayer).Play();
        }

        private void Records_Read()
        {
            if (File.Exists(Records_Path))
            {
                string[] Records = File.ReadAllLines(Records_Path);
                for (int i = 0; i < Records.Length; i++)
                {
                    Records_List.Add(Records[i].Split(':'));
                }
            }
        }

    }
}
