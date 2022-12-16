using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Core.Models.EmployeeModels
{
    public class TasksCountViewModel
    {
        public int ExpectedCount { get; set; }
        public int DepartCount { get; set; }
        public int CurrentlyIn { get; set; }
        public int OverdueCount { get; set; }
        public int TotalCount { get; set; }
    }
}
