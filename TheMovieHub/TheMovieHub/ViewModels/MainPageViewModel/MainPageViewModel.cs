using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using MvvmCross.Commands;
using TheMovieHub.Models.MainPageModels;
using TheMovieHub.Utility;
using TheMovieHub.WebServices;
using MvvmCross.ViewModels;
using MvvmCross.Navigation;
using Xamarin.Forms;
using MvvmCross;
using TheMovieHub.Helper.HelperInterface;
using System.Linq;

namespace TheMovieHub.ViewModels.MainPageViewModel
{
    public class MainPageViewModel :BaseViewModel
    {

        private ObservableCollection<MovieDetails> _mainMovieList;
        public ObservableCollection<MovieDetails> MainMovieList
        {
            get { return _mainMovieList; }
            set
            {
                _mainMovieList = value;
                NotifyPropertyChanged();
                RaisePropertyChanged(() => MainMovieList);
            }
        }

        private ObservableCollection<MovieDetails> _movieList;
        public ObservableCollection<MovieDetails> MovieList
        {
            get { return _movieList; }
            set
            {
                _movieList = value;
                NotifyPropertyChanged();
                RaisePropertyChanged(() => MovieList); 
            }
        }


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

       
        private MovieDetails _selectedMovie;
        public MovieDetails SelectedMovie
        {
            get { return _selectedMovie; }
            set
            {
                _selectedMovie = value;
                NotifyPropertyChanged();
                RaisePropertyChanged(() => SelectedMovie);
                SelectMovieCommand?.Execute(SelectedMovie);
            }
        }

        private ObservableCollection<PickerValues> _filterList;
        public ObservableCollection<PickerValues> FilterList
        {
            get { return _filterList; }
            set
            {
                _filterList = value;
                NotifyPropertyChanged();
                RaisePropertyChanged(() => FilterList);

            }
        }

        private PickerValues _selectedFilterType;
        public PickerValues SelectedFilterType
        {
            get { return _selectedFilterType; }
            set
            {
                _selectedFilterType = value;
                NotifyPropertyChanged();
                RaisePropertyChanged(() => SelectedFilterType);
                SelectFilterCommand?.Execute(SelectedFilterType);
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
                SelectGenresCommand?.Execute(SelectedGenres);
            }
        }

        private string _txtSearchTextChanged;
        public string TxtSearchTextChanged
        {
            get { return _txtSearchTextChanged; }
            set
            {
                _txtSearchTextChanged = value;
                NotifyPropertyChanged();
                RaisePropertyChanged(() => TxtSearchTextChanged);
                SearchCommand?.Execute(TxtSearchTextChanged);
            }
        }

        private ICommand _SearchCommand;
        public ICommand SearchCommand
        {
            get
            {
                _SearchCommand = _SearchCommand ?? new MvxCommand<string>(ProcessSearchCommand);
                return _SearchCommand;
            }
        }

       

