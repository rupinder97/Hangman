using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Views.Animations;

namespace App3
{
    [Activity(Label = "HangmanGame" , MainLauncher = true)]
    public class FirstPage : Activity
    {

         TextView txt;
        Animation animation;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.firstpage);


            txt = FindViewById<TextView>(Resource.Id.txt);

            animation = AnimationUtils.LoadAnimation(this,Resource.Animation.myanimation);


            txt.StartAnimation(animation);


            animation.SetAnimationListener(new NewAnimationListener(this));



        }
    }

    class NewAnimationListener : Java.Lang.Object,
        Android.Views.Animations.Animation.IAnimationListener
    {
        Activity first;

        public NewAnimationListener(Activity first)
        {
            this.first = first;
        }

        public void OnAnimationEnd(Animation animation)
        {
            first.StartActivity(new Intent(first, typeof(secondpage)));
            first.Finish();
        }

        public void OnAnimationRepeat(Animation animation)
        {
        }

        public void OnAnimationStart(Animation animation)
        {
        }
    }
}