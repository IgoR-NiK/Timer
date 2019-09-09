using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MVVMAqua.Commands;
using MVVMAqua.ViewModels;

namespace Timer.ViewModels
{
    class SettingsVM : BaseVM
    {
        public string Label { get; set; }
        public DateTime EndDate { get; set; }

		public RelayCommand OkClick { get; }
		public RelayCommand GoBackClick { get; }
		public RelayCommand CloseClick { get; }

		public SettingsVM()
        {
            WindowTitle = "Настройки";

            Label = Properties.Settings.Default.Title;
            EndDate = Properties.Settings.Default.EndDate;

			OkClick = new RelayCommand(() => ViewNavigator.CloseLastView());
			GoBackClick = new RelayCommand(() => ViewNavigator.CloseLastView(false));
			CloseClick = new RelayCommand(() => ViewNavigator.CloseWindow());
		}
    }
}