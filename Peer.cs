using System;
using System.Threading;

namespace p2p {
    class Peer {
        private Content content;
        public Peer() {
            content = new Content("test");
        }

        public void Start() {
            startNewPeer();
        }

        public void Join() {
            string ip = getIPAddressFromUser();
            new Thread(() => startContentListener(ip)).Start();
            new Thread(() => sendRequest(ip)).Start();
        }

        string getIPAddressFromUser() {
            Console.WriteLine("IP address: ");
            return Console.ReadLine();
        }

        void startContentListener(string ip) {
            UDPContenetListener cl = new UDPContenetListener(content);
            cl.receiveContent();
            startNewPeer(ip);
        }

        void sendRequest(string ip) {
            UDPClient client = new UDPClient();
            client.sendTo(ip, Common.REQ_LISTENER_PORT);
        }

        void startNewPeer(string ip) {
            UDPReqListener listener = new UDPReqListener(content, Common.REQ_LISTENER_PORT);
            listener.addIP(ip);
            runNewPeer(listener);
        }

        void startNewPeer()  {
            UDPReqListener listener = new UDPReqListener(content, Common.REQ_LISTENER_PORT);
            runNewPeer(listener);
        }
        
        void runNewPeer(UDPReqListener listener) {
            new Thread(() => listener.start()).Start();
            new Thread(() => listener.waitForContent()).Start();
        }
    }
}