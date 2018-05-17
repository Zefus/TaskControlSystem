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
        private DelegateCommand _createTaskCommand;
        private DelegateCommand _editTaskCommand;
        private DelegateCommand _showCreateViewCommand;
        private DelegateCommand _cancelCommand;
        private bool _isVisibleCreateView;
        private bool _isVisibleCreateButton;
        private bool _isTerminalTask;
        private bool _isVisibleEditingView;

        public ObservableCollection<SystemTask> Tasks
        {
            get => _tasks;
            set
            {
                _tasks = value;
                OnPropertyChanged();
            }
        }

        public bool IsVisibleCreateView
        {
            get => _isVisibleCreateView;
            set
            {
                _isVisibleCreateView = value;
                OnPropertyChanged();
            }
        }

        public bool IsVisibleEditingView
        {
            get => _isVisibleEditingView;
            set
            {
                _isVisibleEditingView = value;
                OnPropertyChanged();
            }
        }

        public bool IsVisibleCreateButton
        {
            get => _isVisibleCreateButton;
            set
            {
                _isVisibleCreateButton = value;
                OnPropertyChanged();
            }
        }

        public bool IsTerminalTask { get; set; }

        public SystemTask SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                IsVisibleCreateButton = false;
                IsVisibleCreateView = false;
                IsVisibleEditingView = true;
                OnPropertyChanged();
            }
        }

        public DelegateCommand CreateTaskCommand
        {
            get => _createTaskCommand ?? (_createTaskCommand = new DelegateCommand(CreateTask));
        }

        public DelegateCommand EditTaskCommand
        {
            get => _editTaskCommand ?? (_editTaskCommand = new DelegateCommand(EditTask));
        }

        public DelegateCommand ShowCreateViewCommand
        {
            get => _showCreateViewCommand ?? (_showCreateViewCommand = new DelegateCommand(ShowCreateView));
        }

        public DelegateCommand CancelCommand
        {
            get => _cancelCommand ?? (_cancelCommand = new DelegateCommand(Cancel));
        }

        public MainViewModel()
        {
            SelectedTask = new SystemTask();
            IsVisibleCreateButton = true;
            IsVisibleCreateView = false;
            IsVisibleEditingView = false;
        }

        public void ShowCreateView()
        {
            SelectedTask = new SystemTask();
            IsVisibleCreateButton = false;
            IsVisibleCreateView = true;
            IsVisibleEditingView = false;
        }

        public void Cancel()
        {
            IsVisibleCreateButton = true;
            IsVisibleCreateView = false;
            IsVisibleEditingView = false;
        }

        public void CreateTask()
        {
            CreateTaskOperation.Execute(SelectedTask);
            ReloadTasks();
        }

        public void EditTask()
        {
            EditTaskOperation.Execute(SelectedTask);
            ReloadTasks();
            IsVisibleCreateButton = true;
            IsVisibleCreateView = false;
            IsVisibleEditingView = false;
        }

        //public void AddSubTask()
        //{
        //    AddSubTaskOperation.Execute(SelectedTask);
        //}

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
        [Import]
        public IEditTaskOperation EditTaskOperation { get; set; }
        [Import]
        public IAddSubTaskOperation AddSubTaskOperation { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
