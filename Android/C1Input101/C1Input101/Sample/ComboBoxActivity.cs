using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using C1.Android.Input;

namespace C1Input101
{
    [Activity(Label = "@string/combobox")]
    public class ComboBoxActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.SetContentView(Resource.Layout.activity_combobox);

            ActionBar.Title = GetString(Resource.String.combobox);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            TextView editableLabel = ((TextView)this.FindViewById(Resource.Id.textView1));
            editableLabel.SetText(Resource.String.editable);

            C1ComboBox comboBox = (C1ComboBox)this.FindViewById(Resource.Id.editable_combobox);
            comboBox.DisplayMemberPath = "Name";
            comboBox.ItemsSource = Country.GetDemoDataList();

            TextView nonEditableLabel = ((TextView)this.FindViewById(Resource.Id.textView2));
            nonEditableLabel.SetText(Resource.String.noneditable);

            C1ComboBox nonEditableComboBox = (C1ComboBox)this.FindViewById(Resource.Id.noneditable_combobox);
            nonEditableComboBox.ItemsSource = Country.GetDemoDataList();
            nonEditableComboBox.DisplayMemberPath = "Name";
            nonEditableComboBox.IsEditable = false;
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

