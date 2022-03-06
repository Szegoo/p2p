using System.Net;
using System.Net.Sockets;
using System.Text;

namespace p2p {
    class UDPClient {

        public void sendContentTo(string ip, int port, string content = "GET") {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPAddress broadcast = IPAddress.Parse(ip);

            byte[] request = Encoding.ASCII.GetBytes(content);
            IPEndPoint endpoint = new IPEndPoint(broadcast, port);
            s.SendTo(request, endpoint);

            System.Console.WriteLine("Request sent to " + ip + ":" +port.ToString());
        }
    }
}