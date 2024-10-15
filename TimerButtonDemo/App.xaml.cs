namespace TimerButtonDemo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(new AppShell());

            const int newWidth = 400;
            const int newHeight = 600;

            window.X = (int)(DeviceDisplay.MainDisplayInfo.Width - newWidth) / 2;
            window.Y = (int)(DeviceDisplay.MainDisplayInfo.Height - newHeight) / 2;

            window.Width = newWidth;
            window.Height = newHeight;


            return window;
        }
    }
}
