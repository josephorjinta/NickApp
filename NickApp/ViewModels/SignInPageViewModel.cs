using Xamarin.Forms;
using Xamarin.Forms.Internals;
using NickApp.Pages;
using Util;
//using Acr.UserDialogs;
using NickApp.Services;
using NickApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace NickApp.ViewModels
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SignInPageViewModel : BaseViewModel
    {
        #region Fields

        private readonly IUserAccountService _userAccountService;



        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="SignInPageViewModel" /> class.
        /// </summary>
        public SignInPageViewModel(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;

          

             this.LoginCommand = new Command(this.LoginClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);


        }

        #endregion

        #region property

        /// <summary>
        /// Gets or sets the property that is bound with an entry that gets the password from user in the login page.
        /// </summary>
        private string password;

        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                password = value;

                OnPropertyChanged("Password");
            }
        }

        private string userName;

        public string UserName
        {
            get
            {
                return this.userName;
            }

            set
            {
                userName = value;

                OnPropertyChanged("UserName");
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

        #region methods


     

        private async Task<bool> DoSignIn()
        {
            bool success = false;

            try
            {
                 success = false;




                string epassword = Password;

                string password = "";

                IEnumerable<UserAccount> _list;
                _list = await App.SQLiteDb.GetUserAccountByUserName(UserName);

                if (_list.Count() > 0)
                {

                }
                else
                {
                    _list = await _userAccountService.GetUserAccountByUserName(UserName);



                }
                // var _list = await _userAccountService.GetUserAccountByUserName(PhoneNumber);
                foreach (UserAccount j in _list)
                {
                    password = Encryption.Decrypt(j.PasswordHash, j.PasswordSalt);
                    if (password == epassword)
                    {
                       

                       SyncDatabase syncdb = new Services.SyncDatabase();
                       await syncdb.SynchronizeDatabase();


                       


                       


                        success = true;

                    }

                }

                return success;
            }
            catch(Exception ex)
            {
                string nes = ex.Message;
            }

            return success;
        }

        /// <summary>
        /// Invoked when the Log In button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private  async void LoginClicked(object obj)
        {

          
            bool success = true;
            if(UserName == null)
            {
               // UserDialogs.Instance.Alert("Please provide User Name");

                await Application.Current.MainPage.DisplayAlert("Please provide User Name", "NickApp", "Cancel");

                return;
            }
            if (Password == null)
            {
              //  UserDialogs.Instance.Alert("Please provide Password");
                await Application.Current.MainPage.DisplayAlert("Please provide Password", "NickApp", "Cancel");

                return;
            }
         
            //using (UserDialogs.Instance.Loading("Signing you in ..."))
            //{
                
               success =  await Task.Run(DoSignIn);

                if (success == true)
                {

                    
                        await Shell.Current.GoToAsync("/IncidentPage", true);
                  
                }
                else
                {
                   // UserDialogs.Instance.Alert("Sign in failed. Check your Sign in Credentials.");

                   await  Application.Current.MainPage.DisplayAlert("Sign in failed. Check your Sign in Credentials.","NickApp","Cancel");

                  
                }

          //  }

            
           

        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private  async void SignUpClicked(object obj)
        {
           
                await Shell.Current.GoToAsync("/SignUpPage");

           
        
        }

      

     

        #endregion
    }
}