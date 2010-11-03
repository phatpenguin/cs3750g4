using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace BBQRMSSolution.ViewModels
{
	public class CustomerOrderScreenViewModel
	{
		public ObservableCollection<Menu> menus { get; set; }

		public CustomerOrderScreenViewModel()
		{
			menus = new ObservableCollection<Menu> { 
				new Menu { 
					name = "Sides", 
					menuItems= {
						new MenuItem {name="Fries",id=1,price=3.5m},	
						new MenuItem {name="Beans",id=2,price=4.00m},
						new MenuItem {name="Potato Salad",id=3,price=5.00m},
						new MenuItem {name="Coleslaw",id=4,price=1.25m},
						new MenuItem {name="Corn on the Cob",id=5,price=2.25m}
					}

				},
				new Menu {
								name="Drinks",
								menuItems={
															new MenuItem {name="Soda",id=1,price=1.25m},
															new MenuItem {name="Beer",id=2,price=2.25m},
															new MenuItem {name="Bottled Water",id=3,price=1.00m},
															new MenuItem {name="Sobe",id=4,price=1.99m}
								}
				}

			};


		}

		public void addItemToOrder(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}
