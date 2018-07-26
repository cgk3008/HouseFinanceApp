using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using HouseFinanceApp.Models;
using System.Threading.Tasks;
using System.Web;

namespace HouseFinanceApp.Models
{
    public static class Extensions
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static decimal SumTransactions(this Transaction trans)
        {

            var acctId = db.PersonalAccounts.Where(p => p.Id == trans.AccountId);
            var transact = trans.ReconciledAmount;
            var acctTransactions = db.Transactions.Where(t => t.AccountId == acctId).Include(r => r.ReconciledAmount);
            var recBal = db.PersonalAccounts.

        var rBal = transact



                    //decimal sumTransactions = acctTransactions.Sum();

            //var sumTransactions2 = new List<decimal> { acctTransactions };

            if ((trans.Reconciled == true) && (trans.AccountId == acctId))
            {
                foreach (transact)
                {


                }




            }




            var ClaimsTransaction = (Transaction)trans;
            var claim = ClaimsTransaction.ReconciledAmount.


                if (claim != null)
            {
                return claim.Value;

            }

        }


        //public class TransEdit
        //{
        //    public static string TransactionEdit(string amount)

        //    {
        //        var amt = amount.Where
        //    }
        //}



        public class StringUtilities
        {
            public static string CurrencyString(string currency)
            {
                if (currency == null) return "";


                const int maxlen = 80;
                int len = currency.Length;
                bool prevdash = false;
                var sb = new StringBuilder(len);
                char c;
                for (int i = 0; i < len; i++)
                {
                    c = currency[i];
                    if ((c >= '0' && c <= '9'))
                    {
                        sb.Append(c);
                        prevdash = false;
                    }
                    else if (c == '$')
                    {
                        //tricky way to convert to lowercase
                        sb.Append((char)(c | 32));
                        prevdash = false;
                    }
                    else if (c == ',')
                    {
                        if (!prevdash && sb.Length > 0)
                        {
                            sb.Append('-');
                            prevdash = true;
                        }
                    }

                    if (sb.Length == maxlen) break;
                }
                if (prevdash)
                    return sb.ToString().Substring(0, sb.Length - 1);
                else
                    return sb.ToString();

            }

        }

        public static string GetFullName(this IIdentity user)
        {
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();

            var ClaimsUser = (ClaimsIdentity)user;
            var claim = ClaimsUser.Claims.FirstOrDefault(c => c.Type == "Name");
            if (claim != null)
            {
                return claim.Value;

            }
            else
            {
                return null;
            }
        }


        public static string GetCreatedByName(this IIdentity user)
        {
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();

            var ClaimsUser = (ClaimsIdentity)user;
            var claim = ClaimsUser.Claims.FirstOrDefault(c => c.Type == "Name");
            if (claim != null)
            {
                return claim.Value;

            }
            else
            {
                return null;
            }
        }


        //public static int? GetHouseName(this IIdentity user)
        //{
        //    var claimsIdentity = (ClaimsIdentity)user;


        //    var HouseholdClaim = claimsIdentity.Claims
        //      .FirstOrDefault(c => c.Type == "HouseholdId");

        //    var HouseholdClaim1 = claimsIdentity.Claims.Where(u => u.Type == "HouseholdId").


        //    if (HouseholdClaim != null)
        //        return int.Parse(HouseholdClaim.Value);
        //    else
        //        return null;
        //}


        public static int? GetHouseholdId(this IIdentity user)
        {
            var claimsIdentity = (ClaimsIdentity)user;
            var HouseholdClaim = claimsIdentity.Claims
              .FirstOrDefault(c => c.Type == "HouseholdId");
            if (HouseholdClaim != null)
                return int.Parse(HouseholdClaim.Value);
            else
                return null;
        }

        public static int? LeaveHousehold(this IIdentity user)
        {
            var claimsIdentity = (ClaimsIdentity)user;
            var HouseholdClaim = claimsIdentity.Claims
              .FirstOrDefault(c => c.Type == "HouseholdId");
            if (HouseholdClaim != null)
                return int.Parse(HouseholdClaim.Value);
            else
                return null;
        }



        //public static List<Claim> GetPersonalAccounts(this IIdentity user)
        //{
        //    var claimsIdentity = (ClaimsIdentity)user;
        //    var PersonalAccountClaim = claimsIdentity.Claims
        //      .Where(c => c.Type == "Accounts").ToList();
        //    if (PersonalAccountClaim != null)
        //        return (PersonalAccountClaim);
        //    else
        //        return null;
        //}


        public static bool IsInHousehold(this IIdentity user)
        {
            var cUser = (ClaimsIdentity)user;
            var hid = cUser.Claims.FirstOrDefault(c => c.Type == "HouseholdId");
            return (hid != null && !string.IsNullOrWhiteSpace(hid.Value));
        }

        //make sure it goes to create page if not in a household or join house.


    }
}
