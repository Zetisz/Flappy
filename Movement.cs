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
        internal void Move(Label burd) // Burd movement
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

        internal void ObstacleMovement(List<Label> obstacles) // Obstacle Movement
        {
            int randomX = rand.Next(-200, 200); // random num for height change
            for (int i = 0; i < obstacles.Count(); i++)
            {
                double newPos = Canvas.GetRight(obstacles[i]) + obstacleSpeed++ / 1000; // speeds up over time
                Canvas.SetRight(obstacles[i], newPos);

                if (newPos > 600)
                {
                    Canvas.SetRight(obstacles[i], -100);

                    if (i % 2 == 0)
                    {
                        Canvas.SetTop(obstacles[i], -200 + randomX);
                    }
                    else
                    {
                        Canvas.SetTop(obstacles[i], 500 - randomX);
                    }
                }
            }
        }
    }
}
