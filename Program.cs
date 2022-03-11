using System;
using System.Threading;

namespace p2p
{ 
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
            runNewPeer(listener);
        }

        static void startNewPeer()  {
            UDPReqListener listener = new UDPReqListener(Common.REQ_LISTENER_PORT);
            runNewPeer(listener);
        }
        static void runNewPeer(UDPReqListener listener) {
            new Thread(() => listener.start()).Start();
            new Thread(() => listener.waitForContent()).Start();
        }
    } 
}
