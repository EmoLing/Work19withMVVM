using InterfacesLib;

namespace BankSystem
{
    public abstract class Client : IAccount
    {
        public abstract int Id { get; set; }
        public abstract string Department { get; set; }
        public abstract int AccountNumber { get; set; }
        public abstract decimal AmountOfMoney { get; set; }
    }
}