using System.Net;
using System.Net.Sockets;

namespace p2p {
    class UDPContenetListener : UDPListener {
        private Content content;
        public UDPContenetListener(Content content): base(Common.CONTENT_LISTENER_PORT) {
            this.content = content;
        }
        public string receiveContent() {
            try {
                receive();
            }catch(SocketException e) {
                System.Console.WriteLine(e);
            }
            return "";
        }
        private void receive() {
            while(true) {
                try {
                    updateContent();
                }catch(ContentVersionOutdated e) {}
                break;
            }
        }

        private byte[] receiveBytes() { 
            return listener.Receive(ref groupEp);
        }

        private void updateContent() {
            byte[] bytes = receiveBytes();
            int version = getVersionFromPacket(Common.packetToString(bytes));
            checkVersion(version);
            string newContent = getContentFromPacket(Common.packetToString(bytes));
            content.setContent(newContent, version);
            System.Console.WriteLine("Content: " + content.getContent());
        }

        private void checkVersion(int version) {
            if(content.getVersion() < version) {
                throw new ContentVersionOutdated();
            }
        }
    }
}