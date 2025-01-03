using Budgeter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Budgeter.Controllers
{
    public class HouseholdsController : Controller
    {
		private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Households
        public ActionResult Index()
        {
            return View(db.Households.ToList());
        }

		public ActionResult Details(int? id)
		{
			HouseholdViewModel model = new HouseholdViewModel();
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.household = db.Households.Find(id);
			if (model.household == null)
			{
				return HttpNotFound();
			}

			model.household.Total = 0;

			foreach (var bankAccount in model.household.BankAccounts)
			{
				if (bankAccount.Deleted != true)
				{
					if (bankAccount.Balance <= 0)
					{
						ViewBag.OverdraftError = "You are in danger of overdrawing your account!";
					}

					var balance = bankAccount.Balance;
					model.household.Total += balance;
				}
			}

			model.household.TotalBudget = 0;

			foreach (var budget in model.household.Budgets)
			{
				foreach (var item in budget.Items)
				{
					var amount = item.Amount;
					model.household.TotalBudget += amount;
				}
			}

			int catTotal = 0;

			foreach (var category in db.Categories)
			{
				catTotal++;
			}

			int catCount = 0;
			model.MyCategories = new int[catTotal - 1];

			foreach (var budget in model.household.Budgets)
			{
				foreach (var item in budget.Items)
				{
					model.MyCategories[catCount] = item.Category.Id;
					catCount++;
				}
			}

			model.CategoryCount = catCount;

			int transCount = 0;
			model.CategoryTotals = new double[catTotal + 1];

			foreach (var category in model.MyCategories)
			{
				foreach (var account in model.household.BankAccounts)
				{
					foreach (var transaction in account.Transactions)
					{
						if (transaction.CategoryId == category)
						{
							model.CategoryTotals[transCount] += transaction.Amount;
						}
					}
				}
				transCount++;
			}

			db.SaveChanges();
			return View(model);
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id, Name, CreatedById, Deleted")] Household household)
		{
			if (ModelState.IsValid)
			{
				string userId = User.Identity.GetUserId();
				household.CreatedById = userId;
				db.Households.Add(household);

				HouseholdUsersHelper helper = new HouseholdUsersHelper(db);
				helper.AddUserToHousehold(household.Id, userId);

				Budget budget = new Budget
				{
					HouseholdId = household.Id,
					household = db.Households.Find(household.Id),
					Name = household.Name + "'s Budget",
					Amount = 0
				};
				db.Budgets.Add(budget);
				db.SaveChanges();

				return RedirectToAction("Index", "Home");
			}
			return View(household);
		}

		public ActionResult Assign(int id)
		{
			var household = db.Households.Find(id);
			HouseholdUsersHelper helper = new HouseholdUsersHelper(db);
			var model = new AssignUsersViewModel();

			model.Household = household;
			model.SelectedUsers = helper.ListAssignedUsers(id).Select(u => u.Id).ToArray();
            model.Users = new MultiSelectList(
                db.Users.Where(u => u.DisplayName != "N/A" && u.DisplayName != "(Remove Assigned User)")
                        .OrderBy(user => user.FirstName),
                "Id", "DisplayName", model.SelectedUsers
            );
            return View(model);
		}

		[HttpPost]
		public ActionResult Assign(AssignUsersViewModel model)
		{
			foreach (var user in db.Users.Select(r => r.Id).ToList())
			{
				if (model.SelectedUsers != null)
				{

				}
			}
			return RedirectToAction("Index", "Home");
		}
    }
}