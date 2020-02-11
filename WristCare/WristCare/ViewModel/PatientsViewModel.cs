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
using Xamarin.Forms;

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
		private ObservableCollection<User> _patients;
		private List<User> _patientsList;
		private string _searchText;

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

		public string SearchText
		{
			get => _searchText;
			set
			{
				if (Set(ref _searchText, value))
				{
					SearchPatients(_searchText);
				}

			}
		}

		public ObservableCollection<User> Patients
		{
			get => _patients;
			set => Set(ref _patients, value);
		}

		public ObservableCollection<IDevice> Devices { get; set; }

		public List<User> PatientsList
		{
			get => _patientsList;
			set => Set(ref _patientsList, value);
		}

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
			//PatientRfid = "prf2200022312"; //string is necessary because the rfid value contains characters
			Patients = new ObservableCollection<User>();
			PatientsList = new List<User>();
			Devices = new ObservableCollection<IDevice>();
			IsBusy = false;
			IsEnabled1 = false;
			
		}

		public ICommand RegisterPatientCommand => new RelayCommand(async () => await RegisterPatient());
		public ICommand ScanRfidCommand => new RelayCommand(AddRfidToPatient);
		public ICommand AddPatientsCommand => new RelayCommand(AddPatients);
		public ICommand RefreshCommand
		{
			get
			{
				return new Command(async () => await RefreshProc());
			}
		}
		private async Task RefreshProc()
		{
			IsBusy = true;

			try
			{
				await GetPatientsAsync();
			}
			catch
			{
				await PopupHelper.ActionResultMessage("Error", "Something went wrong!");
			}

			IsBusy = false;
		}

		//public ICommand SearchPatientCommand => new RelayCommand();

		private void AddPatients()
		{
			try
			{
				Patient = new User
				{
					UserAccountId = Guid.NewGuid().ToString()
				};
				navigationService.NavigateTo(Locator.AddPatientInformationPage);
				IsEnabled1 = true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		private void AddRfidToPatient()
		{
			if (!string.IsNullOrEmpty(App.Locator.ScanViewModel.RfidData))
			{
				PatientRfid = App.Locator.ScanViewModel.RfidData;
			}
			
		}


		private async Task RegisterPatient()
		{
			try
			{
				var confirm = await PopupHelper.ProceedMessage("Confirm", "Proceed to admitting patient");
				if (confirm)
				{
					var newUserPatient = Patient;
					newUserPatient.UserAccountId = Patient.UserAccountId;

					var addedUserPatient = await _userService.RegisterUser(newUserPatient);

					var newPatientAccount = new Account
					{
						AccountNumber = newUserPatient.UserAccountId,
						UserId = addedUserPatient.UserId,
						AccountTypeId = 4,
						AccountTypeName = "patient",
						AdmissionDateTime = DateTime.Now,
					};

					var response = await _patientService.RegisterPatient(newPatientAccount);

					if (response.AccountId != 0)
					{
						await PopupHelper.ActionResultMessage("Success", "Patient added");
					}
					await GetPatientsAsync();
				}
				else
				{
					return;
				}

			}
			catch 
			{
				await PopupHelper.ActionResultMessage("Unsuccessful", "Patient not added");
			}

			navigationService.GoBack();
		}

		private async Task GetPatientsAsync()
		{
			//IsBusy = true;
			Patients.Clear();
			var userPatients = await _patientService.GetAllPatientsAsync();
			PatientsList = userPatients;

			Patients = new ObservableCollection<User>(PatientsList);
			//IsBusy = false;
		}

		private void SearchPatients(string query)
		{
			IsBusy = true;
			if (string.IsNullOrEmpty(query)) Patients = new ObservableCollection<User>(PatientsList);
			Patients = new ObservableCollection<User>(PatientsList.FindAll(c=>
				c.UserAccountId.ToLowerInvariant().Contains(query.ToLowerInvariant())
				|| c.FirstName.ToLowerInvariant().Contains(query.ToLowerInvariant())
				|| c.LastName.ToLowerInvariant().Contains(query.ToLowerInvariant())));
			IsBusy = false;
		}
	}
}
