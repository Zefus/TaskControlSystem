using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskControlSystem.ViewModels.Commands
{
    public class DelegateCommand : DelegateCommandBase
    {
        private readonly Action _action;
        private readonly Func<bool> _canExecutePredicate;

        public DelegateCommand(Action action, Func<bool> canExecutePredicate = null)
        {
            _action = action;
            _canExecutePredicate = canExecutePredicate;
        }

        protected override bool CanExecute(object parameter)
        {
            return _canExecutePredicate == null || _canExecutePredicate();
        }

        protected override void Execute(object parameter)
        {
            _action();
        }
    }
}
