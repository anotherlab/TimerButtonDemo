namespace TimerButtonDemo
{
    public partial class MainPage : ContentPage
    {
        private int _numberOfTaps;

        public MainPage()
        {
            InitializeComponent();

            // As a workaround, we can set the font name. we'll need to update this when the issue is fixed
            // https://github.com/dotnet/maui/issues/9252
            // The licence for this font can be found at https://www.1001fonts.com/baking-sauce-font.html

#if ANDROID
            TimerBtn.FontFamily = "BAKINGSAUCE.otf";
#elif IOS
            TimerBtn.FontFamily = "BakingSauce";
#elif WINDOWS
            TimerBtn.FontFamily = "BAKINGSAUCE.otf#BakingSauce";
#endif

            TimerBtn3.DelayTime = TimerBtn2.DelayTime = TimerBtn.DelayTime;
        }

        private async void OnStartTimerClicked(object sender, EventArgs e)
        {
            TimerLbl.Text = "Timer Started";
            //await TimerBtn.StartTimerAsync();

            // Fire off all three timer buttons
            var tasks = new List<Task>
            {
                TimerBtn.StartTimerAsync(),
                TimerBtn2.StartTimerAsync(),
                TimerBtn3.StartTimerAsync()
            };

            await Task.WhenAll(tasks);
        }

        private void TimerBtn_TimerExpired(object sender, EventArgs e)
        {
            // Make 100% sure we're on the main thread before updating the UI
            MainThread.BeginInvokeOnMainThread(() =>
            {
                TimerLbl.Text = "Timer Expired";
            });
        }

        private void TimerBtn_TimerTapped(object sender, EventArgs e)
        {
            TimerBtn.StopTimer();
            TimerBtn2.StopTimer();
            TimerBtn3.StopTimer();

            TimerLbl.Text = $"Timer Tapped: {++_numberOfTaps}";
        }
    }

}
