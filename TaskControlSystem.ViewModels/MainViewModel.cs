using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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
        private AsyncCommand _createTaskCommand;
        //private DelegateCommand _createTaskCommand;
        private DelegateCommand _editTaskCommand;
        private DelegateCommand _removeTaskCommand;
        private DelegateCommand _addSubTaskCommand;
        private DelegateCommand _showCreateViewCommand;
        private DelegateCommand _showAddSubTaskViewCommand;
        private DelegateCommand _cancelCommand;
        private bool _isVisibleCreateView;
        private bool _isVisibleCreateButton;
        private bool _isTerminalTask;
        private bool _isVisibleEditingView;
        private bool _isVisibleAddSubTaskView;

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

        public bool IsVisibleAddSubTaskView
        {
            get => _isVisibleAddSubTaskView;
            set
            {
                _isVisibleAddSubTaskView = value;
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

        public CreateTaskViewModel CreateTaskViewModel
        {
            get => _createTaskViewModel;
            set
            {
                _createTaskViewModel = value;
                OnPropertyChanged();
            }
        }

        public AsyncCommand CreateTaskCommand
        {
            get => _createTaskCommand ?? (_createTaskCommand = new AsyncCommand(CreateTask));
        }

        //public DelegateCommand EditTaskCommand
        //{
        //    get => _editTaskCommand ?? (_editTaskCommand = new DelegateCommand(EditTask));
        //}

        //public DelegateCommand RemoveTaskCommand
        //{
        //    get => _removeTaskCommand ?? (_removeTaskCommand = new DelegateCommand(RemoveTask));
        //}

        //public DelegateCommand AddSubTaskCommand
        //{
        //    get => _addSubTaskCommand ?? (_addSubTaskCommand = new DelegateCommand(AddSubTask));
        //}

        public DelegateCommand ShowCreateViewCommand
        {
            get => _showCreateViewCommand ?? (_showCreateViewCommand = new DelegateCommand(ShowCreateView));
        }

        public DelegateCommand CancelCommand
        {
            get => _cancelCommand ?? (_cancelCommand = new DelegateCommand(Cancel));
        }

        public DelegateCommand ShowAddSubTaskViewCommand
        {
            get => _showAddSubTaskViewCommand ?? (_showAddSubTaskViewCommand = new DelegateCommand(ShowAddSubTaskView));
        }

        public MainViewModel()
        {
            SelectedTask = new SystemTask();
            CreateTaskViewModel = new CreateTaskViewModel();
            ShowMainWindow();
        }

        public void ShowCreateView()
        {
            SelectedTask = new SystemTask();
            IsVisibleCreateButton = false;
            IsVisibleCreateView = true;
            IsVisibleEditingView = false;
            IsVisibleAddSubTaskView = false;
        }

        public void ShowAddSubTaskView()
        {
            IsVisibleCreateButton = false;
            IsVisibleCreateView = false;
            IsVisibleEditingView = false;
            IsVisibleAddSubTaskView = true;
        }

        public void Cancel()
        {
            ShowMainWindow();
        }

        public void ShowMainWindow()
        {
            IsVisibleCreateButton = true;
            IsVisibleCreateView = false;
            IsVisibleEditingView = false;
            IsVisibleAddSubTaskView = false;
        }

        public async Task CreateTask()
        {
            await CreateTaskOperation.ExecuteAsync(CreateTaskViewModel);
            await ReloadTasks();
            CreateTaskViewModel = new CreateTaskViewModel();
            ShowMainWindow();
        }

        //public void EditTask()
        //{
        //    EditTaskOperation.Execute(SelectedTask);
        //    ReloadTasks();
        //    SelectedTask = new SystemTask();
        //    ShowMainWindow();
        //}

        //public void AddSubTask()
        //{
        //    AddSubTaskOperation.Execute(SelectedTask);
        //    ReloadTasks();
        //}

        //public void RemoveTask()
        //{
        //    RemoveTaskOperation.Execute(SelectedTask);
        //    ReloadTasks();
        //}

        [Import(typeof(IRepositoryProvider))]
        private IRepositoryProvider RepositoryProvider { get; set; }

        public async Task ReloadTasks() => Tasks = new ObservableCollection<SystemTask>(
           (await RepositoryProvider
                .GetRepository<SystemTask>()
                .GetAsync(t => t.ParentSystemTask == null))
                .ToList());

        [Import]
        public ICreateTaskOperation CreateTaskOperation { get; set; }
        [Import]
        public IEditTaskOperation EditTaskOperation { get; set; }
        [Import]
        public IAddSubTaskOperation AddSubTaskOperation { get; set; }
        [Import]
        public IRemoveTaskOperation RemoveTaskOperation { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
