using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Halcon.Window;
using HalconDotNet;

namespace Test
{
    public abstract class ModuleBase
    {
        public abstract string Name { get; set; }
        public int Index { get; set; }

        public ModuleStatus Status { get; set; }



        public Process Process { get; set; }


        public HalconWindow HWindow { get; set; }


        //public Dictionary<string, HObject> InHObjectParams = new Dictionary<string, HObject>();
        //public Dictionary<string, HTuple> InHTupleParams = new Dictionary<string, HTuple>();

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

            //FieldInfo[] properties = GetType().GetFields(BindingFlags.NonPublic);
            //foreach (FieldInfo item in properties)
            //{
            //    if (item.FieldType == typeof(HObject))
            //    {
            //        InHObjectParams.Add(item.Name, item.GetValue(this) as HObject);
            //    }
            //    else if (item.FieldType == typeof(HTuple))
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


        public virtual void Save(/*XmlDocument doc, XmlElement parentNode*/)
        {
            FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (var item in fields)
            {
                if (item.FieldType == typeof(HObject))
                {
                    HObject obj = (HObject)item.GetValue(this);
                    if (obj != null && obj.IsInitialized())
                    {
                        string path = $"projects\\{Process.Project.Name}\\{Process.Name}\\[{Index}]{Name}";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        path = $"{path}\\{item.Name}.obj".Replace("\\", "/");
                        HOperatorSet.WriteObject(obj, path);
                    }
                    
                }
                else if (item.FieldType == typeof(HTuple))
                {
                    HTuple tuple = (HTuple)item.GetValue(this);
                    if (tuple != null && tuple.Length > 0)
                    {
                        string path = $"projects\\{Process.Project.Name}\\{Process.Name}\\[{Index}]{Name}";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        path = $"{path}\\{item.Name}.tup".Replace("\\", "/");
                        HOperatorSet.WriteTuple(tuple, path);
                    }
                }
                //else if (item.Name != nameof(OutHObjectParams) && item.Name != nameof(OutHTupleParams))
                //{
                //    XmlElement field = doc.CreateElement("field");
                //    field.SetAttribute("name", item.Name);
                //    field.SetAttribute("value", item.GetValue(this).ToString());
                //    parentNode.AppendChild(field);
                //}
            }
        }


        public virtual void Load(/*XmlDocument doc, XmlElement parentNode*/)
        {
            FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (var item in fields)
            {
                if (item.FieldType == typeof(HObject))
                {
                    string fileName = $"projects\\{Process.Project.Name}\\{Process.Name}\\[{Index}]{Name}\\{item.Name}.obj";
                    if (File.Exists(fileName))
                    {
                        HObject obj = new HObject();
                        HOperatorSet.ReadObject(out obj, fileName);
                        item.SetValue(this, obj);
                    }
                }
                else if (item.FieldType == typeof(HTuple))
                {
                    string fileName = $"projects\\{Process.Project.Name}\\{Process.Name}\\[{Index}]{Name}\\{item.Name}.tup";
                    if (File.Exists(fileName))
                    {
                        HTuple tuple = new HTuple();
                        HOperatorSet.ReadTuple(fileName, out tuple);
                        item.SetValue(this, tuple);
                    }
                }
                //else
                //{
                //    string xpath = $"field[@name=\"{item.Name}\"]";
                //    XmlElement node = parentNode.SelectSingleNode(xpath) as XmlElement;
                    
                //    if (node != null)
                //    {
                //        object value = null;
                //        string strValue = node.GetAttribute("value");

                //        // 类型转换
                //        TypeConverter converter = TypeDescriptor.GetConverter(item.FieldType);
                //        if (converter.CanConvertFrom(strValue.GetType()))
                //        {
                //            value = converter.ConvertFrom(strValue);
                //            item.SetValue(this, value);
                //        }
                //        else
                //        {
                //            converter = TypeDescriptor.GetConverter(strValue);
                //            if (converter.CanConvertTo(item.FieldType))
                //            {
                //                value = converter.ConvertTo(item, item.FieldType);
                //                item.SetValue(this, value);
                //            }
                //        }
                //    }
                //}
            }
        }


        public abstract bool Run();

        public abstract void ShowDialog();


        public HObject GetHObject(string path)
        {
            string[] array = path.Split('.');

            if (array.Length != 3 || array[2] == "(集合)")
            {
                return null;
            }

            string process = array[0];
            int index = Convert.ToInt32(array[1].Split('[', ']')[1]) - 1;
            string name = array[2];

            foreach (var item in Process.Project)
            {
                if (item.Name == process)
                {
                    return item[index].OutHObjectParams[name];
                }
            }

            return null;
        }


        public HTuple GetHTuple(string path)
        {
            string[] array = path.Split('.');

            if (array.Length != 3 || array[2] == "(集合)")
            {
                return null;
            }

            string process = array[0];
            int index = Convert.ToInt32(array[1].Split('[', ']')[1]) - 1;
            string name = array[2];

            foreach (var item in Process.Project)
            {
                if (item.Name == process)
                {
                    return item[index].OutHTupleParams[name];
                }
            }

            return null;
        }

    }


    public enum ModuleStatus { Null, Disable, OK, NG }
}
