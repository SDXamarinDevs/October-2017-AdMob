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
    public class ViewDViewModel : ViewModelBase
    {
        public ViewDViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
                                     IDeviceService deviceService)
            : base(navigationService, pageDialogService, deviceService)
        {
            Title = "ViewD";
            Icon = "fa-trash";
            NavigateCommand = new DelegateCommand<string>(OnNavigateCommandExecuted);
        }

        public DelegateCommand<string> NavigateCommand { get; }

        private async void OnNavigateCommandExecuted(string uri) =>
            await _navigationService.NavigateAsync(uri);

    }
}