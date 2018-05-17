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

        public void Execute(SystemTask selectedTask)
        {
            var repository = _repositoryProvider.GetRepository<SystemTask>();

            var taskToEdit = repository.Find(selectedTask.Id);

            taskToEdit.Title = selectedTask.Title;
            taskToEdit.Description = selectedTask.Description;
            taskToEdit.Executors = selectedTask.Executors;
            taskToEdit.RegisterDate = selectedTask.RegisterDate;
            taskToEdit.CompletionDate = selectedTask.CompletionDate;

            _repositoryProvider.SaveChanges();
        }
    }
}
