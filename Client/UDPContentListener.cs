using System.Net;
using System.Net.Sockets;

namespace p2p {
    class UDPContenetListener : UDPListener {
        Content content;
        public UDPContenetListener() : base(Common.CONTENT_LISTENER_PORT) {
            content = new Content("");
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
                byte[] bytes = listener.Receive(ref groupEp);
                content.setContent(Common.requestToString(bytes));
                System.Console.WriteLine("Content: " + content.getContent());
                break;
            }
        }
    }
}