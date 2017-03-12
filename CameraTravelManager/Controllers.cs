using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace CameraTravelManager
{
    class BatteryController
    {
        public static Battery CreateNewBattery()
        {
            Battery newBattery = new Battery();
            addBattery(newBattery);
            return newBattery;
        }

        public static Battery CreateNewBattery(string label)
        {
            Battery newBattery = new Battery(label);
            addBattery(newBattery);
            return newBattery;
        }

        public static Battery CreateNewBattery(string label, double batteryLevel)
        {
            Battery newBattery = new Battery(label, batteryLevel);
            addBattery(newBattery);
            return newBattery;
        }

        public static ObservableCollection<Battery> GetBatteries()
        {
            var remoteSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            var serializedBatteryList = (string)remoteSettings.Values["batteries"];
            if (serializedBatteryList == null)
            {
                return null;
            } else
            {
                return JsonConvert.DeserializeObject<ObservableCollection<Battery>>(serializedBatteryList);
            }
        }

        public static void addBattery(Battery battery)
        {
            var remoteSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            var serializedBatteryList = (string)remoteSettings.Values["batteries"];
            var batteryList = new ObservableCollection<Battery>();
            if (serializedBatteryList != null)
            {
                batteryList = JsonConvert.DeserializeObject<ObservableCollection<Battery>>(serializedBatteryList);
            }
            batteryList.Add(battery);
            remoteSettings.Values["batteries"] = JsonConvert.SerializeObject(batteryList);
        }

        public static void updateBatteryList(ObservableCollection<Battery> batteryList)
        {
            var remoteSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            remoteSettings.Values["batteries"] = JsonConvert.SerializeObject(batteryList);
        }
    }
}
