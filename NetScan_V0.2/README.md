#  NetScan V0.2

NetScan is a lightweight **C# TCP port scanner** inspired by network scanning tools such as **Nmap**.

This project is developed as a learning project to understand **TCP networking, asynchronous programming, port scanning, concurrency, and basic cybersecurity concepts** using C# and .NET.

> ⚠️ **Educational & Authorized Use Only:** Use NetScan only on systems and networks that you own or have explicit permission to scan.

---

##  Features

*  Scan an IP address or hostname
*  TCP port scanning
*  Asynchronous port scanning
*  Maximum **100 concurrent scans**
*  Port range scanning
*  Specific port scanning
*  Detect open TCP ports
*  Common service identification
*  Invalid port input handling
*  Duplicate port removal
*  500 ms connection timeout
*  Lightweight console application

---

##  Technologies

* **C#**
* **.NET 10**
* `TcpClient`
* `async/await`
* `Task`
* `SemaphoreSlim`
* `CancellationTokenSource`
* `Dictionary`
* `HashSet`

---

##  Project Structure

```text
NetScan/
│
├── NetScan_V0.2/
│   ├── Program.cs
│   ├── NetScan.csproj
│   └── ...
│
└── README.md
```

---

##  How NetScan Works

NetScan receives a target and a list or range of ports through command-line arguments.

### Basic Syntax

```bash
NetScan <target> -p <ports>
```

The scanner attempts to establish a TCP connection to each selected port.

If the TCP connection succeeds, the port is reported as **OPEN**.

---

##  Supported Port Formats

### 1. Port Range

```bash
NetScan 127.0.0.1 -p 1-100
```

Scans:

```text
1, 2, 3, 4, ... 100
```

---

### 2. Multiple Specific Ports

```bash
NetScan 127.0.0.1 -p 22,80,443
```

Scans:

```text
22
80
443
```

---

### 3. Single Port

```bash
NetScan 127.0.0.1 -p 80
```

Scans only port `80`.

---

## 📊 Example Output

```text
-------------------------------------------------
                 NetScan V0.2
-------------------------------------------------

Target : 127.0.0.1
Ports  : 1-100

Scanning...

-------------------------------------------------
PORT       STATE       SERVICE
-------------------------------------------------
80         OPEN       HTTP
443        OPEN       HTTPS
-------------------------------------------------
Open ports found: 2
Scan completed.
```

> The actual results depend on the services running on the target system.

---

##  Service Detection

NetScan includes a basic mapping between common TCP ports and their commonly associated services.

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

> **Note:** The service name is based on the conventional port number. It does not confirm the actual application running on that port.

---

##  Asynchronous Scanning

NetScan V0.2 uses C# asynchronous programming to scan multiple ports efficiently.

The project uses:

```csharp
async/await
```

and:

```csharp
SemaphoreSlim
```

to control concurrent connections.

The current concurrency limit is:

```csharp
new SemaphoreSlim(100);
```

This allows a maximum of **100 port scans to run concurrently**.

This approach is faster than scanning every port sequentially.

---

##  Connection Timeout

Each TCP connection has a timeout of:

```text
500 milliseconds
```

This prevents the scanner from waiting indefinitely for a connection.

The timeout is implemented using:

```csharp
CancellationTokenSource
```

---

##  Port Validation

NetScan supports valid TCP ports between:

```text
1 - 65535
```

Invalid port numbers are ignored.

Example:

```bash
NetScan 127.0.0.1 -p 99999
```

Output:

```text
Invalid port input.
```

Duplicate ports are automatically removed.

For example:

```bash
NetScan 127.0.0.1 -p 80,80,443
```

will scan each port only once.

---

#  Running NetScan

##  Method  — Run from Terminal

NetScan can be executed using the `.NET CLI`.

### 1. Open a Terminal

You can use:

* Command Prompt
* PowerShell
* Visual Studio Terminal
* Windows Terminal

---

### 2. Navigate to the Project Folder

Navigate to the folder containing the `.csproj` file.

Example:

```bash
cd D:\vsApp\NetScan\NetScan
```

