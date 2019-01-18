using System.Collections.Generic;

namespace Module
{
    public class Project
    {
        private static Project instance;
        private static readonly object locker = new object();

        public string Name { get; set; }

        public Process MainProcess { get; set; }

        public Dictionary<string, Process> Items { get; set; } = new Dictionary<string, Process>();

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

        private Project()
        {

        }

        /// <summary>
        /// 获取Project单个实例
        /// </summary>
        /// <returns></returns>
        public static Project GetInstance()
        {
            // 双重锁定
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new Project();
                    }
                }
            }

            return instance;
        }

        public void Clear()
        {
            Items.Clear();
            MainProcess = null;
            Name = string.Empty;
        }
    }
}
