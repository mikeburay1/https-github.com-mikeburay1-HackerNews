using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel;
using System.Globalization;

namespace WebApplication1
{
    public class HackerNewsConverter : CustomCreationConverter<HackerNews>
    {
        public override HackerNews Create(Type objectType)
        {
            HackerNews Hacker = new HackerNews();
          //  Hacker.Time = 99999;

            return Hacker;
        }
    }

    public class HackerNews
    {
        //   private int test;
        private string _time;

        [JsonProperty(PropertyName = "title")]
        public string? Title { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string? Url { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string? Type { get; set;  }

        [JsonProperty(PropertyName = "time")]
        public string Time { get => _time;
            set {
                DateTime convertedDate;
                int i = 20140820;
                convertedDate = DateTime.Parse(i.ToString());
                _time = convertedDate.ToShortDateString();
            }  
        } 

        [JsonProperty(PropertyName = "score")]
        public int Score { get; set; }

    }
}
