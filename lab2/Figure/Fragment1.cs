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
    public class Fragment1 : Fragment
    {
        RadioGroup rg;
        Button btn;
        RadioButton rb;
        RadioButton rb1, rb2;
        int checkedI;
        EditText storona, hstorona, h1storona;
        TextView help;
        CheckBox checkBox2, checkBox1;
        string result;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment1, container, false);
            storona = (EditText)view.FindViewById(Resource.Id.storona);
            hstorona = (EditText)view.FindViewById(Resource.Id.hstorona);
            h1storona = (EditText)view.FindViewById(Resource.Id.h1storona);
            help = (TextView)view.FindViewById(Resource.Id.help);
            checkBox1 = (CheckBox)view.FindViewById(Resource.Id.checkBox1);
            checkBox2 = (CheckBox)view.FindViewById(Resource.Id.checkBox2);
            btn = (Button)view.FindViewById(Resource.Id.button1);
            rg = view.FindViewById<RadioGroup>(Resource.Id.radioGroup1);
            rb1 = (RadioButton)view.FindViewById(Resource.Id.radioButton1);
            rb2 = (RadioButton)view.FindViewById(Resource.Id.radioButton2);
            btn.Click += Button_On;
            rg.CheckedChange += Rg_CheckCh;
            help.Click += Button_On_TextView;
            return view;
        }
        public void Button_On(object sender, EventArgs e)
        {
            Fragment2 frag = new Fragment2();
            Bundle args = new Bundle();
            Result();
            help.Text = result;
            args.PutString("help", help.Text);
            help.Visibility = ViewStates.Visible;
            frag.Arguments = args;
            var fragmentTransaction = FragmentManager.BeginTransaction();
            fragmentTransaction.Replace(Resource.Id.fragment1, frag);
            fragmentTransaction.Commit();
        }
        public void Button_On_TextView(object sender, EventArgs e)
        {
            help.Visibility = ViewStates.Invisible;
        }
        public void Rg_CheckCh(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            checkedI = rg.CheckedRadioButtonId;
            rb = View.FindViewById<RadioButton>(checkedI);
            if (rb.Text == "Квадрат")
            {
                storona.Visibility = ViewStates.Visible;
                hstorona.Visibility = ViewStates.Gone;
                h1storona.Visibility = ViewStates.Gone;
            }
            else
            {
                storona.Visibility = ViewStates.Gone;
                hstorona.Visibility = ViewStates.Visible;
                h1storona.Visibility = ViewStates.Visible;
            }
        }
        private void Result()
        {
            if (((rb1.Checked) && string.IsNullOrWhiteSpace(storona.Text)) ||
                     ((rb2.Checked) && (string.IsNullOrWhiteSpace(hstorona.Text)
                     || string.IsNullOrWhiteSpace(h1storona.Text))))
            {
                result = "Введите значения";
            }
            else if (((rb1.Checked) && Convert.ToInt32(storona.Text) <= 0) ||
                ((rb2.Checked) && Convert.ToInt32(hstorona.Text) <= 0)
                || ((rb2.Checked) && Convert.ToInt32(h1storona.Text) <= 0))
            {
                result = "Введите корректные значения";
            }
            else if (checkBox2.Checked && checkBox1.Checked)
            {
                result = "Периметр = " +
                    Convert.ToString((rb1.Checked) ?
                    (4 * Convert.ToInt32(storona.Text)) : 2 * (Convert.ToInt32(hstorona.Text)
                    + Convert.ToInt32(h1storona.Text))) +
                    "\nПлощадь = " +
                    Convert.ToString((rb1.Checked) ?
                    (Math.Pow(Convert.ToInt32(storona.Text), 2)) : (Convert.ToInt32(hstorona.Text)
                    * Convert.ToInt32(h1storona.Text)));
            }
            else if (checkBox1.Checked)
            {
                result = "Периметр = " +
                    Convert.ToString((rb1.Checked) ?
                    (4 * Convert.ToInt32(storona.Text)) : 2 * (Convert.ToInt32(hstorona.Text)
                    + Convert.ToInt32(h1storona.Text)));
            }
            else if (checkBox2.Checked)
            {
                result = "Площадь = " +
                    Convert.ToString((rb1.Checked) ?
                    (Math.Pow(Convert.ToInt32(storona.Text), 2)) : (Convert.ToInt32(hstorona.Text)
                    * Convert.ToInt32(h1storona.Text)));
            }

            else if (!checkBox1.Checked && !checkBox2.Checked)
            {
                result = "Поставьте галки";
            }
        }
    }
}