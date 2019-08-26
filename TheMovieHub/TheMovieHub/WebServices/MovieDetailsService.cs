using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using RestSharp.Portable;
using TheMovieHub.ServiceResponce;
using TheMovieHub.Utility;

namespace TheMovieHub.WebServices
{
    public class MovieDetailsService : BaseWebService
    {
        public async Task<MovieBaseResponce> ProcessToGetMovieListFromServer()
        {
            try
            {
                var request = new RestRequest(Constants.RestAPI.Movie);

                var response = await ExecuteGet<MovieBaseResponce>(request, true);

                if (response != null)
                {

                    return response;

                }
                return response;
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message, null, "OK");
                return null;
            }
        }

        public async Task<MovieBaseResponce> ProcessToSearchMovieListFromServer(string SearchText)
        {
            try
            {
                var request = new RestRequest(Constants.RestAPI.Search);
                request.AddOrUpdateQueryParameter(Constants.RestRequestsKeys.Query, SearchText);

                var response = await ExecuteGet<MovieBaseResponce>(request, true);

                if (response != null)
                {

                    return response;

                }
                return response;
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message, null, "OK");
                return null;
            }
        }

        public async Task<GenersBaseResponce> ProcessToGetGenersListFromServer()
        {

            try
            {
                var request = new RestRequest(Constants.RestAPI.Genres);

                var response = await ExecuteGet<GenersBaseResponce>(request, true);

                if (response != null)
                {

                    return response;

                }
                return response;
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message, null, "OK");
                return null;
            }
        }
    }
}
