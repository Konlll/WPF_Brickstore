using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Brickstore
{
    internal class LegoItem
    {
        public LegoItem(string? itemID, string? itemTypeID, int? colorID, string? itemName, string? itemTypeName, string? colorName, int? categoryID, string? categoryName, string? status, int? qty, double? price, string? condition, int? origQty)
        {
            ItemID = itemID;
            ItemTypeID = itemTypeID;
            ColorID = colorID;
            ItemName = itemName;
            ItemTypeName = itemTypeName;
            ColorName = colorName;
            CategoryID = categoryID;
            CategoryName = categoryName;
            Status = status;
            Qty = qty;
            Price = price;
            Condition = condition;
            OrigQty = origQty;
        }

        public string? ItemID { get; set; }
        public string? ItemTypeID { get; set; }
        public int? ColorID { get; set; }
        public string? ItemName { get; set; }
        public string? ItemTypeName { get; set; }
        public string? ColorName { get; set; }
        public int? CategoryID {  get; set; }
        public string? CategoryName { get; set; }
        public string? Status { get; set; }
        public int? Qty { get; set; }
        public double? Price { get; set; }
        public string? Condition { get; set; }
        public int? OrigQty { get; set; }
    }
}
