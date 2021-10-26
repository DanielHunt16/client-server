/***
 * The program is synchronous so it can only accept one request at a time
 * Needs to be made asynchronous to accept multiple requests at a time
***/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //Sets IP address/port and starts listening 
            IPEndPoint ep = new IPEndPoint(IPAddress.Loopback, 1234);
            TcpListener listener = new TcpListener(ep);
            listener.Start();

            Console.WriteLine($@"  
            ===================================================  
                   Started listening requests at: {ep.Address}:{ep.Port}  
            ===================================================");

            while (true)
            {
                //Data is sent over the network as bytes 
                const int bytesize = 1024 * 1024;
                string message = null;
                byte[] buffer = new byte[bytesize];

                var sender = listener.AcceptTcpClient();
                Console.WriteLine("Data received");
                //Reads bytes from the network 
                sender.GetStream().Read(buffer, 0, bytesize);

                message = CleanMessage(buffer);
                Console.WriteLine(message);

                byte[] bytes = System.Text.Encoding.Unicode.GetBytes("Thank you for your message");
                sender.GetStream().Write(bytes, 0, bytes.Length); 
            }

            Console.ReadLine();
        }

        //Takes message as byte array and coverts it to a string and removes any null values
        static string CleanMessage(byte[] bytes)
        {
            string message = System.Text.Encoding.Unicode.GetString(bytes);

            string messageToPrint = null;

            foreach(var nullChar in message)
            {
                if(nullChar != '\0')
                {
                    messageToPrint += nullChar;
                }
            }

            return messageToPrint;
        }

        static void SendMessage(byte[] bytes, TcpClient client)
        {
            client.GetStream().Write(bytes, 0, bytes.Length);
        }

        static void sendEmail()
        {
            using (SmtpClient client = new SmtpClient("")) ;
        }
    }
}
