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
    public class ViewAViewModel : ViewModelBase
    {
        public ViewAViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
                                     IDeviceService deviceService)
            : base(navigationService, pageDialogService, deviceService)
        {
            Title = "ViewA";
            Icon = "fa-dashboard";
            NavigateCommand = new DelegateCommand<string>(OnNavigateCommandExecuted);
        }

        public DelegateCommand<string> NavigateCommand { get; }

        private async void OnNavigateCommandExecuted(string uri) =>
            await _navigationService.NavigateAsync(uri);

    }
}