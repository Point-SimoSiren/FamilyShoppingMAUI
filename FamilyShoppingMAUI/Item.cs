using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyShoppingMAUI
{
    internal class Item
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int Pieces { get; set; }
        public DateTime TimeAdded { get; set; }
    }
}
