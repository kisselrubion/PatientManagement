using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using WristCare.Service.Navigation;

namespace WristCare.ViewModel.Base
{
	public class BaseViewModel : ViewModelBase
	{
		bool isBusy;
		public NavigationService navigationService;
		public string Name { get; set; }
		public NavigationService NavigationService => navigationService;
		public bool IsBusy
		{
			get => isBusy;
			set => Set(ref isBusy, value);
		}
		public BaseViewModel()
		{
			navigationService = App.NavigationService;
		}
	}
}
