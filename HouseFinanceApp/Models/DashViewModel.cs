using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseFinanceApp.Models
{
    public class DashViewModel
    {

        public IEnumerable<Household> Households { get; set; }
        public IEnumerable<PersonalAccount> PersonalAccounts { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
        public IEnumerable<Invite> Invites { get; set; }
        public IEnumerable<PersonalAccount> HouseAccounts { get; set; }
        public IEnumerable<Transaction> HouseTransactions { get; set; }

        //public ApplicationUser Member { get; set; }

        //public bool IsJoinHouse { get; set; }
        //public int? HHId { get; set; }
        //public string HHName { get; set; }


    }
}