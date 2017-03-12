using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace CameraTravelManager.Controls
{
    public sealed partial class AddBattery : UserControl
    {
        public event EventHandler BatteryAdded;

        public AddBattery()
        {
            this.InitializeComponent();
        }

        private void AddBatteryClick(object sender, RoutedEventArgs e)
        {
            BatteryController.CreateNewBattery(BatteryLabelBox.Text, BatteryLevelSlider.Value);
            BatteryAdded(null, null);
        }
    }
}
