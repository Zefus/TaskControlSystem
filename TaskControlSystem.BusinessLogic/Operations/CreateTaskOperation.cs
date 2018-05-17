using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskControlSystem.BusinessLogic.TaskViewModels;
using System.ComponentModel.Composition;
using TaskControlSystem.Infrastructure;
using TaskControlSystem.DataAccess.Models;

namespace TaskControlSystem.BusinessLogic.Operations
{
    [Export(typeof(ICreateTaskOperation))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CreateTaskOperation : ICreateTaskOperation
    {
        [Import]
        private IRepositoryProvider _repositoryProvider;

        public void Execute(SystemTask createdTask)
        {
            var repository = _repositoryProvider.GetRepository<SystemTask>();

            SystemTask task = new SystemTask
            {
                Title = createdTask.Title,
                Description = createdTask.Description,
                Executors = createdTask.Executors,
                Status = TaskStatus.Appointed,
                RegisterDate = DateTime.Now,
                ParentSystemTask = null
            };

            repository.Add(task);
            _repositoryProvider.SaveChanges();
        }
    }
}
