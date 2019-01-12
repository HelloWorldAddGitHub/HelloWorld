using System.Collections.Generic;

namespace Module
{
    public class Project
    {
        public Dictionary<string, Process> Items { get; set; } = new Dictionary<string, Process>();

        public string Name { get; set; }

        public Process this[string name]
        {
            get
            {
                //Predicate<Process> pre = (p) =>
                //{
                //    return p.Name == name ? true : false;
                //};

                //return Exists(pre) ? Find(pre) : null;
                Process value;
                Items.TryGetValue(name, out value);
                return value;
            }
            //set
            //{
            //    Predicate<Process> pre = (p) =>
            //    {
            //        return p.Name == name ? true : false;
            //    };

            //    RemoveAll(pre);
            //    Add(value);
            //}
        }

        public Process MainProcess { get; set; }

        public Project(string name)
        {
            Name = name;
        }
    }
}
