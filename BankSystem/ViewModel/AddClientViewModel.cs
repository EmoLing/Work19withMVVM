using BankSystem.Model;
using Prism.Commands;
using Prism.Mvvm;

namespace BankSystem.ViewModel
{
    public class AddClientViewModel : BindableBase
    {
        private Bank bank;
        private object selectedBank;
        public object SelectedBank
        {
            get { return selectedBank; }
            set
            {
                selectedBank = value;
                RaisePropertyChanged("SelectedBank");
            }
        }
        public AddClientViewModel(Bank bank)
        {
            this.bank = bank;
            AddCommand = new DelegateCommand<object>((i) =>
            {
                if (i != null)
                {
                    bank.AddClient(i);
                }
            });
        }

        public DelegateCommand<object> AddCommand { get; }

    }
}