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





    }
}
  