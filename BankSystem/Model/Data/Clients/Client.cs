using InterfasesLib;
using Prism.Mvvm;

namespace BankSystem
{
    public abstract class Client : BindableBase,IAccount
    {
        public abstract int Id { get; set; }
        public abstract string Department { get; set; }
        public abstract int AccountNumber { get; set; }
        public abstract decimal AmountOfMoney { get; set; }
        public abstract decimal CheckContribution { get; set; }
        public abstract decimal CheckDebt { get; set; }
    }
}