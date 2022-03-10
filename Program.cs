using System;
using System.Text;
using System.Threading;

namespace p2p
{

    class Common {
        public static int REQ_LISTENER_PORT = 2005;
        public static int CONTENT_LISTENER_PORT = 4001;

        public static string requestToString(byte[] bytes) {
            return Encoding.ASCII.GetString(bytes);
        }
    }
    class Content {
        private string content;
        public Content(string c) {
            content = c;
        }
        public string getContent() {
            return content;
        }
        public void setContent(string newContent) {
            content = newContent;
        }
        public void appendToContnet(string newContent) {
            content += newContent;
        }
    }

    class IPTable {
        private string[] ipAddresses = new string[100];
        private int ipCount = 0;
        public string[] getIpAddresses() {
            return ipAddresses;
        }
        public void addIP(string ip) {
            ipAddresses[ipCount] = ip;
            ipCount++;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            UDPClient client = new UDPClient();
            string action = getAction();
            executeAction(action);
        }

        static string getAction() {
            Console.WriteLine("Join: 1\nStart new: 2");
            return Console.ReadLine();
        }

        static void executeAction(string action) {
            if(action == "1") {
                join();
            }else {
                startNewPeer();
            }
        }

        static void join() {
            string ip = getIPAddressFromUser();
            new Thread(() => startContentListener(ip)).Start();
            new Thread(() => sendRequest(ip)).Start();
        }

        static string getIPAddressFromUser() {
            Console.WriteLine("IP address: ");
            return Console.ReadLine();
        }

        static void startContentListener(string ip) {
            UDPContenetListener cl = new UDPContenetListener();
            cl.receiveContent();
            startNewPeer(ip);
        }

        static void sendRequest(string ip) {
            UDPClient client = new UDPClient();
            client.sendTo(ip, Common.REQ_LISTENER_PORT);
        }

        static void startNewPeer(string ip) {
            UDPReqListener listener = new UDPReqListener(Common.REQ_LISTENER_PORT);
            listener.addIP(ip);
            listener.start();
        }

        static void startNewPeer()  {
            UDPReqListener listener = new UDPReqListener(Common.REQ_LISTENER_PORT);
            listener.start();
        }
    } 
}
