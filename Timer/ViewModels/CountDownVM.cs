using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

using Timer.Helpers;
using MVVMAqua.Commands;
using MVVMAqua.ViewModels;

namespace Timer.ViewModels
{
    class CountDownVM : BaseVM
    {
        public DateTime EndDate { get; set; }

		public RelayCommand CloseClick { get; }
		public RelayCommand SettingsClick { get; }

		public CountDownVM()
        {
            WindowTitle = Properties.Settings.Default.Title;
            EndDate = Properties.Settings.Default.EndDate;

			var timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(500000);
            timer.Start();

			CloseClick = new RelayCommand(() => ViewNavigator.CloseWindow());
            SettingsClick = new RelayCommand(() => ViewNavigator.NavigateTo(new SettingsVM(), null, viewModel =>			
            {
                Properties.Settings.Default.Title = WindowTitle = viewModel.Label;
                Properties.Settings.Default.EndDate = EndDate = viewModel.EndDate;
            }));
        }

        private int days;
        public int Days
        {
            get => days;
			set => SetProperty(ref days, value, () => 
			{
				OnPropertyChanged(nameof(DaysVisible));
				OnPropertyChanged(nameof(DaysLabel));
			});
        }
        
        private int hours;
        public int Hours
        {
            get => hours;
			set => SetProperty(ref hours, value, () => 
			{
				OnPropertyChanged(nameof(HoursVisible));
				OnPropertyChanged(nameof(HoursLabel));
			});
        }

        private int minutes;
        public int Minutes
        {
            get => minutes;
			set => SetProperty(ref minutes, value, () => 
			{
				OnPropertyChanged(nameof(MinutesVisible));
				OnPropertyChanged(nameof(MinutesLabel));
			});
        }

        private int seconds;
        public int Seconds
        {
            get => seconds;
			set => SetProperty(ref seconds, value, () =>
			{
				OnPropertyChanged(nameof(SecondsVisible));
				OnPropertyChanged(nameof(SecondsLabel));
			});
        }

        public bool DaysVisible => Days != 0;
        public bool HoursVisible => Days != 0 || Hours != 0;
        public bool MinutesVisible => Days != 0 || Hours != 0 || Minutes != 0;
        public bool SecondsVisible => Days != 0 || Hours != 0 || Minutes != 0 || Seconds != 0;

        public string DaysLabel => DeclensionHelper.GetDeclension(Days, "День", "Дня", "Дней");
        public string HoursLabel => DeclensionHelper.GetDeclension(Hours, "Час", "Часа", "Часов");
        public string MinutesLabel => DeclensionHelper.GetDeclension(Minutes, "Минута", "Минуты", "Минут");
        public string SecondsLabel => DeclensionHelper.GetDeclension(Seconds, "Секунда", "Секунды", "Секунд");

        private void timer_Tick(object sender, EventArgs e)
        {
            var currentDate = DateTime.Now;
            var delta = EndDate - currentDate;

            if (delta < TimeSpan.Zero)
                delta = TimeSpan.Zero;
            
            Days = delta.Days;
            Hours = delta.Hours;
            Minutes = delta.Minutes;
            Seconds = delta.Seconds;
        }
    }
}