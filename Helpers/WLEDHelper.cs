using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace SimHub.WledRevTransmitter.Helpers
{
    public class WLEDHelper
    {
        private UdpClient LedClient { get; }
        public WLEDHelper(string ip, int port)
        {
            LedClient = new UdpClient(ip, port);
        }
        public void SendData(int red, int green, int blue, int amount)
        {
            var ledData = new List<Byte> { 0x01, 0x01 };
            for (int i = 0; i < amount; i++)
                ledData.AddRange(new List<Byte> { IntToByte(i), IntToByte(red), IntToByte(green), IntToByte(blue) });

            for (int i = amount; i < 60; i++)
                ledData.AddRange(new List<Byte> { IntToByte(i), 0x00, 0x00, 0x00 });

            LedClient.Send(ledData.ToArray(), ledData.ToArray().Length);
        }
        private Byte IntToByte(int input)
        {
            return BitConverter.GetBytes(input)[0];
        }
    }
}
