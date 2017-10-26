using Xamarin.Forms;
using MakinMoney.Models;

namespace MakinMoney.Controls
{
    public class PeopleDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate PeopleTemplate { get; set; }

        public DataTemplate AdMobTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            switch (item)
            {
                case PersonAd ad:
                    return AdMobTemplate;
                default:
                    return PeopleTemplate;
            }
        }
    }
}