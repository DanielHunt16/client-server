using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string message = "Hello World";
            byte[] bytes = SendMessage(System.Text.Encoding.Unicode.GetBytes(message));

            Console.ReadLine();
        }

        static byte[] SendMessage(byte[] messageBytes)
        {
            const int byteSize = 1024 * 1024;
            try
            {
                System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient("127.0.0.1", 1234);
                NetworkStream stream = client.GetStream();

                stream.Write(messageBytes, 0, messageBytes.Length);
                Console.WriteLine("================================");
                Console.WriteLine("=   Connected to the server    =");
                Console.WriteLine("================================");
                Console.WriteLine("Waiting for response...");

                messageBytes = new byte[byteSize];

                stream.Read(messageBytes, 0, messageBytes.Length);

                stream.Dispose();
                stream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return messageBytes;
        }
    }
}
