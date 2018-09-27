using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camera
{
    public struct IntParam
    {
        // 步长
        public long Inc { get; set; }

        // 最大值
        public long Max { get; set; }

        // 最小值
        public long Min { get; set; }

        // 获取或设置值
        public long Value { get; set; }
    }
}
