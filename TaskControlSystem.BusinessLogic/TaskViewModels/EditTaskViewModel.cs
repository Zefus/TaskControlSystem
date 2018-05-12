using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskControlSystem.BusinessLogic.TaskViewModels
{
    public class EditTaskViewModel
    {
        public string Title { get; set; }
        public string Descriptiion { get; set; }
        public string Executors { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime CompletionDate { get; set; }
    }
}
