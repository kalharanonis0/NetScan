using System;
using System.Net;
using System.Net.Sockets;

class program {

    static readonly Dictionary<int, string> Services = new()
    {
        { 20, "FTP-Data" },
        { 21, "FTP" },
        { 22, "SSH" },
        { 23, "Telnet" },
        { 25, "SMTP" },
        { 53, "DNS" },
        { 80, "HTTP" },
        { 110, "POP3" },
        { 143, "IMAP" },
        { 443, "HTTPS" },
        { 445, "SMB" },
        { 3306, "MySQL" },
        { 3389, "RDP" },
        { 5432, "PostgreSQL" },
        { 8080, "HTTP-Proxy" }
    };


    static void Main() {
        Boolean xc=true;

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("                 NetScan V0.1");
        Console.WriteLine("-------------------------------------------------");

        do
        {
            Console.Write("\nEnter target ip :");
            string target = Console.ReadLine();

            Console.Write("Enter start port :");
            int startPort = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter stop port :");
            int endPort = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"\nScanning {target}...\n");

            int openPorts = 0;


            for (int port = startPort; port <= endPort; port++)
            {
                if (ScanPort(target, port))
                {
                    string service = GetServiceName(port);

                    Console.WriteLine(
                        $"{port,-10} OPEN       {service}"
                    );

                    openPorts++;
                }
            }

            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine($"Open ports found: {openPorts}");
            Console.WriteLine("Scan completed.");

            Console.Write("You want to continue (Y) :");
            Char continueStates =Convert.ToChar(Console.ReadLine());
            Console.WriteLine("-------------------------------------------------");

            if (continueStates == 'Y' || continueStates == 'y') {
                xc = true;
            }
            else if(continueStates==null) {
                xc = false;
            }
            else {
                xc = false;
            }



        }while (xc);
    }


    static bool ScanPort(string target, int port)
    {
        try
        {
            using TcpClient client = new TcpClient();

            var result = client.BeginConnect(
                target,
                port,
                null,
                null
            );

            bool connected = result.AsyncWaitHandle.WaitOne(500);

            if (connected && client.Connected)
            {
                return true;
            }
        }
        catch( Exception ex ) 
        {
            Console.WriteLine(ex.ToString() );
        }

        return false;
    }

    static string GetServiceName(int port)
    {
        if (Services.TryGetValue(port, out string? service))
        {
            return service;
        }

        return "Unknown";
    }
}


