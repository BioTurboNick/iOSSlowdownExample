using iOSSlowdown.Enums;
using System.Windows.Input;

namespace iOSSlowdown.ViewModels;

/// <summary>
/// Represents a counter.
/// </summary>
public class CounterViewModel : ViewModel
{
    int? _Value;
    int? _BaseValue;

    /// <summary>
    /// Instantiate the counter.
    /// </summary>
    public CounterViewModel(int? initialValue = null)
    {
        _Value = initialValue;
        Increment = new IncrementCommand(this);
    }


    /// <summary>
    /// Fired when a counter in this square is changed.
    /// </summary>
    public event EventHandler<CounterValueChangedEventArgs>? CounterValueChanged;


    /// <summary>
    /// Gets or sets the value of the counter.
    /// </summary>
    public int? Value
    {
        get => _Value;
        private set => Set(ref _Value, value);
    }

    /// <summary>
    /// Gets or sets the value that the counter will reset to.
    /// </summary>
    public int? BaseValue
    {
        get => _BaseValue;
        set
        {
            Set(ref _BaseValue, value, OnChanged);

            void OnChanged()
            {
                if (_BaseValue.HasValue && (!_Value.HasValue || _Value < _BaseValue))
                    Value = _BaseValue;

                CounterValueChanged?.Invoke(this, new CounterValueChangedEventArgs(CounterValueChangeType.BaseChange));
            }
        }
    }

    /// <summary>
    /// Gets the command to set the counter.
    /// </summary>
    public ICommand Set { get; }

    /// <summary>
    /// Gets the command to increment the counter.
    /// </summary>
    public ICommand Increment { get; }

    /// <summary>
    /// Gets the command to decrement the counter.
    /// </summary>
    public ICommand Decrement { get; }

    /// <summary>
    /// Gets the command to reset the counter.
    /// </summary>
    public ICommand Reset { get; }


    /// <summary>
    /// Coerces the Value to be set to BaseValue
    /// </summary>
    public void CoerceBaseValue()
    {
        Value = BaseValue;
        CounterValueChanged?.Invoke(this, new CounterValueChangedEventArgs(CounterValueChangeType.BaseChange));
    }


    class IncrementCommand : Commands.Command<CounterViewModel>
    {
        public IncrementCommand(CounterViewModel target) : base(target) { }

        public override bool CanExecute(object? parameter) => !Target._Value.HasValue || Target._Value < int.MaxValue;

        public override void Execute(object? parameter)
        {
            if (!CanExecute(parameter)) return;

            if (Target._Value == null)
                Target.Value = 1;
            else
                Target.Value++;

            OnCanExecuteChanged();
            Target.CounterValueChanged?.Invoke(Target, new CounterValueChangedEventArgs(CounterValueChangeType.Increment));
        }
    }
}
