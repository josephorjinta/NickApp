using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using NickApp.ViewModels;
using Xamarin.Essentials;
using System;
using System.Linq;
using Xamarin.Forms;
//using Acr.UserDialogs;

namespace NickApp.Pages
{
    /// <summary>
    /// Page to sign in with user details.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignUpPage" /> class.
        /// </summary>
         private readonly SignUpPageViewModel _signUpPageViewModel;

        public SignUpPage()
        {
            this.InitializeComponent();
           

            //if (Settings.LoggedIn == true)
            //{
            //    AsyncHelpers.RunSync(()=> Shell.Current.GoToAsync("//main"));
            //}

            _signUpPageViewModel = Startup.Resolve<SignUpPageViewModel>();
            BindingContext = _signUpPageViewModel;


            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            { 
               
                    //AsyncHelpers.RunSync(() => _signUpPageViewModel.LoadGenders());

                
            }
            else
            {
                //UserDialogs.Instance.Alert("Internet Connection is not available.", "Information", "OK");
                Application.Current.MainPage.DisplayAlert("Internet Connection is not available.", "NickApp", "Cancel");

            }


        }
    }
}