using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using TaskControlSystem.DataAccess.Models;

namespace TaskControlSystem.DataAccess.Models
{
    public class SystemTask : INotifyPropertyChanged
    {
        private ICollection<SystemTask> _childSystemTasks;
        private ICollection<SystemTask> _systemTasks;

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Executors { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime CompletionDate { get; set; }

        public bool IsTerminal
        {
            get => ChildSystemTasks == null ? true : false;
        }

        public bool IsRoot
        {
            get => ParentSystemTask == null ? true : false;
        }

        public int? PlanCompletionTime
        {
            get => CompletionDate.Subtract(RegisterDate).Days + 1
               + ChildSystemTasks.Sum(cst => cst.PlanCompletionTime);
        }

        public int? FactCompletionTime
        {
            get => DateTime.Now.Subtract(RegisterDate).Days + 1
               + ChildSystemTasks.Sum(cst => cst.FactCompletionTime);
        }

        public virtual ICollection<SystemTask> ChildSystemTasks
        {
            get { return _childSystemTasks; }
            set
            {
                _childSystemTasks = value;
                OnPropertyChanged();
            }
        }
        public virtual ICollection<SystemTask> SystemTasks
        {
            get { return _systemTasks; }
            set
            {
                _systemTasks = value;
                OnPropertyChanged();
            }
        }

        public virtual SystemTask ParentSystemTask { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
