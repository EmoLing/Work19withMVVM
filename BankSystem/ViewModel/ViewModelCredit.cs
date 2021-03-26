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
        #region Поля
        private Bank bank;
        private object selectedClient;
        private string boxEvermonth;
        private string boxSum;
        private TextBox boxCountMonth;
        private TextBox boxTestTime;
        private string boxDebt;
        private string boxFirstSum;
        #endregion

        #region Свойства
        /// <summary>
        /// Значение задолжности
        /// </summary>
        public string BoxDebt
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
        /// <summary>
        /// Значение первого платежа
        /// </summary>
        public string BoxFirstSum
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
        /// <summary>
        /// Значение ежемесячного платежа
        /// </summary>
        public string BoxEvermonth
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
        #endregion

        #region Конструктор
        public ViewModelCredit(ref Bank bank, object SelectedClient)
        {
            selectedClient = SelectedClient;
            this.bank = bank;

            SeeTestCreditCommand = new DelegateCommand<object[]>(s => SeeTestCredit(s));
            CreditCommand = new DelegateCommand<object[]>(s => GetCredit(s));
        }
        #endregion

        #region Команды
        /// <summary>
        /// Просмотр кредита
        /// </summary>
        public DelegateCommand<object[]> SeeTestCreditCommand { get; }
        /// <summary>
        /// Открытие кредита
        /// </summary>
        public DelegateCommand<object[]> CreditCommand { get; }
        #endregion

        #region Методы
        /// <summary>
        /// Просмотр тестового кредита
        /// </summary>
        /// <param name="items">TextBoxes</param>
        private void SeeTestCredit(object[] items)
        {
            try
            {
                string LostDebp = string.Empty;
                string firstSum = string.Empty;
                BoxEvermonth = bank.TestCredit(decimal.Parse((items[0] as TextBox).Text),
                    int.Parse((items[1] as TextBox).Text),
                    DateTime.Now, DateTime.Now.AddMonths(int.Parse((items[5] as TextBox).Text)),
                    out LostDebp, out firstSum, selectedClient as IAccount);
                BoxDebt = LostDebp;
                BoxFirstSum = firstSum;
            }
            catch (AgeExceptions exceptions)
            {
                MessageBox.Show(exceptions.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            catch (FormatException)
            {
                MessageBox.Show("НЕ КОРРЕКТНЫЙ ФОРМАТ", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        /// <summary>
        /// Оформление кредита
        /// </summary>
        /// <param name="items">TextBoxes</param>
        private void GetCredit(object[] items)
        {
            try
            {
                bank.Credit(decimal.Parse((items[0] as TextBox).Text),
                    int.Parse((items[1] as TextBox).Text),
                    DateTime.Now, DateTime.Now.AddMonths(int.Parse((items[5] as TextBox).Text)), 
                    selectedClient as IAccount);
            }
            catch (AgeExceptions exceptions)
            {
                MessageBox.Show(exceptions.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }
        #endregion
    }
}