using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace EDPortTest.Port
{
    class PortTester
    {
        static bool TestPort(string IP, int port)
        {
            bool result = false;
            try
            {
                IPAddress address = IPAddress.Parse(IP);

                IPEndPoint endPoint = new IPEndPoint(address, port);

                using (UdpClient client = new UdpClient())
                {
                    client.Connect(endPoint);
                    client.Close();
                    result = true;
                }
            }
            catch (Exception e)
            {

            }
            return result;
        }
    }
}