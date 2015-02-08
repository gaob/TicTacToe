using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.CodeDom.Compiler;

namespace iOSClient
{
	partial class PlayViewController : UIViewController
	{
		string humanSymbol = "x@2x.png";

		public PlayViewController (IntPtr handle) : base (handle)
		{
		}

		partial void UIButton263_TouchUpInside (UIButton sender)
		{
			Bone.SetImage(UIImage.FromFile(humanSymbol), UIControlState.Normal);
		}
	}
}
