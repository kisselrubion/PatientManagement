using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using WristCare.Helpers;
using WristCare.Model;
using WristCare.Service.Patient;
using WristCare.Service.Users;
using WristCare.ViewModel.Base;

namespace WristCare.ViewModel
{
	public class PatientsViewModel : BaseViewModel
	{
		private readonly PatientService _patientService;
		private readonly UserService _userService;
		private User _patient;
		private Account _patientAccount;
		private string _patientRfid;

		public User Patient
		{
			get => _patient;
			set => Set(ref _patient, value);
		}

		public Account PatientAccount
		{
			get => _patientAccount;
			set => Set(ref _patientAccount, value);
		}

		public string PatientRfid
		{
			get => _patientRfid;
			set => Set(ref _patientRfid, value);
		}

		public ObservableCollection<User> Patients { get; set; }
		public PatientsViewModel(PatientService patientService, UserService userService)
		{
			_patientService = patientService;
			_userService = userService;

			InitializeData();
			Task.Run(async () => await GetPatientsAsync());
		}

		public void InitializeData()
		{
			PatientRfid = "prf1001";
			Patients = new ObservableCollection<User>();
			IsBusy = false;
			Patient = new User
			{
				FirstName = "Test",
				LastName = "Patient",
				Address = "Some where down the road",
				Age = "22",
				BirthDate =new DateTime(1996,12,04),
				ContactNumber = "090901010101",
				Email = "testpatient@mail.com",
				IsArchived = false,
				Sex = "male",
			};
		}

		public ICommand RegisterPatientCommand => new RelayCommand(async () => await RegisterPatient());

		private async Task RegisterPatient()
		{
			var newUserPatient = Patient;
			newUserPatient.UserAccountId = PatientRfid;

			var addedUserPatient = await _userService.RegisterUser(newUserPatient);

			var newPatientAccount = new Account
			{
				AccountNumber = newUserPatient.UserAccountId,
				UserId = addedUserPatient.UserId,
				AccountTypeId = 4,
				AccountTypeName = "patient",
			};

			var response = await _patientService.RegisterPatient(newPatientAccount);
			if (response.AccountId != 0)
			{
				await PopupHelper.ActionResultMessage("Success", "Patient added");
			}
		}

		private async Task GetPatientsAsync()
		{
			IsBusy = true;
			var userPatients = await _patientService.GetAllPatientsAsync();
			foreach (var userPatient in userPatients)
			{
				Patients.Add(userPatient);
			}
			IsBusy = false;
		}
	}
}
