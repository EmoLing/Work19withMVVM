namespace BankSystem
{
    /// <summary>
    /// Класс для эвента
    /// </summary>
    public class AccountEventArgs
    {
        public string Message { get; }

        public decimal Sum { get; }

        public AccountEventArgs(string mes, decimal sum)
        {
            Message = mes;
            Sum = sum;
        }

        public AccountEventArgs(string mes)
        {
            Message = mes;
        }
    }
}