using System;

namespace Module
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
