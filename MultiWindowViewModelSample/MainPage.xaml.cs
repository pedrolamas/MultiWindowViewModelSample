using System;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MultiWindowViewModelSample
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel => App.MainViewModel;

        public MainPage()
        {
            DataContext = ViewModel;

            this.InitializeComponent();

            ApplicationView.GetForCurrentView().Consolidated += ApplicationView_OnConsolidated;
        }

        private void ApplicationView_OnConsolidated(ApplicationView s, ApplicationViewConsolidatedEventArgs e)
        {
            if (e.IsAppInitiated || e.IsUserInitiated)
            {
                s.Consolidated -= ApplicationView_OnConsolidated;

                DataContext = null;

                // this is only required if you are using compiled bindings (x:Bind)
                Bindings.StopTracking();
            }
        }

        private async void CreateNewWindowButton_Click(object sender, RoutedEventArgs e)
        {
            var currentView = ApplicationView.GetForCurrentView();

            await CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                var newView = ApplicationView.GetForCurrentView();
                var frame = new Frame();
                frame.Navigate(typeof(MainPage));
                Window.Current.Content = frame;

                Window.Current.Activate();

                ApplicationViewSwitcher.TryShowAsStandaloneAsync(
                    newView.Id,
                    ViewSizePreference.UseMinimum,
                    currentView.Id,
                    ViewSizePreference.UseMinimum);
            });
        }
    }
}
