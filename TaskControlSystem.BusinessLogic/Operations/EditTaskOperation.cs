using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using TaskControlSystem.DataAccess.Models;
using TaskControlSystem.Infrastructure;
using TaskControlSystem.BusinessLogic.TaskViewModels;

namespace TaskControlSystem.BusinessLogic.Operations
{
    [Export(typeof(IEditTaskOperation))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EditTaskOperation : IEditTaskOperation
    {
        [Import]
        private IRepositoryProvider _repositoryProvider;

        public void Execute(SystemTask selectedTask, EditTaskViewModel editTaskViewModel)
        {
            var repository = _repositoryProvider.GetRepository<SystemTask>();

            var taskToEdit = repository.Find(selectedTask.Id);

            taskToEdit.Title = editTaskViewModel.Title;
            taskToEdit.Description = editTaskViewModel.Descriptiion;
            taskToEdit.Executors = editTaskViewModel.Executors;
            taskToEdit.RegisterDate = editTaskViewModel.RegisterDate;
            taskToEdit.CompletionDate = editTaskViewModel.CompletionDate;

            _repositoryProvider.SaveChanges();
        }
    }
}
