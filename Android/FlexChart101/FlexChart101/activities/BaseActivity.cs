using Android.App;
using Android.OS;
using System.Collections.Generic;

namespace FlexChart101
{
    public class BaseActivity : Activity
    {
        protected IList<object> dataSource;
        protected const string DATA_SOURCE = "DATA_SOURCE";
        protected override void OnSaveInstanceState(Bundle outState)
        {
            //outState.PutSerializable(DATA_SOURCE, dataSource);
            base.OnSaveInstanceState(outState);
        }
    }
}
