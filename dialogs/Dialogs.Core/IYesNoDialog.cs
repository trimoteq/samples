using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveDialogs.Core
{
    public interface IYesNoDialog
    {
        // Returns 'true' if yes, 'false' if no.
        IObservable<bool> Prompt(string title, string description);
    }
}
