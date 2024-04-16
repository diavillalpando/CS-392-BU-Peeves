using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        List<string> stops = await GetGreenLineBStops();

        Console.WriteLine("Select a direction:");
        Console.WriteLine("1. Inbound");
        Console.WriteLine("2. Outbound");
        Console.Write("Enter your choice (1 or 2): ");
        int directionChoice = int.Parse(Console.ReadLine());
        string direction = directionChoice == 1 ? "0" : "1";

        Console.WriteLine("Select a stop:");
        for (int i = 0; i < stops.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {stops[i]}");
        }
        Console.Write("Enter your choice: ");
        int stopChoice = int.Parse(Console.ReadLine()) - 1;
        string stopId = stops[stopChoice];

        string arrivalTime = await GetNextTrainArrivalTime(stopId, direction);
        Console.WriteLine($"The next train is expected to arrive at {arrivalTime}");
        Console.ReadKey();
    }

    static async Task<List<string>> GetGreenLineBStops()
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync("https://api-v3.mbta.com/stops?filter[route]=Green-B");
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(responseBody));
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(StopResponse));
                StopResponse stopsResponse = (StopResponse)serializer.ReadObject(stream);
                return stopsResponse.Data.Select(stop => stop.Attributes.Name).ToList();
            }
            else
            {
                throw new Exception($"Failed to fetch Green Line B stops. Status code: {response.StatusCode}");
            }
        }
    }

    static async Task<string> GetNextTrainArrivalTime(string stop, string direction)
    {
        using (HttpClient client = new HttpClient())
        {
            var apiKey = "5e809ee23cfb47d9ae73f773da5e4d8e";
            var baseUrl = "https://api-v3.mbta.com/predictions";
            HttpResponseMessage response = await client.GetAsync($"{baseUrl}?filter[route]=Green-B&filter[direction_id]={direction}&filter[stop]={stop}&sort=departure_time&api_key={apiKey}");
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(responseBody));
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(PredictionResponse));
                PredictionResponse predictionsResponse = (PredictionResponse)serializer.ReadObject(stream);
                string st=predictionsResponse.Data.Select(prediction => prediction.Attributes.ArrivalTime).FirstOrDefault();
                return st;
            }
            else
            {
                throw new Exception($"Failed to fetch arrival time for {direction} train at {stop}. Status code: {response.StatusCode}");
            }
        }
    }
}

[DataContract]
public class StopResponse
{
    [DataMember(Name = "data")]
    public List<StopData> Data { get; set; }
}

[DataContract]
public class StopData
{
    [DataMember(Name = "attributes")]
    public StopAttributes Attributes { get; set; }
}

[DataContract]
public class StopAttributes
{
    [DataMember(Name = "name")]
    public string Name { get; set; }
}

[DataContract]
public class PredictionResponse
{
    [DataMember(Name = "data")]
    public List<PredictionData> Data { get; set; }
}

[DataContract]
public class PredictionData
{
    [DataMember(Name = "attributes")]
    public PredictionAttributes Attributes { get; set; }
}

[DataContract]
public class PredictionAttributes
{
    [DataMember(Name = "arrival_time")]
    public string ArrivalTime { get; set; }
}
