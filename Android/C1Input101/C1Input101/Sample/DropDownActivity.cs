﻿using Android.App;
using Android.OS;
using Android.Views;
using C1.Android.Calendar;
using C1.Android.Input;

namespace C1Input101
{
    [Activity(Label = "@string/dropdown")]
    public class DropDownActivity : Activity
    {
        C1DropDown dropdown;
        C1MaskedTextView header;
        C1Calendar calendar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetContentView(Resource.Layout.activity_dropdown);

            ActionBar.Title = GetString(Resource.String.dropdown);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            dropdown = (C1DropDown)this.FindViewById(Resource.Id.dropdown);
            header = new C1MaskedTextView(this);
            header.Mask = Resources.GetString(Resource.String.date_mask_string);

            calendar = new C1Calendar(this);
            dropdown.Header = header;
            dropdown.DropDown = calendar;
            dropdown.DropDownHeight = 300;
            dropdown.IsAnimated = true;
            dropdown.DropDownMode = DropDownMode.BelowOrAbove;

            calendar.SelectionChanged += (object sender, CalendarSelectionChangedEventArgs e) =>
            {
                dropdown.IsDropDownOpen = false;
                System.DateTime dateTime = calendar.SelectedDates[0];
                string strDate = dateTime.ToString(Resources.GetString(Resource.String.date_mask_format));
                header.Value = strDate;
            };


        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == global::Android.Resource.Id.Home)
            {
                Finish();
                return true;
            }
            else
            {
                return base.OnOptionsItemSelected(item);
            }
        }
    }
}

