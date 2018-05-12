using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskControlSystem.DataAccess.Models;

namespace TaskControlSystem.BusinessLogic
{
    public interface IRemoveTaskOperation
    {
        void Execute(SystemTask selectedTask);
    }
}
