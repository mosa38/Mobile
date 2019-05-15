using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Figure
{
    public class Fragment2 : Fragment
    {
        TextView textView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View view = LayoutInflater.From(Activity).Inflate(Resource.Layout.fragment2, null);

            if (Arguments == null)
            {
                return null;
            }
            else
            {
                string help = Arguments.GetString("help");
                textView = new TextView(this.Activity)
                {
                    TextSize = 24,
                    Text = help
                };
                var scroller = new ScrollView(this.Activity);
                scroller.AddView(textView);
                return scroller;
            }
        }
    }
}
