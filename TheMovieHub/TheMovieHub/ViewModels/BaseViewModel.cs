using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MvvmCross.ViewModels;

namespace TheMovieHub.ViewModels
{
    public class BaseViewModel : MvxViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override Task Initialize()
        {
            //TODO: Add starting logic here

            return base.Initialize();
        }

        public BaseViewModel()
        {
        }

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

