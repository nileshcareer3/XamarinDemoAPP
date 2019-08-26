using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using TheMovieHub.Models.MainPageModels;

namespace TheMovieHub.ServiceResponce
{
    public class GenersBaseResponce
    {
        [JsonProperty("genres")]
        public ObservableCollection<GenresDetails> ListOfGeners { get; set; }
    }
}
