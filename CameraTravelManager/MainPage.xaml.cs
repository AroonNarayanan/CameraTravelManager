using CameraTravelManager.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        private async void ShowNewBatteryFlyout(object sender, RoutedEventArgs e)
        {
            var batteryFlyout = new ContentDialog();
            var batteryContent = new AddBattery(null);
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
            //TODO: use binding so we don't need this method
            batteryListView.ItemsSource = BatteryController.GetBatteries();
        }

        private void ToggleSelection(object sender, RoutedEventArgs e)
        {
            if (batteryListView.SelectionMode == ListViewSelectionMode.None)
            {
                batteryListView.SelectionMode = ListViewSelectionMode.Multiple;
                DeleteButton.Visibility = Visibility.Visible;
                EmptyButton.Visibility = Visibility.Visible;
                AddButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                batteryListView.SelectionMode = ListViewSelectionMode.None;
                DeleteButton.Visibility = Visibility.Collapsed;
                EmptyButton.Visibility = Visibility.Collapsed;
                AddButton.Visibility = Visibility.Visible;
            }
        }

        private void EmptySelectedBatteries(object sender, RoutedEventArgs e)
        {
            foreach (Battery battery in batteryListView.SelectedItems)
            {
                battery.batteryLevel = 0;
            }
            BatteryController.updateBatteryList((ObservableCollection<Battery>)batteryListView.ItemsSource);
            refreshBatteryList();
        }

        private void DeleteSelectedBatteries(object sender, RoutedEventArgs e)
        {
            foreach (Battery battery in batteryListView.SelectedItems)
            {
                ((ObservableCollection<Battery>)batteryListView.ItemsSource).Remove(battery);
            }
            BatteryController.updateBatteryList((ObservableCollection<Battery>)batteryListView.ItemsSource);
        }

        private async void EditBattery(object sender, RoutedEventArgs e)
        {
            var batteryFlyout = new ContentDialog();
            var batteryContent = new AddBattery((Battery)((HyperlinkButton)sender).DataContext);
            batteryContent.BatterySaved += delegate (object battery, EventArgs eDelegate)
            {
                batteryFlyout.Hide();
                //TODO: use ID to index instead of label
                var existingBattery = ((ObservableCollection<Battery>)batteryListView.ItemsSource).FirstOrDefault<Battery>(n => n.label == ((Battery)battery).label);
                existingBattery.batteryLevel = ((Battery)battery).batteryLevel;
                BatteryController.updateBatteryList((ObservableCollection<Battery>)batteryListView.ItemsSource);
                refreshBatteryList();
            };
            batteryFlyout.Content = batteryContent;
            await batteryFlyout.ShowAsync();
        }
    }
}
