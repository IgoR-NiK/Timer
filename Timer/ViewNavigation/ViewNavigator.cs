using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Timer.Views;
using Timer.Windows;
using Timer.ViewModels;

namespace Timer.ViewNavigation
{
    /// <summary>
    /// Менеджер навигации между представлениями.
    /// </summary>
    class ViewNavigator
    {
        /// <summary>
        /// Окно для отображения представлений.
        /// </summary>
        private readonly MainWindow mainWindow;

        /// <summary>
        /// Стек представлений.
        /// </summary>
        private readonly Stack<BaseView> views = new Stack<BaseView>();

        /// <summary>
        /// Установка соответствия ViewModel к View.
        /// </summary>
        private readonly Dictionary<Type, Type> viewModelToViewMap = new Dictionary<Type, Type>()
        {
            [typeof(CountDownVM)] = typeof(CountDownView),
            [typeof(SettingsVM)] = typeof(SettingsView)
        };

        public static ViewNavigator Instance { get; } = new ViewNavigator();

        private ViewNavigator()
        {
            mainWindow = new MainWindow();
            mainWindow.Show();
        }

        /// <summary>
        /// Отображает в окне новое представление, соответствующее указанной <paramref name="viewModel"/>.
        /// </summary>
        /// <param name="viewModel">Указывает на представление, которое необходимо отобразить в окне.</param>
        public void NavigateTo(BaseVM viewModel, Func<BaseVM, bool> afterViewClosed = null)
        {
            if (afterViewClosed != null)
                viewModel.AfterViewClosed = afterViewClosed;

            if (viewModelToViewMap.TryGetValue(viewModel.GetType(), out Type viewType))
            {
                var view = Activator.CreateInstance(viewType) as BaseView;
                view.ViewModel = viewModel;

                views.Push(view);

                mainWindow.Content = view;
                mainWindow.DataContext = viewModel;
            }
        }

        /// <summary>
        /// Закрывает последнее представление и выполняет действие закрытия представления при необходимости.
        /// </summary>
        /// <param name="isCallbackCloseViewHandler">Флаг, указывающий нужно ли выполнять действие закрытия представления.</param>
        public void CloseLastView(bool isCallbackCloseViewHandler = true)
        {
            var lastView = views.Pop();
            if (isCallbackCloseViewHandler)
            {
                if (!lastView.ViewModel.AfterViewClosed?.Invoke(lastView.ViewModel) ?? false)
                {
                    views.Push(lastView);
                    return;
                }
            }

            if (views.Count == 0)
            {
                CloseAllViews();
                return;
            }

            mainWindow.Content = views.Peek();
            mainWindow.DataContext = views.Peek().ViewModel;
        }

        /// <summary>
        /// Закрывает все представления и выходит из главного окна.
        /// </summary>
        public void CloseAllViews()
        {
            mainWindow.Close();
        }
    }
}