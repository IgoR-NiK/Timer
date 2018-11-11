using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Timer.ViewModels;
using Timer.Windows;
using MVVMAqua;

namespace Timer
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
			var bootstrapper = new Bootstrapper();
			bootstrapper.OpenNewWindow(new MainWindow(), new CountDownVM(), _ =>
			{
				Timer.Properties.Settings.Default.Save();
				return true;
			});
		}
	}
}