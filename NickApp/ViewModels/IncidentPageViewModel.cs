using Xamarin.Forms;
using Xamarin.Forms.Internals;
using NickApp.Services;
using NickApp.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
//using Acr.UserDialogs;
using System.Threading;
using System.IO;
using Xamarin.Essentials;
using NickApp.Pages;

namespace NickApp.ViewModels
{
    /// <summary>
    /// ViewModel for WorkerPage.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class IncidentPageViewModel : BaseViewModel, INotifyPropertyChanged
    {
        #region Fields

        private readonly IIncidentService _incidentService;
        private readonly IUserAccountService _userAccountService;




        #endregion



        public IncidentPageViewModel(IIncidentService incidentService, IUserAccountService userAccountService )
        {

            _incidentService = incidentService;
            _userAccountService = userAccountService;
           
          

            this.AddIncidentCommand = new Command(async () => await CreateUpdateIncident());

            this.CancelCommand = new Command(async () => await CancelIncident());

            this.SyncCommand = new Command(async () => await SyncData());

           
            LoadIncidents();

            AddIncidentText = "Add";
           
        }




        #region Commands

        public Command SyncCommand { get; set; }
     
        public Command AddIncidentCommand { get; set; }

        public Command CancelCommand { get; set; }



        public async Task CancelIncident()
        {
            IncidentName = "";
            IncidentLocation = "";
            IncidentRecordBy = "";
            await Task.Delay(0);
        }
        private async Task CreateUpdateIncident()
        {
            try
            {
                if (IncidentName == null)
                {
                    //await UserDialogs.Instance.AlertAsync("Please Provide Incident Name", "Nick Incidents");

                    await Application.Current.MainPage.DisplayAlert("Please Provide Incident Name.", "NickApp", "Cancel");

                    return;
                }
                if (IncidentLocation == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Please Provide Incident Location.", "NickApp", "Cancel");

                    //await UserDialogs.Instance.AlertAsync("Please Provide Incident Location", "Nick Incidents");
                    return;
                }
                if (IncidentRecordBy == null)
                {
                   // await UserDialogs.Instance.AlertAsync("Please Provide who Reported the Incident", "Nick Incidents");

                    await Application.Current.MainPage.DisplayAlert("Please Provide who Reported the Incident", "NickApp", "Cancel");
                    
                    return;
                }
              
                if (AddIncidentText == "Add")
                {
                    //using (UserDialogs.Instance.Loading("Adding ..."))
                    //{
                        var key = Guid.NewGuid().ToString("N");

                        Incident incident = new Incident();

                        incident.IncidentCode = key;
                        incident.IncidentName = IncidentName;
                        incident.IncidentDate = DateTime.Now;
                        incident.IncidentLocation = IncidentLocation;
                        incident.IncidentRecordBy = IncidentRecordBy;
                        incident.IncidentAddressed = false;
                     
                        await App.SQLiteDb.AddIncidentAsync(incident);

                        LoadIncidents();

                   


                    //}
                    IncidentName = "";
                    IncidentLocation = "";
                    IncidentRecordBy = "";

                   



                }
                else if (AddIncidentText == "Update")
                {
                    //using (UserDialogs.Instance.Loading("Updating ..."))
                    //{


                        Incident incident = new Incident();

                        incident = SelectedIncident;

                        incident.IncidentName = IncidentName;
                        incident.IncidentLocation = IncidentLocation;
                        incident.IncidentRecordBy = IncidentRecordBy;
                        incident.IncidentDate =DateTime.Now;
                       

                        await App.SQLiteDb.SaveIncidentAsync(incident);

                    LoadIncidents();


                    // }

                    AddIncidentText = "Add";

                    IncidentName = "";
                    IncidentLocation = "";
                    IncidentRecordBy = "";


                }

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

        }


        private async Task SyncData()
        {
            SyncDatabase syncdb = new Services.SyncDatabase();
            await syncdb.SynchronizeDatabase();
        }
      



        #endregion

        #region form control 


        private string incidentName;
        private string incidentLocation;
        private string incidentRecordBy;

        private string addIncidentText;

        




        public string IncidentName
        {
            get
            {
                return incidentName;
            }
            set

            {
                incidentName = value;
                OnPropertyChanged("IncidentName");
            }
        }


        public string IncidentLocation
        {
            get
            {
                return incidentLocation;
            }
            set

            {
                incidentLocation = value;
                OnPropertyChanged("IncidentLocation");
            }
        }


        public string IncidentRecordBy
        {
            get
            {
                return incidentRecordBy;
            }
            set

            {
                incidentRecordBy = value;
                OnPropertyChanged("IncidentRecordBy");
            }
        }

        public string AddIncidentText
        {
            get
            {
                return addIncidentText;
            }
            set

            {
                addIncidentText = value;
                OnPropertyChanged("AddIncidentText");
            }
        }



        private ObservableCollection<Incident> _incidents;
        public ObservableCollection<Incident> Incidents
        {
            get
            {
                return _incidents;
            }
            set

            {
                _incidents = value;
                OnPropertyChanged("Incidents");
            }

        }

        private Incident selectedIncident;
        public Incident SelectedIncident
        {
            get
            {
                return selectedIncident;
            }
            set

            {
                selectedIncident = value;
                OnPropertyChanged("SelectedIncident");
            }
        }



        #endregion


        #region LoadData



        public async void LoadIncidents()
        {

            ObservableCollection<Incident> objIncident = new ObservableCollection<Incident>();

            IEnumerable<Incident> list = await App.SQLiteDb.GetAllIncidentAsync();

            var objList = list.ToList();

            if (objList.Count() > 0)
            {
                foreach (Incident i in objList)
                {
                    objIncident.Add(i);
                    
                }
            }

            Incidents = objIncident;
        }


        #endregion



    }


}