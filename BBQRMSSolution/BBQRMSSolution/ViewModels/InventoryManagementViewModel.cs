using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace BBQRMSSolution.ViewModels
{

    public class InventoryManagementViewModel:ViewModelBase
    {
        public InventoryManagementViewModel()
        {
            InventoryItems = new ObservableCollection<InventoryItem>
            {
                new InventoryItem {
                                name = "Beef Ribs", 
                                AverageDailyUse = 3,
                                QuantityOnHand = 15,
                                ReorderThreshold = 7,
                                },
                new InventoryItem {
                                name = "Beef Ribs", 
                                AverageDailyUse = 9,
                                QuantityOnHand = 11,
                                ReorderThreshold = 7,
                                },
                new InventoryItem {
                                name = "Roast Beef", 
                                AverageDailyUse = 2,
                                QuantityOnHand = 6,
                                ReorderThreshold = 14,
                                },
                 new InventoryItem {
                                name = "Chicken Breast", 
                                AverageDailyUse = 13,
                                QuantityOnHand = 23,
                                ReorderThreshold = 3,
                                },

            };




            
        }
        public ObservableCollection<InventoryItem> InventoryItems { get; set; }
    }
}
