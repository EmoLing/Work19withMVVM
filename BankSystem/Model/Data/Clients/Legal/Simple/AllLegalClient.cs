using System;
using System.Collections.Generic;
using BankSystem.Legal;

#nullable disable

namespace BankSystem
{
    public partial class AllLegalClient : LegalClient
    {
        private int id;
        private string name;
        private DateTime dateOfBirth;
        private string reputation;
        private string department;
        private int accountNumber;
        private decimal amountOfMoney;
        private decimal checkContribution;
        public decimal checkDebt;

        public override int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                RaisePropertyChanged("Id");
            }
        }

        public override string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }

        public override DateTime DateOfBirth
        {
            get
            {
                return dateOfBirth;
            }
            set
            {
                dateOfBirth = value;
                RaisePropertyChanged("DateOfBirth");
            }
        }

        public string Reputation
        {
            get
            {
                return reputation;
            }
            set
            {
                reputation = value;
                RaisePropertyChanged("Reputation");
            }
        }
        public override string Department
        {
            get
            {
                return department;
            }
            set
            {
                department = value;
                RaisePropertyChanged("Department");
            }
        }

        public override int AccountNumber
        {
            get
            {
                return accountNumber;
            }
            set
            {
                accountNumber = value;
                RaisePropertyChanged("AccountNumber");
            }
        }

        public override decimal AmountOfMoney
        {
            get { return amountOfMoney; }
            set
            {
                amountOfMoney = value;
                RaisePropertyChanged("AmountOfMoney");
            }
        }
        public override decimal CheckContribution
        {
            get { return checkContribution; }
            set
            {
                checkContribution = value;
                RaisePropertyChanged("CheckContribution");
            }

        }

        public override decimal CheckDebt
        {
            get
            {
                return checkDebt;
            }
            set
            {
                checkDebt = value;
                RaisePropertyChanged("CheckDebt");
            }
        }

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
