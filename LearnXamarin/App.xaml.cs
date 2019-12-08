using LearnXamarin.IOC;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace LearnXamarin
{
    public partial class App : Application
    {
        public App()
        {
            DIRegistrar.RegisterTypes();
            InitializeComponent();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
         
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
