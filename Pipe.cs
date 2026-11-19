using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Flappy
{
    internal class Pipe(Image top, Image bottom)
    {
        public Image Top { get; set; } = top;
        public Image Bottom { get; set; } = bottom;
        
        public bool HasScored { get; set; } = false;
    }
}
