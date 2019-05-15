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

namespace Figure
{
    [Activity(Label = "FileOpened")]
    public class FileOpened : Activity
    {
        TextView res;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.fileActive);
            res = (TextView)FindViewById(Resource.Id.res);
            res.Text = Intent.GetStringExtra("final");
        }
    }
}