        private ICommand _selectMovieCommand;
        public ICommand SelectMovieCommand
        {
            get
            {
                _selectMovieCommand = _selectMovieCommand ?? new MvxCommand<MovieDetails>(ProcessSelectMovieCommand);
                return _selectMovieCommand;
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


        private ICommand _selectFilterCommand;
        public ICommand SelectFilterCommand
        {
            get
            {
                _selectFilterCommand = _selectFilterCommand ?? new MvxCommand<PickerValues>(ProcessSelectFilterCommand);
                return _selectFilterCommand;
            }
        }
       

        /// <summary>
        /// Constructor For MovieListViewModel
        /// </summary>
        public MainPageViewModel()
        {
           

            if (MovieCommanUtility.IsConnectedToInternet())
            {
                ProcessToFetchAllGeners().GetAwaiter();
                ProcessToGetMovieList().GetAwaiter();

                var navPar = Mvx.Resolve<INavigationParameter>();
                if (navPar != null)
                {
                    if (navPar.Parameter != null)
                    {
                        if (navPar.Parameter.Item1.Equals(Constants.NavigationParamKeys.SelectedGenres))
                        {
                            SelectedGenres = navPar.Parameter.Item2 as GenresDetails;
                        }
                    }
                }
            }

            InitFilterList();
        }

        private void InitFilterList()
        {

           FilterList = new ObservableCollection<PickerValues>
           {
               new PickerValues { ValueId = 1, ValueName = "High To Low"},
               new PickerValues { ValueId = 2, ValueName = "Low To High"}
           };

        }

        private async Task ProcessToGetMovieList()
        {
            try
            {
                var movieDetailsService = new MovieDetailsService();
                var responce = await movieDetailsService.ProcessToGetMovieListFromServer();
                if (responce != null)
                {
                    if (responce.ListOfMovies != null && responce.ListOfMovies.Count > 0)
                    {
                        MovieList = responce.ListOfMovies;
                        foreach (var item in MovieList)
                        {
                            item.ImagePath = Constants.ImageBaseUrl + item.PosterPath;
                            item.ShowVoteAverage = "IMDb:" + " " + item.VoteAverage;
                           
                        }
                        MainMovieList = MovieList;
                        if(MainMovieList != null)
                        {
                            if(MainMovieList.Count > 0 && SelectedGenres != null)
                            {

                                if (SelectedGenres.GenerId == 0)
                                {
                                    MovieList = MainMovieList;
                                    return;
                                }
                                var List = MainMovieList;
                                var ListSelected = List.Where(movie => movie.GenreIds.Contains(SelectedGenres.GenerId));

                                MovieList = new ObservableCollection<MovieDetails>(ListSelected);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message, "Apologies", "Ok");
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
                        GenresDetails genresDetails = new GenresDetails { GenerId = 0, GenerName ="All" };
                        GenersList.Insert(0, genresDetails);
                    }
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message, "Apologies", "Ok");
            }
        }


        private void ProcessSelectMovieCommand(MovieDetails movieDetails)
        {
            try
            {
                var navPar = Mvx.Resolve<INavigationParameter>();
                navPar.Parameter = new Tuple<string, object>(Constants.NavigationParamKeys.SelectedMovie, movieDetails);
                //ShowViewModel<MainDetailPageDetailsViewModel>();
                Mvx.Resolve<IMvxNavigationService>().Navigate<MainDetailPageDetailsViewModel>();
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message, "Apologies", "Ok");
            }
        }

        private void ProcessSelectFilterCommand(PickerValues selectedType)
        {
            try
            {
                var filterMovieList = MainMovieList;
                if (selectedType.ValueId == 2)
                {

                    var orderByResult = from singleItem in filterMovieList
                                        orderby singleItem.VoteAverage
                                        select singleItem;

                    MovieList = new ObservableCollection<MovieDetails>(orderByResult);


                }
                else if (selectedType.ValueId == 1)
                {
                    var orderByDescendingResult = from singleItem in filterMovieList
                                                  orderby singleItem.VoteAverage descending
                                                  select singleItem;
                    MovieList = new ObservableCollection<MovieDetails>(orderByDescendingResult);
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
                if(selectedGenres.GenerId == 0)
                {
                   MovieList = MainMovieList;
                   return;
                }
                var List = MainMovieList;
                var ListSelected = List.Where(movie => movie.GenreIds.Contains(selectedGenres.GenerId));

                MovieList = new ObservableCollection<MovieDetails>(ListSelected);
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message, "Apologies", "Ok");
            }
           
        }


        private void ProcessSearchCommand(string movieName)
        {
            try
            {
                if(string.IsNullOrEmpty(movieName))
                {
                    MovieList = MainMovieList;
                }
                else
                {
                    ProcessToSearchMovie(movieName).GetAwaiter();
                }
                
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message, "Apologies", "Ok");
            }
        }

        private async Task ProcessToSearchMovie(string searchText)
        {
            try
            {
             
                var movieDetailsService = new MovieDetailsService();
                var responce = await movieDetailsService.ProcessToSearchMovieListFromServer(searchText);
                if (responce != null)
                {
                    if (responce.ListOfMovies != null && responce.ListOfMovies.Count > 0)
                    {
                        MovieList = responce.ListOfMovies;
                     
                    }
                }

            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message, "Apologies", "Ok");
            }
        }
    }
}
