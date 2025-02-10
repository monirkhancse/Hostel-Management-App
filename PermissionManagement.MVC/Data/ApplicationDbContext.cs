using HostelManagementApp.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PermissionManagement.MVC.Models;

namespace PermissionManagement.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly DbContextOptions _options;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            _options = options;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Member> Members { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<MemberWiseReportViewModel> reportViewModels { get; set; }
    }
}