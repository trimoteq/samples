using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using MonoTouch.UIKit;
using MonoTouch.UIKit.Rx;
using ReactiveDialogs.Core;

namespace ReactiveDialogs.iOS
{
    public class AlertDialog : IYesNoDialog
    {
        public IObservable<bool> Prompt(string title, string description)
        {
            var delegateRx = new UIAlertViewDelegateRx();
            var alertView = new UIAlertView(title, description, delegateRx, "No", "Yes");
            alertView.Show();

            return delegateRx.ClickedObs
                .Take(1)
                .Select(x => x.Item2 == 1);
        }
    }
}
