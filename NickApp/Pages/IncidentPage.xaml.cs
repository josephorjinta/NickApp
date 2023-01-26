using System;
using NickApp.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
//using Acr.UserDialogs;
using Xamarin.Essentials;
using NickApp.ViewModels;
using NickApp.Models;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace NickApp.Pages
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IncidentPage
    {
        //private double pageWidth;

        private readonly IncidentPageViewModel  _incidentPageViewModel;

        const uint AnimationSpeed = 300;
      

        //private GridLayout gridLayout;
        public IncidentPage()
        {
            try
            {
                this.InitializeComponent();

               
              
                try
                {
                    var current = Connectivity.NetworkAccess;

                    if (current == NetworkAccess.Internet)
                    {





                    }
                    else
                    {
                        //UserDialogs.Instance.Alert("Internet Connection is not available.", "Information", "OK");

                         Application.Current.MainPage.DisplayAlert("Internet Connection is not available.", "NickApp", "Cancel");


                    }
                    _incidentPageViewModel = Startup.Resolve<IncidentPageViewModel>();
                    BindingContext = _incidentPageViewModel;



                }
                catch
                {

                }
              



            }
            catch (Exception ex)
            {
                string exs = ex.Message;
            }
        }





        private void tiSignOut_Clicked(object sender, EventArgs e)
        {


            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();

        }

        private void dataIncidentGrid_SelectionChanged(object sender, Syncfusion.SfDataGrid.XForms.GridSelectionChangedEventArgs e)
        {
            Incident selectedCIncident = (e.AddedItems[0] as Incident);

            _incidentPageViewModel.IncidentName = selectedCIncident.IncidentName;
            _incidentPageViewModel.IncidentLocation = selectedCIncident.IncidentLocation;
            _incidentPageViewModel.IncidentRecordBy = selectedCIncident.IncidentRecordBy;

            _incidentPageViewModel.SelectedIncident = selectedCIncident;

            _incidentPageViewModel.AddIncidentText = "Update";
        }

        private void dataIncidentGrid_QueryRowHeight(object sender, Syncfusion.SfDataGrid.XForms.QueryRowHeightEventArgs e)
        {
            var height = (dataIncidentGrid.View.Records.Count * dataIncidentGrid.RowHeight) + dataIncidentGrid.HeaderRowHeight;
            dataIncidentGrid.MinimumHeightRequest = (double)height;
            dataIncidentGrid.HeightRequest = (double)height;
        }
    }
}