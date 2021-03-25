using System;
using System.Collections.Generic;
using BankSystem.Legal;

#nullable disable

namespace BankSystem
{
    public partial class AllVipLegalClient : LegalClient
    {
        public override int Id { get; set; }
        public override string Name { get; set; }
        public override DateTime DateOfBirth { get; set; }
        public override string Department { get; set; }
        public override int AccountNumber { get; set; }
        public override decimal AmountOfMoney { get; set; }
        public override decimal CheckContribution { get; set; }
        public override decimal CheckDebt { get; set; }

        public AllVipLegalClient()
        {
        }

        public AllVipLegalClient(string name, DateTime dateOfBirth, string department)
        {
            Id = ClientsFunc.GetId(nameof(AllVipLegalClient));
            Name = name;
            DateOfBirth = dateOfBirth;
            Department = department;
            AccountNumber = ClientsFunc.GetAccountNumber(nameof(AllVipLegalClient));
            AmountOfMoney = 0;
            CheckContribution = 0;
            CheckDebt = 0;
        }
    }
}
