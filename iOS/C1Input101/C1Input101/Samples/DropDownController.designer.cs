﻿// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace C1Input101
{
    [Register ("DropDownController")]
    partial class DropDownController
    {
        [Outlet]
        C1.iOS.Input.C1DropDown DropDown { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (DropDown != null) {
                DropDown.Dispose ();
                DropDown = null;
            }
        }
    }
}