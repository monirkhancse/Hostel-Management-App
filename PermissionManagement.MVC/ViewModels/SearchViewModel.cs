using Microsoft.AspNetCore.Mvc.Rendering;
using PermissionManagement.MVC.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace PermissionManagement.MVC.ViewModels
{
    public class SearchViewModel
    {
        public PaginatedList<Deposit> Deposits { get; set; }
        public PaginatedList<Meal> Meals { get; set; }
        public PaginatedList<Expense> Expenses { get; set; }
        public SelectList Members { get; set; }
        public int? SelectedMemberId { get; set; }
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
    }
}
