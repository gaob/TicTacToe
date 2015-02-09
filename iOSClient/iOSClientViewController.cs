using System;
using System.Drawing;
using System.Net.Http;
using Microsoft.WindowsAzure.MobileServices;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreImage;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Newtonsoft.Json.Linq;

namespace iOSClient
{
    public partial class iOSClientViewController : UIViewController
    {
        public iOSClientViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        private MobileServiceHelper client;

        #region View lifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            client = MobileServiceHelper.DefaultService;

			URLText.Text = client.applicationURL;
			KeyText.Text = client.applicationKey;
        }

		partial void segmentValueChanged (UISegmentedControl sender)
		{
			int selectedID = sender.SelectedSegment;

			switch (selectedID)
			{
				case 0:
				URLText.Text = MobileServiceHelper.DotNetURL;
				KeyText.Text = MobileServiceHelper.DotNetKey;
				PlayViewController.serviceImple = ".NET";
				PlayViewController.serviceColor = UIColor.Red;
				break;
				case 1:
				URLText.Text = MobileServiceHelper.JavaScriptURL;
				KeyText.Text = MobileServiceHelper.JavaScriptKey;
				PlayViewController.serviceImple = "JavaScript";
				PlayViewController.serviceColor = UIColor.FromRGB(255, 192, 203);		//Pink
				break;
				case 2:
				URLText.Text = "";
				KeyText.Text = "";
				PlayViewController.serviceImple = "User";
				break;
				default:
				URLText.Text = "Error!";
				KeyText.Text = "Error!";
				return;
			}
		}

		partial void firstPlayerValueChanged (UISegmentedControl sender)
		{
			int selectedID = sender.SelectedSegment;

			if (selectedID == 0)
			{
				PlayViewController.humanSymbol = "X";
				PlayViewController.serviceSymbol = "O";
			}
			else
			{
				PlayViewController.serviceSymbol = "X";
				PlayViewController.humanSymbol = "O";
			}
		}

		partial void StartButton_TouchUpInside (UIButton sender)
		{
			try
			{
				client.selectUser(URLText.Text, KeyText.Text);
			}
			catch (Exception ex)
			{
				URLText.Text = "Mobile Service Access Error!";
				KeyText.Text = ex.Message;

				return;
			}

			PlayViewController aViewController = this.Storyboard.InstantiateViewController("PlayViewController") as PlayViewController;
			if (aViewController != null) {
				this.NavigationController.PushViewController(aViewController, true);
			} else {
				URLText.Text = "Start Game Board Error!";
			}
		}

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }

        #endregion
    }
}
