namespace TaskControlSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Collections.ObjectModel;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TaskControlSystem.DataAccess.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TaskControlSystem.DataAccess.TaskContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TaskControlSystem.DataAccess.TaskContext context)
        {
            var Task1 = new SystemTask
            {
                Title = "title1",
                Description = "Description1",
                RegisterDate = DateTime.Now,
                CompletionDate = DateTime.Now,
                Status = TaskStatus.Appointed,
                Executors = "Executor1",
                ParentSystemTask = null,
                ChildSystemTasks = new ObservableCollection<SystemTask>()
            };

            var Task2 = new SystemTask
            {
                Title = "title1.1",
                Description = "Description1.1",
                RegisterDate = DateTime.Now,
                CompletionDate = DateTime.Now,
                Status = TaskStatus.Appointed,
                Executors = "Executor1.1",
                ParentSystemTask = Task1,
                ChildSystemTasks = new ObservableCollection<SystemTask>()
            };

            var Task3 = new SystemTask
            {
                Title = "title1.1.1",
                Description = "Description1.1.1",
                RegisterDate = DateTime.Now,
                CompletionDate = DateTime.Now,
                Status = TaskStatus.Appointed,
                Executors = "Executor1.1.1",
                ParentSystemTask = Task2,
                ChildSystemTasks = new ObservableCollection<SystemTask>()
            };

            var Task4 = new SystemTask
            {
                Title = "title1.2",
                Description = "Description1.2",
                RegisterDate = DateTime.Now,
                CompletionDate = DateTime.Now,
                Status = TaskStatus.Appointed,
                Executors = "Executor1.2",
                ParentSystemTask = Task1,
                ChildSystemTasks = new ObservableCollection<SystemTask>()
            };

            Task2.ChildSystemTasks.Add(Task3);
            Task1.ChildSystemTasks.Add(Task2);
            Task1.ChildSystemTasks.Add(Task4);

            context.Tasks.Add(Task1);
            context.SaveChanges();
        }
    }
}
