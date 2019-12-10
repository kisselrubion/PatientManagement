using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Plugin.BLE.Abstractions.Contracts;
using WristCare.Helpers;
using WristCare.Model;
using WristCare.Service.PatientServ;
using WristCare.Service.Scanners;
using WristCare.Service.Users;
using WristCare.ViewModel.Base;

namespace WristCare.ViewModel
{
	public class PatientsViewModel : BaseViewModel
	{
		private readonly PatientService _patientService;
		private readonly RfidScannerService _rfidScannerService;
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
		public ObservableCollection<IDevice> Devices { get; set; }
		public PatientsViewModel(PatientService patientService, UserService userService, RfidScannerService rfidScannerService)
		{
			_patientService = patientService;
			_userService = userService;
			_rfidScannerService = rfidScannerService;

			InitializeData();
			Task.Run(async () => await GetPatientsAsync());
		}

		public void InitializeData()
		{
			//Todo : remove this sample data
			PatientRfid = "prf2200022312"; //string is necessary because the rfid value contains characters
			Patients = new ObservableCollection<User>();
			Devices = new ObservableCollection<IDevice>();
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
		public ICommand ScanRfidCommand => new RelayCommand(async() => await AddRfidToPatient());

		private async Task AddRfidToPatient()
		{

		}


		private async Task RegisterPatient()
		{
			try
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

				await GetPatientsAsync();
			}
			catch 
			{
				await PopupHelper.ActionResultMessage("Unsuccessful", "Patient not added");
			}

		}

		private async Task GetPatientsAsync()
		{
			IsBusy = true;
			Patients.Clear();
			var userPatients = await _patientService.GetAllPatientsAsync();
			foreach (var userPatient in userPatients)
			{
				Patients.Add(userPatient);
			}
			IsBusy = false;
		}
	}
}
