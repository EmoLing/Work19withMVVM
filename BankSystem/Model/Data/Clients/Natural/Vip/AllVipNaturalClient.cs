using System;
using System.Collections.Generic;

#nullable disable

namespace BankSystem
{
    public partial class AllVipNaturalClient : NaturalClient
    {
        public override int Id { get; set; }
        public override string FirstName { get; set; }
        public override string LastName { get; set; }
        public override DateTime DateOfBirth { get; set; }
        public override string Department { get; set; }
        public override int AccountNumber { get;  set; }
        public override decimal AmountOfMoney { get; set; }
        public decimal CheckContribution { get; set; }
        public decimal CheckDebt { get; set; }


        public AllVipNaturalClient()
        {
            
        }
        public AllVipNaturalClient(string firstName, string lastName, DateTime dateOfBirth, string department)
        {
            Id = ClientsFunc.GetId(nameof(AllVipNaturalClient));
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Department = department;
            AccountNumber = ClientsFunc.GetAccountNumber(nameof(AllVipNaturalClient));
            AmountOfMoney = 0;
            CheckContribution = 0;
            CheckDebt = 0;
        }
    }
}
