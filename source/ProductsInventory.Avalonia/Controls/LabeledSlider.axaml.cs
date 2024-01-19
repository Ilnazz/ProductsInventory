using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Media;

namespace ProductsInventory.Avalonia.Controls;

[TemplatePart("PART_ValueTextBlock", typeof(TextBlock))]
public class LabeledSlider : Slider
{
    #region Styled properties
    public static readonly StyledProperty<string?> LabelProperty =
        AvaloniaProperty.Register<LabeledSlider, string?>(nameof(Label));

    public string? Label
    {
        get => GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    public static readonly StyledProperty<string?> MeasureUnitProperty =
        AvaloniaProperty.Register<LabeledSlider, string?>(nameof(MeasureUnit));

    public string? MeasureUnit
    {
        get => GetValue(MeasureUnitProperty);
        set => SetValue(MeasureUnitProperty, value);
    }

    public static readonly StyledProperty<IBrush?> ValueForegroundProperty =
        AvaloniaProperty.Register<LabeledSlider, IBrush?>(nameof(ValueForeground));

    public IBrush? ValueForeground
    {
        get => GetValue(ValueForegroundProperty);
        set => SetValue(ValueForegroundProperty, value);
    }

    //public static readonly StyledProperty<IValueConverter?> ValueConverterProperty =
    //    AvaloniaProperty.Register<LabeledSlider, IValueConverter?>(nameof(ValueConverter));
    
    //public IValueConverter? ValueConverter
    //{
    //    get => GetValue(ValueConverterProperty);
    //    set => SetValue(ValueConverterProperty, value);
    //}

    public static readonly StyledProperty<string?> ValueFormatProperty =
        AvaloniaProperty.Register<LabeledSlider, string?>(nameof(ValueFormat));

    public string? ValueFormat
    {
        get => GetValue(ValueFormatProperty);
        set => SetValue(ValueFormatProperty, value);
    }
    #endregion

    #region Direct properties
    public static readonly DirectProperty<LabeledSlider, int> MinimumIntProperty =
        AvaloniaProperty.RegisterDirect<LabeledSlider, int>(nameof(MinimumInt), o => o.MinimumInt, (o, v) => o.MinimumInt = v);

    public int MinimumInt
    {
        get => (int)Minimum;
        set
        {
            if (MinimumInt == value)
                return;

            var oldValue = MinimumInt;
            Minimum = value;
            RaisePropertyChanged(MinimumIntProperty, oldValue, MinimumInt);
        }
    }

    public static readonly DirectProperty<LabeledSlider, int> MaximumIntProperty =
        AvaloniaProperty.RegisterDirect<LabeledSlider, int>(nameof(MaximumInt), o => o.MaximumInt, (o, v) => o.MaximumInt = v);

    public int MaximumInt
    {
        get => (int)Maximum;
        set
        {
            if (MaximumInt == value)
                return;

            var oldValue = MaximumInt;
            Maximum = value;
            RaisePropertyChanged(MaximumIntProperty, oldValue, MaximumInt);
        }
    }

    public static readonly DirectProperty<LabeledSlider, int> SmallChangeIntProperty =
        AvaloniaProperty.RegisterDirect<LabeledSlider, int>(nameof(SmallChangeInt), o => o.SmallChangeInt, (o, v) => o.SmallChangeInt = v);

    public int SmallChangeInt
    {
        get => (int)SmallChange;
        set
        {
            if (SmallChangeInt == value)
                return;

            var oldValue = SmallChangeInt;
            SmallChange = value;
            RaisePropertyChanged(SmallChangeIntProperty, oldValue, SmallChangeInt);
        }
    }

    public static readonly DirectProperty<LabeledSlider, int> LargeChangeIntProperty =
        AvaloniaProperty.RegisterDirect<LabeledSlider, int>(nameof(LargeChangeInt), o => o.LargeChangeInt, (o, v) => o.LargeChangeInt = v);

    public int LargeChangeInt
    {
        get => (int)LargeChange;
        set
        {
            if (LargeChangeInt == value)
                return;

            var oldValue = LargeChangeInt;
            LargeChange = value;
            RaisePropertyChanged(LargeChangeIntProperty, oldValue, LargeChangeInt);
        }
    }

    public static readonly DirectProperty<LabeledSlider, int> ValueIntProperty =
        AvaloniaProperty.RegisterDirect<LabeledSlider, int>
        (
            nameof(ValueInt),
            o => o.ValueInt,
            (o, v) => o.ValueInt = v,
            defaultBindingMode: BindingMode.TwoWay
        );

    public int ValueInt
    {
        get => (int)Value;
        set
        {
            if (ValueInt == value)
                return;

            var oldValue = ValueInt;
            Value = value;

            _skipValueChangedNotification = true;
            RaisePropertyChanged(ValueIntProperty, oldValue, ValueInt);
        }
    }
    #endregion

    #region Fields
    private TextBlock? _valueTextBlock;

    private bool _skipValueChangedNotification;
    #endregion

    #region Methods
    #region Event handlers
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        _valueTextBlock = e.NameScope.Find<TextBlock>("PART_ValueTextBlock")!;
        UpdateValueTextBlockText();
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property != ValueProperty)
            return;
        
        if (_skipValueChangedNotification)
            _skipValueChangedNotification = false;
        else
            RaisePropertyChanged(ValueIntProperty, (int)((double)change.OldValue), (int)((double)change.NewValue));
        
        UpdateValueTextBlockText();
    }
    #endregion

    private void UpdateValueTextBlockText()
    {
        if (_valueTextBlock is not null)
            _valueTextBlock.Text = Value.ToString(ValueFormat);
    }
    #endregion
}