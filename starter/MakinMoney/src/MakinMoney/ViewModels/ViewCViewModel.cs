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
using MvvmHelpers;

namespace MakinMoney.ViewModels
{
    public class ViewCViewModel : ViewModelBase
    {
        public ViewCViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
                                     IDeviceService deviceService)
            : base(navigationService, pageDialogService, deviceService)
        {
            Title = "ViewC";
            Icon = "fa-users";
            NavigateCommand = new DelegateCommand<string>(OnNavigateCommandExecuted);
            People = new ObservableRangeCollection<Person>();
        }

        public ObservableRangeCollection<Person> People { get; set; }

        public DelegateCommand<string> NavigateCommand { get; }

        private async void OnNavigateCommandExecuted(string uri) =>
            await _navigationService.NavigateAsync(uri);

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            for (int i = 0; i < names.Length; i++)
            {
                People.Add(new Person()
                {
                    Name = names[i]
                });

                if (i % 4 == 0)
                    People.Add(new PersonAd());
            }
        }

        string[] names = new string[]
        {
            "Meredith Schneider",
            "Patti Page",
            "Ricky Garrett",
            "Latoya Patrick",
            "Lynne Mendoza",
            "Elias Greene",
            "Norman Nichols",
            "Gina Davidson",
            "Loretta Mccarthy",
            "Cheryl Gonzales",
            "Frankie Mathis",
            "Chelsea Todd",
            "Elijah Cross",
            "Emanuel Stanley",
            "Randolph Pena",
            "Helen Goodwin",
            "Teresa Sanchez",
            "Jerome Bowman",
            "Floyd Ortiz",
            "Leslie Santiago",
            "Stephen Harris",
            "Franklin Patterson",
            "Keith Lindsey",
            "Bradley Kennedy",
            "Roy Dean"
            };
    }
}