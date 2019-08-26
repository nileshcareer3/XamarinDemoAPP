using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Plugin.Connectivity;

namespace TheMovieHub.Utility
{
    public enum DeviceTypeEnum
    {
        Android = 0,
        iOS = 1
    }

    public static class MovieCommanUtility
    {
        private static bool isVisible = false;

        public static bool IsConnectedToInternet()
        {
            return CrossConnectivity.Current.IsConnected;
        }

        public async static Task UnAuthorisedPopUp(object sender, string unAuthMsg, bool isFromResend = false)
        {

            await Task.Delay(100);
            UserDialogs.Instance.Alert(new AlertConfig
            {
                Message = "UnAuthorized User",
                OkText = "OK"

            });


        }

    }
}
