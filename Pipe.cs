using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Shapes;

namespace Flappy
{
    internal class Pipe
    {
        Rectangle top;
        Rectangle bottom;

        public Pipe(Rectangle top, Rectangle bottom)
        {
            Top = top;
            Bottom = bottom;
        }

        public Rectangle Top { get => top; set => top = value; }
        public Rectangle Bottom { get => bottom; set => bottom = value; }
    }
}
