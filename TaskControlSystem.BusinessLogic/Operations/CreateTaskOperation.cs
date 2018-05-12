using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void Execute(CreateTaskViewModel createTaskViewModel)
        {
            var repository = _repositoryProvider.GetRepository<SystemTask>();

            SystemTask task = new SystemTask
            {
                Title = createTaskViewModel.Title,
                Description = createTaskViewModel.Descriptiion,
                Executors = createTaskViewModel.Executors,
                Status = createTaskViewModel.Status,
                RegisterDate = createTaskViewModel.RegisterDate,
                CompletionDate = createTaskViewModel.CompletionDate,
                ParentSystemTask = null
            };

            repository.Add(task);
            _repositoryProvider.SaveChanges();
        }
    }
}
