namespace iOSSlowdown.Commands;

/// <summary>
/// Base class for all commands acting on a target.
/// </summary>
public abstract class Command<T> : Command where T : class
{
    /// <summary>
    /// Create a command acting on a target.
    /// </summary>
    /// <param name="target">The target of this command.</param>
    protected Command(T target) => Target = target;


    /// <summary>
    /// Gets the target of this command.
    /// </summary>
    public T Target { get; }
}
