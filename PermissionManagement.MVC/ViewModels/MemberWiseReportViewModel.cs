using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelManagementApp.ViewModels
{
    public class MemberWiseReportViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal TotalDeposit { get; set; }
        public decimal TotalMeals { get; set; }
        public decimal TotalExpenses { get; set; }
    }
}
