using System;
using System.Windows;
using System.Windows.Controls;
using BankSystem.Model;
using InterfasesLib;
using Prism.Commands;
using Prism.Mvvm;

namespace BankSystem.ViewModel
{
    public class ViewModelCredit : BindableBase
    {
        private Bank bank;
        private object selectedClient;
        private TextBox boxEvermonth;
        private TextBox boxSum;
        private TextBox boxCountMonth;
        private TextBox boxTestTime;
        private TextBox boxDebt;
        private TextBox boxFirstSum;

        public TextBox BoxDebt
        {
            get
            {
                return boxDebt;
            }
            set
            {
                boxDebt = value;
                RaisePropertyChanged("boxDebt");
            }
        }
        public TextBox BoxFirstSum
        {
            get
            {
                return boxFirstSum;
            }
            set
            {
                boxFirstSum = value;
                RaisePropertyChanged("boxFirstSum");
            }
        }
        public TextBox BoxEvermonth
        {
            get
            {
                return boxEvermonth;
            }
            set
            {
                boxEvermonth = value;
                RaisePropertyChanged("BoxEvermonth");
            }
        }

        public ViewModelCredit(ref Bank bank, object SelectedClient)
        {
            selectedClient = SelectedClient;
            this.bank = bank;

            SeeTestCreditCommand = new DelegateCommand<object[]>(s => SeeTestCredit(s));
        }

        public DelegateCommand<object[]> SeeTestCreditCommand { get; }

        private void SeeTestCredit(object[] items)
        {
            try
            {
                string LostDebp = string.Empty;
                string firstSum = string.Empty;
                BoxEvermonth.Text = bank.TestCredit(decimal.Parse((items[0] as TextBox).Text),
                    int.Parse((items[1] as TextBox).Text),
                    DateTime.Now, DateTime.Now.AddMonths(int.Parse((items[5] as TextBox).Text)), 
                    out LostDebp, out firstSum, selectedClient as IAccount);
                BoxDebt.Text = LostDebp;
                BoxFirstSum.Text = firstSum;
            }
            catch (AgeExceptions exceptions)
            {
                MessageBox.Show(exceptions.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }
    }
}