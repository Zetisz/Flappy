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
        Random rand = new Random();
        bool canJump = true;
        DateTime lastJump;
        int flapCooldown = 200;
        int flapHeight = 80;
        double fallSpeed = 10;
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
                fallSpeed = 10;
            }
            else
            {
                newPos = Canvas.GetTop(burd) + (fallSpeed++ / 10); // Exponential fall speed
            }

            // Check jump cooldown
            if (!canJump && (DateTime.Now - lastJump).TotalMilliseconds >= flapCooldown)
            {
                canJump = true;
            }

            Canvas.SetTop(burd, newPos);
        }

        internal void PipeMovement(Pipe pipe)
        {
            int randomX = rand.Next(-200, 200); // random num for height change

            // Speed up
            Canvas.SetRight(pipe.Top, Canvas.GetRight(pipe.Top) + obstacleSpeed++ / 1000);
            Canvas.SetRight(pipe.Bottom, Canvas.GetRight(pipe.Bottom) + obstacleSpeed++ / 1000);

            // If pipe goes out of screen
            if (Canvas.GetRight(pipe.Top) > 600)
            {
                Canvas.SetRight(pipe.Top, -100);
                Canvas.SetRight(pipe.Bottom, -100);

                Canvas.SetTop(pipe.Top, -200 + randomX);
                Canvas.SetTop(pipe.Bottom, 500 - randomX);
            }
        }
    }
}
