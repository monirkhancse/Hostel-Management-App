using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PermissionManagement.MVC.Models
{
    public class Manager
    {
        public int ManagerId { get; set; }
        [Required]
        [DisplayName("Current Month")]
        public string Month { get; set; }
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }
        [Required]
        public int? MemberId { get; set; }
        public virtual Member Member { get; set; }
    }
}
