using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

StartServer();

void StartServer()
{
    IPHostEntry host = Dns.GetHostEntry("localhost");
    IPAddress ipAddress = host.AddressList[0];
    IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

    try
    {

        // Create a Socket that will use Tcp protocol
        Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        // A Socket must be associated with an endpoint using the Bind method
        listener.Bind(localEndPoint);
        // Specify how many requests a Socket can listen before it gives Server busy response.
        // We will listen 10 requests at a time
        listener.Listen(10);

        Console.WriteLine("Waiting for a connection...");
        Socket handler = listener.Accept();

        // Incoming data from the client.

        string? input = Console.ReadLine();
        while (input != "exit")
        {
            byte[]? msg = Encoding.ASCII.GetBytes(input ?? "");
            handler.Send(msg);
            Thread.Sleep(0);
            input = Console.ReadLine();
        }

        handler.Shutdown(SocketShutdown.Both);
        handler.Close();
    }
    catch (Exception e)
    {
        Console.WriteLine(e.ToString());
    }

    Console.WriteLine("\n Press any key to continue...");
    Console.ReadKey();
}