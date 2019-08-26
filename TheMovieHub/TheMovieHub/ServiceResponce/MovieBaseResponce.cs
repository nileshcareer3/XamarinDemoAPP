using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using TheMovieHub.Models.MainPageModels;

namespace TheMovieHub.ServiceResponce
{
    public class MovieBaseResponce
    {
        [JsonProperty("page")]
        public int PageNo { get; set; }

        [JsonProperty("total_results")]
        public int TotalResults { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty("results")]
        public ObservableCollection<MovieDetails> ListOfMovies { get; set; }
    }
}
