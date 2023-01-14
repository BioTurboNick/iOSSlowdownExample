using iOSSlowdown.Views;

namespace iOSSlowdown;

public partial class App : Application
{
    public App()
	{
		InitializeComponent();
        MainPage = new MainPage();
	}
}
