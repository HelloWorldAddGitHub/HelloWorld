using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Project : List<Process>
    {
        public string Name { get; set; }

        public Process this[string name]
        {
            get
            {
                Predicate<Process> pre = (p) =>
                {
                    return p.Name == name ? true : false;
                };

                return Exists(pre) ? Find(pre) : null;
            }
            set
            {
                Predicate<Process> pre = (p) =>
                {
                    return p.Name == name ? true : false;
                };

                RemoveAll(pre);
                Add(value);
            }
        }

        public Process MainProcess { get; set; }

        public Project(string name)
        {
            Name = name;
        }
    }
}
