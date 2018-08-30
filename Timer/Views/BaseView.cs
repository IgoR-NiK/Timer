using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using Timer.ViewModels;

namespace Timer.Views
{
    public class BaseView : UserControl
    {
        protected BaseVM viewModel;
        public BaseVM ViewModel
        {
            get => viewModel;
            set
            {
                viewModel = value;
                DataContext = viewModel;
            }
        }
    }
}
