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
		private bool _isVisible;
		private bool _isDisabled;
		private bool _isEnabled1;
		private bool _isEnabled2;
		private bool _isEnabled3;
		public string Name { get; set; }
		public NavigationService NavigationService => navigationService;
		public bool IsBusy
		{
			get => isBusy;
			set => Set(ref isBusy, value);
		}

		public bool IsVisible
		{
			get => _isVisible;
			set => Set(ref _isVisible, value);
		}

		public bool IsDisabled
		{
			get => _isDisabled;
			set => Set(ref _isEnabled1, value);
		}

		public bool IsEnabled1
		{
			get => _isEnabled1;
			set => Set(ref _isEnabled1, value);
		}
		public bool IsEnabled2
		{
			get => _isEnabled2;
			set => Set(ref _isEnabled2, value);
		}
		public bool IsEnabled3
		{
			get => _isEnabled3;
			set => Set(ref _isEnabled3, value);
		}



		public BaseViewModel()
		{
			navigationService = App.NavigationService;
		}
	}
}
