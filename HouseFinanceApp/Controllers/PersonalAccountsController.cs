﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using HouseFinanceApp.Models;
using Microsoft.AspNet.Identity;

namespace HouseFinanceApp.Controllers
{
    public class PersonalAccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PersonalAccounts
        public ActionResult Index()
        {
            var personalAccounts = db.PersonalAccounts.Include(p => p.CreatedBy).Include(p => p.Household);
            return View(personalAccounts.ToList());
        }

        // GET: HouseholdPersonalAccounts
        public ActionResult HouseAccounts()
        {
            //var personalAccounts = db.PersonalAccounts.Include(p => p.CreatedBy).Include(p => p.Household);
            //return View(personalAccounts.ToList());

            var userHousehold = User.Identity.GetHouseholdId();

            var userName = User.Identity.GetFullName();

            //var acctId = db.PersonalAccounts.Where(p => p.Id == );

            var houseAccounts = db.PersonalAccounts.Where(h => h.HouseholdId == userHousehold).ToList();

            //var acctTransactions = db.Transactions.Where(i => i.AccountId == acctId).Sum(t => t.ReconciledAmount);


            //var createdByName = db.PersonalAccounts.Where(n => n.CreatedById == User.Identity.GetUserId).ToList();

            return View(houseAccounts);

        }

        //GET: Account with list of Transaction to reconcile
        public ActionResult TransactAccountBalance(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }

            var acctId = db.PersonalAccounts.Where(p => p.Id == id).FirstOrDefault();
            var acctTransactions = db.Transactions.Where(t => t.AccountId == id).ToList();


            TransactionAccountViewModel model = new TransactionAccountViewModel()
            {
                //PersAcct = acctId,
                Transactions = acctTransactions,

            };


            return View(model);
        }





        // GET: PersonalAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalAccount personalAccount = db.PersonalAccounts.Find(id);
            if (personalAccount == null)
            {
                return HttpNotFound();
            }
            return View(personalAccount);
        }

        // GET: PersonalAccounts/Create
        public ActionResult Create()
        {
            //ViewBag.CreatedById = new SelectList(db.Users, "Id", "FullName");
            //var user = User.Identity.GetUserId();
            //var houseId = db.Households.Find(user);
            //ViewBag.HouseholdId = new SelectList(user, "Id", "Name", houseId.Id);
            return View();
        }

        // POST: PersonalAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HouseholdId,Name,Balance,ReconciledBalance,CreatedById,IsDeleted")] PersonalAccount personalAccount)
        {
            if (ModelState.IsValid)
            {

                //personalAccount.HouseholdId = User.Identity.GetHouseholdId().Value;

                personalAccount.CreatedById = User.Identity.GetUserId();
                db.PersonalAccounts.Add(personalAccount);
                personalAccount.IsDeleted = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.CreatedById = new SelectList(db.Users, "Id", "FullName", personalAccount.CreatedById);
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", personalAccount.HouseholdId);
            return View(personalAccount);
        }

        // GET: PersonalAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalAccount personalAccount = db.PersonalAccounts.Find(id);
            if (personalAccount == null)
            {
                return HttpNotFound();
            }
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

           
            NumberStyles styles;
            var value = personalAccount.Balance;

     

            //decimal balParse = decimal.Parse(bal.Replace("$", ""));
                
            // styles = NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands/*, new CultureInfo("en-US")*/;
            //ShowNumericValue(value, styles);

            ViewBag.CreatedById = new SelectList(db.Users, "Id", "FullName", personalAccount.CreatedById);
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", personalAccount.HouseholdId);
            return View(personalAccount);
        }

        // POST: PersonalAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HouseholdId,Name,Balance,ReconciledBalance,CreatedById,IsDeleted")] PersonalAccount personalAccount)
        {
            if (ModelState.IsValid)
            {
                //var Bal = StringUtilities personalAccount.Balance
                //if (!Decimal.TryParse/*personalAccount.Balance != decimal*/)


               



                db.Entry(personalAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "FullName", personalAccount.CreatedById);
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", personalAccount.HouseholdId);
            return View(personalAccount);
        }

        // GET: PersonalAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalAccount personalAccount = db.PersonalAccounts.Find(id);
            if (personalAccount == null)
            {
                return HttpNotFound();
            }
            return View(personalAccount);
        }

        // POST: PersonalAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonalAccount personalAccount = db.PersonalAccounts.Find(id);
            db.PersonalAccounts.Remove(personalAccount);
            db.SaveChanges();
            return RedirectToAction("Index");
        }






        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
