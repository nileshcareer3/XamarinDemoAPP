using System;
using Newtonsoft.Json;

namespace TheMovieHub.Models.MainPageModels
{
    public class GenresDetails
    {
        [JsonProperty("id")]
        public int GenerId { get; set; }

        [JsonProperty("name")]
        public string GenerName { get; set; }
    }
}
