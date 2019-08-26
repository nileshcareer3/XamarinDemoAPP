using System;
using System.Collections.Generic;

namespace TheMovieHub.Helper.HelperInterface
{
    public interface INavigationParameter
    {
        Tuple<string, object> Parameter { get; set; }
        List<Tuple<string, object>> Parameters { get; set; }
    }
}
