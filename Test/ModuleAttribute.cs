using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class ModuleAttribute : Attribute
    {
        public string Name { get; }

        public string Category { get; }

        public ModuleAttribute(string name, string category)
        {
            Name = name;
            Category = category;
        }
    }
}
