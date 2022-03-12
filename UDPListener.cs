using System;
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
 
        private protected int getVersionFromPacket(string packet) {
            int separatorIndx = getSeparatorIndx(packet);
            return Convert.ToInt32(packet.Substring(0, separatorIndx-1));
        }

        private protected string getContentFromPacket(string packet) {
            int separatorIndx = getSeparatorIndx(packet);
            return packet.Substring(separatorIndx+1);
        }

        private protected int getSeparatorIndx(string req) {
            return req.IndexOf(':');
        }
    }
}