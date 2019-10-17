using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskControlSystem.Infrastructure
{
    public class InternalHelper
    {
        public static Task CompletedTask { get; } = Task.FromResult(1);
    }
}
