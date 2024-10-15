namespace TimerButtonDemo.Drawables;

public class TimerButtonDrawable : IDrawable
{
    #region Properties
    // The dimensions of the drawable
    public int Width { get; set; } = 0;
    public int Height { get; set; } = 0;

    // The offset from the edge of the drawable to draw the progress arc
    public int Offset { get; set; } = 5;

    // Used here to measure the maximum size of the text
    public int DelayTime { get; set; } = 30;

    // If true and a custom font is defined, the font size will be calculated to fit the drawable
    public bool AutoFontSize { get; set; } = true;

    // otherwise the font size will be set to the value of FontSize
    public float FontSize { get; set; } = 18.0f;

    // Currently not supported correctly, see https://github.com/dotnet/maui/issues/9252
    // There is a workaround in the MainPage.xaml.cs file
    public string? FontFamily { get; set; }
    public Color ButtonColor { get; set; } = Colors.Blue;
    public Color ProgressColor { get; set; } = Colors.White;

    // The size of the stroke for the progress arc
    public float StrokeSize { get; set; } = 4.0f;

    public int SecondsLeft { get; set; } = 0;
    public double Progress { get; set; } = 0;

    // If true, the drawable will not be drawn when the progress is 0
    public bool HideWhenDone { get; set; } = false;

    // If true, the number of seconds left will be displayed, otherwise an X will be displayed
    public bool ShowCountdown { get; set; } = true;
    #endregion


    #region Helper Methods

    /// <summary>
    /// Used to get the widest value of the delay time
    /// Makes a lot of scary assumptions about the font size of the characters
    /// </summary>
    /// <param name="delay"></param>
    /// <returns>A string of "M" with the number of characters equal to the number of digits of delay</returns>
    private static string GetDelayString(decimal delay)
    {
        var len = Math.Abs(delay).ToString().Length;

        return "MMMMMMMMMMM"[..len];
    }

    /// <summary>
    /// Calculates the font size to fit the drawable area
    /// Loosely based on the code in Draw() from https://github.com/john-hollander/Maui-Tests/blob/master/IDrawableFontTest/DrawableTest.cs
    /// </summary>
    /// <param name="canvas"></param>
    /// <param name="dirtyRect"></param>
    /// <param name="font"></param>
    /// <returns>The font size to fit the area</returns>
    private float CalcFontSize(ICanvas canvas, RectF dirtyRect, Microsoft.Maui.Graphics.Font font)
    {
        var textToDisplay = GetDelayString(DelayTime);

        // Get the size of the text to display using a large font size
        var size = canvas.GetStringSize(textToDisplay, font, 128f,
            HorizontalAlignment.Center, VerticalAlignment.Center);

        // Shrink the rectangle by 20% to give some padding
        RectF rect = dirtyRect.Inflate(-0.2f * dirtyRect.Width, -0.2f * dirtyRect.Height);

        // Calculate the font size to fit the rectangle
        var fontSize = 128f * Math.Min(rect.Width / size.Width, rect.Height / size.Height);

        return fontSize;
    }
    #endregion

    // implement the IDrawable.Draw method
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        // If the progress is 0 and we are hiding when done, then we don't need to draw anything
        if (HideWhenDone && Progress <= 0)
        {
            return;
        }

        // Save the current state of the canvas
        canvas.SaveState();

        // Get the smallest dimension of the drawing area
        var width = Width != 0 ? Width : dirtyRect.Width;
        var height = Height != 0 ? Height : dirtyRect.Height;

        // get the diameter of the circle from the lesser of the width and height
        var diameter = width > height ? height : width;

        // get the center of the circle
        PointF centerOfCircle = new(width / 2, height / 2);

        // Draw the circle
        canvas.FillColor = this.ButtonColor;
        canvas.FillCircle(centerOfCircle, diameter / 2);

        // draw percentage as an arc and number of seconds left
        if (Progress >= 0)
        {
            // Calculate the end angle of the arc
            var endAngle = 90 - (int)Math.Round(Progress * 360, MidpointRounding.AwayFromZero);

            canvas.StrokeColor = ProgressColor;
            canvas.StrokeSize = StrokeSize;
            canvas.StrokeLineCap = LineCap.Round;

            // This code assumes that the width and height are the same
            canvas.DrawArc(Offset, Offset,
                (dirtyRect.Width - (Offset << 1)), (dirtyRect.Height - (Offset << 1)),
                90, endAngle, false, false);

            if (ShowCountdown)
            {
                if (SecondsLeft > 0)
                {
                    canvas.FontColor = ProgressColor;
                    canvas.FontSize = FontSize;
                    try
                    {
                        var textToDisplay = SecondsLeft.ToString();

                        if (FontFamily != null)
                        {
                            var font = new Microsoft.Maui.Graphics.Font(FontFamily);

                            if (AutoFontSize)
                            {
                                canvas.FontSize = CalcFontSize(canvas, dirtyRect, font);
                            }

                            canvas.Font = font;
                        }

                        canvas.DrawString(SecondsLeft.ToString(), dirtyRect,
                            HorizontalAlignment.Center, VerticalAlignment.Center, TextFlow.OverflowBounds);
                    }
                    catch (Exception e)
                    {
                        // Using a custom font on UWP will throw an exception
                        Console.WriteLine(e.Message);
                    }

                }
            }
            else
            {
                // Draw an "X" shape
                float xSize;
                if (AutoFontSize)
                {
                    xSize = width * 0.6f;
                    canvas.StrokeSize = StrokeSize * 2;
                }
                else
                {
                    xSize = width * 0.6f;
                }

                canvas.DrawLine(xSize, dirtyRect.Height - xSize, dirtyRect.Width - xSize, xSize);
                canvas.DrawLine(xSize, xSize, dirtyRect.Width - xSize, dirtyRect.Height - xSize);
            }
        }

        // Restore the canvas to the state it was in before we started drawing
        canvas.RestoreState();
    }
}
