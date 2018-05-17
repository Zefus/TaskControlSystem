using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using TaskControlSystem.DataAccess.Models;
using TaskControlSystem.Infrastructure;

namespace TaskControlSystem.BusinessLogic.Operations
{
    [Export(typeof(IRemoveTaskOperation))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RemoveTaskOperation : IRemoveTaskOperation
    {
        [Import]
        private IRepositoryProvider _repositoryProvider;

        public void Execute(SystemTask selectedTask)
        {
            var repository = _repositoryProvider.GetRepository<SystemTask>();

            var taskToRemove = repository.Find(selectedTask.Id);

            repository.Remove(taskToRemove);

            _repositoryProvider.SaveChanges();
        }
    }
}
