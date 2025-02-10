using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PermissionManagement.MVC.Models
{
    public class Member
    {
        [DisplayName("Border No.")]
        public int MemberId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DisplayName("Mobile No.")]
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public ICollection<Deposit> Deposits { get; set; }
        public ICollection<Meal> Meals { get; set; }
        public ICollection<Manager> Managers { get; set; }
        public ICollection<Expense> Expenses { get; set; }
    }
}
