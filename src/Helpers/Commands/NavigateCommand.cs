using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using TypingCombo.src.ViewModels;

namespace TypingCombo.src.Helpers.Commands
{
    public class NavigateCommand<TViewModel> : CommandBase
        where TViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createNewView;

        public NavigateCommand(NavigationStore navigationService, Func<TViewModel> createNewView)
        {
            _navigationStore = navigationService;
            _createNewView = createNewView;
        }

        public override void Execute(object? parameter)
        {
            _navigationStore.CurrentViewModel =  _createNewView();
        }
    }
}
