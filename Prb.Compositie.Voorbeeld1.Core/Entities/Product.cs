using System;
using System.Collections.Generic;
using System.Text;

namespace Prb.Compositie.Voorbeeld1.Core.Entities
{
    public class Product
    {
        private string code;

        public string Code
        {
            get { return code; }
            set
            {
                if (value == "")
                    value = Guid.NewGuid().ToString();
                code = value.ToUpper();
            }
        }

        public string Description { get; set; }
        public decimal NetPrice { get; set; }
        public VAT Vat { get; set; }
        public Group ProductGroup { get; set; }
        public decimal GrossPrice
        {
            get
            {
                return NetPrice + (NetPrice * Vat.Tarif);
            }
        }
        public Product()
        { }
        public Product(string code, string description, decimal netPrice, VAT vat, Group productGroup)
        {
            Code = code;
            Description = description;
            NetPrice = netPrice;
            Vat = vat;
            ProductGroup = productGroup;
        }
        public override string ToString()
        {
            return $"{Code} - {Description}";
        }
    }
}
