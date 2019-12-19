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
		public ObservableCollection<User> Doctors { get; set; }

		public DoctorsViewModel(DoctorService doctorService, UserService userService)
		{
			_doctorService = doctorService;
			_userService = userService;
			
			InitializeData();
		}

		private void InitializeData()
		{
			Doctor = new User
			{
				FirstName = "Test",
				LastName = "Doctor",
				Address = "Somewhere down the road",
				Age = "22",
				BirthDate = new DateTime(1996, 12, 04),
				ContactNumber = "090901010101",
				Email = "testdoctor@mail.com",
				IsArchived = false,
				Sex = "male",
			};

			Doctors = new ObservableCollection<User>();
			Task.Run(async () => await GetDoctors());
		}

		public ICommand RegisterDoctorCommand => new RelayCommand(async () => await RegisterDoctor());
		public ICommand AddDoctorsCommand => new RelayCommand(AddDoctors);

		private void AddDoctors()
		{
			try
			{
				navigationService.NavigateTo(Locator.AddDoctorInformationPage);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

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
					AdmissionDateTime = DateTime.Now,
				};

				var response = await _doctorService.RegisterDoctor(newDoctorAccount);

				if (response.AccountId != 0)
				{
					await PopupHelper.ActionResultMessage("Success", "Doctor added");
				}

				await GetDoctors();
			}
			catch
			{
				await PopupHelper.ActionResultMessage("Unsuccessful", "Doctor not added");

			}

			navigationService.GoBack();
		}

		private async Task GetDoctors()
		{
			//IsBusy = true;
			Doctors.Clear();
			var doctors = await _doctorService.GetUserDoctors();
			if (doctors.Count != 0)
			{
				foreach (var doctor in doctors)
				{
					Doctors.Add(doctor);
				}
			}
			//IsBusy = false;
		}
	}
}
