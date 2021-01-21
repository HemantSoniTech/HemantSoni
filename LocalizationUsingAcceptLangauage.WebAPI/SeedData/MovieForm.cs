using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizationUsingAcceptLangauage.WebAPI.SeedData
{
    public class MovieForm
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("releaseDate")]
        public DateTime ReleaseDate { get; set; }

    }
}
