using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using WristCare.Helpers;
using WristCare.Model;
using WristCare.Service.Doctors;
using WristCare.Service.Users;
using WristCare.ViewModel.Base;

namespace WristCare.ViewModel
{
	public class DoctorsViewModel : BaseViewModel
	{
		private readonly DoctorService _doctorService;
		private readonly UserService _userService;
		private User _doctor;

		public User Doctor
		{
			get => _doctor;
			set => Set(ref _doctor, value);
		}

		public ObservableCollection<User> Doctors;
		public DoctorsViewModel(DoctorService doctorService, UserService userService)
		{
			_doctorService = doctorService;
			_userService = userService;
			Doctors = new ObservableCollection<User>();
		}

		public ICommand RegisterDoctorCommand => new RelayCommand(async () => await RegisterDoctor());

		private async Task RegisterDoctor()
		{
			try
			{
				var newDoctor = Doctor;
				newDoctor.UserAccountId = Guid.NewGuid().ToString();

				var addedUserPatient = await _userService.RegisterUser(newDoctor);

				var newDoctorAccount = new Account
				{
					AccountNumber = newDoctor.UserAccountId,
					UserId = addedUserPatient.UserId,
					AccountTypeId = 5,
					AccountTypeName = "doctor",
				};

				var response = await _doctorService.RegisterDoctor(newDoctorAccount);

				if (response.AccountId != 0)
				{
					await PopupHelper.ActionResultMessage("Success", "Patient added");
				}

				//await GetDoctors();
			}
			catch
			{
				await PopupHelper.ActionResultMessage("Unsuccessful", "Doctor not added");

			}

			navigationService.GoBack();
		}

		private async Task GetDoctors()
		{
			IsBusy = true;
			Doctors.Clear();
			var doctors = await _doctorService.GetDoctors();
			if (doctors.Count != 0)
			{
				foreach (var doctor in doctors)
				{
					Doctors.Add(doctor);
				}
			}
			IsBusy = false;
		}
	}
}
