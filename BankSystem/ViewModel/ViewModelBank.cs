using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using BankSystem.Model;
using BankSystem.View;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace BankSystem.ViewModel
{
    public class ViewModelBank : BindableBase
    {
        private Model.Bank bank = new Bank();
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
        public ViewModelBank()
        {
            bank.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };

            RemoveCommand = new DelegateCommand<object>(i =>
            {
                if (i != null)
                    bank.RemoveValue(i);
            });
            EditCommand = new DelegateCommand(()=> SaveChages());
            AddCommand = new DelegateCommand(() =>
            {
                AddClientViewModel addClientViewModel = new AddClientViewModel(bank);
                AddClientWindow addClientWindow = new AddClientWindow(addClientViewModel);
                addClientWindow.Show();
            });

            TransactCommand = new DelegateCommand<object>(i =>
            {
                if (i == null)
                {
                    MessageBox.Show("ВЫБЕРИТЕ КЛИЕНТА!!!!");
                }
                else
                {
                    TransactViewModel viewModel = new TransactViewModel(ref bank, i);
                    TransactWindow transactWindow = new TransactWindow(viewModel);

                    if (transactWindow.ShowDialog() == true)
                    {
                        transactWindow.Show();
                        transactWindow.Activate();
                    }
                    SaveChages();
                }

            });

            OpenContibutionCommand = new DelegateCommand<object>(s =>
            {
                if (s == null)
                {
                    MessageBox.Show("ВЫБЕРИТЕ КЛИЕНТА!!!!");
                }
                else
                {
                    ViewModelContibution viewModel = new ViewModelContibution(ref bank, s);
                    ContributionWindow contibutionWindow = new ContributionWindow(viewModel);

                    if (contibutionWindow.ShowDialog() == true)
                    {
                        contibutionWindow.Show();
                        contibutionWindow.Activate();
                    }
                    SaveChages();
                }
            });
        }


        public void SaveChages()
        {
            bank.clientsDbContext.SaveChanges();
        }

        public DelegateCommand AddCommand { get; }
        public DelegateCommand<object> RemoveCommand { get; }
        public DelegateCommand EditCommand { get; }
        public DelegateCommand<object> TransactCommand { get; }
        public DelegateCommand<object> OpenContibutionCommand { get; }

        public ReadOnlyObservableCollection<AllLegalClient> AllLegalClients => bank.AllLegalClients;
        public ReadOnlyObservableCollection<AllNaturalClient> AllNaturalClients => bank.AllNaturalClients;
        public ReadOnlyObservableCollection<AllVipLegalClient> AllVipLegalClients => bank.AllVipLegalClients;
        public ReadOnlyObservableCollection<AllVipNaturalClient> AllVipNaturalClients => bank.AllVipNaturalClients;
    }
}