using System;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using TheMovieHub.Helper;
using TheMovieHub.Helper.HelperInterface;
using TheMovieHub.ViewModels.MainPageViewModel;
using Xamarin.Forms;

namespace TheMovieHub
{
    public class CoreApp : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<MainDashBoardViewModel>();


            //Mvx.LazyConstructAndRegisterSingleton<IMvxMessenger, MvxMessengerHub>();
            //Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
            Mvx.RegisterSingleton<INavigationParameter>(new NavigationParameter());

          
        }
    }
}

