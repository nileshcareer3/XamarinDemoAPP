using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Forms.Views;
using TheMovieHub.ViewModels.MainPageViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheMovieHub.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashBoardDetail : MvxContentPage<MainDetailPageDetailsViewModel>
    {
        public DashBoardDetail()
        {
            InitializeComponent();
        }
    }
}
