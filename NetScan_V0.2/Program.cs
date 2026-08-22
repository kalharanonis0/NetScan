using System;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

class Program
{
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

    static async Task Main(string[] args)
    {
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("                 NetScan V0.2");
        Console.WriteLine("-------------------------------------------------");

        
        if (args.Length < 3 || args[1] != "-p")
        {
            ShowHelp();
            return;
        }

        string target = args[0];
        string portInput = args[2];

        
        List<int> ports = ParsePorts(portInput);

        if (ports.Count == 0)
        {
            Console.WriteLine("Invalid port input.");
            return;
        }

        Console.WriteLine($"\nTarget : {target}");
        Console.WriteLine($"Ports  : {portInput}");
        Console.WriteLine("\nScanning...\n");

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("PORT       STATE       SERVICE");
        Console.WriteLine("-------------------------------------------------");

        int openPorts = 0;

        
        using SemaphoreSlim semaphore = new SemaphoreSlim(100);

        List<Task<(int Port, bool IsOpen)>> tasks = new();

        foreach (int port in ports)
        {
            tasks.Add(
                ScanWithLimitAsync(
                    target,
                    port,
                    semaphore
                )
            );
        }

        
        var results = await Task.WhenAll(tasks);

        
        foreach (var result in results)
        {
            if (result.IsOpen)
            {
                string service = GetServiceName(result.Port);

                Console.WriteLine(
                    $"{result.Port,-10} OPEN       {service}"
                );

                openPorts++;
            }
        }

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine($"Open ports found: {openPorts}");
        Console.WriteLine("Scan completed.");
    }


   
    static async Task<(int Port, bool IsOpen)> ScanWithLimitAsync(
        string target,
        int port,
        SemaphoreSlim semaphore)
    {
        await semaphore.WaitAsync();

        try
        {
            bool isOpen = await ScanPortAsync(target, port);

            return (port, isOpen);
        }
        finally
        {
            semaphore.Release();
        }
    }


    
    static async Task<bool> ScanPortAsync(
        string target,
        int port)
    {
        try
        {
            using TcpClient client = new TcpClient();

            using CancellationTokenSource cts =
                new CancellationTokenSource(500);

            await client.ConnectAsync(
                target,
                port,
                cts.Token
            );

            return client.Connected;
        }
        catch
        {
            return false;
        }
    }


    
    static string GetServiceName(int port)
    {
        if (Services.TryGetValue(
            port,
            out string? service))
        {
            return service;
        }

        return "Unknown";
    }


    
    static List<int> ParsePorts(string input)
    {
        List<int> ports = new List<int>();

        input = input.Trim();

        
        if (input.Contains(","))
        {
            string[] parts = input.Split(',');

            foreach (string part in parts)
            {
                if (int.TryParse(
                    part.Trim(),
                    out int port))
                {
                    if (port >= 1 && port <= 65535)
                    {
                        ports.Add(port);
                    }
                }
            }
        }

       
        else if (input.Contains("-"))
        {
            string[] parts = input.Split('-');

            if (parts.Length == 2 &&
                int.TryParse(
                    parts[0].Trim(),
                    out int startPort) &&
                int.TryParse(
                    parts[1].Trim(),
                    out int endPort))
            {
                if (startPort >= 1 &&
                    endPort <= 65535 &&
                    startPort <= endPort)
                {
                    for (
                        int port = startPort;
                        port <= endPort;
                        port++)
                    {
                        ports.Add(port);
                    }
                }
            }
        }

        
        else
        {
            if (int.TryParse(
                input,
                out int port))
            {
                if (port >= 1 && port <= 65535)
                {
                    ports.Add(port);
                }
            }
        }

        
        return new List<int>(
            new HashSet<int>(ports)
        );
    }


    
    static void ShowHelp()
    {
        Console.WriteLine(
            "NetScan - Network Port Scanner"
        );

        Console.WriteLine();

        Console.WriteLine("Usage:");
        Console.WriteLine(
            "NetScan <target> -p <ports>"
        );

        Console.WriteLine();

        Console.WriteLine("Examples:");

        Console.WriteLine(
            "NetScan 127.0.0.1 -p 1-100"
        );

        Console.WriteLine(
            "NetScan 127.0.0.1 -p 22,80,443"
        );

        Console.WriteLine(
            "NetScan 127.0.0.1 -p 80"
        );
    }
}