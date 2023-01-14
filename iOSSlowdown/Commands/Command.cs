using System.Windows.Input;

namespace iOSSlowdown.Commands;

/// <summary>
/// Base class for all commands. Executable by default.
/// </summary>
public abstract class Command : ICommand
{
    #region ICommand Members
    /// <summary>
    /// Occurs when changes occur that affect whether or not the command should execute.
    /// </summary>
    public event EventHandler? CanExecuteChanged;


    /// <summary>
    /// Determines whether the command should execute.
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns><c>True</c> if the command should execute; otherwise <c>false</c>.</returns>
    public virtual bool CanExecute(object? parameter) => true;

    /// <summary>
    /// Execute the command.
    /// </summary>
    /// <param name="parameter"></param>
    public abstract void Execute(object? parameter);
    #endregion


    /// <summary>
    /// Raises <see cref="CanExecuteChanged"/>.
    /// </summary>
    protected void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, new EventArgs());
}
