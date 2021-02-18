using System.Text.Json.Serialization;

namespace PrimeNumbers.TestApp.Models
{
    internal class Settings
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }
    }
}
