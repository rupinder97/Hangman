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

namespace App3
{
    [Activity(Label = "Categories")]
    public class secondpage : Activity
    {
        TextView txtflower, txtanimals, txtbird, txtcity;
        Button btn;
        EditText edt;
      
        string[] item;
        string temp="";


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.secondpage);
            txtflower = FindViewById<TextView>(Resource.Id.txtflower);
            txtanimals = FindViewById<TextView>(Resource.Id.txtanimals);
            txtbird = FindViewById<TextView>(Resource.Id.txtbird);
            txtcity = FindViewById<TextView>(Resource.Id.txtcity);

            Dialog dd = new Dialog(this);
            dd.SetContentView(Resource.Layout.dialog);
            edt = dd.FindViewById<EditText>(Resource.Id.edt);
            btn = dd.FindViewById<Button>(Resource.Id.btn);
            dd.SetCancelable(false);

            txtcity.Click += (s, e) => {


                dd.Show();

                btn.Click += (s1, e1) =>
                {
                    String text = edt.Text.ToString();

                    if (text.Equals(""))
                    {

                        Toast.MakeText(this, "Please enter name", ToastLength.Short).Show();
                    }
                    else
                    {
                        dd.Dismiss();
                        StartActivity(new Intent(this, typeof(Gamenew)).PutExtra("sid", "" + 0).PutExtra("name", text));
                        Finish();


                    }

                };



            };

            txtbird.Click += (s, e) => {

                dd.Show();

                btn.Click += (s1, e1) =>
                {
                    String text = edt.Text.ToString();

                    if (text.Equals(""))
                    {

                        Toast.MakeText(this, "Please enter name", ToastLength.Short).Show();
                    }
                    else
                    {
                        dd.Dismiss();
                        StartActivity(new Intent(this, typeof(Gamenew)).PutExtra("sid", "" + 1).PutExtra("name", text));
                        Finish();


                    }

                };

            };

            txtanimals.Click += (s, e) => {

                    dd.Show();

                    btn.Click += (s1, e1) =>
                    {
                        String text = edt.Text.ToString();

                        if (text.Equals(""))
                        {

                            Toast.MakeText(this, "Please enter name", ToastLength.Short).Show();
                        }
                        else
                        {
                            dd.Dismiss();
                            StartActivity(new Intent(this, typeof(Gamenew)).PutExtra("sid", "" + 2).PutExtra("name", text));
                            Finish();


                        }

                    };

            };

            txtflower.Click += (s, e) =>
                    {

                        dd.Show();

                        btn.Click += (s1, e1) =>
                        {
                            String text = edt.Text.ToString();

                            if (text.Equals(""))
                            {

                                Toast.MakeText(this, "Please enter name", ToastLength.Short).Show();
                            }
                            else
                            {
                                dd.Dismiss();
                                StartActivity(new Intent(this, typeof(Gamenew)).PutExtra("sid", "" + 3).PutExtra("name", text));
                                Finish();


                            }
                        };

                    };


        }



        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            // set the menu layout on Main Activity  
            MenuInflater.Inflate(Resource.Menu.menu1, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_menu:
                    {
                        
                        StartActivity(new Intent(this, typeof(Totalscores)));
                        Finish();
                        return true;
                    }
               
            }

            return base.OnOptionsItemSelected(item);
        }

    }
}