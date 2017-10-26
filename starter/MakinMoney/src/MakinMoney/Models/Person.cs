using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using MvvmHelpers;

namespace MakinMoney.Models
{
    public class Person : ObservableObject
    {
        public string Name { get; set; }
    }
}