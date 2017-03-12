using CameraTravelManager.Controls;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CameraTravelManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            refreshBatteryList();
        }

        private async void ShowBatteryFlyout(object sender, RoutedEventArgs e)
        {
            var batteryFlyout = new ContentDialog();
            var batteryContent = new AddBattery();
            batteryContent.BatteryAdded += delegate (object senderDelegate, EventArgs eDelegate)
            {
                batteryFlyout.Hide();
                refreshBatteryList();
            };
            batteryFlyout.Content = batteryContent;
            await batteryFlyout.ShowAsync();
        }

        private void refreshBatteryList()
        {
            batteryListView.ItemsSource = BatteryController.GetBatteries();
        }

        private void BatteryLevelChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            BatteryController.saveBattery((System.Collections.ObjectModel.ObservableCollection<Battery>)batteryListView.ItemsSource);
        }
    }
}
