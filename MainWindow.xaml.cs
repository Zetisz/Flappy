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
        Pipe pipe;
        Label burd;
        bool isGoing = false;
        
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
                movementSys.PipeMovement(pipe);
                IsColliding();
            }
        }


        private void IsColliding()
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

            var canv = new Canvas() { Background = Brushes.AliceBlue };

            // init burd
            burd = new()
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

            var part = new Rectangle()
            {
                Width = 70,
                Height = 500,
                Fill = Brushes.DarkOliveGreen
            };

            var part2 = new Rectangle()
            {
                Width = 70,
                Height = 500,
                Fill = Brushes.DarkOliveGreen
            };

            pipe = new Pipe(part, part2);
            canv.Children.Add(pipe.Top);
            canv.Children.Add(pipe.Bottom);

            Canvas.SetRight(pipe.Top, 0);
            Canvas.SetRight(pipe.Bottom, 0);

            Canvas.SetTop(pipe.Top, -200);
            Canvas.SetTop(pipe.Bottom, 500);

            isGoing = true; // Game has started
        }
    }
}