namespace BlazorWebAssembly.Components;

using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class WeatherService
{
    private static readonly HttpClient _httpClient = new HttpClient();

    public async Task<WeatherData> GetWeatherData(string lat, string lon)
    {
        try
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&units=metric&appid=75e73ada7ef377cd1214d34f926bbd45";
            
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var weatherData = JsonSerializer.Deserialize<WeatherData>(jsonResponse);

            return weatherData;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }
    
    public class WeatherData
    // First level JSON keys
    {
        [JsonPropertyName("main")]
        public WeatherMain Main { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("clouds")]
        public WeatherClouds Clouds { get; set; }
        
        [JsonPropertyName("wind")]
        public WeatherWind Wind { get; set; }
        
        [JsonPropertyName("weather")]
        public WeatherArray[] weatherArray { get; set; }
    }

    public class WeatherMain
    {
        [JsonPropertyName("temp")]
        public double Temperature { get; set; }
        
        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
        
        [JsonPropertyName("pressure")]
        public int Pressure { get; set; }
    }
    
    public class WeatherClouds
    {
        [JsonPropertyName("all")]
        public int allClouds { get; set; }
    }
    
    public class WeatherWind
    {
        [JsonPropertyName("speed")]
        public double windSpeed { get; set; }
    }
    
    public class WeatherArray
    {
        [JsonPropertyName("icon")]
        public string weatherIcon { get; set; }
    }
}