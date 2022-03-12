using System.Text;

namespace p2p {
    class Common {
        public static int REQ_LISTENER_PORT = 2005;
        public static int CONTENT_LISTENER_PORT = 4001;

        public static string packetToString(byte[] bytes) {
            return Encoding.ASCII.GetString(bytes);
        }
    }
    class Content {
        private string content;
        private int version = 0;
        public Content(string c) {
            content = c;
        }
        public string getContent() {
            return content;
        }
        public void setContent(string newContent, int version) {
            content = newContent;
            this.version = version;
            System.Console.WriteLine("Content updated: " + content);
        }
        public void appendToContnet(string newContent) {
            content += newContent;
            version++;
        }

        public int getVersion() {
            return version;
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