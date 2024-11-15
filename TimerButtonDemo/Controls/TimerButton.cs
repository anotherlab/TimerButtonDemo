using TimerButtonDemo.Drawables;

namespace TimerButtonDemo.Controls;

public class TimerButton : GraphicsView
{
    private CancellationTokenSource _cancellationTokenSource = new();
    private DateTime _startTime;

    // define the properties
    #region Properties
    public int DelayTime
    {
        get => (int)GetValue(DelayTimeProperty);
        set => SetValue(DelayTimeProperty, value);
    }
    public int SecondsLeft
    {
        get => (int)GetValue(SecondsLeftProperty);
        set => SetValue(SecondsLeftProperty, value);
    }
    public int Offset
    {
        get => (int)GetValue(OffsetProperty);
        set => SetValue(OffsetProperty, value);
    }

    public double Progress
    {
        get => (double)GetValue(ProgressProperty);
        set => SetValue(ProgressProperty, value);
    }

    public Color ButtonColor
    {
        get => (Color)GetValue(ButtonColorProperty);
        set => SetValue(ButtonColorProperty, value);
    }

    public Color ProgressColor
    {
        get => (Color)GetValue(ProgressColorProperty);
        set => SetValue(ProgressColorProperty, value);
    }

    public bool ShowCountdown
    {
        get => (bool)GetValue(ShowCountdownProperty);
        set => SetValue(ShowCountdownProperty, value);
    }
    public bool ColorCycle
    {
        get => (bool)GetValue(ColorCycleProperty);
        set => SetValue(ColorCycleProperty, value);
    }
    public bool AutoFontSize
    {
        get => (bool)GetValue(AutoFontSizeProperty);
        set => SetValue(AutoFontSizeProperty, value);
    }

    public float FontSize
    {
        get => (float)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    public string FontFamily
    {
        get => (string)GetValue(FontFamilyProperty);
        set => SetValue(FontFamilyProperty, value);
    }

    public bool HideWhenDone
    {
        get => (bool)GetValue(HideWhenDoneProperty);
        set => SetValue(HideWhenDoneProperty, value);
    }

    #endregion

    // define the bindable properties
    #region BindableProperties
    private static readonly BindableProperty DelayTimeProperty = BindableProperty.Create(nameof(DelayTime), typeof(int), typeof(TimerButton), 30,
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            if (newValue != null && bindableObject is TimerButton button)
            {
                button.UpdateDelayTime();
            }
        });

