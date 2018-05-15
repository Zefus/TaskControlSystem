using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ComponentModel.Composition;
using TaskControlSystem.Infrastructure;
using TaskControlSystem.DataAccess.Models;

namespace TaskControlSystem.ViewModels
{
    [Export]
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<SystemTask> _tasks;
        private SystemTask _selectedTask;

        public ObservableCollection<SystemTask> Tasks
        {
            get { return _tasks; }
            set
            {
                _tasks = value;
                OnPropertyChanged();
            }
        }

        public SystemTask SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                //IsVisibleCreateView = false;
                //IsVisibleEditView = true;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
        }

        [Import(typeof(IRepositoryProvider))]
        private IRepositoryProvider RepositoryProvider { get; set; }
        public void ReloadTasks()
        {
            Tasks = new ObservableCollection<SystemTask>(
                RepositoryProvider
                .GetRepository<SystemTask>()
                .GetAll()
                .Where(t => t.ParentSystemTask == null)
                .ToList());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
