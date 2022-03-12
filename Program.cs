using System;
using System.Threading;

namespace p2p
{ 
    class Program
    {
        static void Main(string[] args)
        {
            string action = getAction();
            executeAction(action);
        }

        static string getAction() {
            Console.WriteLine("Join: 1\nStart new: 2");
            return Console.ReadLine();
        }

        static void executeAction(string action) {
            Peer p = new Peer();
            if(action == "1") {
                p.Join();
            }else {
                p.Start();
            }
        }
    } 
}
