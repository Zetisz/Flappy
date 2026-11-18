using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Flappy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        Movement movementSys;
        Label burd = new()
        {
            Width = 50,
            Height = 50,
            Background = Brushes.GreenYellow
        };
        List<Label> obstacles = [];
        bool isGoing = false;
        
        

        public MainWindow()
        {
            InitializeComponent();
            CompositionTarget.Rendering += OnRender;
        }

        private void OnRender(object sender, EventArgs e)
        {
            if (isGoing)
            {
                movementSys.Move(burd);
                movementSys.ObstacleMovement(obstacles);
                IsColliding();
            }
        }


        private void IsColliding()
        {
            var burdRect = new Rect(Canvas.GetRight(burd), Canvas.GetTop(burd), burd.Width, burd.Height);
            var pipeTopRect = new Rect(Canvas.GetRight(obstacles[0]), Canvas.GetTop(obstacles[0]), obstacles[0].Width, obstacles[0].Height);
            var pipeBottomRect = new Rect(Canvas.GetRight(obstacles[1]), Canvas.GetTop(obstacles[1]), obstacles[1].Width, obstacles[1].Height);

            if (burdRect.IntersectsWith(pipeBottomRect) || burdRect.IntersectsWith(pipeTopRect))
            {
                isGoing = false;
                MessageBox.Show("Game Over!");
            }
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e) // Start Game & Setup
        {
            GridMain.Children.Clear();

            var canv = new Canvas() { Background = Brushes.AliceBlue };

            var spawnIndex = 0;
            for (int i = 0; i < 2; i++) // init obstacles
            {
                var obstacle = new Label()
                {
                    Width = 70,
                    Height = 500,
                    Background = Brushes.DarkOliveGreen
                };

                canv.Children.Add(obstacle);
                obstacles.Add(obstacle);
                Canvas.SetRight(obstacle, 0 - spawnIndex * 100);

                if (i % 2 == 0)
                {
                    Canvas.SetTop(obstacle, -200);
                }
                else
                {
                    Canvas.SetTop(obstacle, 500);
                    spawnIndex++;
                }
                
            }

            canv.Children.Add(burd);
            GridMain.Children.Add(canv);
            Canvas.SetRight(burd, this.Width - 150);
            Canvas.SetTop(burd, 200);

            isGoing = true; // Game has started
        }
    }
}