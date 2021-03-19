using System;
using System.Collections.Generic;
using System.Text;

namespace Prb.Compositie.Voorbeeld1.Core.Entities
{
    public class VAT
    {
        public decimal Tarif { get; set; }
        public VAT()
        { }
        public VAT(decimal tarif)
        {
            Tarif = tarif;
        }
        public override string ToString()
        {
            return $"{Tarif.ToString("0.00")}";
        }
    }
}
