using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeHandler
{
    class H3Layout
    {
        public int x;//co-ordinates is cartisian space
        public int y;
        public int z;


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
