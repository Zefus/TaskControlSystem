﻿using System;
using System.Windows.Input;

namespace TaskControlSystem.ViewModels.Commands
{
    public abstract class DelegateCommandBase : ICommand
    {
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute(parameter);
        }

        void ICommand.Execute(object parameter)
        {
            Execute(parameter);
        }

        protected abstract void Execute(object parameter);
        protected abstract bool CanExecute(object parameter);

        public event EventHandler CanExecuteChanged;

        public virtual void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
