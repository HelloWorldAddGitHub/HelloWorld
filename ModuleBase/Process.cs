using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Module
{
    public class Process : List<ModuleBase>
    {
        public string Name { get; set; }
        

        private CancellationTokenSource runCancelToken;

        public Project Project { get; set; }

        public Process(string name, Project project)
        {
            Name = name;
            Project = project;
        }


        public void RunOne()
        {
            runCancelToken?.Cancel();
            runCancelToken = new CancellationTokenSource();

            Task.Run(() =>
            {
                foreach (var item in this)
                {
                    if (runCancelToken.IsCancellationRequested)
                    {
                        return;
                    }

                    item.Run();
                }
            });
        }


        public void Start()
        {
            runCancelToken?.Cancel();
            runCancelToken = new CancellationTokenSource();

            Task.Run(() =>
            {
                while (true)
                {
                    foreach (var item in this)
                    {
                        if (runCancelToken.IsCancellationRequested)
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
            runCancelToken?.Cancel();
        }


        public static implicit operator string(Process process)
        {
            return process.Name;
        }
    }
}
