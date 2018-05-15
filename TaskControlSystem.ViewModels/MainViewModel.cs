using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Expression.Interactivity.Core;
using TaskControlSystem.ViewModels.Commands;
using System.ComponentModel.Composition;
using TaskControlSystem.Infrastructure;
using TaskControlSystem.BusinessLogic;
using TaskControlSystem.BusinessLogic.TaskViewModels;
using TaskControlSystem.DataAccess.Models;

namespace TaskControlSystem.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<SystemTask> _tasks;
        private SystemTask _selectedTask;
        private CreateTaskViewModel _createTaskViewModel;
        private DelegateCommand _createTaskCommand;

        public ObservableCollection<SystemTask> Tasks
        {
            get => _tasks;
            set
            {
                _tasks = value;
                OnPropertyChanged();
            }
        }

        public CreateTaskViewModel CreateTaskViewModel
        {
            get => _createTaskViewModel;
            set
            {
                _createTaskViewModel = value;
                OnPropertyChanged();
            }
        }

        public SystemTask SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                //IsVisibleCreateView = false;
                //IsVisibleEditView = true;
                OnPropertyChanged();
            }
        }

        public DelegateCommand CreateTaskCommand
        {
            get => _createTaskCommand ?? (_createTaskCommand = new DelegateCommand(CreateTask));
        }

        public MainViewModel()
        {
            CreateTaskViewModel = new CreateTaskViewModel();
        }

        public void CreateTask()
        {
            CreateTaskOperation.Execute(CreateTaskViewModel);
            ReloadTasks();
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

        [Import]
        public ICreateTaskOperation CreateTaskOperation { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
