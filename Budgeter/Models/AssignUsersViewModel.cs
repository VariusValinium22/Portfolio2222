using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Budgeter.Models
{
    public class AssignUsersViewModel
    {
        public Household Household { get; set; } // Ensure Household is properly defined in Models.
        public string[] SelectedUsers { get; set; } // Holds the IDs of the selected users.
        public MultiSelectList Users { get; set; } // Provides the list of users for selection.
    }
}