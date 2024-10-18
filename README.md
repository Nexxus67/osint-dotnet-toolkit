
### OSINT ToolKit Library for .NET

The OSINT Toolkit is a nuget package designed to gather and analyze information about domains using various online services. Currently, it integrates with the VirusTotal API to fetch domain details, including WHOIS information and resolution history. This tool aims to aid in Open Source Intelligence (OSINT) investigations.

### Features

Retrieve domain information from VirusTotal.
Access WHOIS data.
List historical resolutions of the domain.

### To-Do

Integrate with Shodan API for device and service information.
Add support for Have I Been Pwned API to check for data breaches.
Implement additional OSINT tools for enhanced domain analysis.
Optimize error handling and logging for better debugging.

### Usage

Download the package: 

```bash
dotnet add package OsintManager.Net
```

Then create an instance of OsintManager, like this for example: 

```csharp
using System;
using OsintToolkit;
using Newtonsoft.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string apiKey = "your-api-key"; 

        OsintManager osintManager = new OsintManager(apiKey); 

        try
        {
            DomainInfo domainInfo = await osintManager.GetDomainInfoAsync("tiboapp.com");
            Console.WriteLine($"Domain: {domainInfo.Domain}");
            Console.WriteLine($"WHOIS: {domainInfo.Whois}");
            Console.WriteLine("Resolutions:");
            foreach (Resolution resolution in domainInfo.Resolutions)
            {
                Console.WriteLine($"IP: {resolution.IpAddress}, Last Resolved: {resolution.LastResolved}");
            }
        }
        catch (JsonException jsonEx)
        {
            Console.WriteLine($"JSON Error: {jsonEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}


```


Make sure to replace the your-api-key with your virustotal key and replace yourdomain.com with the desired domain
