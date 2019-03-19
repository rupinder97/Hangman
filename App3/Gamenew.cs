﻿using Android.App;
using Android.Widget;
using Android.OS;
using Java.Util;
using Android.Views;
using Java.Interop;
using System.Collections.Generic;
using Android.Content;

namespace App3
{
    [Activity(Label = "Game", Icon = "@mipmap/icon")]
    public class Gamenew : Activity
    {

      
        DataStore dataStore;
         Random random;
         string current;
         LinearLayout words;
         TextView[] charViews;
         GridView alphabets;
        private int numc;
        private int corrected;
        static int points = 0;
        LetterAdapter adp;
         ImageView[] images,images2;
         int numParts = 6;
        string[] sdata = { "0", "0", "0", "0", "1", "1", "1", "1", "2", "2", "2", "2", "3", "3", "3", "3" };
        List<model> listdata;
        model m;
        private int currPart;
        string[] cdata = { "AUCKLAND", "NAPIER", "DUNEDIN", "HAMILTON","CHICKEN","DUCK","FALCON","DOVE","LION","TIGER","BAT","BEAR", "ROSE", "SUNFLOWER", "LILY", "ASTER" };

        string nameuser;

        TextView txtpoints;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resourc
            SetContentView(Resource.Layout.gamenew);


            words = FindViewById<LinearLayout>(Resource.Id.words);
            alphabets = FindViewById<GridView>(Resource.Id.alphabets);

            txtpoints = FindViewById<TextView>(Resource.Id.txtpoints);

            nameuser = Intent.GetStringExtra("name");
            dataStore = new DataStore(this);

            dataStore.deleterecord(this);

            listdata = new List<model>();



           
                for (int i = 0; i < sdata.Length; i++)
                {
                    m = new model();
                m.Subid = sdata[i];
                m.Name = cdata[i];
                    dataStore.insercat(this, m);
                }




            listdata = dataStore.getcat(this,Intent.GetStringExtra("sid"));

            random = new Random();
            current = "";



            images = new ImageView[numParts];
            images[0] = FindViewById<ImageView>(Resource.Id.manhead); 
             images[1] = FindViewById<ImageView>(Resource.Id.manbody); 
             images[2] = FindViewById<ImageView>(Resource.Id.manarm1); 
             images[3] = FindViewById<ImageView>(Resource.Id.manarm2); 
             images[4] = FindViewById<ImageView>(Resource.Id.manleg1);
            images[5] = FindViewById<ImageView>(Resource.Id.manleg2);

            images2 = new ImageView[numParts];
            images2[0] = FindViewById<ImageView>(Resource.Id.manhead1);
            images2[1] = FindViewById<ImageView>(Resource.Id.manbody1);
            images2[2] = FindViewById<ImageView>(Resource.Id.manarm11);
            images2[3] = FindViewById<ImageView>(Resource.Id.manarm21);
            images2[4] = FindViewById<ImageView>(Resource.Id.manleg11);
            images2[5] = FindViewById<ImageView>(Resource.Id.manleg21);

            playGame();

        }

        private void playGame()
        {
            
            string newWord = listdata[random.NextInt(listdata.Count)].Name;
           
            while (newWord.Equals(current))
            {
                newWord = listdata[random.NextInt(listdata.Count)].Name;
            }
            current = newWord;

            charViews = new TextView[current.Length];
            words.RemoveAllViews();

            for (int c = 0; c < current.Length; c++)
            {
                charViews[c] = new TextView(this);
                charViews[c].Text= current[c]+"";

                charViews[c].LayoutParameters=new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
                charViews[c].Gravity= GravityFlags.Center;
                charViews[c].SetTextColor(Resources.GetColor(Resource.Color.white));
                charViews[c].SetBackgroundResource(Resource.Drawable.letter_bg);
                words.AddView(charViews[c]);

            }
            adp = new LetterAdapter(this);
            alphabets.SetAdapter(adp);

            currPart = 0;
            numc = current.Length;
            corrected = 0;

            for (int p = 0; p < numParts; p++)
            {
                images[p].Visibility = ViewStates.Visible;
               images2[p].Visibility = ViewStates.Gone;

            }
        }

        [Export("letterPressed")]
        public void letterPressed(View view)
        {

            string ltr = ((TextView)view).Text.ToString();
            char letterChar = ltr[0];

            view.Enabled=false;
            view.SetBackgroundResource(Resource.Drawable.letter_down);


            bool correct = false;
            for (int k = 0; k < current.Length; k++)
            {
                if (current[k] == letterChar)
                {
                    correct = true;
                    corrected++;
                    charViews[k].SetTextColor(Resources.GetColor(Resource.Color.black));
                }
            }

            if (correct)
            {
                if (corrected == numc)
                {
                   
                    disableBtns();

                    AlertDialog.Builder winBuild = new AlertDialog.Builder(this);
                    winBuild.SetTitle("Yeah ! You Saved the Man");
                    winBuild.SetMessage("\n\nCorrect answer :\n\n" + current);
                    points = points + 10;
                    txtpoints.Text="Total Points : "+points;
                    winBuild.SetPositiveButton("Play Again", (c, ev) =>
                    {
                        this.playGame();
                       
                    });

                    winBuild.SetNegativeButton("Close", (c, ev) =>
                    {
                       
                        dataStore.inserscore(this, nameuser, points + "");
                        StartActivity(new Intent(this, typeof(Result)).PutExtra("name", nameuser).PutExtra("score", points + ""));
                        this.Finish();
                    });



                    winBuild.Show();
                }
            }
            else if (currPart < numParts)
            {
                images[currPart].Visibility = ViewStates.Gone;
                images2[currPart].Visibility = ViewStates.Visible;

                currPart++;
            }
            else
            {
                disableBtns();
                AlertDialog.Builder loseBuild = new AlertDialog.Builder(this);
                loseBuild.SetTitle("Hanged Up");
                loseBuild.SetMessage("\n\nThe correct answer is :\n\n" + current);
                txtpoints.Text = "Total Points : " + points;

                loseBuild.SetPositiveButton("Start Again", (c, ev) =>
            {
                this.playGame();
            });

                loseBuild.SetNegativeButton("Close", (c, ev) =>
            {
                    
                dataStore.inserscore(this, nameuser, points+"");
                StartActivity(new Intent(this, typeof(Result)).PutExtra("name", nameuser).PutExtra("score", points+""));
                this.Finish();
            });


                loseBuild.Show();

            }
        }
		            



        public void disableBtns()
        {
            int numLetters = alphabets.Count;
            for (int l = 0; l < numLetters; l++)
            {
                alphabets.GetChildAt(l).Enabled=false;
            }
        }


    }

}

