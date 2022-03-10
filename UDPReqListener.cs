using System.Net;
using System.Net.Sockets;
using System.Text;

namespace p2p {
    class UDPReqListener : UDPListener {
        private Content content;
        private IPTable ipTable;
        public UDPReqListener(int port) : base(port) {
            content = new Content("test");
            ipTable = new IPTable();
        }

        public void start() {
            try {
                receiveBytes();
            }catch(SocketException e) {
                System.Console.WriteLine(e);
            }
        }

        private byte[] receiveBytes() {
            System.Console.WriteLine("Waiting for requests...");
            while(true) {
                byte[] bytes = listener.Receive(ref groupEp);
                System.Console.WriteLine($"Received: {Common.requestToString(bytes)}");
                classifyRequest(bytes);
            }
        }

        private void classifyRequest(byte[] bytes) {
            switch(Common.requestToString(bytes)) {
                case "GET":
                    UDPClient c = new UDPClient();
                    string ip = groupEp.Address.ToString();
                    addIP(ip);
                    c.sendTo(ip, Common.CONTENT_LISTENER_PORT, content.getContent());
                    break;
            }
        }

        public void addIP(string ip) {
            ipTable.addIP(ip);
        }
    }
}