using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Shapes;

namespace Flappy
{
    internal class Pipe(Rectangle top, Rectangle bottom)
    {
        public Rectangle Top { get; set; } = top;
        public Rectangle Bottom { get; set; } = bottom;
        
        public bool HasScored { get; set; } = false;
    }
}
