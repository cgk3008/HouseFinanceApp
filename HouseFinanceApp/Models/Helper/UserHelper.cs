using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseFinanceApp.Models.Helper
{
    public class UserHelper
    {

        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        private static ApplicationDbContext db = new ApplicationDbContext();

        public static string GetUserName(string userId)
        {
            ApplicationUser user = db.Users.Find(userId);
            return user.FullName;
        }


        public bool IsUserInRole(string UserId, string Role)
        {
            try   /*use try catch statements whenever utlizing/clicking light bulb for  "using Microsoft.AspNet.Identity;" or other one you did not make. or write to external file that lists errors*/
            {
                var result = userManager.IsInRole(UserId, Role);
                return result;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            //finally use if want to display or bundle/categorize errors

        }


        public ICollection<string> ListRolesForUser(string UserId)
        {
            return userManager.GetRoles(UserId);
        }

        //modify this? see ListRolesNotForUser, string not "User"
        public ICollection<ApplicationUser> ListUsersInRole(string Role)
        {
            List<ApplicationUser> roleUsers = new List<ApplicationUser>();
            List<ApplicationUser> users = userManager.Users.ToList();

            foreach (var u in users)
            {
                if (IsUserInRole(u.Id, Role))
                {
                    roleUsers.Add(u);
                }
            }
            return roleUsers;
        }


        public bool AddUserToRole(string UserId, string Role)
        {
            try
            {
                var result = userManager.AddToRole(UserId, Role);
                return result.Succeeded;
            }
            catch
            {
                return false;
            }

        }

        public bool RemoveUserFromRole(string UserId, string Role)
        {
            try
            {
                var result = userManager.RemoveFromRole(UserId, Role);
                return result.Succeeded;
            }

            catch
            {
                return false;
            }
        }


        //modify this? see ListRolesNotForUser, string not "User"
        public ICollection<ApplicationUser> ListUsersNotInRole(string Role)
        {
            List<ApplicationUser> roleUsers = new List<ApplicationUser>();
            List<ApplicationUser> users = userManager.Users.ToList();

            foreach (var u in users)
            {
                if (!IsUserInRole(u.Id, Role))
                {
                    roleUsers.Add(u);
                }
            }
            return roleUsers;
        }



    }
}