using HouseFinanceApp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseFinanceApp.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AuthHouse]
        public ActionResult Index()
        {
            var id = User.Identity.GetHouseholdId();

            //Household householdId = db.Households.Find(id);


            if (id == null)
            {
                return HttpNotFound();
            }
            //return View(household);

            var userId = User.Identity.GetUserId();
            //var acctIdByHouseId = db.Users.Find(id)/*.Accounts.ToList()*/;

            var houseAccounts = db.PersonalAccounts.Where(p => p.CreatedById == userId || p.HouseholdId == id).ToList();
            //var houseTransactions = db.Transactions.Where(t => t.AccountId == acctIdByHouseId).ToList();
            var userPersonalAccounts = db.PersonalAccounts.Where(p => p.CreatedById == userId).ToList();
            var userTransactions = db.Transactions.Where(t => t.EnteredById == userId  ).ToList();
            


            DashViewModel model = new DashViewModel()
            {
                PersonalAccounts = userPersonalAccounts,
                Transactions = userTransactions,
                HouseAccounts = houseAccounts,
               //HouseTransactions = acctIdByHouseId,
                //Households = householdName
            };


            return View(model);

        }

        public ActionResult Dash2()
        {
            return View();
        }



        public JsonResult GetChartDataAjax(string type)
        {
            if (type == "Current")
            {
                var trx = db.Households.Find(User.Identity.GetHouseholdId())
                .Accounts.SelectMany(t => t.Transactions)
                .Where(t => t.Date.Month == DateTime.Now.Month);
                List<ChartData> data = new List<ChartData>();
                foreach (Transaction t in trx)
                {
                    data.Add(new ChartData() { X = t.Category.Name, Y = t.Amount/*.ToString()*/ });
                }
                return Json(data);
            }
            else if (type == "Last")
            {
                var trx = db.Households.Find(User.Identity.GetHouseholdId())
                .Accounts.SelectMany(t => t.Transactions)
                .Where(t => t.Date.Month == DateTime.Now.Month - 1);
                List<ChartData> data = new List<ChartData>();
                foreach (Transaction t in trx)
                {
                    data.Add(new ChartData() { X = t.Category.Name, Y = t.Amount/*.ToString()*/ });
                }
                return Json(data);
            }
            return null;
        }


        public JsonResult FusionDataAjax()
        {
            var trx = db.Households.Find(User.Identity.GetHouseholdId())
            .Accounts.SelectMany(t => t.Transactions);
            //.Where(t => t.Date.Month == DateTime.Now.Month);
            List<FusionData> data = new List<FusionData>();
            foreach (Transaction t in trx)
            {
                data.Add(new FusionData() { Label = t.Category.Name, Value = t.Amount/*.ToString()*/ });
            }
            return Json(data);
        }

        public JsonResult AccountDataAjax()
        {
            var trx = db.Households.Find(User.Identity.GetHouseholdId())
            .Accounts;
            List<FusionData> data = new List<FusionData>();
            foreach (PersonalAccount t in trx)
            {
                data.Add(new FusionData() { Label = t.Name, Value = t.Balance/*.ToString()*/ });
            }
            return Json(data);
        }

        public JsonResult HouseChart()
        {
            var HouseId = db.Households.Find(User.Identity.GetHouseholdId())
            .Accounts;
            List<HouseChart> data = new List<HouseChart>();
            foreach (PersonalAccount a in HouseId)
            {
                data.Add(new HouseChart() { Label = a.Name, Value = a.Balance/*.ToString()*/ });
            }
            return Json(data);
        }






        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}