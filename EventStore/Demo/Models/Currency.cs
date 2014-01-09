using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models
{
    public sealed class Currency
    {
        public string Name { get; private set; }

        public Currency(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            var currency = obj as Currency;
            if (currency == null)
                return false;

            return Name == currency.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
