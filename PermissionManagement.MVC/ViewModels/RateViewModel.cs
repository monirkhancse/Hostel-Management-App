using PermissionManagement.MVC.Models;
using System.Collections.Generic;

namespace PermissionManagement.MVC.ViewModels
{
    public class RateViewModel
    {
        public IEnumerable<Meal> Meals { get; set; }
        public IEnumerable<Expense> Expenses { get; set; }
        public IEnumerable<Deposit> Deposits { get; set; }
        public IEnumerable<Manager> Managers { get; set; }
        public IEnumerable<Member> Members { get; set; }
    }
}
