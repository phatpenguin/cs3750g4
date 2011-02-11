using BBQRMSSolution.ViewModels;
using Controls;

namespace BBQRMSSolution.Models
{
	public class OrderItem : BaseModel
	{
	    private BBQRMSSolution.ServerProxy.MenuItem _mi;
		public BBQRMSSolution.ServerProxy.MenuItem MenuItem { get { return _mi; } set { _mi = value; Price = (decimal)_mi.Price; Name = _mi.Name; } }

		private int _q;
        public int Quantity { get { return _q; } set { _q = value; Price = (decimal)MenuItem.Price*value; NotifyPropertyChanged("quantity"); NotifyPropertyChanged("price");} }
		public DelegateCommand DoAction { get; set; }

        public string Name { get; private set; }
	    public decimal Price { get; private set; }

	    public OrderItem()
		{

		}
	}
}
