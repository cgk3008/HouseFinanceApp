using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HouseFinanceApp.Models
{
    public class TransactionAccountViewModel
    {
      

        public int AccountId { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal CurrentBal { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal ReconciledBal { get; set; }

        public PersonalAccount PersAcct { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
       
        

        //public ApplicationUser Member { get; set; }

        //public bool IsJoinHouse { get; set; }
        //public int? HHId { get; set; }
        //public string HHName { get; set; }


    }






}
