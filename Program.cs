using System;
using System.Text;
using System.Threading;

namespace p2p
{

    class Common {
        public static int REQ_LISTENER_PORT = 2005;
        public static int CONTENT_LISTENER_PORT = 5000;

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
    class Program
    {
        static void Main(string[] args)
        {
            UDPClient client = new UDPClient();
            Console.WriteLine("Join: 1\nStart new: 2");
            string decision = Console.ReadLine();
            if(decision == "1") {
                join();
            }else startNew();
        }
        static void join() {
            new Thread(() => {
                UDPContenetListener cl = new UDPContenetListener();
                cl.receiveContent();
            }).Start();
            Thread.Sleep(100);
            new Thread(() => {
                UDPClient client = new UDPClient();
                client.sendContentTo("127.0.0.1", Common.REQ_LISTENER_PORT);
            }).Start();
        }
        static void startNew()  {
            UDPReqListener listener = new UDPReqListener(Common.REQ_LISTENER_PORT);
            listener.start();    
        }
    } 
}
