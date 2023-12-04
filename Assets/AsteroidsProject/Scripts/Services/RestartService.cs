using AsteroidsProject.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AsteroidsProject.Services
{
    public class RestartService : IRestartService, IDisposable
    {
        private readonly List<IRestartable> restartables;

        public RestartService(IEnumerable<IRestartable> restartables)
        {
            this.restartables = restartables.ToList();
        }

        public void Restart()
        {
            foreach (var restartable in this.restartables)
            {
                restartable.Restart();
            }
        }

        public void Dispose()
        {
            restartables.Clear();
        }
    }
}