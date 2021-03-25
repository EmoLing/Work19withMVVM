using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using BankSystem.Model;
using InterfasesLib;
using Prism.Commands;
using Prism.Mvvm;

namespace BankSystem.ViewModel
{
    public class ViewModelContibution : BindableBase
    {
        private Bank bank;
        private object selectedClient;
        private string testContribution;

        public string TestContribution
        {
            get
            {
                return testContribution;
            }
            set
            {
                testContribution = value;
                RaisePropertyChanged("TestContribution");
            }
        }

        public ViewModelContibution(ref Bank bank, object SelectedClient)
        {
            this.bank = bank;
            selectedClient = SelectedClient;

            TestContributionCommand = new DelegateCommand<object[]>(s =>
            {
                RadioButton capitalism = null;
                foreach (var item in s)
                {
                    if (item is RadioButton && ((item as RadioButton).IsChecked == true))
                    {
                        capitalism = item as RadioButton;
                    }
                }

                DateTime time = DateTime.Today;
                time = time.AddMonths(-int.Parse(s[1].ToString()));

                TestContribution = this.bank.Test_Contribution((capitalism.Content == "без капитализации") ? false : true, 
                    DateTime.Now, time, decimal.Parse(s[0].ToString()),
                    selectedClient as IAccount);
            });


            OpenContributionCommand = new DelegateCommand<object[]>(s =>
            {
                RadioButton capitalism = null;
                foreach (var item in s)
                {
                    if (item is RadioButton && ((item as RadioButton).IsChecked == true))
                    {
                        capitalism = item as RadioButton;
                    }
                }

                DateTime time = DateTime.Today;
                time = time.AddMonths(-int.Parse(s[1].ToString()));

                this.bank.Contribution((capitalism.Content == "без капитализации") ? false : true,
                    DateTime.Now, time, decimal.Parse(s[0].ToString()),
                    selectedClient as IAccount);
            });
        }

        public DelegateCommand<object[]>TestContributionCommand { get; }
        public DelegateCommand<object[]> OpenContributionCommand { get; }

    }
}