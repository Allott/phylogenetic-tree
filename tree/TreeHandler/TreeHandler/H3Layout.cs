using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeHandler
{
    class H3Layout
    {
        public double x;//co-ordinates is cartisian space
        public double y;
        public double z;

        public double r = 0;

        public H3Layout()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        public string GetCartisianPosition()
        {
            return x + " " + y + " " + z;
        }
    }
}
