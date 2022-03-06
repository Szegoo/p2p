using System.Net;
using System.Net.Sockets;

namespace p2p {
    abstract class UDPListener {
        private protected UdpClient listener;
        private protected IPEndPoint groupEp;
        public UDPListener(int port) {
            listener = new UdpClient(port);    
            groupEp = new IPEndPoint(IPAddress.Any, port);
        }
    }
}