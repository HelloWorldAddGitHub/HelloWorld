using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Halcon.Window;
using HalconDotNet;

namespace Module
{
    /// <summary>
    /// 模块基类，在派生类中需要增加特性
    /// [Module("名称", "类别")]
    /// </summary>
    public abstract class ModuleBase
    {
        public int Index { get; set; }

        public abstract string Name { get; set; }
        

        //public ModuleStatus Status { get; set; }

        public bool IsSaveHObject;

        public Process Process;


        public HalconWindow HWindow;


        //public Dictionary<string, HObject> InHObjectParams = new Dictionary<string, HObject>();
        //public Dictionary<string, HTuple> InHTupleParams = new Dictionary<string, HTuple>();

        public Dictionary<string, HObject> OutHObjectParams = new Dictionary<string, HObject>();
        public Dictionary<string, HTuple> OutHTupleParams = new Dictionary<string, HTuple>();

        


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


        public virtual void Save()
        {
            FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (var item in fields)
            {
                if (item.FieldType == typeof(HObject))
                {
                    if (IsSaveHObject)
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


        public virtual void Load()
        {
            FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (var item in fields)
            {
                if (item.FieldType == typeof(HObject))
                {
                    if (IsSaveHObject)
                    {
                        string fileName = $"projects\\{Process.Project.Name}\\{Process.Name}\\[{Index}]{Name}\\{item.Name}.obj";
                        if (File.Exists(fileName))
                        {
                            HObject obj = new HObject();
                            HOperatorSet.ReadObject(out obj, fileName);
                            item.SetValue(this, obj);
                        }
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


    //public enum ModuleStatus { Null, Disable, OK, NG }
}
