using System;
using System.Collections.Generic;
using BankSystem.Legal;

#nullable disable

namespace BankSystem
{
    public partial class AllLegalClient : LegalClient
    {
        public override int Id { get; set; }
        public override string Name { get; set; }
        public override DateTime DateOfBirth { get; set; }
        public string Reputation { get; set; }
        public override string Department { get; set; }
        public override int AccountNumber { get; set; }
        public override decimal AmountOfMoney { get; set; }
        public decimal CheckContribution { get; set; }
        public decimal CheckDebt { get; set; }

        public AllLegalClient()
        {
        }
        public AllLegalClient(string name, DateTime dateOfBirth, string department)
        {
            Id = ClientsFunc.GetId(nameof(AllLegalClient));
            Name = name;
            DateOfBirth = dateOfBirth;
            Reputation = ClientsFunc.GetReputatuion();
            Department = department;
            AccountNumber = ClientsFunc.GetAccountNumber(nameof(AllLegalClient));
            AmountOfMoney = 0;
            CheckContribution = 0;
            CheckDebt = 0;
        }
    }
}
