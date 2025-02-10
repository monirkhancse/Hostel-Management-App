using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PermissionManagement.MVC.Models
{
    public class Meal
    {
        [DisplayName("Meal No.")]
        public int MealId { get; set; }
        [DisplayName("Entry Date")]
        [DataType(DataType.Date)]
        public DateTime EntryDate { get; set;}
        [DisplayName("Today's Meal")]
        public decimal TodayMeal { get; set;}
        [Required]
        public int? MemberId { get; set; }
        public virtual Member Member { get; set; }
        public string Remarks { get; set; }
    }
}
