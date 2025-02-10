using System.Collections.Generic;

namespace PermissionManagement.MVC.Constants
{
public static class Permissions
{
    public static List<string> GeneratePermissionsForModule(string module)
    {
        return new List<string>()
        {
            $"Permissions.{module}.Create",
            $"Permissions.{module}.View",
            $"Permissions.{module}.Edit",
            $"Permissions.{module}.Delete",
        };
    }

    public static class Members
    {
        public const string View = "Permissions.Members.View";
        public const string Create = "Permissions.Members.Create";
        public const string Edit = "Permissions.Members.Edit";
        public const string Delete = "Permissions.Members.Delete";
    }
        public static class Deposits
        {
            public const string View = "Permissions.Deposits.View";
            public const string Create = "Permissions.Deposits.Create";
            public const string Edit = "Permissions.Deposits.Edit";
            public const string Delete = "Permissions.Deposits.Delete";
        }
        public static class Meals
        {
            public const string View = "Permissions.Meals.View";
            public const string Create = "Permissions.Meals.Create";
            public const string Edit = "Permissions.Meals.Edit";
            public const string Delete = "Permissions.Meals.Delete";
        }
        public static class Expenses
        {
            public const string View = "Permissions.Expenses.View";
            public const string Create = "Permissions.Expenses.Create";
            public const string Edit = "Permissions.Expenses.Edit";
            public const string Delete = "Permissions.Expenses.Delete";
        }
        public static class Managers
        {
            public const string View = "Permissions.Managers.View";
            public const string Create = "Permissions.Managers.Create";
            public const string Edit = "Permissions.Managers.Edit";
            public const string Delete = "Permissions.Managers.Delete";
        }
    }
}