using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using TheMovieHub.Helper.HelperInterface;
using TheMovieHub.Models.MainPageModels;
using TheMovieHub.Utility;
using TheMovieHub.WebServices;

namespace TheMovieHub.ViewModels.MainPageViewModel
{
    public class MainDetailPageMasterViewModel : BaseViewModel
    {

        private ObservableCollection<GenresDetails> _genersList;
        public ObservableCollection<GenresDetails> GenersList
        {
            get { return _genersList; }
            set
            {
                _genersList = value;
                NotifyPropertyChanged();
                RaisePropertyChanged(() => GenersList);

            }
        }

        private GenresDetails _selectedGenres;
        public GenresDetails SelectedGenres
        {
            get { return _selectedGenres; }
            set
            {
                _selectedGenres = value;
                NotifyPropertyChanged();
                RaisePropertyChanged(() => SelectedGenres);
                //SelectGenresCommand?.Execute(SelectedGenres);
            }
        }


        private ICommand _selectGenresCommand;
        public ICommand SelectGenresCommand
        {
            get
            {
                _selectGenresCommand = _selectGenresCommand ?? new MvxCommand<GenresDetails>(ProcessSelectGenresCommand);
                return _selectGenresCommand;
            }
        }


        public MainDetailPageMasterViewModel()
        {
            if (MovieCommanUtility.IsConnectedToInternet())
            { 
                ProcessToFetchAllGeners().GetAwaiter();
            }
        }

        private async Task ProcessToFetchAllGeners()
        {
            try
            {
                var movieDetailsService = new MovieDetailsService();
                var responce = await movieDetailsService.ProcessToGetGenersListFromServer();
                if (responce != null)
                {
                    if (responce.ListOfGeners != null && responce.ListOfGeners.Count > 0)
                    {
                        GenersList = responce.ListOfGeners;

                    }
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message, "Apologies", "Ok");
            }
        }

        private void ProcessSelectGenresCommand(GenresDetails selectedGenres)
        {
            try
            {

                try
                {
                    var navPar = Mvx.Resolve<INavigationParameter>();
                    navPar.Parameter = new Tuple<string, object>(Constants.NavigationParamKeys.SelectedGenres, selectedGenres);
                    //ShowViewModel<MainDetailPageDetailsViewModel>();
                    Mvx.Resolve<IMvxNavigationService>().Navigate<MainPageViewModel>();
                }
                catch (Exception ex)
                {
                    UserDialogs.Instance.Alert(ex.Message, "Apologies", "Ok");
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message, "Apologies", "Ok");
            }

        }
    }
}
