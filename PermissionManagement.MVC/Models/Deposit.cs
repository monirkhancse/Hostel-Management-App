using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PermissionManagement.MVC.Models
{
    public class Deposit
    {
        [DisplayName("Deposit No.")]
        public int DepositId { get; set; }
        [DisplayName("Deposit Date")]
        [DataType(DataType.Date)]
        public DateTime DepositDate { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        [Required]
        public int? MemberId { get; set; }
        public virtual Member Member { get; set; }
    }
}
