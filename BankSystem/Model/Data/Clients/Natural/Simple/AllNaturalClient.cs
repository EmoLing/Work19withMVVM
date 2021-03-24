using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

#nullable disable

namespace BankSystem
{
    public partial class AllNaturalClient : NaturalClient
    {
        public override int Id { get; set; }
        public override string FirstName { get; set; }
        public override string LastName { get; set; }
        public override DateTime DateOfBirth { get; set; }
        public string Reputation { get; set; }
        public override string Department { get; set; }
        public override int AccountNumber { get; set; }
        public override decimal AmountOfMoney { get; set; }
        public decimal CheckContribution { get; set; }
        public decimal CheckDebt { get; set; }

        public AllNaturalClient()
        {
            
        }
        public AllNaturalClient(string firstName, string lastName, DateTime dateOfBirth, string department)
        {
            Id = ClientsFunc.GetId(nameof(AllNaturalClient));
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Reputation = ClientsFunc.GetReputatuion();
            Department = department;
            AccountNumber = ClientsFunc.GetAccountNumber(nameof(AllNaturalClient));
            AmountOfMoney = 0;
            CheckContribution = 0;
            CheckDebt = 0;
        }
    }
}

