using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using EgyTrans.OwenSystem.SystemOfWork.Interface;
using EgyTrans.Platforms.Android;

namespace EgyTrans
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            CreateNotificationFromIntent(Intent);
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                PermissionStatus status = await Permissions.RequestAsync<NotificationPermission>();
                RequestPermissions(new string[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage }, 0);
            }
        }

        protected override void OnNewIntent(Intent? intent)
        {
            base.OnNewIntent(intent);

            CreateNotificationFromIntent(intent);
   
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            if (requestCode == 0)
            {
                if (grantResults.Length > 0 && grantResults[0] == Permission.Granted)
                {
                    Console.WriteLine("Storage permission granted");
                }
                else
                {
                    Console.WriteLine("Storage permission denied");
                }
            }
        }

        static void CreateNotificationFromIntent(Intent intent)
        {
            if (intent?.Extras != null)
            {
                string title = intent.GetStringExtra(EgyTrans.Platforms.Android.NotificationManagerService.TitleKey);
                string message = intent.GetStringExtra(EgyTrans.Platforms.Android.NotificationManagerService.MessageKey);

                var service = IPlatformApplication.Current.Services.GetService<INotificationManagerService>();
                service.ReceiveNotification(title, message);
            }
        }

    }
}
