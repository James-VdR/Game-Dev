using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enumerator_test
{
    internal class Inventory
    {
        private List<Item> items = new List<Item>();

        public Inventory()
        {

        }

        //voor wie // wat // naam // ( argumenten ) { scope/body }
        public List<Item> GetItems()
        {
            return items;
        }
        //vooer wie? (public of private) wat geeft de functie terug? (void of bool of item of whatever?) wat is de naam van de functie? ( argumenten) { scope }

        public void AddItem(Item item)
        {
            items.Add(item);
        }
        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }
    }
}
