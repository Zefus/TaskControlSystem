using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskControlSystem.BusinessLogic.TaskViewModels;
using System.ComponentModel.Composition;
using TaskControlSystem.Infrastructure;
using TaskControlSystem.DataAccess.Models;
using TaskStatus = TaskControlSystem.DataAccess.Models.TaskStatus;

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
                Description = createTaskViewModel.Description,
                Executors = createTaskViewModel.Executors,
                Status = TaskStatus.Appointed,
                RegisterDate = DateTime.Now,
                PlanCompletionTime = createTaskViewModel.PlanCompletionTime,
                ParentSystemTask = null
            };
            task.CompletionDate = task.RegisterDate.AddDays(Convert.ToDouble(task.PlanCompletionTime));

            repository.Add(task);
            _repositoryProvider.SaveChanges();
        }

        public async Task<bool> ExecuteAsync(CreateTaskViewModel createTaskViewModel)
        {
            var repository = _repositoryProvider.GetRepository<SystemTask>();

            SystemTask task = new SystemTask
            {
                Title = createTaskViewModel.Title,
                Description = createTaskViewModel.Description,
                Executors = createTaskViewModel.Executors,
                Status = TaskStatus.Appointed,
                RegisterDate = DateTime.Now,
                PlanCompletionTime = createTaskViewModel.PlanCompletionTime,
                ParentSystemTask = null
            };
            task.CompletionDate = task.RegisterDate.AddDays(Convert.ToDouble(task.PlanCompletionTime));

            await repository.AddAsync(task);
            await _repositoryProvider.SaveChangesAsync();

            return true;
        }
    }
}
