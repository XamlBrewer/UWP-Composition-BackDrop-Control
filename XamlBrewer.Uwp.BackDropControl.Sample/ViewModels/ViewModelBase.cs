using System.Collections.ObjectModel;

namespace Mvvm
{
    class ViewModelBase : BindableBase
    {
        private static readonly ObservableCollection<MenuItem> AppMenu = new ObservableCollection<MenuItem>();

        public ViewModelBase()
        {}

        public ObservableCollection<MenuItem> Menu => AppMenu;
    }
}
