using Xamarin.Forms;
using Xamarin.Forms.Internals;
using NickApp.Services;
using NickApp.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using NickApp.Pages;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.IO;
//using Acr.UserDialogs;
using System.Windows.Input;
using Util;

namespace NickApp.ViewModels
{
    /// <summary>
    /// ViewModel for sign-up page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SignUpPageViewModel : BaseViewModel, INotifyPropertyChanged
    {
        #region Fields

        private readonly IIncidentService _incidentService;
        private readonly IUserAccountService _userAccountService;


        

        

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="SignUpPageViewModel" /> class.
        /// </summary>
        public SignUpPageViewModel(IIncidentService incidentService, IUserAccountService userAccountService)
        {
            _incidentService = incidentService;
            _userAccountService = userAccountService;


            this.LoginCommand = new Command(this.LoginClicked);
            this.SignUpCommand = new Command(async () => await CreateAccount());


            //SelectedCountry = CountryUtils.GetCountryModelByName(_country);
            //ShowPopupCommand = new Command(async _ => await ExecuteShowPopupCommand());
            //CountrySelectedCommand = new Command(country => ExecuteCountrySelectedCommand(country as CountryModel));



        }





        //public ICommand ShowPopupCommand { get; }
        //public ICommand CountrySelectedCommand { get; }


        //#endregion

        //#region methods


        //private Task ExecuteShowPopupCommand()
        //{
        //    var popup = new ChooseCountryPopup(SelectedCountry)
        //    {
        //        CountrySelectedCommand = CountrySelectedCommand
        //    };
        //    return Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(popup);
        //}

        //private void ExecuteCountrySelectedCommand(CountryModel country)
        //{
        //    SelectedCountry = country;
        //}


        private async Task CreateAccount()
        {
            try
            {
                if (this.UserName == "")
                {
                    await Application.Current.MainPage.DisplayAlert("Please provide User Name", "NickApp", "Cancel");

                //    await UserDialogs.Instance.AlertAsync("Please provide User Name", "Sign Up");
                    return;
                }
                if (this.FirstName == "")
                {
                    await Application.Current.MainPage.DisplayAlert("Please provide First Name", "NickApp", "Cancel");

                  //  await UserDialogs.Instance.AlertAsync("Please provide First Name", "Sign Up");
                    return;
                }
                if (this.LastName == "")
                {
                    await Application.Current.MainPage.DisplayAlert("Please provide Last Name", "NickApp", "Cancel");

                   // await UserDialogs.Instance.AlertAsync("Please provide Last Name", "Sign Up");
                    return;
                }

                    if (this.PhoneNumber == "")
                    {
                    await Application.Current.MainPage.DisplayAlert("Please provide Phone No", "NickApp", "Cancel");

                  ///  await UserDialogs.Instance.AlertAsync("Please provide Phone No ", "Sign Up");
                        return;
                    }

                    var li = await _userAccountService.GetUserAccountByUserName(UserName);
                    if (li.Count() > 0)
                    {
                       // await UserDialogs.Instance.AlertAsync("Sign Up Failed, User Name is already used to sign up. Use Another ", "Sign Up");
                         await Application.Current.MainPage.DisplayAlert("Sign Up Failed, User Name is already used to sign up. Use Another", "NickApp", "Cancel");

                      return;

                    }

                    if (Password1 == "")
                    {
                       // UserDialogs.Instance.Alert("Please provide New Password", "Sign Up");

                    await Application.Current.MainPage.DisplayAlert("Please provide New Password", "NickApp", "Cancel");

                    return;
                    }
                    if (Password2 == "")
                    {
                    await Application.Current.MainPage.DisplayAlert("Please Confirm  Password", "NickApp", "Cancel");

                   // UserDialogs.Instance.Alert("Please Confirm Password", "Sign Up");
                        return;
                    }
                    if (this.Password1 != Password2)
                    {
                      //  await UserDialogs.Instance.AlertAsync("There is Password Mismatch, Please provide matching Password and Confirm Password values ", "Sign Up");

                    await Application.Current.MainPage.DisplayAlert("There is Password Mismatch, Please provide matching Password and Confirm Password values ", "NickApp", "Cancel");
                    return;
                    }




                    //using (UserDialogs.Instance.Loading("Creating Account ..."))
                    //{
                        var key = Guid.NewGuid().ToString("N");

                        UserAccount userAccount = new UserAccount();

                        string _userAccountCode = Guid.NewGuid().ToString("N");

                        userAccount.UserAccountCode = _userAccountCode;
                        userAccount.UserName = UserName;

                        string passwordHash = Encryption.Encrypt(Password1.ToString(), key);


                        userAccount.PasswordHash = passwordHash;
                        userAccount.PasswordSalt = key;
                        userAccount.FirstName = FirstName;
                        userAccount.LastName = LastName;
                        userAccount.PhoneNumber = PhoneNumber;
                        userAccount.Name = FirstName + " " + LastName;




                        await _userAccountService.AddUserAccount(userAccount);





                        SyncDatabase sync = new SyncDatabase();
                         await sync.SynchronizeDatabase();

                        await Task.Delay(20);


                      ///  await UserDialogs.Instance.AlertAsync(FirstName + ", You have successfully Signed Up. Please Sign In.");

                await Application.Current.MainPage.DisplayAlert(FirstName + ", You have successfully Signed Up. Please Sign In.", "NickApp", "Cancel");


                await Shell.Current.GoToAsync("..");

                   // }
                
            }
            catch (Exception ex)
            {
              //  await UserDialogs.Instance.AlertAsync(FirstName + ", No Internet Connection for Sign Up. Try Again.");
                await Application.Current.MainPage.DisplayAlert("No Internet Connection for Sign Up. Try Again.", "NickApp", "Cancel");

                string exs = ex.Message;
            }
        }
        #endregion

        #region Property

        private string userName;
        private string password1;
        private string password2;

        private string firstName;
        private string lastName;
        private string phoneNumber;

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the name from user in the Sign Up page.
        /// </summary>
        public string UserName
        {
            get
            {
                return this.userName;
            }

            set
            {
                if (this.userName == value)
                {
                    return;
                }

                this.SetProperty(ref this.userName, value);
            }
        }
        public string Password1
        {
            get
            {
                return this.password1;
            }

            set
            {
                if (this.password1 == value)
                {
                    return;
                }

                this.SetProperty(ref this.password1, value);
            }
        }

        public string Password2
        {
            get
            {
                return this.password2;
            }

            set
            {
                if (this.password2 == value)
                {
                    return;
                }

                this.SetProperty(ref this.password2, value);
            }
        }

        public string FirstName
        {
            get
            {
                return this.firstName;
            }

            set
            {
                if (this.firstName == value)
                {
                    return;
                }

                this.SetProperty(ref this.firstName, value);
            }
        }

        public string LastName
        {
            get
            {
                return this.lastName;
            }

            set
            {
                if (this.lastName == value)
                {
                    return;
                }

                this.SetProperty(ref this.lastName, value);
            }
        }


        public string PhoneNumber
        {
            get
            {
                return this.phoneNumber;
            }

            set
            {
                if (this.phoneNumber == value)
                {
                    return;
                }

                this.SetProperty(ref this.phoneNumber, value);
            }
        }

       
        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public Command LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }
        #endregion

        #region Methods

     
  

        /// <summary>
        /// Invoked when the Log in button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void LoginClicked(object obj)
        {

            await Shell.Current.GoToAsync("///SignInPage");

           

        }



        #endregion



        ///// <summary>
        ///// Gets the collection property, which contains the countries data. 
        ///// </summary>


    }
}