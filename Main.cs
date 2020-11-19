using GameReaderCommon;
using SimHub.Plugins;
using System;
using System.Diagnostics;
using SimHub.WledRevTransmitter.Helpers;

namespace SimHub.WledRevTransmitter
{
    [PluginDescription("A plugin to communicate with WLED")]
    [PluginAuthor("Mushtario Pepperoni - Maxim Van den Eede")]
    [PluginName("WLED Rev Transmitter")]
    public class Main : IPlugin, IDataPlugin, IWPFSettings
    {
        public PluginManager PluginManager { get; set; }
        private WLEDHelper LedHelper { get; set; }
        public void DataUpdate(PluginManager pluginManager, ref GameData data)
        {
            if (!data.GameRunning) return;

            Debug.WriteLine($"Redline: {data.NewData.MaxRpm}. RPM: {data.NewData.Rpms}");

            var rpmPerLed = data.NewData.MaxRpm / 60.0;

            var ledAmount = data.NewData.Rpms / rpmPerLed;

            if (ledAmount >= 60) ledAmount = 59;
            if (ledAmount < 0) ledAmount = 0;

            LedHelper.SendData(255, 0, 0, Convert.ToInt32(Math.Floor(ledAmount)));
        }
        public void End(PluginManager pluginManager)
        {

        }
        public System.Windows.Controls.Control GetWPFSettingsControl(PluginManager pluginManager)
        {
            return null;
        }

        public void Init(PluginManager pluginManager)
        {
            Logging.Current.Info("Starting plugin");
            LedHelper = new WLEDHelper("192.168.0.12", 21324);
        }
    }
}