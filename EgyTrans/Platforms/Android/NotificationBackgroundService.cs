using Android.App;
using Android.Content;
using Android.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyTrans.Platforms.Android
{
    [Service]
    public class NotificationBackgroundService : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            // Handle incoming intents here
            if (intent?.Extras != null)
            {
                string title = intent.GetStringExtra("title");
                string message = intent.GetStringExtra("message");
                NotificationManagerService.Instance.Show(title, message);
            }
            return StartCommandResult.Sticky;
        }
    }
}
