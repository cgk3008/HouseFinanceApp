using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseFinanceApp.Models
{
    public class DashViewModel
    {

        //I added this thinking I need it passed to Home Index page for chart generation
        public ApplicationDbContext db = new ApplicationDbContext();

        public int HouseId { get; set; }
        public IEnumerable<ChartData> ChartsData { get; set; }
        public IEnumerable<FusionData> FusionsData { get; set; }
        public IEnumerable<HouseChart> HouseCharts { get; set; }

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