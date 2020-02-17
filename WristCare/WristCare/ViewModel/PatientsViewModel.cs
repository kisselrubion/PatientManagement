using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Plugin.BLE.Abstractions.Contracts;
using Rg.Plugins.Popup.Services;
using WristCare.Helpers;
using WristCare.Model;
using WristCare.Service.PatientServ;
using WristCare.Service.Scanners;
using WristCare.Service.Users;
using WristCare.ViewModel.Base;
using WristCare.Views;
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
		private string _statusText;

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
		public string StatusText
		{
			get => _statusText;
			set => Set(ref _statusText, value);
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
			IsEnabled1 = false;
			
		}

		public ICommand RegisterPatientCommand => new RelayCommand(async () => await RegisterPatient());
		public ICommand ScanRfidCommand => new RelayCommand(async () => await AddRfidToPatient());
		public ICommand AddPatientsCommand => new RelayCommand(async () => await AddPatients());
		public ICommand SavePatientRfidCommand => new RelayCommand(async () => await SaveRfidToPatient());
		public ICommand RefreshCommand
		{
			get
			{
				return new Command(async () => await RefreshProc());
			}
		}

		public ICommand ClosePopupCommand => new RelayCommand(async () => await ClosePopUpPage());
		private async Task ClosePopUpPage()
		{
			await PopupNavigation.Instance.PopAsync();
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

		private async Task AddPatients()
		{
			try
			{

				Patient = new User
				{
					UserAccountId = PatientRfid
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

		private async Task AddRfidToPatient()
		{
			await PopupNavigation.Instance.PushAsync(new AddRfidToPatientPage());
			SavePatientRfidCommand.Execute(0);
		}

		private async Task SaveRfidToPatient()
		{
			StatusText = "Scanning...";
			IsBusy = true;
			await Task.Delay(3000);
			if (!string.IsNullOrEmpty(App.Locator.ScanViewModel.RfidData))
			{
				PatientRfid = App.Locator.ScanViewModel.RfidData;
				await PopupHelper.ActionResultMessage("Success", "Rfid registered!");
			}
			StatusText = "Scan Complete";
			IsBusy = false;
		}


		private async Task RegisterPatient()
		{
			try
			{
				var confirm = await PopupHelper.ProceedMessage("Confirm", "Proceed to admitting patient");
				if (confirm)
				{
					if (string.IsNullOrEmpty(PatientRfid))
					{
						await PopupHelper.ActionResultMessage("Try again", "Scan Wristband!");
					}
					else
					{
						var newUserPatient = Patient;
						newUserPatient.UserAccountId = PatientRfid;

						var usedRfidWristband = Patients.FirstOrDefault(c => c.UserAccountId == PatientRfid);
						if (usedRfidWristband == null)
						{
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
							await PopupHelper.ActionResultMessage("Warning", "Try different wristband");
						}
					}
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
