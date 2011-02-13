using BBQRMSSolution.ViewModels;

namespace BBQRMSSolution.Messages
{
	public class ShowScreen
	{
		public ShowScreen(ViewModelBase viewModel)
		{
			ViewModelToShow = viewModel;
		}

		public ViewModelBase ViewModelToShow { get; private set; }
	}
}