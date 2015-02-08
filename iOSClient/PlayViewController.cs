using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.CodeDom.Compiler;

namespace iOSClient
{
	partial class PlayViewController : UIViewController
	{
		public string humanSymbol = "x@2x.png";
		public string serviceSymbol = "o@2x.png";

		public PlayViewController (IntPtr handle) : base (handle)
		{
		}

		partial void UIButton263_TouchUpInside (UIButton sender)
		{
			MakeMove("one", humanSymbol);
		}

		void MakeMove (string Bname, string humanSymbol)
		{
			UIButton thisButton;

			switch (Bname) {
				case "one":
					thisButton = Bone;
					break;
				case "two":
					thisButton = Btwo;
					break;
				case "three":
					thisButton = Bthree;
					break;
				case "four":
					thisButton = Bfour;
					break;
				case "five":
					thisButton = Bfive;
					break;
				case "six":
					thisButton = Bsix;
					break;
				case "seven":
					thisButton = Bseven;
					break;
				case "eight":
					thisButton = Beight;
					break;
				case "nine":
					thisButton = Bnine;
					break;
				default:
					throw new InvalidOperationException();
			}
			thisButton.SetImage (UIImage.FromFile (humanSymbol), UIControlState.Normal);
		}
	}
}
