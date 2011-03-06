using System;

namespace BBQRMSSolution.ViewModels
{
	public interface ICashDrawer
	{
		void OpenDrawer();
		bool IsDrawerOpen { get; }
		void Claim();
		void Enable();
		void Release();
	}
}