    public static readonly BindableProperty ShowCountdownProperty = BindableProperty.Create(nameof(ShowCountdown), typeof(bool), typeof(TimerButton), true,
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            if (newValue != null && bindableObject is TimerButton button)
            {
                button.UpdateShowCountdown();
            }
        });

    private static readonly BindableProperty ColorCycleProperty = BindableProperty.Create(nameof(ColorCycle), typeof(bool), typeof(TimerButton), false,
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            if (newValue != null && bindableObject is TimerButton button)
            {
                button.UpdateColorCycle();
            }
        });

    public static readonly BindableProperty AutoFontSizeProperty = BindableProperty.Create(nameof(AutoFontSize), typeof(bool), typeof(TimerButton), true,
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            if (newValue != null && bindableObject is TimerButton button)
            {
                button.UpdateAutoFontSize();
            }
        });

    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(float), typeof(TimerButton), 18.0f,
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            if (newValue != null && bindableObject is TimerButton button)
            {
                button.UpdateFontSize();
            }
        });

    private static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(TimerButton), null,
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            if (newValue != null && bindableObject is TimerButton button)
            {
                button.UpdateFontFamily();
            }
        });

    private static readonly BindableProperty SecondsLeftProperty = BindableProperty.Create(nameof(SecondsLeft), typeof(int), typeof(TimerButton), 0,
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            if (newValue != null && bindableObject is TimerButton button)
            {
                button.UpdateSecondsLeft();
            }
        });

    private static readonly BindableProperty OffsetProperty = BindableProperty.Create(nameof(Offset), typeof(int), typeof(TimerButton), 0,
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            if (newValue != null && bindableObject is TimerButton button)
            {
                button.UpdateOffset();
            }
        });

    private static readonly BindableProperty ProgressProperty = BindableProperty.Create(nameof(Progress), typeof(double), typeof(TimerButton), 0.0,
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            if (newValue != null && bindableObject is TimerButton button)
            {
                button.UpdateProgress();
            }
        });

    private static readonly BindableProperty ProgressColorProperty = BindableProperty.Create(nameof(ProgressColor), typeof(Color), typeof(TimerButton), Colors.White,
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            if (newValue != null && bindableObject is TimerButton button)
            {
                button.UpdateProgressColor();
            }
        });

    private static readonly BindableProperty ButtonColorProperty = BindableProperty.Create(nameof(ButtonColor), typeof(Color), typeof(TimerButton), Colors.Green,
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            if (newValue != null && bindableObject is TimerButton button)
            {
                button.UpdateButtonColor();
            }
        });
    public static readonly BindableProperty HideWhenDoneProperty = BindableProperty.Create(nameof(HideWhenDone), typeof(bool), typeof(TimerButton), false,
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            if (newValue != null && bindableObject is TimerButton button)
            {
                button.UpdateHideWhenDone();
            }
        });

    #endregion

    // Define the methods to update drawable properties
    #region UI Updates
    
    private bool DrawableIsDefined()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        return (TimerButtonDrawable != null);
    }
    private void UpdateDelayTime()
    {
        // if (TimerButtonDrawable == null)
        //     return;

        if (!DrawableIsDefined()) return;
        
        TimerButtonDrawable.DelayTime = DelayTime;

        Invalidate();
    }

    private void UpdateShowCountdown()
    {
        if (!DrawableIsDefined()) return;

        TimerButtonDrawable.ShowCountdown = ShowCountdown;

        Invalidate();
    }

    private void UpdateColorCycle()
    {
        if (!DrawableIsDefined()) return;

        Invalidate();
    }
    private void UpdateAutoFontSize()
    {
        if (!DrawableIsDefined()) return;

        TimerButtonDrawable.AutoFontSize = AutoFontSize;

        Invalidate();
    }

    private void UpdateFontSize()
    {
        if (!DrawableIsDefined()) return;

        TimerButtonDrawable.FontSize = FontSize;

        Invalidate();
    }

    private void UpdateFontFamily()
    {
        if (!DrawableIsDefined()) return;

        TimerButtonDrawable.FontFamily = FontFamily;

        Invalidate();
    }
    private void UpdateSecondsLeft()
    {
        if (!DrawableIsDefined()) return;

        TimerButtonDrawable.SecondsLeft = SecondsLeft;

        Invalidate();
    }
    private void UpdateOffset()
    {
        if (!DrawableIsDefined()) return;

        TimerButtonDrawable.Offset = Offset;

        Invalidate();
    }
    private void UpdateProgress()
    {
        if (!DrawableIsDefined()) return;

        TimerButtonDrawable.Progress = Progress;

        Invalidate();
    }
    private void UpdateProgressColor()
    {
        if (!DrawableIsDefined()) return;

        TimerButtonDrawable.ProgressColor = ProgressColor;

        Invalidate();
    }

    private void UpdateButtonColor()
    {
        if (!DrawableIsDefined()) return;

        TimerButtonDrawable.ButtonColor = ButtonColor;

        Invalidate();
    }
    private void UpdateHideWhenDone()
    {
        if (!DrawableIsDefined()) return;

        TimerButtonDrawable.HideWhenDone = HideWhenDone;

        Invalidate();
    }
    #endregion


    #region Events

    // Allow the caller to subscribe to a TimerExpired event
    public event EventHandler<EventArgs>? TimerExpired;

    // Allow the caller to subscribe to a TimerTapped event
    public event EventHandler<EventArgs>? TimerTapped;

    #endregion

    // Define the drawing context for this control
    TimerButtonDrawable TimerButtonDrawable { get; set; }

    public TimerButton()
    {
        Drawable = TimerButtonDrawable = new TimerButtonDrawable();

        // Add a tap gesture recognizer to the button to make it quasi-interactive
        GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = new Command(() =>
            {
                // If the TimerTapped event is not null, invoke it
                // Otherwise, stop the timer
                if (TimerTapped != null)
                {
                    TimerTapped.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    StopTimer();
                }
            })
        });

    }

    public async Task StartTimerAsync()
    {
        await UpdateTimer();
    }

    public void StopTimer()
    {
        _cancellationTokenSource.Cancel();
    }

    private async Task UpdateTimer()
    {
        _startTime = DateTime.Now;
        _cancellationTokenSource = new();

        // Run until this loop cancels the token or the token is cancelled elsewhere
        while (!_cancellationTokenSource.Token.IsCancellationRequested)
        {
            TimeSpan elapsedTime = DateTime.Now - _startTime;
            int secondsRemaining = (int)(DelayTime - elapsedTime.TotalSeconds);

            // If times up, cancel the token and raise the TimerExpired event
            if (secondsRemaining <= 0)
            {
                Progress = 0;

                await _cancellationTokenSource.CancelAsync();
                RaiseTimerExpired();
                
                return;
            }

            // convert progress to a value between 0 and 1
            // This will be used to draw the progress as a percentage of 360 degrees

            // Set progress to the remainder of elapsedTime.TotalSecond divided by DelayTime
            var progress = elapsedTime.TotalSeconds % DelayTime;

            // divide by DelayTime to get a value between 0 and 1
            progress /= DelayTime;

            // Update the properties
            Progress = progress;
            SecondsLeft = secondsRemaining;

            // Wait for 100 milliseconds
            await Task.Delay(100);

            if (ColorCycle)
            {
                // Do a color shift by incrementing the hue
                var hue = ButtonColor.GetHue();
                hue += 0.01f;
                hue %= 1.0f;
                ButtonColor = ButtonColor.WithHue(hue);
            }
        }
    }

    /// <summary>
    /// Raise the TimerExpired event, if defined
    /// </summary>
    void RaiseTimerExpired()
    {
        TimerExpired?.Invoke(this, EventArgs.Empty);
    }
}
