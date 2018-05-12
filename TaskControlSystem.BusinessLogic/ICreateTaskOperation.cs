using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskControlSystem.BusinessLogic.TaskViewModels;

namespace TaskControlSystem.BusinessLogic
{
    public interface ICreateTaskOperation
    {
        void Execute(CreateTaskViewModel createTaskViewModel);
    }
}
