using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Prism.Mvvm;

namespace BankSystem.Model
{
    public class Bank : BindableBase
    {
        public ClientsDBContext clientsDbContext = new ClientsDBContext();

        private readonly ObservableCollection<AllLegalClient> allLegalClients = new ObservableCollection<AllLegalClient>();
        public ReadOnlyObservableCollection<AllLegalClient> AllLegalClients;

        private  readonly ObservableCollection<AllNaturalClient> allNaturalClients = new ObservableCollection<AllNaturalClient>();
        public ReadOnlyObservableCollection<AllNaturalClient> AllNaturalClients;


        private readonly ObservableCollection<AllVipLegalClient> allVipLegal = new ObservableCollection<AllVipLegalClient>();
        public ReadOnlyObservableCollection<AllVipLegalClient> AllVipLegalClients;

        private readonly ObservableCollection<AllVipNaturalClient> allVipNatural = new ObservableCollection<AllVipNaturalClient>();
        public ReadOnlyObservableCollection<AllVipNaturalClient> AllVipNaturalClients;

        

        public Bank()
        {
            clientsDbContext.AllNaturalClients.Load();
            allNaturalClients = clientsDbContext.AllNaturalClients.Local.ToObservableCollection();

            clientsDbContext.AllLegalClients.Load();
            allLegalClients = clientsDbContext.AllLegalClients.Local.ToObservableCollection();

            clientsDbContext.AllVipNaturalClients.Load();
            allVipNatural = clientsDbContext.AllVipNaturalClients.Local.ToObservableCollection();

            clientsDbContext.AllVipLegalClients.Load();
            allVipLegal = clientsDbContext.AllVipLegalClients.Local.ToObservableCollection();

            AllLegalClients = new ReadOnlyObservableCollection<AllLegalClient>(allLegalClients);
            AllNaturalClients = new ReadOnlyObservableCollection<AllNaturalClient>(allNaturalClients);
            AllVipLegalClients = new ReadOnlyObservableCollection<AllVipLegalClient>(allVipLegal);
            AllVipNaturalClients = new ReadOnlyObservableCollection<AllVipNaturalClient>(allVipNatural);

        }

        public void RemoveValue<T>(T item)
        {
            if (item is AllNaturalClient)
            {
                if (allNaturalClients.Contains(item as AllNaturalClient))
                {
                    allNaturalClients.Remove(item as AllNaturalClient);
                    clientsDbContext.SaveChanges();
                }
            }
            else if (item is AllLegalClient)
            {
                if (allLegalClients.Contains(item as AllLegalClient))
                {
                    allLegalClients.Remove(item as AllLegalClient);
                    clientsDbContext.SaveChanges();
                }
            }
            else if (item is AllVipNaturalClient)
            {
                if (allVipNatural.Contains(item as AllVipNaturalClient))
                {
                    allVipNatural.Remove(item as AllVipNaturalClient);
                    clientsDbContext.SaveChanges();
                }
            }
            else if (item is AllVipLegalClient)
            {
                if (allVipLegal.Contains(item as AllVipLegalClient))
                {
                    allVipLegal.Remove(item as AllVipLegalClient);
                    clientsDbContext.SaveChanges();
                }
            }
        }

        public void AddClient<T>(List<T> item, string depart, string type)
        {
            List<string> itemTemp = new List<string>();

            foreach (var temp in item)
            {
                itemTemp.Add(temp.ToString());
            }

            if (type == "Обычный" && depart == "Физ лицо")
            {
                allNaturalClients.Add(new AllNaturalClient(itemTemp[0],itemTemp[1],DateTime.Parse(itemTemp[2]), "Физический"));
                clientsDbContext.SaveChanges();
            }
            else if (type == "Обычный" && depart == "Юр лицо")
            {

            }
            else if (type == "VIP" && depart == "Физ лицо")
            {

            }
            else if (type == "VIP" && depart == "Юр лицо")
            {

            }
            if (item is AllNaturalClient)
            {
                if (allNaturalClients.Contains(item as AllNaturalClient))
                {
                    allNaturalClients.Add(item as AllNaturalClient);
                    clientsDbContext.SaveChanges();
                }
            }
            else if (item is AllLegalClient)
            {
                if (allLegalClients.Contains(item as AllLegalClient))
                {
                    allLegalClients.Add(item as AllLegalClient);
                    clientsDbContext.SaveChanges();
                }
            }
            else if (item is AllVipNaturalClient)
            {
                if (allVipNatural.Contains(item as AllVipNaturalClient))
                {
                    allVipNatural.Add(item as AllVipNaturalClient);
                    clientsDbContext.SaveChanges();
                }
            }
            else if (item is AllVipLegalClient)
            {
                if (allVipLegal.Contains(item as AllVipLegalClient))
                {
                    allVipLegal.Add(item as AllVipLegalClient);
                    clientsDbContext.SaveChanges();
                }
            }
        }

    }
}