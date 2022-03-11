using System.Net.Sockets;

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
            string req = Common.requestToString(bytes);
            switch(req) {
                case "GET":
                    acceptNewPeer();
                    break;
                default:
                    content.setContent(req);
                    break;
            }
        }

        private void acceptNewPeer(){
            UDPClient c = new UDPClient();
            string ip = groupEp.Address.ToString();
            addIP(ip);
            c.sendTo(ip, Common.CONTENT_LISTENER_PORT, content.getContent());
        }

        public void addIP(string ip) {
            ipTable.addIP(ip);
        }

        public void addContent(string content) {
            this.content.appendToContnet(content);
            this.notifyTheNetwork();
        }

        private void notifyTheNetwork() {
            UDPClient client = new UDPClient();
            string[] addresses = ipTable.getIpAddresses();
            for(int i = 0 ; i < addresses.Length; i++) {
                client.sendTo(addresses[i], Common.REQ_LISTENER_PORT, content.getContent());
            }
        }
    }
}