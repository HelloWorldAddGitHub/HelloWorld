using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using HalconDotNet;

namespace Module
{
    /// <summary>
    /// 模块基类，在派生类中需要增加特性
    /// [Module("名称", "类别")]
    /// </summary>
    public abstract class ModuleBase : IDisposable
    {
        public int Index { get; set; }

        public abstract string Name { get; set; }


        public Process Owner;


        public Dictionary<string, HObject> OutHObjectParams = new Dictionary<string, HObject>();
        public Dictionary<string, HTuple> OutHTupleParams = new Dictionary<string, HTuple>();




        public ModuleBase()
        {
            InitParam();
        }


        public ModuleBase(Process process)
        {
            Owner = process;
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
            //FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            PropertyInfo[] properties = GetType().GetProperties();


            foreach (var property in properties/*fields*/)
            {
                //if (property.PropertyType == typeof(HObject))
                //{
                //    if (IsSaveHObject)
                //    {
                //        HObject obj = (HObject)property.GetValue(this);
                //        if (obj != null && obj.IsInitialized())
                //        {
                //            string path = $"projects\\{Owner.Owner.Name}\\{Owner.Name}\\[{Index}]{Name}";
                //            if (!Directory.Exists(path))
                //            {
                //                Directory.CreateDirectory(path);
                //            }

                //            path = $"{path}\\{property.Name}.obj".Replace("\\", "/");
                //            HOperatorSet.WriteObject(obj, path);
                //        }
                //    }
                //}
                /*else */
                if (property.PropertyType == typeof(HTuple))
                {
                    HTuple tuple = (HTuple)property.GetValue(this);
                    if (tuple != null && tuple.Length > 0)
                    {
                        string path = $"projects\\{Project.GetInstance().Name}\\{Owner.Name}\\[{Index}]{Name}";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }


                        path = $"{path}\\{property.Name}.HTuple".Replace("\\", "/");
                        HOperatorSet.WriteTuple(tuple, path);
                    }
                }
            }
        }


        public virtual void Load()
        {
            //FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            PropertyInfo[] properties = GetType().GetProperties();

            foreach (var property in properties/*fields*/)
            {
                //if (property.PropertyType == typeof(HObject))
                //{
                //    if (IsSaveHObject)
                //    {
                //        string fileName = $"projects\\{Owner.Owner.Name}\\{Owner.Name}\\[{Index}]{Name}\\{property.Name}.obj";
                //        if (File.Exists(fileName))
                //        {
                //            HObject obj = new HObject();
                //            HOperatorSet.ReadObject(out obj, fileName);
                //            property.SetValue(this, obj);
                //        }
                //    }
                //}
                /*else */
                if (property.PropertyType == typeof(HTuple))
                {
                    string fileName = $@".\projects\{Project.GetInstance().Name}\{Owner.Name}\[{Index}]{Name}\{property.Name}.HTuple";
                    if (File.Exists(fileName))
                    {
                        HTuple tuple = new HTuple();
                        HOperatorSet.ReadTuple(fileName, out tuple);
                        property.SetValue(this, tuple);
                    }
                }
            }
        }


        public abstract bool Execute();

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

            foreach (var item in Project.GetInstance().Items)
            {
                if (item.Key == process)
                {
                    return item.Value[index].OutHObjectParams[name];
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

            foreach (var item in Project.GetInstance().Items)
            {
                if (item.Key == process)
                {
                    return item.Value[index].OutHTupleParams[name];
                }
            }

            return null;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var hObject in OutHObjectParams.Values)
                {
                    hObject.Dispose();
                }
            }
        }
    }
}
