using System;
using System.Collections.Generic;
using System.Text;

namespace Packt.Shared
{
    public class BankAccount
    {
        public string AccountName; // член экземпляра
        public decimal Balance; // член экземпляра
        public static decimal InterestRate; // общий член
    }
}
