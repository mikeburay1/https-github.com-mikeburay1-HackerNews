using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel;
using System.Globalization;

namespace WebApplication1
{  
    public class HackerNews
    { 
        [JsonProperty(PropertyName = "title")]
        public string? Title { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string? Url { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string? Type { get; set;  }

        [JsonProperty(PropertyName = "time")]
        public int Time { get; set; }
      
        [JsonProperty(PropertyName = "score")]
        public int Score { get; set; }

    }
}
