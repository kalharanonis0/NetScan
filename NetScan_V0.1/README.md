# 🔍 NetScan V0.1

**NetScan** is a simple C# console-based TCP port scanning tool inspired by network scanning tools such as Nmap.

The project is created for **learning network programming, TCP connections, port scanning, and basic cybersecurity concepts** using C# and .NET.

> ⚠️ **Educational Use Only:** Use NetScan only on systems and networks that you own or have explicit permission to scan.

---

##  Features

*  TCP port scanning
*  Scan a specific target IP address or hostname
*  Custom start and end port range
*  Detect open TCP ports
*  Basic service identification based on common port numbers
*  Display scan results in a simple table
*  Continue scanning multiple targets without restarting the application
*  500 ms connection timeout for each port
*  Lightweight C# console application

---

##  Technologies

* **C#**
* **.NET**
* `System.Net.Sockets`
* `TcpClient`
* `Dictionary`
* Console Application

---

##  Project Structure

```text
NetScan_V0.1/
│
├── Program.cs
└── README.md
```

---

##  How It Works

NetScan attempts to establish a TCP connection to each port within the selected port range.

For example:

```text
Target: 127.0.0.1
Start Port: 1
End Port: 100
```

The application checks each port and reports ports where a TCP connection can be established.

Example:

```text
-------------------------------------------------
                 NetScan V0.1
-------------------------------------------------

Enter target ip :127.0.0.1
Enter start port :1
Enter stop port :100

Scanning 127.0.0.1...

22         OPEN       SSH
80         OPEN       HTTP

-------------------------------------------------
Open ports found: 2
Scan completed.
```

---

##  Supported Services

NetScan currently recognizes several common ports:

| Port | Service    |
| ---: | ---------- |
|   20 | FTP-Data   |
|   21 | FTP        |
|   22 | SSH        |
|   23 | Telnet     |
|   25 | SMTP       |
|   53 | DNS        |
|   80 | HTTP       |
|  110 | POP3       |
|  143 | IMAP       |
|  443 | HTTPS      |
|  445 | SMB        |
| 3306 | MySQL      |
| 3389 | RDP        |
| 5432 | PostgreSQL |
| 8080 | HTTP-Proxy |

If an open port is not included in the service list, NetScan displays:

```text
Unknown
```

---

##  How to Run

### 1. Clone the repository

```bash
git clone https://github.com/kalharanonis0/NetScan.git
```

### 2. Open the project

Open the project using:

* Visual Studio
* Visual Studio Code
* JetBrains Rider

### 3. Build the project

```bash
dotnet build
```

### 4. Run the application

```bash
dotnet run
```

### 5. Enter the scan information

```text
Enter target ip : 127.0.0.1
Enter start port : 1
Enter stop port : 100
```

---

##  Example

Scanning a local machine:

```text
Target IP: 127.0.0.1
Start Port: 1
Stop Port: 100
```

Possible result:

```text
Scanning 127.0.0.1...

22         OPEN       SSH
80         OPEN       HTTP

-------------------------------------------------
Open ports found: 2
Scan completed.
```

---


##  Disclaimer

NetScan is developed for **educational and authorized security testing purposes**.

Do not scan computers, servers, networks, or systems without permission from the owner.

The developer is not responsible for misuse of this software.

---

## 👨‍💻 Author

**Kalhara Nonis**

A learning project focused on:

**C# • Networking • Cybersecurity • Network Programming**

---

##  Project Status

**Version:** `v0.1`

**Status:** 🚧 In Development

NetScan is currently a basic TCP port scanner. More advanced network security features will be added in future versions.
