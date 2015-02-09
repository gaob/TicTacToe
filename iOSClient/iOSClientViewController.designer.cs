// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.CodeDom.Compiler;

namespace iOSClient
{
	[Register ("iOSClientViewController")]
	partial class iOSClientViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton CallAPIGetButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton CallAPIPostButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel KeyLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField KeyText { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISegmentedControl MobileServiceSegments { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel OutputLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton StartButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel StatusLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel URLLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField URLText { get; set; }

		[Action ("firstPlayerValueChanged:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void firstPlayerValueChanged (UISegmentedControl sender);

		[Action ("segmentValueChanged:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void segmentValueChanged (UISegmentedControl sender);

		[Action ("StartButton_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void StartButton_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (CallAPIGetButton != null) {
				CallAPIGetButton.Dispose ();
				CallAPIGetButton = null;
			}
			if (CallAPIPostButton != null) {
				CallAPIPostButton.Dispose ();
				CallAPIPostButton = null;
			}
			if (KeyLabel != null) {
				KeyLabel.Dispose ();
				KeyLabel = null;
			}
			if (KeyText != null) {
				KeyText.Dispose ();
				KeyText = null;
			}
			if (MobileServiceSegments != null) {
				MobileServiceSegments.Dispose ();
				MobileServiceSegments = null;
			}
			if (OutputLabel != null) {
				OutputLabel.Dispose ();
				OutputLabel = null;
			}
			if (StartButton != null) {
				StartButton.Dispose ();
				StartButton = null;
			}
			if (StatusLabel != null) {
				StatusLabel.Dispose ();
				StatusLabel = null;
			}
			if (URLLabel != null) {
				URLLabel.Dispose ();
				URLLabel = null;
			}
			if (URLText != null) {
				URLText.Dispose ();
				URLText = null;
			}
		}
	}
}
