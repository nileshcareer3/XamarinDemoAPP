﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Acr.UserDialogs;
using ModernHttpClient;
using Newtonsoft.Json;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using RestSharp.Portable.HttpClient.Impl;
using TheMovieHub.Utility;

namespace TheMovieHub.WebServices
{
    public class BaseWebService
    {
        RestClient _restClient;

        public static string BaseUrl = "https://api.themoviedb.org/3/";

        public BaseWebService()
        {
            _restClient = new RestClient(BaseUrl)
            {
                IgnoreResponseStatusCode = true,
                Timeout = TimeSpan.FromMinutes(1),
                HttpClientFactory = new MovieClientFactory()
            };
        }

        public async Task<T> ExecuteGet<T>(RestRequest request, bool isTokenToBeSent, bool isTempTokenToBeSent = false) where T : new()
        {
            request.Method = Method.GET;
            request.AddOrUpdateQueryParameter(Constants.RestRequestsKeys.APIKey, Constants.APIKeyValue);
            request.AddOrUpdateQueryParameter(Constants.RestRequestsKeys.LanguageCode, Constants.LanguageCodeValue);
            if (MovieCommanUtility.IsConnectedToInternet())
            {
                try
                {
                    var response = await _restClient.Execute(request);

                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            {
                                JsonSerializerSettings settings = new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore
                                };
                                return JsonConvert.DeserializeObject<T>(response.Content, settings);
                            }

                        case HttpStatusCode.InternalServerError:
                            {
                                UserDialogs.Instance.Alert(response.StatusDescription + " " + response.StatusCode);
                            }
                            break;

                        case HttpStatusCode.BadRequest:
                            {
                                UserDialogs.Instance.Alert(response.StatusDescription + " " + response.StatusCode);
                            }
                            break;

                        case HttpStatusCode.NotFound:
                            {
                                //TODO: Need to handle
                            }
                            break;
                        case HttpStatusCode.RequestTimeout:
                            {
                                UserDialogs.Instance.Alert(response.StatusDescription + " " + response.StatusCode);
                            }
                            break;

                    }
                }
                catch (WebException ex)
                {
                    UserDialogs.Instance.Alert(ex.Message, null, "OK");
                }
                catch (Exception ex)
                {
                    UserDialogs.Instance.Alert(ex.Message, null, "OK");
                }
            }
            else
            {
                //UserDialogs.Instance.Alert(AppResources.Msg_InternetConnectionNotAvailable, null, AppResources.BtnTitle_Ok);
            }
            return default(T);
        }

        public HttpClient GetHttpClient()
        {
            var client = new HttpClient(new NativeMessageHandler());
            //Add Request Headers
            //client.DefaultRequestHeaders.Add("languageCode", "En-US");
            return client;
        }
    }

    public class MovieClientFactory : DefaultHttpClientFactory
    {
        protected override HttpMessageHandler CreateMessageHandler(IRestClient client)
        {
            return new NativeMessageHandler();
        }
    }
}
