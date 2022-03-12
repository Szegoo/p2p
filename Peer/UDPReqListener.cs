using System;
using System.Net.Sockets;

namespace p2p {
    class UDPReqListener : UDPListener {
        private Content content;
        private IPTable ipTable;
        public UDPReqListener(Content content, int port) : base(port) {
            this.content = content;
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
                System.Console.WriteLine($"Received: {Common.packetToString(bytes)}");
                classifyRequest(bytes);
            }
        }

        private void classifyRequest(byte[] bytes) {
            string req = Common.packetToString(bytes);
            switch(req) {
                case "GET":
                    acceptNewPeer();
                    break;
                default:
                    int version = getVersionFromPacket(req);
                    string reqC = getContentFromPacket(req);
                    content.setContent(reqC, version);
                    break;
            }
        }

        private void acceptNewPeer(){
            UDPClient c = new UDPClient();
            string ip = groupEp.Address.ToString();
            System.Console.WriteLine("contentPacket: " + getContentPacket());
            addIP(ip);
            c.sendTo(ip, Common.CONTENT_LISTENER_PORT, getContentPacket());
        }

        private string getContentPacket() {
            return content.getVersion().ToString() + ":" + content.getContent();
        }

        public void addIP(string ip) {
            ipTable.addIP(ip);
        }

        public void waitForContent() {
            System.Console.WriteLine("New content: ");
            string newContent = Console.ReadLine();
            addContent(newContent);
        }
        private void addContent(string content) {
            this.content.appendToContnet(content);
            this.notifyTheNetwork();
        }

        private void notifyTheNetwork() {
            UDPClient client = new UDPClient();
            string[] addresses = ipTable.getIpAddresses();
            for(int i = 0 ; i < ipTable.getIPCount(); i++) {
                client.sendTo(addresses[i], Common.REQ_LISTENER_PORT, getContentPacket());
            }
            waitForContent();
        }
    }
}