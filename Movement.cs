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
        private int flapStrength = 300;
        private double gravity = 800;
        double flapCooldown;
        private double velocity;
        private double pipeSpeed = 2;
        private DateTime lastFrame = DateTime.Now;
        
        internal void BurdMovement(Rectangle burd) // Burd movement
        {
            var currentFrame = DateTime.Now;
            var deltaTime = (currentFrame - lastFrame).TotalSeconds;
            lastFrame = currentFrame;
            var currentPos = Canvas.GetTop(burd);
            
            deltaTime = Math.Min(deltaTime, 0.05);

            if (Canvas.GetTop(burd) >= 800) // Game Over
            {
                MessageBox.Show("Game Over");
            }

            if (Keyboard.IsKeyDown(Key.Space) && canJump && (Canvas.GetTop(burd) - flapStrength > 0)) // flap
            {
                velocity = -flapStrength;
                canJump = false;
                flapCooldown = 0;
            }
            
            // Allow another flap after releasing Space
            if (!Keyboard.IsKeyDown(Key.Space) || ++flapCooldown > 30)
            {
                canJump = true;
            }
            
            // Apply forces
            velocity += gravity * deltaTime;
            currentPos += velocity * deltaTime;

            Canvas.SetTop(burd, currentPos);
        }

        internal void PipeMovement(Pipe pipe, List<Pipe> pipes)
        {
            Canvas.SetLeft(pipe.Top, Canvas.GetLeft(pipe.Top) - pipeSpeed);
            Canvas.SetLeft(pipe.Bottom, Canvas.GetLeft(pipe.Bottom) - pipeSpeed);

            // If pipe goes out of screen
            if (Canvas.GetLeft(pipe.Top) < -pipe.Top.Width)
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
                
                pipe.HasScored = false;
            }
        }
    }
}
