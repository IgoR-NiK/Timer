using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Timer.ViewModels
{
    class CountDownVM : INotifyPropertyChanged
    {
        DateTime EndDate { get; } = new DateTime(2018, 8, 30);

        public CountDownVM()
        {
            Title = "До защиты диплома осталось:";

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(500000);
            dispatcherTimer.Start();
        }

        private string title;
        public string Title
        {
            get => title;
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged();
                }
            }
        }

        private int days;
        public int Days
        {
            get => days;
            set
            {
                days = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DaysVisible));                
            }
        }
        
        private int hours;
        public int Hours
        {
            get => hours;
            set
            {
                hours = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HoursVisible));
            }
        }

        private int minutes;
        public int Minutes
        {
            get => minutes;
            set
            {
                minutes = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(MinutesVisible));                
            }
        }

        private int seconds;
        public int Seconds
        {
            get => seconds;
            set
            {
                seconds = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SecondsVisible));
            }
        }

        public bool DaysVisible => Days != 0;
        public bool HoursVisible => Days != 0 || Hours != 0;
        public bool MinutesVisible => Days != 0 || Hours != 0 || Minutes != 0;
        public bool SecondsVisible => Days != 0 || Hours != 0 || Minutes != 0 || Seconds != 0;

        private void dispatcherTimer_Tick(object sender, EventArgs e)
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
