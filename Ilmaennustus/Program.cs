using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
class Program
{
    HttpClient client = new HttpClient();
    static async Task Main(string[] args) 
    {
        Program program = new Program();
        await program.Ilmaennustus();
    }

    private async Task Ilmaennustus()
    {
        client.DefaultRequestHeaders.Add("User-Agent", "Ilmaennustus/1.0 (markjakobo@gmail.com)");
        string response = await client.GetStringAsync(
            "https://api.met.no/weatherapi/locationforecast/2.0/compact?lat=59.4370&lon=24.7536");
        Root root = JsonConvert.DeserializeObject<Root>(response);

        foreach (Timeseries timeseries in root.properties.timeseries)
        {
            Console.WriteLine(timeseries.time + " " +  timeseries.data.instant.details.air_temperature + "C");
        }
    }



    public class Root
    {
        public Properties properties { get; set; }
    }

    public class Properties
    {
        public List<Timeseries> timeseries { get; set; }
    }

    public class Timeseries
    {
        public DateTime time { get; set; }
        public Data data { get; set; }
    }
    public class Data 
    {
         public Instant instant { get; set; }
    }
    public class Instant 
    {
        public Details details { get; set; }

    }

    public class Details 
    {
        public double air_temperature { get; set; }
    }
}