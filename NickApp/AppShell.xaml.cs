using System;
using System.Collections.Generic;
using NickApp.ViewModels;
using Xamarin.Forms;
using NickApp.Pages;

namespace NickApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {

      
        public AppShell()
        {
            InitializeComponent();
            BindingContext = this;

           

            Routing.RegisterRoute("SignInPage", typeof(SignInPage));
            Routing.RegisterRoute("SignUpPage", typeof(SignUpPage));
            Routing.RegisterRoute("IncidentPage", typeof(IncidentPage));



        }

      
    }
}
