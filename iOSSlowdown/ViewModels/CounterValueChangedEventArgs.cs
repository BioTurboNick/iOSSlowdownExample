using iOSSlowdown.Enums;

namespace iOSSlowdown.ViewModels;

public class CounterValueChangedEventArgs : EventArgs
{
    public CounterValueChangedEventArgs(CounterValueChangeType changeType, int counterIndex = -1)
    {
        CounterIndex = counterIndex;
        ChangeType = changeType;
    }
    
    /// <summary>
    /// Gets the index of the counter that changed.
    /// </summary>
    public int CounterIndex { get; }

    /// <summary>
    /// Gets the type of change the counter had.
    /// </summary>
    public CounterValueChangeType ChangeType { get; }
}
