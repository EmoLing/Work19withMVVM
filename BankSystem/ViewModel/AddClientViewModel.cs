using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Documents;
using BankSystem.Model;
using Microsoft.VisualBasic;
using Prism.Commands;
using Prism.Mvvm;

namespace BankSystem.ViewModel
{
    public class AddClientViewModel : BindableBase
    {
        private Bank bank;
        private string date;

        public string Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                RaisePropertyChanged("Date");
            }
        }

        public AddClientViewModel(Bank bank)
        {
            this.bank = bank;
            AddCommand = new DelegateCommand<object[]>((i) => AddClientCommand(i));
            TextIsDateCommand = new DelegateCommand<string>(text=>
            {
                Date = text;
                if (!TextIsDate(text))
                {
                    MessageBox.Show("ОШИБКА");
                    Date = string.Empty;
                }
                
            });
        }

        public DelegateCommand<object[]> AddCommand { get; }
        public DelegateCommand<string>TextIsDateCommand { get; set; }
        private void AddClientCommand(object[] temp)
        {
            if (temp == null)
                return;

            List<object> list = new List<object>();
            foreach (var item in temp)
            {
                if (!string.IsNullOrEmpty(item.ToString()))
                {
                    list.Add(item);
                }
            }
        }


        /// <summary>
        /// Проверка на то, чтобы дата была по формату dd.mm.yyyy
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        static bool TextIsDate(string text)
        {
            var dateFormat = "dd.MM.yyyy";
            var dateFormat2 = "dd,MM,yyyy";
            DateTime scheduleDate;
            if (DateTime.TryParseExact(text, dateFormat, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out scheduleDate)
                || DateTime.TryParseExact(text, dateFormat2, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out scheduleDate))
            {
                return true;
            }
            return false;
        }
    }
}