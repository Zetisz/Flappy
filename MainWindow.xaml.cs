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
        private Movement movementSys;
        private Label burd;
        private bool isGoing;
        private List<Pipe> pipes = [];
        private int pipeNumber = 2;
        
        public MainWindow()
        {
            InitializeComponent();
            movementSys = new Movement();
            CompositionTarget.Rendering += OnRender;
        }

        private void OnRender(object sender, EventArgs e)
        {
            if (isGoing)
            {
                movementSys.BurdMovement(burd);
                
                foreach (var pipe in pipes)
                {
                    movementSys.PipeMovement(pipe, pipes);
                    IsColliding(pipe);
                }
            }
        }


        private void IsColliding(Pipe pipe)
        {
            var burdRect = new Rect(Canvas.GetRight(burd), Canvas.GetTop(burd), burd.Width, burd.Height);
            var pipeTopRect = new Rect(Canvas.GetRight(pipe.Top), Canvas.GetTop(pipe.Top), pipe.Top.Width, pipe.Top.Height);
            var pipeBottomRect = new Rect(Canvas.GetRight(pipe.Bottom), Canvas.GetTop(pipe.Bottom), pipe.Bottom.Width, pipe.Bottom.Height);

            if (burdRect.IntersectsWith(pipeBottomRect) || burdRect.IntersectsWith(pipeTopRect))
            {
                isGoing = false;
                MessageBox.Show("Game Over!");
            }
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e) // Start Game & Setup
        {
            GridMain.Children.Clear();
            pipes.Clear();

            var canv = new Canvas() { Background = Brushes.AliceBlue };

            // init burd
            burd = new Label
            {
                Width = 50,
                Height = 50,
                Background = Brushes.GreenYellow
            };

            canv.Children.Add(burd);
            GridMain.Children.Add(canv);
            Canvas.SetRight(burd, this.Width - 150);
            Canvas.SetTop(burd, 200);

            // init pipe
            
            for (var i = 0; i < pipeNumber; i++) // init obstacles
            {
                var part1 = new Rectangle
                {
                    Width = 70,
                    Height = 500,
                    Fill = Brushes.DarkOliveGreen
                };
                
                var part2 = new Rectangle
                {
                    Width = 70,
                    Height = 500,
                    Fill = Brushes.DarkOliveGreen
                };

                var pipe = new Pipe(part1, part2);

                canv.Children.Add(pipe.Top);
                canv.Children.Add(pipe.Bottom);
                
                Canvas.SetRight(pipe.Top, -i * 350);
                Canvas.SetRight(pipe.Bottom, -i * 350);
                Canvas.SetTop(pipe.Top, -200);
                Canvas.SetTop(pipe.Bottom, 500);
                pipes.Add(pipe);
            }
            isGoing = true; // Game has started
        }
    }
}