using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovieHub.Views
{
    public class DashBoardMenuItem
    {
        public DashBoardMenuItem()
        {
            TargetType = typeof(DashBoardDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}