Check the files:

```bash
dir
```

You should see something similar to:

```text
NetScan.csproj
Program.cs
```

---

### 3. Build the Project

Run:

```bash
dotnet build
```

If everything is correct, you should see:

```text
Build succeeded.
```

---

### 4. Run NetScan

The general command is:

```bash
dotnet run -- <target> -p <ports>
```

The `--` separates the `dotnet run` options from the arguments passed to NetScan.

---

###  Scan a Port Range

For example:

```bash
dotnet run -- 127.0.0.1 -p 1-100
```

This scans ports:

```text
1 → 100
```

---

###  Scan Specific Ports

```bash
dotnet run -- 127.0.0.1 -p 22,80,443
```

This scans:

```text
22
80
443
```

---

###  Scan a Single Port

```bash
dotnet run -- 127.0.0.1 -p 80
```

---

##  Method 2 — Run the Compiled `.exe`

After building the project:

```bash
dotnet build
```

the executable will be available inside the `bin` folder.

For example:

```bash
.\bin\Debug\net10.0\NetScan.exe 127.0.0.1 -p 1-100
```

You can also run:

```bash
.\bin\Debug\net10.0\NetScan.exe 127.0.0.1 -p 22,80,443
```

---

##  Method 3 — Run Using Visual Studio

If you are using Visual Studio:

1. Open the NetScan project.
2. Open the project properties.
3. Go to **Debug**.
4. Open the debug launch profile settings.
5. Add the command-line arguments.

Example:

```text
127.0.0.1 -p 1-100
```

Then press:

```text
F5
```

or click **Start**.

---

## 📋 Command Examples

| Command                                              | Description              |
| ---------------------------------------------------- | ------------------------ |
| `dotnet run -- 127.0.0.1 -p 1-100`                   | Scan ports 1–100         |
| `dotnet run -- 127.0.0.1 -p 22,80,443`               | Scan selected ports      |
| `dotnet run -- 127.0.0.1 -p 80`                      | Scan a single port       |
| `.\bin\Debug\net10.0\NetScan.exe 127.0.0.1 -p 1-100` | Run compiled application |

---

##  Learning Objectives

This project helps demonstrate:

* TCP/IP fundamentals
* TCP port concepts
* Socket programming
* `TcpClient`
* Asynchronous programming
* `async/await`
* Task-based programming
* Concurrency control
* `SemaphoreSlim`
* Cancellation and timeout handling
* `Dictionary`
* `HashSet`
* Command-line arguments
* Basic network security concepts

---

##  Future Improvements

Planned features for future versions:

* [ ] Host availability detection
* [ ] Scan time measurement
* [ ] Scan progress indicator
* [ ] Better command-line argument parser
* [ ] Service/banner detection
* [ ] More accurate service identification
* [ ] Network host discovery
* [ ] JSON report export
* [ ] CSV report export
* [ ] TXT report export
* [ ] Colored console output
* [ ] GUI version
* [ ] Configuration options
* [ ] Improved error handling
* [ ] Additional scan options

---

## ⚠️ Disclaimer

NetScan is developed for **educational and authorized security testing purposes**.

Only scan systems, devices, and networks that you own or have explicit permission to test.

Unauthorized network scanning may violate organizational policies or applicable laws.

The developer is not responsible for misuse of this software.

---

##  Author

**Kalhara Nonis**

Learning and building projects in:

* C#
* .NET
* Networking
* Cybersecurity
* Network Programming

---

##  Project Information

| Property  | Details             |
| --------- | ------------------- |
| Project   | NetScan             |
| Version   | V0.2                |
| Language  | C#                  |
| Framework | .NET 10             |
| Type      | Console Application |
| Status    | 🚧 In Development   |

---

## 🔗 GitHub Repository

Source code:

**https://github.com/kalharanonis0/NetScan**

Project directory:

**NetScan_V0.2**

---

## ⭐ Project Status

**NetScan V0.2 — In Development**

The project is being developed step-by-step to explore network programming and cybersecurity concepts using C# and .NET.
