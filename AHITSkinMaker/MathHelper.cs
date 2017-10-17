using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHITSkinMaker
{
    public static class MathHelper
    {
        public static int Clamp(int v, int low, int high)
        {
            return v < low ? low : v > high ? high : v;
        }
    }
}
