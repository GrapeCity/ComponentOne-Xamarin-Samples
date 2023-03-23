using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using C1.Android.Grid;
using System;

namespace FlexGrid101
{
    [Activity(Label = "@string/SelectionModesTitle", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class SelectionModesActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SelectionModes);
            ActionBar.Title = GetString(Resource.String.SelectionModesTitle);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            var grid = FindViewById<FlexGrid>(Resource.Id.Grid);
            var modes = FindViewById<Spinner>(Resource.Id.Spinner);
            SelectionTextView = FindViewById<TextView>(Resource.Id.SelectionTextView);

            SelectionTextView.Text = "";

            var items = new GridSelectionMode[] { GridSelectionMode.None, GridSelectionMode.Cell, GridSelectionMode.CellRange, GridSelectionMode.Row, GridSelectionMode.RowRange, GridSelectionMode.Column, GridSelectionMode.ColumnRange };
            var adapteritems = Resources.GetStringArray(Resource.Array.SelectionModeTable);
            modes.Adapter = new ArrayAdapter(BaseContext, global::Android.Resource.Layout.SimpleSpinnerItem, adapteritems);
            modes.SetSelection(2);
            modes.ItemSelected += (s, e) =>
            {
                grid.SelectionMode = items[modes.SelectedItemPosition];
            };

            grid.ItemsSource = Customer.GetCustomerList(100);
            grid.SelectionChanged += OnSelectionChanged;
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
        TextView SelectionTextView { get; set; }
        private void OnSelectionChanged(object sender, GridCellRangeEventArgs e)
        {
            if (e.CellRange != null && e.CellRange.Row != -1)
            {
                int rowsSelected = Math.Abs(e.CellRange.Row2 - e.CellRange.Row) + 1;
                int colsSelected = Math.Abs(e.CellRange.Column2 - e.CellRange.Column) + 1;

                SelectionTextView.Text = (rowsSelected * colsSelected).ToString() + " " + Resources.GetString(Resource.String.CellsSelectedText);
            }
        }
    }
}