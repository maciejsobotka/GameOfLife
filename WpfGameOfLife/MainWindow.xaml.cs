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
using System.Drawing;
using System.Windows.Threading;

namespace WpfGameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Rectangle> recList{ get; set; }
        private GameOfLife GoL= new GameOfLife();
        private SolidColorBrush green = new SolidColorBrush(Color.FromRgb(0, 255, 0));
        private SolidColorBrush white = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        DispatcherTimer gameUpdateTimer = new DispatcherTimer();
        DispatcherTimer iterationTimer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            recList = new List<Rectangle>();
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++ )
                {
                    recList.Add(new Rectangle { Width = 20, Height = 20, Stroke = Brushes.Red });
                    gameArea.Children.Add(recList[20 * i + j]);
                    Canvas.SetLeft(recList[20 * i + j], j * 20);
                    Canvas.SetTop(recList[20 * i + j], i * 20);
                }
            GoL.GenerateWorld();
            UpdateGameArea();

            gameUpdateTimer.Tick += gameUpdateTimer_Tick;
            gameUpdateTimer.Interval = TimeSpan.FromSeconds(0.0);

            iterationTimer.Tick += iterationTimer_Tick;
            iterationTimer.Interval = TimeSpan.FromSeconds(0.1);
        }

        private void gameUpdateTimer_Tick(object sender, EventArgs e)
        {
            UpdateGameArea();
            gameArea.UpdateLayout();
        }
        private void iterationTimer_Tick(object sender, EventArgs e)
        {
            PlayGame();
        }
        private void UpdateGameArea()
        {
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                {
                    if (GoL.IsCellAlive(i + 1, j + 1))
                        recList[20 * i + j].Fill = green;
                    else
                        recList[20 * i + j].Fill = white;
                }
        }
        private void PlayGame()
        {
            System.Threading.Thread.Sleep(500);
            GoL.Iteration();
        }
        private void generateButton_Click(object sender, RoutedEventArgs e)
        {
            iterationTimer.Stop();
            gameUpdateTimer.Stop();
            GoL.GenerateWorld();
            UpdateGameArea();
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            iterationTimer.Start();
            gameUpdateTimer.Start();
            
        }
    }
}
