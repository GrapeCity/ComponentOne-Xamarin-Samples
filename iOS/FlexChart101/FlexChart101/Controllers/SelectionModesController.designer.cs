﻿// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace FlexChart101
{
    [Register ("SelectionModesController")]
    partial class SelectionModesController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPickerView picker { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (picker != null) {
                picker.Dispose ();
                picker = null;
            }
        }
    }
}