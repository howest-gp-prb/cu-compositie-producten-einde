using System;
using System.Collections.Generic;
using System.Text;
using Prb.Compositie.Voorbeeld1.Core.Entities;
using System.Linq;

namespace Prb.Compositie.Voorbeeld1.Core.Services
{
    public class GlobalService
    {
        public List<Product> Products { get; private set; }
        public List<Group> Groups { get; private set; }
        public List<VAT> VATS { get; private set; }

        public GlobalService()
        {
            VATS = new List<VAT>();
            Products = new List<Product>();
            Groups = new List<Group>();
            DoSeeding();
        }
        private void DoSeeding()
        {
            //// versie 1 
            //VAT vat006 = new VAT(0.06m);
            //VAT vat021 = new VAT(0.21m);
            //Group grpRepen = new Group("Repen");
            //Group grpFrisdranken = new Group("Frinsdranken");

            //VATS.Add(vat006);
            //VATS.Add(vat021);
            //Groups.Add(grpRepen);
            //Groups.Add(grpFrisdranken);
            //Products.Add(new Product("A001", "TWIX", 15.45M, vat021, grpRepen));
            //Products.Add(new Product("A002", "MARS", 16.15M, vat021, grpRepen));
            //Products.Add(new Product("A003", "BOUNTY", 16M, vat021, grpRepen));
            //Products.Add(new Product("A004", "SNICKER", 15.45M, vat021, grpRepen));
            //Products.Add(new Product("A005", "MILKY WAY", 9.99M, vat021, grpRepen));
            //Products.Add(new Product("B001", "COCA COLA", 33.14M, vat006, grpFrisdranken));
            //Products.Add(new Product("B002", "COCA COLA LIGHT", 33.14M, vat006, grpFrisdranken));
            //Products.Add(new Product("B003", "ICE TEA", 29.99M, vat006, grpFrisdranken));

            // versie 2
            VATS.Add(new VAT(0.06M));
            VATS.Add(new VAT(0.21M));

            Groups.Add(new Group("Repen"));
            Groups.Add(new Group("Frisdranken"));

            Products.Add(new Product("A001", "TWIX", 15.45M, FindVAT(0.21m), FindGroup("Repen")));
            Products.Add(new Product("A002", "MARS", 16.15M, FindVAT(0.21m), FindGroup("Repen")));
            Products.Add(new Product("A003", "BOUNTY", 16M, FindVAT(0.21m), FindGroup("Repen")));
            Products.Add(new Product("A004", "SNICKER", 15.45M, FindVAT(0.21m), FindGroup("Repen")));
            Products.Add(new Product("A005", "MILKY WAY", 9.99M, FindVAT(0.21m), FindGroup("Repen")));
            Products.Add(new Product("B001", "COCA COLA", 33.14M, FindVAT(0.06m), FindGroup("Frisdranken")));
            Products.Add(new Product("B002", "COCA COLA LIGHT", 33.14M, FindVAT(0.06m), FindGroup("Frisdranken")));
            Products.Add(new Product("B003", "ICE TEA", 29.99M, FindVAT(0.06m), FindGroup("Frisdranken")));
        }

        private VAT FindVAT(decimal tarif)
        {
            foreach(VAT vat in VATS)
            {
                if(vat.Tarif == tarif)
                {
                    return vat;
                }    
            }
            return null;
        }
        private Group FindGroup(string groupName)
        {
            foreach(Group group in Groups)
            {
                if(group.GroupName == groupName)
                {
                    return group;
                }
            }
            return null;
        }

        private void Sort()
        {
            Products = Products.OrderBy(p => p.Code).ToList();
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
            Sort();
        }
        public void DeleteProduct(Product product)
        {
            Products.Remove(product);
        }
    }
}
