using System.ComponentModel.DataAnnotations;
namespace TaskControlSystem.DataAccess.Models
{
    public enum TaskStatus : byte
    {
        [Display(Name = "Назначена")]
        Appointed = 1,
        [Display(Name = "Выполняется")]
        Performed = 2,
        [Display(Name = "Остановлена")]
        Suspended = 3,
        [Display(Name = "Завершена")]
        Completed = 4
    }
}
