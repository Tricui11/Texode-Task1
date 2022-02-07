using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InfoCards.ClientApp.ViewModels.Helpers {

  public class ClientCommand : ICommand {
    #region Fields 
    readonly Action<object> _execute;
    readonly Predicate<object> _canExecute;
    #endregion // Fields 
    #region Constructors 
    public ClientCommand(Action<object> execute) : this(execute, null) { }
    public ClientCommand(Action<object> execute, Predicate<object> canExecute) {
      if (execute == null)
        throw new ArgumentNullException("execute");
      _execute = execute; _canExecute = canExecute;
    }

    public ClientCommand(Action execute, Func<bool> canExecute = null)
        : this(obj => execute(), obj => canExecute?.Invoke() ?? true) {
    }

    #endregion // Constructors 
    #region ICommand Members 
    [DebuggerStepThrough]
    public bool CanExecute(object parameter) {
      return _canExecute == null ? true : _canExecute(parameter);
    }
    public event EventHandler CanExecuteChanged {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    }
    public virtual void Execute(object parameter) { _execute(parameter); }
    #endregion // ICommand Members 
  }

  public class ClientCommand<T> : ICommand {
    private readonly Action<T> _execute;
    private readonly Predicate<T> _canExecute;

    public ClientCommand(Action<T> execute, Predicate<T> canExecute = null) {
      _execute = execute;
      _canExecute = canExecute;
    }

    [DebuggerStepThrough]
    public bool CanExecute(object parameter) {
      if (_canExecute == null) {
        return true;
      }

      if (parameter == null && typeof(T).IsValueType) {
        return _canExecute.Invoke(default);
      }

      return _canExecute.Invoke((T)parameter);
    }

    public void Execute(object parameter) {
      _execute.Invoke((T)parameter);
    }

    public event EventHandler CanExecuteChanged {
      add => CommandManager.RequerySuggested += value;
      remove => CommandManager.RequerySuggested -= value;
    }
  }
  public class CatchingClientCommand : ClientCommand {
    public CatchingClientCommand(Action<object> execute) : base(execute) { }
    public CatchingClientCommand(Action<object> execute, Predicate<object> canExecute) : base(execute, canExecute) { }

    public override void Execute(object parameter) {
      try {
        base.Execute(parameter);
      }
      catch (Exception e) {
        MessageBox.Show($"Ошибка: {e.Message}");
      }

    }
  }

  public class AsyncRelayCommand : ICommand {
    protected readonly Predicate<object> _canExecute;
    protected Func<object, Task> _asyncExecute;

    public event EventHandler CanExecuteChanged {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    }

    public AsyncRelayCommand(Func<object, Task> execute)
        : this(execute, null) {
    }

    public AsyncRelayCommand(Func<Task> execute, Func<bool> canExecute = null)
        : this(obj => execute(), obj => canExecute?.Invoke() ?? true) {
    }

    public AsyncRelayCommand(Func<object, Task> asyncExecute,
        Predicate<object> canExecute) {
      _asyncExecute = asyncExecute;
      _canExecute = canExecute;
    }

    public AsyncRelayCommand(Func<Task> execute, Predicate<object> canExecute)
        : this(o => execute(), canExecute) { }

    public bool CanExecute(object parameter) {
      if (_canExecute == null) {
        return true;
      }

      return _canExecute(parameter);
    }

    public async void Execute(object parameter) {
      await ExecuteAsync(parameter);
    }

    protected virtual async Task ExecuteAsync(object parameter) {
      await _asyncExecute(parameter);
    }
  }

  public class AsyncRelayCommand<T> : ICommand {
    private readonly Predicate<T> _canExecute;
    private readonly Func<T, Task> _asyncExecute;

    public event EventHandler CanExecuteChanged {
      add => CommandManager.RequerySuggested += value;
      remove => CommandManager.RequerySuggested -= value;
    }

    public AsyncRelayCommand(Func<T, Task> asyncExecute,
        Predicate<T> canExecute = null) {
      _asyncExecute = asyncExecute;
      _canExecute = canExecute;
    }

    public bool CanExecute(object parameter) {
      if (_canExecute == null) {
        return true;
      }

      if (parameter == null && typeof(T).IsValueType) {
        return _canExecute.Invoke(default);
      }

      return _canExecute.Invoke((T)parameter);
    }

    public async void Execute(object parameter) {
      await _asyncExecute((T)parameter);
    }
  }

  public class CatchingAsyncRelayCommand : AsyncRelayCommand {
    public CatchingAsyncRelayCommand(Func<object, Task> execute) : base(execute) { }
    public CatchingAsyncRelayCommand(Func<object, Task> asyncExecute, Predicate<object> canExecute) : base(asyncExecute, canExecute) { }

    protected override async Task ExecuteAsync(object parameter) {
      try {
        await base.ExecuteAsync(parameter);
      }
      catch (Exception e) {
        MessageBox.Show($"Ошибка: {e.Message}");
      }

    }
  }
}
