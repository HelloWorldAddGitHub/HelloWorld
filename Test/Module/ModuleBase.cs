using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Halcon.Window;
using HalconDotNet;

namespace Test
{
    public abstract class ModuleBase
    {
        public abstract string Name { get; set; }
        public int Index { get; set; }

        
        public Process Process { get; set; }


        public HalconWindow HWindow { get; set; }


        //public Dictionary<string, HObject> InHObjectParams { get; set; } = new Dictionary<string, HObject>();
        //public Dictionary<string, HTuple> InHTupleParams { get; set; } = new Dictionary<string, HTuple>();

        public Dictionary<string, HObject> OutHObjectParams = new Dictionary<string, HObject>();
        public Dictionary<string, HTuple> OutHTupleParams = new Dictionary<string, HTuple>();


        //public Dictionary<string, object> OutParams = new Dictionary<string, object>();


        public ModuleBase()
        {
            InitParam();
        }


        public ModuleBase(Process process, HalconWindow window)
        {
            Process = process;
            HWindow = window;

            InitParam();
        }


        private void InitParam()
        {
            //Type type = GetType();

            //PropertyInfo[] properties = type.GetProperties();
            //foreach (PropertyInfo item in properties)
            //{
            //    if (item.PropertyType == typeof(HObject))
            //    {
            //        InHObjectParams.Add(item.Name, item.GetValue(this) as HObject);
            //    }
            //    else if (item.PropertyType == typeof(HTuple))
            //    {
            //        InHTupleParams.Add(item.Name, item.GetValue(this) as HTuple);
            //    }
            //}

            FieldInfo[] fields = GetType().GetFields();
            foreach (FieldInfo item in fields)
            {
                if (item.FieldType == typeof(HObject))
                {
                    OutHObjectParams.Add(item.Name, item.GetValue(this) as HObject);
                }
                else if (item.FieldType == typeof(HTuple))
                {
                    OutHTupleParams.Add(item.Name, item.GetValue(this) as HTuple);
                }
            }
        }



        public abstract bool Run();

        public abstract void ShowDialog();

    }
}
