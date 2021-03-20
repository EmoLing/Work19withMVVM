using BankSystem.Model;
using Prism.Mvvm;

namespace BankSystem.ViewModel
{
    public class AddClientViewModel : BindableBase
    {
        private Bank bank;
        public AddClientViewModel(Bank bank)
        {
            this.bank = bank;
        }

        
    }
}