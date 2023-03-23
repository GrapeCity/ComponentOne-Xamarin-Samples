using C1.DataCollection;
using C1.iOS.Grid;
using Foundation;
using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UIKit;

namespace FlexGrid101
{
    public partial class NewRowController : UIViewController
    {
        public NewRowController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var data = Customer.GetCustomerList(100);
            Grid.AutoGeneratingColumn += (s, e) => { e.Column.MinWidth = 110; e.Column.Width = GridLength.Star; };
            Grid.ItemsSource = new CustomDataCollection<Customer>(data);
            Grid.NewRowPosition = GridNewRowPosition.Top;
            Grid.NewRowPlaceholder = NSBundle.MainBundle.GetLocalizedString("Click here to add a new row", "");
            Grid.AllowDragging = GridAllowDragging.Both;
            Grid.HeadersVisibility = GridHeadersVisibility.All;
        }
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            Grid.RemoveFromSuperview();
            ReleaseDesignerOutlets();
        }
    }

    public class CustomDataCollection<T> : C1DataCollection<T>
        where T : class
    {
        public CustomDataCollection(IEnumerable source)
            : base(source)
        {
        }

        public override async Task<int> InsertAsync(int index, object item, CancellationToken cancellationToken = default)
        {
            await Task.Delay(1000, cancellationToken); //simulates a network operation
            return await base.InsertAsync(index, item, cancellationToken);
        }

        public override async Task RemoveAsync(int index, CancellationToken cancellationToken = default)
        {
            await Task.Delay(1000, cancellationToken); //simulates a network operation
            await base.RemoveAsync(index, cancellationToken);
        }

        public override async Task RemoveRangeAsync(int index, int count, CancellationToken cancellationToken = default)
        {
            await Task.Delay(1000, cancellationToken); //simulates a network operation
            await base.RemoveRangeAsync(index, count, cancellationToken);
        }

        public override async Task<int> ReplaceAsync(int index, object item, CancellationToken cancellationToken = default)
        {
            await Task.Delay(1000, cancellationToken); //simulates a network operation
            return await base.ReplaceAsync(index, item, cancellationToken);
        }

        public override async Task MoveAsync(int fromIndex, int toIndex, CancellationToken cancellationToken = default)
        {
            await Task.Delay(1000); //simulates a network operation
            await base.MoveAsync(fromIndex, toIndex, cancellationToken);
        }
    }
}