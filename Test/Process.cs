using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Process : List<ModuleBase>
    {
        //public List<ModuleBase> Modules = new List<ModuleBase>();
        public string Name { get; set; }
        

        //public Project Project { get; set; }

        public Process(string name/*, Project project*/)
        {
            Name = name;
            //Project = project;
        }

        public bool Start()
        {
            foreach (var item in this)
            {
                item.Run();
            }

            return true;
        }

        public void Stop()
        {
            
        }
    }
}
