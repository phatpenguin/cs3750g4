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
        	                 		new InventoryItem
        	                 			{
        	                 				name = "Ribs (pounds)",
        	                 				AverageDailyUse = 3,
        	                 				QuantityOnHand = 15,
        	                 				ReorderThreshold = 7,
        	                 			},
        	                 		new InventoryItem
        	                 			{
        	                 				name = "Brisket (pounds)",
        	                 				AverageDailyUse = 9,
        	                 				QuantityOnHand = 11,
        	                 				ReorderThreshold = 7,
        	                 			},
        	                 		new InventoryItem
        	                 			{
        	                 				name = "Pork (pounds)",
        	                 				AverageDailyUse = 2,
        	                 				QuantityOnHand = 6,
        	                 				ReorderThreshold = 14,
        	                 			},
        	                 		new InventoryItem
        	                 			{
        	                 				name = "Chicken (pounds)",
        	                 				AverageDailyUse = 13,
        	                 				QuantityOnHand = 23,
        	                 				ReorderThreshold = 3,
        	                 			},
        	                 		new InventoryItem
        	                 			{
        	                 				name = "Beer (can)",
        	                 				AverageDailyUse = 50,
        	                 				QuantityOnHand = 23,
        	                 				ReorderThreshold = 25,
        	                 			},
        	                 		new InventoryItem
        	                 			{
        	                 				name = "Cabbage (head)",
        	                 				AverageDailyUse = 13,
        	                 				QuantityOnHand = 23,
        	                 				ReorderThreshold = 3,
        	                 			},
        	                 		new InventoryItem
        	                 			{
        	                 				name = "Beans (#10 can)",
        	                 				AverageDailyUse = 13,
        	                 				QuantityOnHand = 23,
        	                 				ReorderThreshold = 3,
        	                 			},

        	                 	};





        }
        public ObservableCollection<InventoryItem> InventoryItems { get; set; }
    }
}
