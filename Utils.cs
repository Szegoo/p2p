using System.Text;

namespace p2p {
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
            System.Console.WriteLine("Content updated");
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

        public int getIPCount() {
            return ipCount;
        }
    }
}