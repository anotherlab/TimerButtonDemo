namespace TimerButtonDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            // As a workaround, we can set the font name. we'll need to update this when the issue is fixed
//#if ANDROID
//            TimerBtn.FontFamily = "BAKINGSAUCE.otf";
//#elif IOS || MACCATALYST
//            TimerBtn.FontFamily = "BakingSauce";
//#elif WINDOWS
//            // This doesn't work on UWP
//            //TimerBtn.FontFamily = "BAKINGSAUCE.otf#BakingSauce";
//#endif

#if ANDROID
            TimerBtn.FontFamily = "SHOWG.TTF";
#elif IOS || MACCATALYST
            TimerBtn.FontFamily = "Showcard Gothic";
#elif WINDOWS
            // This doesn't work on UWP
            TimerBtn.FontFamily = "SHOWG.TTF#Showcard Gothic";
#endif


            TimerBtn2.DelayTime = TimerBtn.DelayTime;
            TimerBtn3.DelayTime = TimerBtn.DelayTime;
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            TimerLbl.Text = "Timer Started";
            //await TimerBtn.StartTimerAsync();

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
            TimerLbl.Text = "Timer Expired";
        }

        private void TimerBtn_TimerTapped(object sender, EventArgs e)
        {
            TimerBtn.StopTimer();
            TimerBtn2.StopTimer();
            TimerBtn3.StopTimer();
            TimerLbl.Text = "Timer Tapped";
        }
    }

}
