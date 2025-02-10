using HostelManagementApp.ViewModels;
using PermissionManagement.MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelManagementApp.Services
{
    public class MemberWiseReportService
    {
        private readonly ApplicationDbContext _context;

        public MemberWiseReportService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<MemberWiseReportViewModel> GetMemberReportData()
        {
            return _context.Members
                .Select(member => new MemberWiseReportViewModel
                {
                    Id = member.MemberId,
                    Name = member.Name,
                    TotalDeposit = member.Deposits.Sum(d => d.Amount),
                    TotalMeals = member.Meals.Count(),
                    TotalExpenses = member.Expenses.Sum(e => e.BazarCost),
                })
                .ToList();
        }
    }
}
