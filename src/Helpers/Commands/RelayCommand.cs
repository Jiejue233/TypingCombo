using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TypingCombo.src.Helpers.Commands
{
    public class RelayCommand : CommandBase
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }


        public override bool CanExecute(object? parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            execute(parameter);
        }


    }

}