using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;

namespace ReactiveDialogs.Core.ViewModels
{
    [DataContract]
    public class MainViewModel : ReactiveObject, IEnableLogger
    {
        [IgnoreDataMember]
        public ReactiveCommand<Task> DeleteData { get; protected set; }

        [IgnoreDataMember]
        public IScreen HostScreen { get; protected set; }

        public MainViewModel(IScreen screen = null, IYesNoDialog dialogFactory = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();

            // If the constructor hasn't passed in its own implementation,
            // use one from the resolver. This makes it easy to test DeleteData
            // via providing a dummy implementation.
            dialogFactory = dialogFactory ?? Locator.Current.GetService<IYesNoDialog>();

            var title = "Delete the data?";
            var desc = "Should we delete your important Data?";

            DeleteData = ReactiveCommand.CreateAsyncObservable(arg => dialogFactory.Prompt(title, desc)
                .Where(okButton => okButton == true)
                .Select(async x => DeleteTheData()));

            DeleteData.ThrownExceptions.Subscribe(ex => this.Log().WarnException("Couldn't delete the data", ex));
        }

        private void DeleteTheData()
        {
            this.Log().Info("The data has been deleted!");
        }
    }
}