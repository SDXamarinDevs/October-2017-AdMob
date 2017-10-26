using System;
using System.Collections.Generic;
using System.Linq;
using Prism.AppModel;
using Prism.Commands;
using Prism.Events;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using MakinMoney.Strings;
using MakinMoney.Models;

namespace MakinMoney.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, 
                                     IDeviceService deviceService) 
            : base(navigationService, pageDialogService, deviceService)
        {
            Title = "MainPage";

            NavigateCommand = new DelegateCommand<string>(OnNavigateCommandExecuted);
        }

        public DelegateCommand<string> NavigateCommand { get; }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            // TODO: Implement your initialization logic
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            // TODO: Handle any final tasks before you navigate away
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            switch(parameters.GetNavigationMode())
            {
                case NavigationMode.Back:
                    // TODO: Handle any tasks that should occur only when navigated back to
                    break;
                case NavigationMode.New:
                    // TODO: Handle any tasks that should occur only when navigated to for the first time
                    break;
            }

            // TODO: Handle any tasks that should be done every time OnNavigatedTo is triggered
        }

        private async void OnNavigateCommandExecuted(string pageName) =>
            await _navigationService.NavigateAsync($"NavigationPage/{pageName}");
    }
}