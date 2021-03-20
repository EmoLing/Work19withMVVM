﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BankSystem
{
    public partial class AllVipLegalClient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; }
        public int AccountNumber { get; set; }
        public decimal AmountOfMoney { get; set; }
        public decimal CheckContribution { get; set; }
        public decimal CheckDebt { get; set; }
    }
}
