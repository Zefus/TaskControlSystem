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
    [Export(typeof(IAddSubTaskOperation))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AddSubTaskOperation : IAddSubTaskOperation
    {
        [Import]
        private IRepositoryProvider _repositoryProvider;

        public void Execute(SystemTask parentTask, CreateTaskViewModel childTask)
        {
            var repository = _repositoryProvider.GetRepository<SystemTask>();

            SystemTask task = new SystemTask
            {
                Title = childTask.Title,
                Description = childTask.Descriptiion,
                Executors = childTask.Executors,
                Status = TaskStatus.Appointed,
                RegisterDate = childTask.RegisterDate,
                CompletionDate = childTask.CompletionDate,
                ParentSystemTask = parentTask
            };

            repository.Find(parentTask.Id).ChildSystemTasks.Add(task);
            _repositoryProvider.SaveChanges();
        }
    }
}
