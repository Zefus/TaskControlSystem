using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Reflection;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Microsoft.Mef.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;
using TaskControlSystem.ViewModels;

namespace TaskControlSystem.Views
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var assembly = new AssemblyCatalog(Assembly.GetEntryAssembly());
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(assembly);
            catalog.Catalogs.Add(new DirectoryCatalog("."));
            var _container = new CompositionContainer(catalog);
            var mefServiceLocator = new MefServiceLocator(_container);
            ServiceLocator.SetLocatorProvider(() => mefServiceLocator);
            _container.ComposeExportedValue<IServiceLocator>(mefServiceLocator);
            MainViewModel viewModel = new MainViewModel();
            MainWindow window = new MainWindow();
            window.DataContext = viewModel;

            try
            {
                _container.ComposeParts(viewModel);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }

            viewModel.ReloadTasks();
            window.Show();
        }

        //[Import(RequiredCreationPolicy = CreationPolicy.Shared)]
        //public IServiceLocator Locator { get; set; }
    }
}
