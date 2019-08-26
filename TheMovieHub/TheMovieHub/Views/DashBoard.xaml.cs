using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using TheMovieHub.ViewModels.MainPageViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheMovieHub.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Root, WrapInNavigationPage = false, Title = "MasterDetail Page")]
    public partial class DashBoard : MvxMasterDetailPage<MainDashBoardViewModel>
    {
        public DashBoard()
        {
            InitializeComponent();
            //MasterPage.ListView.ItemSelected += ListView_ItemSelected;
            
        }

    }
}
