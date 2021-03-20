using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

#nullable disable

namespace BankSystem
{
    public partial class AllNaturalClient : INotifyPropertyChanged
    {
        private int id;
        private string firstName;
        private string lastName;
        private DateTime dateOfBirth;
        private string repuration;
        private string department;
        private int accountNumber;
        private decimal amountOfMoney;
        private decimal checkContribution;
        private decimal checkDebt;
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged(nameof(id));
            }
        }
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(firstName));
            }
        }
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(lastName));
            }
        }
        public DateTime DateOfBirth
        {
            get
            {
                return dateOfBirth;
            }
            set
            {
                dateOfBirth = value;
                OnPropertyChanged(nameof(dateOfBirth));
            }
        }
        public string Reputation
        {
            get
            {
                return repuration;
            }
            set
            {
                repuration = value;
                OnPropertyChanged(nameof(repuration));
            }
        }
        public string Department
        {
            get
            {
                return department;
            }
            set
            {
                department = value;
                OnPropertyChanged(nameof(department));
            }
        }
        public int AccountNumber
        {
            get
            {
                return accountNumber;
            }
            set
            {
                accountNumber = value;
                OnPropertyChanged(nameof(accountNumber));
            }
        }
        public decimal AmountOfMoney
        {
            get
            {
                return amountOfMoney;
            }
            set
            {
                amountOfMoney = value;
                OnPropertyChanged(nameof(amountOfMoney));
            }
        }
        public decimal CheckContribution
        {
            get
            {
                return checkContribution;
            }
            set
            {
                checkContribution = value;
                OnPropertyChanged(nameof(checkContribution));
            }
        }
        public decimal CheckDebt
        {
            get
            {
                return checkDebt;
            }
            set
            {
                checkDebt = value;
                OnPropertyChanged(nameof(checkDebt));
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
