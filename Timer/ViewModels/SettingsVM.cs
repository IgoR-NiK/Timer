using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Timer.Commands;
using Timer.ViewNavigation;

namespace Timer.ViewModels
{
    class SettingsVM : BaseVM
    {
        public string Label { get; set; }

        public DateTime EndDate { get; set; }

        public SettingsVM()
        {
            Title = "Настройки";

            Label = Properties.Settings.Default.Title;
            EndDate = Properties.Settings.Default.EndDate;
        }

        public RelayCommand OkClick { get; } = new RelayCommand(() =>
        {
            ViewNavigator.Instance.CloseLastView();
        });

        public RelayCommand GoBackClick { get; } = new RelayCommand(() =>
        {
            ViewNavigator.Instance.CloseLastView(false);
        });

        public RelayCommand CloseClick { get; } = new RelayCommand(() =>
        {
            ViewNavigator.Instance.CloseAllViews();
        });
    }
}
