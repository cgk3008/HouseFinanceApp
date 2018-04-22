using HouseFinanceApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseFinanceApp.Controllers
{
    public class HouseholdsViewsModelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HouseholdsViewsModel
        public ActionResult Index()
        {
            return View();
        }



        //    [HttpGet]
        //    public ActionResult GetChart()
        //    {
        //        //var s = new [] { new { year= "2008", value= 20 },
        //        // new { year= "2009", value= 5 },
        //        // new { year= "2010", value= 7 },
        //        // new { year= "2011", value= 10 },
        //        // new { year= "2012", value= 20 }};
        //        var house = db.Households.Find(User.Identity.GetHouseholdId<int>());
        //        var tod = DateTimeOffset.Now;
        //        decimal totalExpense = 0;
        //        decimal totalBudget = 0;
        //        var totalAcc = (from a in house.Accounts
        //                        select a.Balance).DefaultIfEmpty().Sum();
        //        //var totalAcc = house.Accounts.Select(a => a.Balance).DefaultIfEmpty().Sum();
        //        var bar = (from c in house.Categories
        //                   where c.CategoryType.Name == "Expense"
        //                   let aSum = (from t in c.Transactions
        //                               where t.TransDate.Year == tod.Year && t.TransDate.Month == tod.Month
        //                               select t.Amount).DefaultIfEmpty().Sum()
        //                   let bSum = (from b in c.BudgetItems
        //                               select b.Amount).DefaultIfEmpty().Sum()


        //                   let _ = totalExpense += aSum
        //                   let __ = totalBudget += bSum
        //                   select new
        //                   {
        //                       Name = c.Name,
        //                       Actual = aSum,
        //                       Budgeted = bSum
        //                   }).ToArray();

        //        var donut = (from c in house.Categories
        //                     where c.CategoryType.Name == "Expense"
        //                     let aSum = (from t in c.Transactions
        //                                 where t.TransDate.Year == tod.Year && t.TransDate.Month == tod.Month
        //                                 select t.Amount).DefaultIfEmpty().Sum()
        //                     select new
        //                     {
        //                         label = c.Name,
        //                         value = aSum
        //                     }).ToArray();
        //        var result = new
        //        {
        //            totalAcc = totalAcc,
        //            totalBudget = totalBudget,
        //            totalExpense = totalExpense,
        //            bar = bar,
        //            donut = donut
        //        };

        //        return Content(JsonConvert.SerializeObject(result), "application/json");
        //    }
        //    public ActionResult GetMonthly()
        //    {
        //        var household = db.Households.Find(User.Identity.GetHouseholdId<int>());
        //        var monthsToDate = Enumerable.Range(1, DateTime.Today.Month)
        //        .Select(m => new DateTime(DateTime.Today.Year, m, 1))
        //        .ToList();
        //        var sums = from month in monthsToDate
        //                   select new
        //                   {
        //                       month = month.ToString("MMM"),
        //                       income = (from account in household.Accounts
        //                                 from transaction in account.Transactions
        //                                 where transaction.Category.CategoryType.Name == "Income" &&
        //                                 transaction.TransDate.Month == month.Month
        //                                 select transaction.Amount).DefaultIfEmpty().Sum(),
        //                       //income = household.Accounts.SelectMany(t => t.Transactions).Where(c =>
        //                       c.Category.CategoryType.Name == "Income" && c.TransDate.Month == month.Month).Select(t => t.Amount).DefaultIfEmpty().Sum(), expense = (from account in household.Accounts from transaction in account.Transactions where transaction.Category.CategoryType.Name == "Expense" && transaction.TransDate.Month == month.Month select transaction.Amount).DefaultIfEmpty().Sum(),
        //        //expenses = household.Accounts.SelectMany(a => a.Transactions).Where(t => t.Category.CategoryType.Name == "Expense" && t.TransDate.Month == month.Month).Select(t => t.Amount).DefaultIfEmpty().Sum(), 

        //        budget = household.BudgetItems.Select(b => b.Amount).DefaultIfEmpty().Sum(),
        //        };
        //    return Content(JsonConvert.SerializeObject(sums), "application/json");
        //}







    }
}
  