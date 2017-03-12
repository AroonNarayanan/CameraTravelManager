using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace CameraTravelManager.Controls
{
    public sealed partial class AddBattery : UserControl
    {
        public event EventHandler BatteryAdded;
        public event EventHandler BatterySaved;

        Battery currentBattery;
        bool newBattery;

        public AddBattery(Battery battery)
        {
            this.InitializeComponent();
            if (battery != null)
            {
                currentBattery = battery;
                newBattery = false;
                AddBatteryButton.Content = "Save Battery";
                // TODO: replace with data binding:
                BatteryLabelBox.Text = battery.label;
                BatteryLabelBox.IsEnabled = false;
                BatteryLevelSlider.Value = battery.batteryLevel;
            } else
            {
                newBattery = true;
                AddBatteryButton.Content = "Save Battery";
            }
        }

        private void AddBatteryClick(object sender, RoutedEventArgs e)
        {
            if (newBattery)
            {
                addNewBattery();
            } else
            {
                currentBattery.batteryLevel = BatteryLevelSlider.Value;
                currentBattery.label = BatteryLabelBox.Text;
                BatterySaved(currentBattery, null);
            }
        }

        private void addNewBattery()
        {
            BatteryController.CreateNewBattery(BatteryLabelBox.Text, BatteryLevelSlider.Value);
            BatteryAdded(null, null);
        }
    }
}
