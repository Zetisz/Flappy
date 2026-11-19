using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        double obstacleSpeed = 1000;
        internal void BurdMovement(Label burd) // Burd movement
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
            // Speed up
            Canvas.SetRight(pipe.Top, Canvas.GetRight(pipe.Top) + obstacleSpeed++ / 10000);
            Canvas.SetRight(pipe.Bottom, Canvas.GetRight(pipe.Bottom) + obstacleSpeed++ / 10000);

            // If pipe goes out of screen
            if (Canvas.GetRight(pipe.Top) > 600)
            {
                // Find the pipe furthest to the right
                var furthestRight = double.MaxValue;
                
                foreach (Pipe otherPipe in pipes)
                {
                    if (otherPipe != pipe)
                    {
                        var otherRight = Canvas.GetRight(otherPipe.Top);

                        if (otherRight < furthestRight)
                            furthestRight = otherRight;
                    }
                }
                
                // Position this pipe behind the other pipe
                Canvas.SetRight(pipe.Top, furthestRight - 350);
                Canvas.SetRight(pipe.Bottom, furthestRight - 350);

                // Generate a random vertical offset
                var randomY = rand.Next(-100, 101);

                Canvas.SetTop(pipe.Top, -200 + randomY);
                Canvas.SetTop(pipe.Bottom, 500 + randomY);
            }
        }
    }
}
