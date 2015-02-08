using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.CodeDom.Compiler;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace iOSClient
{
	partial class PlayViewController : UIViewController
	{
		public string humanSymbol = "X";
		public string serviceSymbol = "O";
		private string XImage = "x@2x.png";
		private string OImage = "o@2x.png";

		private MobileServiceHelper client;
		private string[] TicBoard;

		public PlayViewController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            client = MobileServiceHelper.DefaultService;
			TicBoard = new string[10];
			for (int i=0;i<=9;i++) {
				TicBoard[i] = "?";
			}
        }

		partial void Bone_TouchUpInside (UIButton sender)
		{
			MakeMove(1, humanSymbol);
		}

		partial void Btwo_TouchUpInside (UIButton sender)
		{
			MakeMove(2, humanSymbol);
		}

		partial void Bthree_TouchUpInside (UIButton sender)
		{
			MakeMove(3, humanSymbol);
		}

		partial void Bfour_TouchUpInside (UIButton sender)
		{
			MakeMove(4, humanSymbol);
		}

		partial void Bfive_TouchUpInside (UIButton sender)
		{
			MakeMove(5, humanSymbol);
		}

		partial void Bsix_TouchUpInside (UIButton sender)
		{
			MakeMove(6, humanSymbol);
		}

		partial void Bseven_TouchUpInside (UIButton sender)
		{
			MakeMove(7, humanSymbol);
		}

		partial void Beight_TouchUpInside (UIButton sender)
		{
			MakeMove(8, humanSymbol);
		}

		partial void Bnine_TouchUpInside (UIButton sender)
		{
			MakeMove(9, humanSymbol);
		}

		async void MakeMove (int Bnumber, string symbol)
		{
			UIButton thisButton;
			MoveResponse aMove;

			switch (Bnumber) {
				case 1:
					thisButton = Bone;
					break;
				case 2:
					thisButton = Btwo;
					break;
				case 3:
					thisButton = Bthree;
					break;
				case 4:
					thisButton = Bfour;
					break;
				case 5:
					thisButton = Bfive;
					break;
				case 6:
					thisButton = Bsix;
					break;
				case 7:
					thisButton = Bseven;
					break;
				case 8:
					thisButton = Beight;
					break;
				case 9:
					thisButton = Bnine;
					break;
				default:
					throw new InvalidOperationException();
			}
			thisButton.SetImage (UIImage.FromFile (symbol=="X" ? XImage : OImage), UIControlState.Normal);

			TicBoard [Bnumber] = symbol;

			aMove = await CallAPIPost (TicBoard);

			OutputLabel.Text = aMove.ToString();
		}

		async Task<MoveResponse> CallAPIPost(string[] thisBoard)
		{
			MoveResponse aMove = new MoveResponse ();

			try
			{
				// Let the user know something is happening
				StatusLabel.Text = "POST Request Made, waiting for response...";
				StatusLabel.TextColor = UIColor.White;
				StatusLabel.BackgroundColor = UIColor.Blue;

				// Create the json to send using an anonymous type 
				JToken payload = JObject.FromObject(new { one = thisBoard[1],
														  two = thisBoard[2],
														  three = thisBoard[3],
														  four = thisBoard[4],
														  five = thisBoard[5],
														  six = thisBoard[6],
														  seven = thisBoard[7],
														  eight = thisBoard[8],
														  nine = thisBoard[9]});
				// Make the call to the hello resource asynchronously using POST verb
				var resultJson = await client.ServiceClient.InvokeApiAsync("hello", payload);

				// Understanding color in iOS http://www.iosing.com/2011/11/uicolor-understanding-colour-in-ios/
				// A dark green: http://www.colorpicker.com/
				StatusLabel.BackgroundColor = UIColor.FromRGB(9, 125, 2);
				StatusLabel.Text = "Request completed!";

				// Verfiy that a result was returned
				if (resultJson.HasValues)
				{
					// Extract the value from the result
					aMove.moveResult = resultJson.Value<string>("move");
					aMove.winnerResult = resultJson.Value<string>("winner");

					// Set the text block with the result
					//OutputLabel.Text = moveResult + ", " + winnerResult;
				}
				else
				{
					StatusLabel.TextColor = UIColor.Black;
					StatusLabel.BackgroundColor = UIColor.Orange;
					OutputLabel.Text = "Nothing returned!";
				}
			}
			catch (Exception ex)
			{
				// Display the exception message for the demo
				OutputLabel.Text = "";
				StatusLabel.Text = ex.Message;
				StatusLabel.BackgroundColor = UIColor.Red;
			}
			finally
			{
				// Let the user know the operaion has completed
			}

			return aMove;
		}
	}
}
