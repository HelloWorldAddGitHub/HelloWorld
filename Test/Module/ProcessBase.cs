using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class ProcessBase : List<ModuleBase>
    {
        //public List<ModuleBase> Modules = new List<ModuleBase>();
        public string Name { get; set; }

        public ProcessBase(string name)
        {
            Name = name;
        }


    }
}
