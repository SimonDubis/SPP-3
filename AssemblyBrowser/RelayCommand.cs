using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AssemblyBrowser
{
    public class RelayCommand : ICommand //Для взаимодействия пользователя и приложения в MVVM используются команды
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged //вызывается при изменении условий, указывающий, может ли команда выполняться.
                                                    //Для этого используется событие CommandManager.RequerySuggested
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter) //определяет, может ли команда выполняться
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter) //собственно выполняет логику команды
        {
            this.execute(parameter);
        }
    }
}
