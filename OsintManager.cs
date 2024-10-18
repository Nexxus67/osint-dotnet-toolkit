using System;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace OsintToolkit
{
    public class OsintManager
    {
        private readonly string _apiKey;

        public OsintManager(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<DomainInfo> GetDomainInfoAsync(string domain)
        {
            var client = new RestClient("https://www.virustotal.com/vtapi/v2/domain/report");
            var request = new RestRequest
            {
                Method = Method.Get
            };

            request.AddParameter("apikey", _apiKey); 
            request.AddParameter("domain", domain);

            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                try
                {
                    var domainInfo = JsonConvert.DeserializeObject<DomainInfo>(response.Content);

                    if (domainInfo == null)
                    {
                        throw new Exception("Error: Deserialization returned null.");
                    }

                    return domainInfo;
                }
                catch (JsonException jsonEx)
                {
                    Console.WriteLine($"Deserialization error: {jsonEx.Message}");
                    Console.WriteLine($"Response: {response.Content}");
                    throw new Exception("Error while deserializing API response.");
                }
            }
            else
            {
                throw new Exception($"Error when obtaining domain information. Error code: {response.StatusCode}");
            }
        }
    }

    public class DomainInfo
    {
        public string Domain { get; set; }
        public string Whois { get; set; }
        public string[] Subdomains { get; set; }
        public string VerboseMsg { get; set; }
        public int ResponseCode { get; set; }
        public List<Resolution> Resolutions { get; set; }
    }

    public class Resolution
    {
        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }

        [JsonProperty("last_resolved")]
        public DateTime LastResolved { get; set; }
    }
}
