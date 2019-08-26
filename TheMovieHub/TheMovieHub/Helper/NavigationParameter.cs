using System;
using System.Collections.Generic;
using TheMovieHub.Helper.HelperInterface;

namespace TheMovieHub.Helper
{
    public class NavigationParameter : INavigationParameter
    {
        public Tuple<string, object> Parameter { get; set; }
        public List<Tuple<string, object>> Parameters { get; set; }
    }
}
