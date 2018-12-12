using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Module;

namespace Project
{
    public class Process : List<ModuleBase>
    {
        public string Name { get; set; }
        
        private CancellationTokenSource cts = new CancellationTokenSource();

        public Project Project { get; set; }

        public Process(string name, Project project)
        {
            Name = name;
            Project = project;
        }


        public void RunOne()
        {
            cts.Cancel();
            cts.Dispose();
            cts = new CancellationTokenSource();

            Task.Run(() =>
            {
                foreach (var item in this)
                {
                    if (cts.IsCancellationRequested)
                    {
                        return;
                    }

                    item.Run();
                }
            });
        }


        public void Start()
        {
            cts.Cancel();
            cts.Dispose();
            cts = new CancellationTokenSource();

            Task.Run(() =>
            {
                while (true)
                {
                    foreach (var item in this)
                    {
                        if (cts.IsCancellationRequested)
                        {
                            return;
                        }

                        item.Run();

                        Thread.Sleep(200);
                    }
                }
            });
        }

        public void Stop()
        {
            cts?.Cancel();
        }
    }
}
