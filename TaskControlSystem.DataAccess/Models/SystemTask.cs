using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        private int? _planCompletionTime;

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Executors { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime CompletionDate { get; set; }

        public bool IsTerminal
        {
            get
            {
                if (ChildSystemTasks == null)
                    return false;
                if (ChildSystemTasks.Count == 0)
                    return true;
                else
                    return false;
            }
            //get => ChildSystemTasks.Count == 0 || ChildSystemTasks == null ? true : false;
        }

        public bool IsRoot
        {
            get => ParentSystemTask == null ? true : false;
        }

        public int? PlanCompletionTime
        {
            get
            {
                if (ChildSystemTasks == null)
                    return 0;
                if (ChildSystemTasks.Count == 0)
                    return _planCompletionTime;
                else
                    return ChildSystemTasks.Sum(cst => cst.PlanCompletionTime);
            }
            //get => ChildSystemTasks.Count == 0 || ChildSystemTasks == null ? 0
            //    : ChildSystemTasks.Sum(cst => cst.PlanCompletionTime);
            set
            {
                _planCompletionTime = value;
                OnPropertyChanged();
            }
        }

        public int? FactCompletionTime
        {
            get => CompletionDate.Subtract(RegisterDate).Days + 1;
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
