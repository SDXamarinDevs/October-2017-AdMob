using System;
using System.Threading.Tasks;
using MakinMoney.Services;
using MakinMoney.Views;
using DryIoc;
using Prism.DryIoc;
using Prism.Logging;
using Xamarin.Forms;
using DebugLogger = MakinMoney.Services.DebugLogger;
using FormsPlugin.Iconize;

namespace MakinMoney
{
    public partial class App : PrismApplication
    {
        /* 
         * NOTE: 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App()
            : this(null)
        {
        }

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            LogUnobservedTaskExceptions();

            await NavigationService.NavigateAsync("SplashScreenPage");
        }

        protected override void RegisterTypes()
        {
            // Register the Popup Plugin Navigation Service
            Container.RegisterPopupNavigationService();


            // Navigating to "TabbedPage?createTab=ViewA&createTab=ViewB&createTab=ViewC will generate a TabbedPage
            // with three tabs for ViewA, ViewB, & ViewC
            // Adding `selectedTab=ViewB` will set the current tab to ViewB
            Container.RegisterTypeForNavigation<IconTabbedPage>(nameof(TabbedPage));
            Container.RegisterTypeForNavigation<IconNavigationPage>(nameof(NavigationPage));
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<SplashScreenPage>();
            Container.RegisterTypeForNavigation<ViewA>();
            Container.RegisterTypeForNavigation<ViewB>();
            Container.RegisterTypeForNavigation<ViewC>();
            Container.RegisterTypeForNavigation<ViewD>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle IApplicationLifecycle
            base.OnSleep();

            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle IApplicationLifecycle
            base.OnResume();

            // Handle when your app resumes
        }

        protected override ILoggerFacade CreateLogger() =>
            new DebugLogger();

        private void LogUnobservedTaskExceptions()
        {
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                Logger.Log(e.Exception);
            };
        }
    }
}
