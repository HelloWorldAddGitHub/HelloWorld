using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camera
{
    public interface IIntParam
    {
        long GetIncrement();
        long GetMaximum();
        long GetMinimum();
        long GetValue();
        void SetValue(long value);
    }
}
