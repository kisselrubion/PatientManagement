using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Poz1.NFCForms.Abstract;
using WristCare.Helpers;
using WristCare.ViewModel.Base;
using Xamarin.Forms;

namespace WristCare.ViewModel
{
	public class CourseViewModel : BaseViewModel
	{
		private readonly INfcForms device;

		public CourseViewModel()
		{
			device = DependencyService.Get<INfcForms>();
			device.NewTag += HandleNewTag;
		}

		void HandleNewTag(object sender, NfcFormsTag e)
		{

		}

		public ICommand AddPatientsCommand => new RelayCommand(AddPatients);
		public ICommand SearchPatientCommand => new RelayCommand(SearchPatients);

		private void AddPatients()
		{
			navigationService.NavigateTo(Locator.AddPatientInformationPage);

		}
		private void SearchPatients()
		{
			navigationService.NavigateTo(Locator.PatientsPage);
		}
	}
}
