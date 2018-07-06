using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HouseFinanceApp.Models
{
    public class Budget
    {
        public Budget()
        {
            BudgetItems = new HashSet<BudgetItem>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int HouseholdId { get; set; }

        public virtual Household Household { get; set; }
        public virtual ICollection<BudgetItem> BudgetItems { get; set; }
    }

    public class BudgetItem
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int BudgetId { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }

        public virtual Category Category { get; set; }
        public virtual Budget Budget { get; set; }

    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Household> Households { get; set; }

    }

    public class Household
    {
        public Household()
        {
            Categories = new HashSet<Category>();
            Members = new HashSet<ApplicationUser>();
            Budgets = new HashSet<Budget>();
            Accounts = new HashSet<PersonalAccount>();
            Invites = new HashSet<Invite>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<ApplicationUser> Members { get; set; }
        public virtual ICollection<PersonalAccount> Accounts { get; set; }
        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<Invite> Invites { get; set; }

        public static implicit operator string(Household v)
        {
            throw new NotImplementedException();
        }
    }

    public class Invite
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }
        public string Email { get; set; }
        public Guid HHToken { get; set; }
        public DateTimeOffset InviteDate { get; set; }
        public string InvitedById { get; set; }
        public bool HasBeenUsed { get; set; }

        public virtual Household Household { get; set; }
        public virtual ApplicationUser InvitedBy { get; set; }
    }

    public class InviteSent
    {
        public int Id { get; set; }
        public int InviteId { get; set; }
        public ApplicationUser User { get; set; }
        public int IsValid { get; set; }
    }

    public class PersonalAccount
    {
        public PersonalAccount()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public int HouseholdId { get; set; }
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal Balance { get; set; }
        [Display(Name = "Reconciled Balance")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal ReconciledBalance { get; set;


//public ApplicationDbContext Db { get => db; set => db = value; }

       //find get method to subtract transaction from balance to equal reconciledBalance

//        set
//            {
                
//var recBal = (db.personalAccounts.Where( b => b. Balance - db.
//            }
                

        }
        public string CreatedById { get; set; }
        [Display(Name = "Deleted")]
        public bool IsDeleted { get; set; }


        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual Household Household { get; set; }
        public virtual ApplicationUser CreatedBy { get; set; }

    }

    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTimeOffset Date { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }
        [Display(Name = "Credit")]
        public bool Type { get; set; }
        public bool Void { get; set; }
        public int CategoryId { get; set; }
        [Display(Name = "Entered By")]
        public string EnteredById { get; set; }
        public bool Reconciled { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal ReconciledAmount { get; set; }
        public bool IsDeleted { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTimeOffset Created { get; set; }

        public virtual PersonalAccount Account { get; set; }
        public virtual Category Category { get; set; }
        public virtual ApplicationUser EnteredBy { get; set; }

    }

    public class ChartData
    {
        //public virtual Transaction Transactions { get; set; }
        public string X { get; set; }
        public decimal Y { get; set; }
    }


    public class FusionData
    {
        //public virtual Transaction Transactions { get; set; }
        public string Label { get; set; }
        public decimal Value { get; set; }
    }

    public class HouseChart
    {
        //public virtual PersonalAccount PersonalAccount { get; set; }
        public string Label { get; set; }
        public decimal Value { get; set; }

    }


}