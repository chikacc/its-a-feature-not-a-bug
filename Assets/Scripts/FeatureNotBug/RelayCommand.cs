using System;
using System.Windows.Input;

namespace FeatureNotBug; 

public sealed class RelayCommand : ICommand {
    readonly Action<object> _execute;
    readonly Predicate<object>? _canExecute;

    public RelayCommand(Action<object> execute, Predicate<object>? canExecute = null) =>
        (_execute, _canExecute) = (execute, canExecute);

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
    public void Execute(object parameter) => _execute(parameter);
}