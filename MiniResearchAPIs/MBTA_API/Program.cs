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
        // get the list of Green Line B stops
        List<string> stops = await GetGreenLineBStops();

        // prompt the user to select a direction
        Console.WriteLine("Select a direction:");
        Console.WriteLine("1. Inbound");
        Console.WriteLine("2. Outbound");
        Console.Write("Enter your choice (1 or 2): ");
        int directionChoice = int.Parse(Console.ReadLine());
        string direction = directionChoice == 1 ? "0" : "1";

        // prompt the user to select a stop
        Console.WriteLine("Select a stop:");
        for (int i = 0; i < stops.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {stops[i]}");
        }
        Console.Write("Enter your choice: ");
        int stopChoice = int.Parse(Console.ReadLine()) - 1;
        string stopId = stops[stopChoice];

        // get the next train arrival time and status
        string arrivalTime = await GetNextTrainArrivalTime(stopId, direction);
        string status = await GetNextTrainStatus(stopId, direction);
        Console.WriteLine($"The next train is expected to arrive at {arrivalTime} status : {status}");
        Console.ReadKey();
    }

    // method to retrieve the list of Green Line B stops
    static async Task<List<string>> GetGreenLineBStops()
    {
        using (HttpClient client = new HttpClient())
        {
            // send a GET request to the MBTA API to fetch Green Line B stops
            HttpResponseMessage response = await client.GetAsync("https://api-v3.mbta.com/stops?filter[route]=Green-B");
            if (response.IsSuccessStatusCode)
            {
                // read the response body as a string
                string responseBody = await response.Content.ReadAsStringAsync();
                // deserialize the JSON response
                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(responseBody));
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(StopResponse));
                StopResponse stopsResponse = (StopResponse)serializer.ReadObject(stream);
                // extract stop names from the response data
                return stopsResponse.Data.Select(stop => stop.Attributes.Name).ToList();
            }
            else
            {
                // throw an exception if fetching stops fails
                throw new Exception($"Failed to fetch Green Line B stops. Status code: {response.StatusCode}");
            }
        }
    }

    // method to get the next train arrival time for a specific stop and direction
    static async Task<string> GetNextTrainArrivalTime(string stop, string direction)
    {
        using (HttpClient client = new HttpClient())
        {
            // your MBTA API key
            var apiKey = "5e809ee23cfb47d9ae73f773da5e4d8e";
            var baseUrl = "https://api-v3.mbta.com/predictions";
            // send a GET request to the MBTA API to fetch predictions for the next train
            HttpResponseMessage response = await client.GetAsync($"{baseUrl}?filter[route]=Green-B&filter[direction_id]={direction}&filter[stop]={stop}&sort=departure_time&api_key={apiKey}");
            if (response.IsSuccessStatusCode)
            {
                // read the response body as a string
                string responseBody = await response.Content.ReadAsStringAsync();
                // deserialize the JSON response
                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(responseBody));
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(PredictionResponse));
                PredictionResponse predictionsResponse = (PredictionResponse)serializer.ReadObject(stream);
                // extract arrival time from the response data
                string arrivalTime = predictionsResponse.Data.Select(prediction => prediction.Attributes.ArrivalTime).FirstOrDefault();
                return arrivalTime;
            }
            else
            {
                // throw an exception if fetching arrival time fails
                throw new Exception($"Failed to fetch arrival time for {direction} train at {stop}. Status code: {response.StatusCode}");
            }
        }
    }

    // method to get the status of the next train for a specific stop and direction
    static async Task<string> GetNextTrainStatus(string stop, string direction)
    {
        using (HttpClient client = new HttpClient())
        {
            // your MBTA API key
            var apiKey = "5e809ee23cfb47d9ae73f773da5e4d8e";
            var baseUrl = "https://api-v3.mbta.com/predictions";
            // send a GET request to the MBTA API to fetch predictions for the next train
            HttpResponseMessage response = await client.GetAsync($"{baseUrl}?filter[route]=Green-B&filter[direction_id]={direction}&filter[stop]={stop}&sort=departure_time&api_key={apiKey}");
            if (response.IsSuccessStatusCode)
            {
                // read the response body as a string
                string responseBody = await response.Content.ReadAsStringAsync();
                // deserialize the JSON response
                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(responseBody));
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(StatusResponse));
                StatusResponse statusResponse = (StatusResponse)serializer.ReadObject(stream);
                // extract train status from the response data
                string status = statusResponse.Data.Select(statusData => statusData.Attributes.Status).FirstOrDefault();
                if (status != null)
                {
                    return status;
                }
                return "Status not updated";
            }
            else
            {
                // throw an exception if fetching train status fails
                throw new Exception($"Failed to fetch arrival time for {direction} train at {stop}. Status code: {response.StatusCode}");
            }
        }
    }
}

// data contract classes for deserializing JSON responses
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

[DataContract]
public class StatusResponse
{
    [DataMember(Name = "data")]
    public List<StatusData> Data { get; set; }
}

[DataContract]
public class StatusData
{
    [DataMember(Name = "attributes")]
    public StatusAttributes Attributes { get; set; }
}

[DataContract]
public class StatusAttributes
{
    [DataMember(Name = "status")]
    public string Status { get; set; }
}
