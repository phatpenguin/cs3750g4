using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using BBQRMSSolution.Models;

namespace BBQRMSSolution.ViewModels
{
	public class CustomerOrderScreenViewModel : ViewModelBase
	{
		public ObservableCollection<Menu> menus { get; set; }
		public ObservableCollection<OrderItem> order { get;set;}

		private decimal st,tp,ta;
		public decimal subTotal { get { return st; } set { st = value; NotifyPropertyChanged("subTotal"); } }
		public decimal totalPrice { get { return tp; } set { tp = value; NotifyPropertyChanged("totalPrice"); } }
		public decimal taxAmount { get { return ta; } set { ta = value; NotifyPropertyChanged("taxAmount"); } }

		private const decimal TAX_PERCENTAGE = 8.25m;

		public DelegateCommand Cancel { get; private set; }

		public CustomerOrderScreenViewModel()
		{
			Cancel = new DelegateCommand(cancelOrder);

			totalPrice = 0.00m;
			subTotal = 0.00m;
			taxAmount = 0.00m;

			order = new ObservableCollection<OrderItem>();
			
			menus = new ObservableCollection<Menu> { 
				new Menu { 
					name = "Sides", 
					menuItems= {
						new MenuItem {name="Fries",id=1,price=3.50m,DoAction=new DelegateCommand(addMenuItem)},	
						new MenuItem {name="Beans",id=2,price=4.00m,DoAction=new DelegateCommand(addMenuItem)},
						new MenuItem {name="Potato Salad",id=3,price=5.00m,DoAction=new DelegateCommand(addMenuItem)},
						new MenuItem {name="Coleslaw",id=4,price=1.25m,DoAction=new DelegateCommand(addMenuItem)},
						new MenuItem {name="Corn on the Cob",id=5,price=2.25m,DoAction=new DelegateCommand(addMenuItem)}
					}

				},
				new Menu {
								name="Drinks",
								menuItems={
												new MenuItem {name="Soda",id=6,price=1.25m,DoAction=new DelegateCommand(addMenuItem)},
												new MenuItem {name="Beer",id=7,price=2.25m,DoAction=new DelegateCommand(addMenuItem)},
												new MenuItem {name="Bottled Water",id=8,price=1.00m,DoAction=new DelegateCommand(addMenuItem)},
															new MenuItem {name="Sobe",id=9,price=1.99m,DoAction=new DelegateCommand(addMenuItem)}
								}
				}
			};
		}
		
		public void addMenuItem(Object mi)
		{
			MenuItem menuItem = (MenuItem)mi;

			bool isFound = true;
			foreach (OrderItem oi in order)
			{
				isFound = true;
				if (oi.menuItem.id == menuItem.id)
				{
					oi.quantity++;
					NotifyPropertyChanged("oi");
					break;
				}
				else isFound = false;
			}
			if (!isFound || order.Count == 0)
			{
				OrderItem orderItem = new OrderItem { menuItem = menuItem, quantity = 1, DoAction=new DelegateCommand(removeMenuItem) };
				order.Add(orderItem);
			}

			subTotal += menuItem.price;
			taxAmount = subTotal * (TAX_PERCENTAGE / (decimal)100);
			totalPrice = subTotal + taxAmount;
		}

		public void removeMenuItem(Object oi)
		{
			OrderItem orderItem = (OrderItem)oi;

			if (orderItem.quantity > 1) orderItem.quantity--;
			else order.Remove(orderItem);

			subTotal -= orderItem.menuItem.price;
			taxAmount = subTotal * (TAX_PERCENTAGE / (decimal)100);
			totalPrice = subTotal + taxAmount;
		}


		public void cancelOrder()
		{
			totalPrice = 0.00m;
			subTotal = 0.00m;
			taxAmount = 0.00m;

			order.Clear();
		}
	}
}
