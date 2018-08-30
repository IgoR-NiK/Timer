using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timer.ViewModels
{
    class SettingsVM : BaseVM
    {
        public string Label { get; set; }

        public DateTime EndDate { get; set; }

        public SettingsVM()
        {
            Title = "Настройки";
            EndDate = DateTime.Now;
        }
    }
}
