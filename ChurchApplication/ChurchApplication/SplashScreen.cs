using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using System.Threading.Tasks;

namespace ChurchApplication
{
    [Activity(Label = "1st Presbyterian Church", Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashScreen : AppCompatActivity
    {
        

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
        }

        protected override void OnResume()
        {
            base.OnResume();

            Task startupWork = new Task(() => {
                Task.Delay(7500);  // Simulate a bit of startup work.
            });

            startupWork.ContinueWith(t => {

                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            }, TaskScheduler.FromCurrentSynchronizationContext());

            startupWork.Start();
        }
    }
}