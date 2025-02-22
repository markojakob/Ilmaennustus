using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Program
{
    static async Task Main(string[] args)
    {
        await Ilmaennustus();
    }

    private static async Task Ilmaennustus()
    {
        using HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", "Ilmaennustus/1.0 (markjakobo@gmail.com)");

        string response = await client.GetStringAsync(
            "https://api.met.no/weatherapi/locationforecast/2.0/compact?lat=59.4370&lon=24.7536");

        // Deserialize into a dynamic object
        dynamic json = JsonConvert.DeserializeObject(response);

        // Loop through timeseries and print air temperature
        foreach (var timeseries in json.properties.timeseries)
        {
            Console.WriteLine($"{timeseries.time} {timeseries.data.instant.details.air_temperature}C");
        }
    }
}
