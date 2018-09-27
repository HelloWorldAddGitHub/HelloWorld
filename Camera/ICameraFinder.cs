using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camera
{
    interface ICameraFinder
    {
        void Find(out int[] cameras);
        void Find(out string[] cameras);
    }
}
