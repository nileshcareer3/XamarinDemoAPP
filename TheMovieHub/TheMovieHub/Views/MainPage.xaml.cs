using System;
using System.Collections.Generic;
using MvvmCross.Forms.Views;
using TheMovieHub.ViewModels.MainPageViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheMovieHub.Views
{
  
    public partial class MainPage : MvxContentPage<MainPageViewModel>
    {
        public MainPage()
        {
            InitializeComponent();
           
        }
    }
}
