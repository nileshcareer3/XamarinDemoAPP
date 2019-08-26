using System;
using MvvmCross;
using TheMovieHub.Helper.HelperInterface;
using TheMovieHub.Models.MainPageModels;
using TheMovieHub.Utility;

namespace TheMovieHub.ViewModels.MainPageViewModel
{
    public class MainDetailPageDetailsViewModel : BaseViewModel
    {
        private MovieDetails _movieDetail;
        public  MovieDetails MovieDetails
        {
            get { return _movieDetail; }
            set
            {
                _movieDetail = value;
                NotifyPropertyChanged();
                RaisePropertyChanged(() => MovieDetails);
            }
        }

        private string _ImagePath;
        public string ImagePath
        {
            get { return _ImagePath; }
            set
            {
                _ImagePath = value;
                NotifyPropertyChanged();
                RaisePropertyChanged(() => ImagePath);
            }
        }

        private string _title;
        public string MovieTitle
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyPropertyChanged();
                RaisePropertyChanged(() => MovieTitle);
            }
        }

        private string _overview;
        public string Overview
        {
            get { return _overview; }
            set
            {
                _overview = value;
                NotifyPropertyChanged();
                RaisePropertyChanged(() => Overview);
            }

        }

        private string _userRating;
        public string UserRating
        {
            get { return _userRating; }
            set
            {
                _userRating = value;
                NotifyPropertyChanged();
                RaisePropertyChanged(() => UserRating);
            }

        }

        private string _releaseDate;
        public string ReleaseDate
        {
            get { return _releaseDate; }
            set
            {
                _releaseDate = value;
                NotifyPropertyChanged();
                RaisePropertyChanged(() => ReleaseDate);
            }

        }
        

        public MainDetailPageDetailsViewModel()
        {

            var navPar = Mvx.Resolve<INavigationParameter>();
            if (navPar != null)
            {
                if (navPar.Parameter != null)
                {
                    if (navPar.Parameter.Item1.Equals(Constants.NavigationParamKeys.SelectedMovie))
                    {
                        MovieDetails = navPar.Parameter.Item2 as MovieDetails;
                        ImagePath = MovieDetails.ImagePath;
                        Overview = MovieDetails.Overview;
                        MovieTitle = MovieDetails.MovieTitle;
                        UserRating = MovieDetails.ShowVoteAverage;
                        ReleaseDate = "Release Date : " +  MovieDetails.ReleaseDate.ToShortDateString();
                    }
                }
            }

        }
    }
}
