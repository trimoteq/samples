using ReactiveUI;
using Splat;

namespace ReactiveDialogs.Core.ViewModels
{
    // CoolStuff: This class and anything under it will automatically get
    // saved and restored by ReactiveUI. This is a great place to put all
    // of your startup code - think of it as the "ViewModel for your app".
    public class AppBootstrapper : ReactiveObject, IScreen
    {
        // The Router holds the ViewModels for the back stack. Because it's
        // in this object, it will be serialized automatically.
        public RoutingState Router { get; protected set; }

        public AppBootstrapper(RoutingState testRouter = null)
        {
            Router = testRouter ?? new RoutingState();

            // This is a good place to set up any other app startup tasks, like setting the logging level
            LogHost.Default.Level = LogLevel.Debug;

            // An IScreen is a ViewModel that contains a Router - practically speaking. 
            // it usually represents a Window (or the RootFrame of a WinRT app). 
            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));

            // Navigate to the opening page of the application
            Router.Navigate.Execute(new MainViewModel(this));
        }
    }
}
