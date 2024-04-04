using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enumerator_test
{
    internal class Item
    {
        private string itemName;

        public Item(string ItemName)
        {

            itemName = ItemName;
        }

        public string GetName() { return itemName; }
    }
}



