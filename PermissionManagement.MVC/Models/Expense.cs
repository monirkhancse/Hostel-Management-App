using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PermissionManagement.MVC.Models
{
    public class Expense
    {
        [DisplayName("Expense No.")]
        public int ExpenseId { get; set; }
        [DisplayName("Expense Date")]
        [DataType(DataType.Date)]
        public DateTime ExpenseDate { get; set;}
        [DisplayName("Bazar Cost")]
        public Decimal BazarCost { get; set; }
        public string Remarks { get; set;}
        [Required]
        public int? MemberId { get; set; }
        public virtual Member Member { get; set; }
    }
}
