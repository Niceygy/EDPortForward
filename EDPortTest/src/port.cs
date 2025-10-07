using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace EDPortTest.Port
{
    class PortTester
    {
        public static bool TestPort(string IP, int port)
        {
            bool result = false;
            try
            {
                IPAddress address = IPAddress.Parse(IP);

                IPEndPoint endPoint = new IPEndPoint(address, port);

                using (UdpClient client = new UdpClient())
                {
                    client.Connect(endPoint);
                    client.Send(new byte[] { 0 }, 1); // Send a single byte

                    client.Client.ReceiveTimeout = 1000; // Set a 1 second timeout
                    try
                    {
                        var remoteEP = new IPEndPoint(IPAddress.Any, 0);
                        var response = client.Receive(ref remoteEP); // Try to receive a response
                        result = true; // Received a response, port is open
                    }
                    catch (SocketException)
                    {
                        result = false; // No response, port may be closed or not responding
                    }

                    client.Close();
                }
            }
            catch (Exception e)
            {
                string t = e.Message;
            }
            return result;
        }
    }
}