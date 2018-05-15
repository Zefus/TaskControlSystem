using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using TaskControlSystem.DataAccess.Models;
using TaskControlSystem.Infrastructure;
using TaskControlSystem.Infrastructure.Repository;
using TaskControlSystem.BusinessLogic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace TaskControlSystem.ConsoleTerminal
{
    class Program
    {
        [Import]
        public IRepositoryProvider repositoryProvider { get; set; }
        [Import]
        public IRepository<SystemTask> Repository { get; set; }

        public CompositionContainer _container;

        private Program()
        {
            var assembly = new AssemblyCatalog(typeof(Program).Assembly);
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(assembly);
            catalog.Catalogs.Add(new DirectoryCatalog("."));
            _container = new CompositionContainer(catalog);

            //Fill the imports of this object  
            try
            {
                _container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                System.Console.WriteLine(compositionException.ToString());
                System.Console.ReadKey();
            }
        }

        static void Main(string[] args)
        {
            Program p = new Program();
        }
    }
}
