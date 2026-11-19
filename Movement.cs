using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Flappy
{
    internal class Movement
    {
        Random rand = new();
        bool canJump = true;
        DateTime lastJump;
        int flapCooldown = 200;
        private int flapHeight = 80;
        double gravity = 3;
        double pipeSpeed = 2;
        
        internal void BurdMovement(Rectangle burd) // Burd movement
        {
            double newPos;

            if (Canvas.GetTop(burd) >= 800) // Game Over
            {
                MessageBox.Show("Game Over");
            }

            if (Keyboard.IsKeyDown(Key.Space) && canJump && (Canvas.GetTop(burd) - flapHeight > 0)) // flap
            {
                newPos = Canvas.GetTop(burd) - flapHeight;
                canJump = false;
                lastJump = DateTime.Now;
                gravity = 3;
            }
            else
            {
                newPos = Canvas.GetTop(burd) + (gravity++ / 20); // Exponential fall speed
            }

            // Check jump cooldown
            if (!canJump && (DateTime.Now - lastJump).TotalMilliseconds >= flapCooldown)
            {
                canJump = true;
            }

            Canvas.SetTop(burd, newPos);
        }

        internal void PipeMovement(Pipe pipe, List<Pipe> pipes)
        {
            Canvas.SetLeft(pipe.Top, Canvas.GetLeft(pipe.Top) - pipeSpeed);
            Canvas.SetLeft(pipe.Bottom, Canvas.GetLeft(pipe.Bottom) - pipeSpeed);

            // If pipe goes out of screen
            if (Canvas.GetLeft(pipe.Top) < 0 - pipe.Top.Width)
            {
                // Find the pipe furthest to the right
                var furthestRight = double.MaxValue;
                
                foreach (var otherPipe in pipes)
                {
                    if (otherPipe != pipe)
                    {
                        var otherRight = Canvas.GetLeft(otherPipe.Top);

                        if (otherRight < furthestRight)
                            furthestRight = otherRight;
                    }
                }
                
                // Pos the pipe behind the other pipe
                Canvas.SetLeft(pipe.Top, furthestRight + 350);
                Canvas.SetLeft(pipe.Bottom, furthestRight + 350);

                // random vertical offset
                var randomY = rand.Next(-200, 200);

                Canvas.SetTop(pipe.Top, -200 + randomY);
                Canvas.SetTop(pipe.Bottom, 500 + randomY);
            }
        }
    }
}
