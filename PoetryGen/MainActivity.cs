using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using PCLStorage;
using Android.Content.PM;
using PoetryEngine;

namespace PoetryGen {
    [Activity(Label = "PoetryGen", MainLauncher = true, Icon = "@drawable/icon",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen", ScreenOrientation = ScreenOrientation.Portrait, NoHistory = true)]
    public class MainActivity : Activity {

        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);
            
            SetContentView(Resource.Layout.Main);
            Generator.Load();

            Button button = FindViewById<Button>(Resource.Id.MyButton);
            var textbox = FindViewById<EditText>(Resource.Id.PoemTextBox);

            textbox.Focusable = false;

            button.Click += delegate { textbox.Text = Generator.Gen(); };
        }

        public override void OnBackPressed() {
            base.OnBackPressed();
            System.Environment.Exit(0);
        }
    }
}

