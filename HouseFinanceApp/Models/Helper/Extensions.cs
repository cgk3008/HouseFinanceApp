using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;

namespace HouseFinanceApp.Models
{
    public static class Extensions
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

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
