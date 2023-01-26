using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using NickApp.ViewModels;
//using Acr.UserDialogs;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NickApp.Pages
{
    /// <summary>
    /// Page to login with user name and password
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignInPage" /> class.
        /// </summary>
        /// 
        private readonly SignInPageViewModel _signInPageViewModel;

        public SignInPage()
        {
            this.InitializeComponent();

            _signInPageViewModel = Startup.Resolve<SignInPageViewModel>();
            BindingContext = _signInPageViewModel;

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



            }
            catch
            {

            }
          
           
        }
    }
}