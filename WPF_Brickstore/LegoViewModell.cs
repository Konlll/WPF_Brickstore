using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Brickstore
{
    internal class LegoViewModell
    {
        public LegoViewModell(string itemID, string itemName, string categoryName, string colorName, int qty)
        {
            ItemID = itemID;
            ItemName = itemName;
            CategoryName = categoryName;
            ColorName = colorName;
            Qty = qty;
        }

        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public string CategoryName { get; set; }
        public string ColorName { get; set; }
        public int Qty { get; set; }
    }
}
