using SystemRandomStudent.Views;

namespace SystemRandomStudent;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new NavigationPage(new MainPage());
    }
}