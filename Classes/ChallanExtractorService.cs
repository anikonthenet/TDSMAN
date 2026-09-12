using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TDSMAN.Classes
{
    public class ChallanRecord
    {
        public string Cin { get; set; }
        public string Amount { get; set; }
        public string Section { get; set; }
        public string AlternateCIN { get; set; }
    }

    public class DateRange
    {
        [JsonProperty("fromDate")]
        public string FromDate { get; set; }

        [JsonProperty("toDate")]
        public string ToDate { get; set; }
    }

    public class RequestPayload
    {
        [JsonProperty("tan")]
        public string Tan { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("dateRanges")]
        public List<DateRange> DateRanges { get; set; }

        [JsonProperty("maxRecords")]
        public int MaxRecords { get; set; }
    }

    public class StreamResponse
    {
        [JsonProperty("stage")]
        public string Stage { get; set; }

        [JsonProperty("cin")]
        public string Cin { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("section")]
        public string Section { get; set; }

        [JsonProperty("alternateCIN")]
        public string AlternateCIN { get; set; }

        [JsonProperty("totalExtracted")]
        public int TotalExtracted { get; set; }

        [JsonProperty("rangeIndex")]
        public int RangeIndex { get; set; }

        [JsonProperty("result")]
        public JObject Result { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }

    public class ChallanExtractorService
    {
        private readonly HttpClient _httpClient;

        // Events for UI updates
        public event Action<string> OnStatusUpdate;
        public event Action<string> OnProgressUpdate;
        public event Action<ChallanRecord> OnChallanExtracted;
        public event Action<List<ChallanRecord>, JObject> OnExtractionComplete;
        public event Action<string> OnError;

        public ChallanExtractorService()
        {
            _httpClient = new HttpClient();
        }

        public async Task ExtractChallansAsync(string tan, string password, string fromDate, string toDate, int maxRecords = 25)
        {
            var payload = new RequestPayload
            {
                Tan = tan,
                Password = password,
                DateRanges = new List<DateRange>
                {
                    new DateRange { FromDate = fromDate, ToDate = toDate }
                },
                MaxRecords = maxRecords
            };

            var challanRecords = new List<ChallanRecord>();

            try
            {
                var jsonPayload = JsonConvert.SerializeObject(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Post, "http://www.tdsman.com/challan-extract-with-progress")
                {
                    Content = content
                };

                var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"HTTP error! Status: {response.StatusCode}");
                }

                // Read stream with traditional using syntax
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();

                        if (string.IsNullOrWhiteSpace(line) || !line.StartsWith("data: "))
                            continue;

                        try
                        {
                            // Remove "data: " prefix
                            var jsonStr = line.Substring(6);
                            var obj = JsonConvert.DeserializeObject<StreamResponse>(jsonStr);

                            // Handle different stages
                            switch (obj.Stage)
                            {
                                case "grid_reached":
                                    //Console.WriteLine("Login successful!");
                                    //OnStatusUpdate?.Invoke("Logged in - Starting extraction...");
                                    OnStatusUpdate?.Invoke("Challan extraction process started...");
                                    
                                    break;

                                case "challan_extracted":
                                    //Console.WriteLine($"Challan {obj.TotalExtracted} extracted: {obj.Cin}");

                                    var record = new ChallanRecord
                                    {
                                        Cin = obj.Cin,
                                        Amount = obj.Amount,
                                        Section = obj.Section,
                                        AlternateCIN = obj.AlternateCIN
                                    };
                                    // Convert date from DDMMYYYY to dd/MM/yyyy format
                                    string formattedDate = DateTime.ParseExact(obj.AlternateCIN.Substring(7, 8), "ddMMyyyy", null)
                                                                     .ToString("dd/MM/yyyy");

                                    challanRecords.Add(record);

                                    // Update UI in real-time
                                    //OnStatusUpdate?.Invoke($"Extracting... {obj.TotalExtracted} challans found");
                                    //OnProgressUpdate?.Invoke($"Range {obj.RangeIndex}: {obj.TotalExtracted} records");
                                    //$('#lblstatus').text(`Extracting Challan No. ${ obj.totalExtracted}dated: ${ formattedDate} `).css('color', 'green');
                                    OnProgressUpdate?.Invoke($"Extracting Challan No. {obj.TotalExtracted} dated: {formattedDate}...");
                                    OnChallanExtracted?.Invoke(record);
                                    break;

                                case "final":
                                    //Console.WriteLine("Extraction complete!"); 
                                    //--
                                    OnStatusUpdate?.Invoke($"{challanRecords.Count} challans extracted");
                                    OnExtractionComplete?.Invoke(challanRecords, obj.Result);
                                    break;

                                case "error":
                                    //Console.WriteLine($"Error during extraction: {obj.Error}");
                                    OnError?.Invoke(obj.Error);
                                    break;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Failed to parse line: {line}, Error: {e.Message}");
                        }
                    }
                }

                Console.WriteLine($"Final challan records: {challanRecords.Count}");
            }
            catch (Exception error)
            {
                Console.WriteLine($"Fetch error: {error.Message}");
                OnError?.Invoke($"Connection error: {error.Message}");
            }
        }
    }
}