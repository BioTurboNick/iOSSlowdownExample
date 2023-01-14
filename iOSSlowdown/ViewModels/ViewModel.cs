using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace iOSSlowdown.ViewModels;

/// <summary>
/// Provides a base class from which all ViewModels must derive, implementing INotifyPropertyChanged support.
/// </summary>
public abstract class ViewModel : INotifyPropertyChanged
{
    #region INotifyPropertyChanged Members
    /// <summary>
    /// Fired when a property on this not null has been modified.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;
    #endregion


    /// <summary>
    /// Raises the PropertyChanged event for the given property name.
    /// </summary>
    /// <param name="property">The name of the property.</param>
    protected void OnPropertyChanged(string property) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

    /// <summary>
    /// Set a property via its backing field and notify of a change.
    /// </summary>
    /// <typeparam name="T">The type of the field.</typeparam>
    /// <param name="field">The field reference.</param>
    /// <param name="value">The value to set.</param>
    /// <param name="onChanged">Any related method to run if the value changed.</param>
    /// <param name="propertyName">The name of the property, populated automatically.</param>
    /// <returns><c>True</c> if a change occurred, otherwise <c>false</c>.</returns>
    protected bool Set<T>(ref T field, T value, Action? onChanged = null, [CallerMemberName] string propertyName = "")
    {
        // note: this structure is confusing, since one might naturally call one with a substitution for the last term
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;

        field = value;
        onChanged?.Invoke();
        OnPropertyChanged(propertyName);

        return true;
    }

    /// <summary>
    /// Sets an underlying property through its getter and setter and notify of a change.
    /// </summary>
    /// <typeparam name="T">the type of the property.</typeparam>
    /// <param name="getter">The underlying property's getter.</param>
    /// <param name="setter">The underlying property's setter.</param>
    /// <param name="value">The value to set.</param>
    /// <param name="onChanged">Any related method to run if the value changed.</param>
    /// <param name="propertyName">The name of the property affected by the change.</param>
    /// <returns><c>True</c> if a change occurred, otherwise <c>false</c>.</returns>
    /// <remarks>
    /// Primary use is to set a property on a Model or Data object from a ViewModel property.
    /// </remarks>
    protected bool Set<T>(Func<T> getter, Action<T> setter, T value, Action? onChanged = null, [CallerMemberName] string propertyName = "")
    {
        if (getter is null) throw new ArgumentNullException(nameof(getter));

        if (EqualityComparer<T>.Default.Equals(getter.Invoke(), value))
            return false;

        setter?.Invoke(value);
        onChanged?.Invoke();
        OnPropertyChanged(propertyName);

        return true;
    }
}
