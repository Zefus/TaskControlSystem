using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TaskControlSystem.DataAccess.Models;

namespace TaskControlSystem.DataAccess
{
    public class TaskContext : DbContext
    {
        public TaskContext() : base("TaskControl") { }

        public IDbSet<SystemTask> Tasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<SystemTask>()
                .Property(p => p.RegisterDate)
                .HasColumnType("datetime2")
                .HasPrecision(0)
                .IsRequired();

            modelBuilder.Entity<SystemTask>()
                .Property(p => p.CompletionDate)
                .HasColumnType("datetime2")
                .HasPrecision(0)
                .IsRequired();
        }
    }
}
