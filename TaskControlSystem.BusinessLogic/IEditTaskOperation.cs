using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskControlSystem.DataAccess.Models;
using TaskControlSystem.BusinessLogic.TaskViewModels;

namespace TaskControlSystem.BusinessLogic
{
    public interface IEditTaskOperation
    {
        void Execute(SystemTask selectedTask, EditTaskViewModel editTaskViewModel);
    }
}
