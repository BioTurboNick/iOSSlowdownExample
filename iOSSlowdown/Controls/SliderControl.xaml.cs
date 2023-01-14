using CommunityToolkit.Maui.Core.Extensions;
using System.Windows.Input;

namespace iOSSlowdown.Controls;

/// <summary>
/// Defines a control with a command when tapped and when a slider is moved into position.
/// </summary>
public partial class SliderControl : Grid
{
    public static readonly BindableProperty HandleSizeFractionProperty = BindableProperty.Create(nameof(HandleSizeFraction), typeof(double), typeof(SliderControl), 0.85);

    public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(SliderControl), Colors.Black);

    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(SliderControl), Colors.White);

    public static readonly BindableProperty IsHandlePlusMarkVisibleProperty = BindableProperty.Create(nameof(IsHandlePlusMarkVisible), typeof(bool), typeof(SliderControl), false);

    public static readonly BindableProperty IsSliderMinusMarkVisibleProperty = BindableProperty.Create(nameof(IsSliderMinusMarkVisible), typeof(bool), typeof(SliderControl), false);

    public static readonly BindableProperty IsMiniCounterVisibleProperty = BindableProperty.Create(nameof(IsMiniCounterVisible), typeof(bool), typeof(SliderControl), false);

    public static readonly BindableProperty HandleImageSourceProperty = BindableProperty.Create(nameof(HandleImageSource), typeof(string), typeof(SliderControl), null);

    public static readonly BindableProperty SlideImageSourceProperty = BindableProperty.Create(nameof(SlideImageSource), typeof(string), typeof(SliderControl), null);

    public static readonly BindableProperty SlideSideVerticalImageSourceProperty = BindableProperty.Create(nameof(SlideSideVerticalImageSource), typeof(string), typeof(SliderControl), null);

    public static readonly BindableProperty SlideSideHorizontalImageSourceProperty = BindableProperty.Create(nameof(SlideSideHorizontalImageSource), typeof(string), typeof(SliderControl), null);

    public static readonly BindableProperty IsSlideSideEnabledProperty = BindableProperty.Create(nameof(IsSlideSideEnabled), typeof(bool), typeof(SliderControl), false);

    static readonly BindablePropertyKey IsHorizontalLayoutPropertyKey = BindableProperty.CreateReadOnly(nameof(IsHorizontalLayout), typeof(bool), typeof(SliderControl), false);

    public static readonly BindableProperty IsHorizontalLayoutProperty = IsHorizontalLayoutPropertyKey.BindableProperty;

    public static readonly BindableProperty TappedCommandProperty = BindableProperty.Create(nameof(TappedCommand), typeof(ICommand), typeof(SliderControl), null);
    
    public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(nameof(LabelText), typeof(string), typeof(SliderControl), string.Empty);

    public static readonly BindableProperty CountValueProperty = BindableProperty.Create(nameof(CountValue), typeof(int?), typeof(SliderControl), null);



    /// <summary>
    /// Initializes a new instance of SliderControl.
    /// </summary>
    public SliderControl()
    {
        InitializeComponent();
    }



    /// <summary>
    /// Gets or sets the fractional size the handle should take up when the side channel is used.
    /// Also used to limit the size if it would otherwise be square.
    /// </summary>
    public double HandleSizeFraction
    {
        get => (double)GetValue(HandleSizeFractionProperty);
        set => SetValue(HandleSizeFractionProperty, Math.Min(1, Math.Max(0, value)));
    }

    public Color Color
    {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public bool IsHandlePlusMarkVisible
    {
        get => (bool)GetValue(IsHandlePlusMarkVisibleProperty);
        set => SetValue(IsHandlePlusMarkVisibleProperty, value);
    }

    public bool IsSliderMinusMarkVisible
    {
        get => (bool)GetValue(IsSliderMinusMarkVisibleProperty);
        set => SetValue(IsSliderMinusMarkVisibleProperty, value);
    }

    public bool IsMiniCounterVisible
    {
        get => (bool)GetValue(IsMiniCounterVisibleProperty);
        set => SetValue(IsMiniCounterVisibleProperty, value);
    }

    public string HandleImageSource
    {
        get => (string)GetValue(HandleImageSourceProperty);
        set => SetValue(HandleImageSourceProperty, value);
    }

    public string SlideImageSource
    {
        get => (string)GetValue(SlideImageSourceProperty);
        set => SetValue(SlideImageSourceProperty, value);
    }

    public string SlideSideVerticalImageSource
    {
        get => (string)GetValue(SlideSideVerticalImageSourceProperty);
        set => SetValue(SlideSideVerticalImageSourceProperty, value);
    }

    public string SlideSideHorizontalImageSource
    {
        get => (string)GetValue(SlideSideHorizontalImageSourceProperty);
        set => SetValue(SlideSideHorizontalImageSourceProperty, value);
    }

    public bool IsSlideSideEnabled
    {
        get => (bool)GetValue(IsSlideSideEnabledProperty);
        set => SetValue(IsSlideSideEnabledProperty, value);
    }

    /// <summary>
    /// Gets whether this not null is in a horizontal layout (wider than tall).
    /// </summary>
    public bool IsHorizontalLayout
    {
        get => (bool)GetValue(IsHorizontalLayoutProperty);
        private set => SetValue(IsHorizontalLayoutPropertyKey, value);
    }

    /// <summary>
    /// Gets or sets the command to fire when the handle is tapped.
    /// </summary>
    public ICommand TappedCommand
    {
        get => (ICommand)GetValue(TappedCommandProperty);
        set => SetValue(TappedCommandProperty, value);
    }
   

    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    public int? CountValue
    {
        get => (int?)GetValue(CountValueProperty);
        set => SetValue(CountValueProperty, value);
    }

    void Tapped(object sender, TappedEventArgs e)
    {
        TappedCommand?.Execute(null);
    }


    protected override void OnSizeAllocated(double width, double height)
    {
        if (width == 0 || height == 0)
            return;

        IsHorizontalLayout = width > height;

        double length = Math.Max(width, height);
        double breadth = Math.Min(width, height);

        double sizeRatio = length == 0 ? 0 : breadth / length;

        double handleChannelSize = (IsSlideSideEnabled ? HandleSizeFraction : 1);
        double handleOffset = 1 - handleChannelSize;

        double handleSize = handleChannelSize * sizeRatio;
        if (sizeRatio > HandleSizeFraction)
            handleSize = HandleSizeFraction;

        double sideChannelLongSize = handleSize;
        double sideChannelOffset = 0.7;
        double sideChannelShortSize = 0.5;


        Rect handleRect, handleChannelRect, sideChannelBackgroundRect, sideChannelRect, exposedChannelRect;
        if (IsHorizontalLayout)
        {
            handleRect = new Rect(0, 0, handleSize, 1);
            handleChannelRect = new Rect(0, 0, 1, handleChannelSize);
            sideChannelBackgroundRect = new Rect(sideChannelOffset, 1, sideChannelLongSize, sideChannelShortSize);
            sideChannelRect = new Rect(sideChannelOffset, 1, sideChannelLongSize, handleOffset);
            exposedChannelRect = new Rect(1, 0, Math.Min(1 - sideChannelLongSize, 0.5), 1);
        }
        else
        {
            handleRect = new Rect(0, 0, 1, handleSize);
            handleChannelRect = new Rect(1, 0, handleChannelSize, 1);
            sideChannelBackgroundRect = new Rect(0, sideChannelOffset, sideChannelShortSize, sideChannelLongSize);
            sideChannelRect = new Rect(0, sideChannelOffset, handleOffset, sideChannelLongSize);
            exposedChannelRect = new Rect(1, 1, 1, Math.Min(1 - sideChannelLongSize, 0.5));
        }

        AbsoluteLayout.SetLayoutBounds(handleChannelAreaBackground, handleChannelRect);
        AbsoluteLayout.SetLayoutBounds(handleChannelArea, handleChannelRect);
        AbsoluteLayout.SetLayoutBounds(handle, handleRect);
        AbsoluteLayout.SetLayoutBounds(exposedSliderPanel, exposedChannelRect);
        AbsoluteLayout.SetLayoutBounds(sideChannelAreaBackground, sideChannelBackgroundRect);
        AbsoluteLayout.SetLayoutBounds(sideChannelArea, sideChannelRect);

        base.OnSizeAllocated(width, height);
    }

}