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

		partial void Bone_TouchUpInside (UIButton sender)
		{
			MakeMove("one", humanSymbol);
		}

		partial void Btwo_TouchUpInside (UIButton sender)
		{
			MakeMove("two", humanSymbol);
		}

		partial void Bthree_TouchUpInside (UIButton sender)
		{
			MakeMove("three", humanSymbol);
		}

		partial void Bfour_TouchUpInside (UIButton sender)
		{
			MakeMove("four", humanSymbol);
		}

		partial void Bfive_TouchUpInside (UIButton sender)
		{
			MakeMove("five", humanSymbol);
		}

		partial void Bsix_TouchUpInside (UIButton sender)
		{
			MakeMove("six", humanSymbol);
		}

		partial void Bseven_TouchUpInside (UIButton sender)
		{
			MakeMove("seven", humanSymbol);
		}

		partial void Beight_TouchUpInside (UIButton sender)
		{
			MakeMove("eight", humanSymbol);
		}

		partial void Bnine_TouchUpInside (UIButton sender)
		{
			MakeMove("nine", humanSymbol);
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
