using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Module
{
    public class Process
    {
        public List<ModuleBase> Items { get; set; } = new List<ModuleBase>();

        public string Name { get; set; }


        private CancellationTokenSource runCancelToken;

        //public Project Owner { get; set; }

        public Process(string name/*, Project project*/)
        {
            Name = name;
            //Owner = project;
        }


        public ModuleBase this[int index]
        {
            get
            {
                return Items[index];
            }
        }


        public void ExecuteOne()
        {
            runCancelToken?.Cancel();
            runCancelToken?.Dispose();
            runCancelToken = new CancellationTokenSource();

            Task.Run(() =>
            {
                foreach (var unit in Items)
                {
                    unit.Execute();

                    if (runCancelToken.IsCancellationRequested)
                    {
                        return;
                    }
                }
            });
        }


        public void ExecuteStart()
        {
            runCancelToken?.Cancel();
            runCancelToken?.Dispose();
            runCancelToken = new CancellationTokenSource();

            Task.Run(() =>
            {
                while (true)
                {
                    foreach (var unit in Items)
                    {
                        unit.Execute();

                        if (runCancelToken.IsCancellationRequested)
                        {
                            return;
                        }
                    }
                }
            });
        }

        public void ExecuteStop()
        {
            runCancelToken?.Cancel();
        }
    }
}
