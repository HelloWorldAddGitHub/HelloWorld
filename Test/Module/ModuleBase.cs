using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public abstract class ModuleBase
    {
        public abstract string Name { get; set; }
        public int Index { get; set; }

        //public abstract Form SetupForm { get; set; }

        public Dictionary<string, object> InParams { get; set; } = new Dictionary<string, object>();
        public Dictionary<string, object> OutParams = new Dictionary<string, object>();
        
        public ModuleBase()
        {
            Type type = GetType();

            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo item in properties)
            {
                InParams.Add(item.Name, item.GetValue(this));
            }

            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo item in fields)
            {
                OutParams.Add(item.Name, item.GetValue(this));
            }
        }

        public abstract bool Run();

        public abstract void ShowDialog();

    }
}
