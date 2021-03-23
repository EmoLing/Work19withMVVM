using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BankSystem.Model;
using Prism.Commands;
using Prism.Mvvm;

namespace BankSystem.ViewModel
{
    public class AddClientViewModel : BindableBase
    {
        private Bank bank;
        private string date;
        private string createDate;
        private bool stateButton;
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
        public string DateCreate
        {
            get
            {
                return createDate;
            }
            set
            {
                date = value;
                RaisePropertyChanged("DateCreate");
            }
        }
        public bool StateButton
        {
            get
            {
                return stateButton;
            }
            set
            {
                stateButton = value;
                RaisePropertyChanged("StateButton");
            }
        }

        public AddClientViewModel(Bank bank)
        {
            this.bank = bank;
            AddCommand = new DelegateCommand<object[]>((i) => AddClientCommand(i));
            TextIsDateCommand = new DelegateCommand<object>(box=>
            {
                if ((box as TextBox).Name == "boxBirthday")
                    Date = (box as TextBox).Text;
                else
                    DateCreate = (box as TextBox).Text;

                if (!TextIsDate((box as TextBox).Text.ToString()))
                {
                    MessageBox.Show("ОШИБКА");
                    Date = string.Empty;
                    if ((box as TextBox).Name == "boxBirthday")
                        Date = string.Empty;
                    else
                        DateCreate = string.Empty;

                    StateButton = false;
                    (box as TextBox).Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        }
                else
                {
                    StateButton = true;
                    (box as TextBox).Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                }
                
            });

            BlockChechCommand = new DelegateCommand<Object[]>(s =>
            {
                foreach (var item in s)
                {
                    if (item is TextBox)
                    {
                        (item as TextBox).IsEnabled = false;
                        (item as TextBox).Text = string.Empty;
                    }
                    else if (item is ComboBox)
                    {
                        (item as ComboBox).IsEnabled = false;
                        (item as ComboBox).Text = string.Empty;
                    }
                }
            });
            UnBlockChechCommand = new DelegateCommand<Object[]>(s =>
            {
                foreach (var item in s)
                {
                    if (item is TextBox)
                    {
                        (item as TextBox).IsEnabled = true;
                    }
                    else if (item is ComboBox)
                    {
                        (item as ComboBox).IsEnabled = true;
                    }
                }
            });
        }

        public DelegateCommand<object[]> AddCommand { get; }
        public DelegateCommand<object>TextIsDateCommand { get; set; }
        public DelegateCommand<Object[]> BlockChechCommand { get; set; }
        public DelegateCommand<Object[]> UnBlockChechCommand { get; set; }




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