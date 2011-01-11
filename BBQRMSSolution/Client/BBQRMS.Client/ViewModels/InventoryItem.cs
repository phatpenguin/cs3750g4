using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBQRMSSolution.ViewModels
{
    public class InventoryItem
    {
        public String name { get; set; }
        public int QuantityOnHand { get; set; }
        public int AverageDailyUse { get; set; }
        public int ReorderThreshold { get; set; }
        //add in additional attributes to this class from the detail screen
        public InventoryItem()
        {

        }

    }
}
