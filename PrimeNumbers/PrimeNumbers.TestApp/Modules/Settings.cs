using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PrimeNumbers.TestApp.Modules
{
    internal class Settings
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }
    }
}
