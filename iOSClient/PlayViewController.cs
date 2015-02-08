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
		public static string humanSymbol = "X";
		public static string serviceSymbol = "O";
		public static string serviceImple = ".NET";
		private string XImage = "x@2x.png";
		private string OImage = "o@2x.png";

		private MobileServiceHelper client;
		private string[] TicBoard;

		public PlayViewController (IntPtr handle) : base (handle)
		{
		}

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

			MoveResponse aMove;

            // Perform any additional setup after loading the view, typically from a nib.
            client = MobileServiceHelper.DefaultService;
			TicBoard = new string[10];
			for (int i=0;i<=9;i++) {
				TicBoard[i] = "?";
			}
				
			if (serviceSymbol == "X") {
				SecondPiece.Text = humanSymbol;
				SecondLabel.Text = "Human";
				SecondLabel.TextColor = UIColor.Yellow;
				FirstPiece.Text = serviceSymbol;
				FirstLabel.Text = serviceImple;

				try {
					ShowServicePiece();
					aMove = await CallAPIPost (TicBoard);

					OutputLabel.Text = aMove.ToString ();

					if (aMove.hasMove) {
						MakeServiceMove (aMove.getMoveNumber (), serviceSymbol);
					}
				} catch (Exception ex) {
					// Display the exception message for the demo
					OutputLabel.Text = "";
					StatusLabel.Text = ex.Message;
					StatusLabel.BackgroundColor = UIColor.Red;
				}
			} else {
				SecondPiece.Text = serviceSymbol;
				SecondLabel.Text = serviceImple;
				FirstPiece.Text = humanSymbol;
				FirstLabel.Text = "Human";
				FirstLabel.TextColor = UIColor.Yellow;

				ShowHumanPiece ();
			}
        }

		void ShowServicePiece ()
		{
			if (serviceSymbol == "X") {
				FirstPiece.Hidden = FirstLabel.Hidden = false;
				SecondPiece.Hidden = SecondLabel.Hidden = true;
			} else {
				FirstPiece.Hidden = FirstLabel.Hidden = true;
				SecondPiece.Hidden = SecondLabel.Hidden = false;
			}
		}

		void ShowHumanPiece ()
		{
			if (serviceSymbol == "X") {
				FirstPiece.Hidden = FirstLabel.Hidden = true;
				SecondPiece.Hidden = SecondLabel.Hidden = false;
			} else {
				FirstPiece.Hidden = FirstLabel.Hidden = false;
				SecondPiece.Hidden = SecondLabel.Hidden = true;
			}
		}

		partial void Bone_TouchUpInside (UIButton sender)
		{
			MakeHumanMove(1, humanSymbol);
		}

		partial void Btwo_TouchUpInside (UIButton sender)
		{
			MakeHumanMove(2, humanSymbol);
		}

		partial void Bthree_TouchUpInside (UIButton sender)
		{
			MakeHumanMove(3, humanSymbol);
		}

		partial void Bfour_TouchUpInside (UIButton sender)
		{
			MakeHumanMove(4, humanSymbol);
		}

		partial void Bfive_TouchUpInside (UIButton sender)
		{
			MakeHumanMove(5, humanSymbol);
		}

		partial void Bsix_TouchUpInside (UIButton sender)
		{
			MakeHumanMove(6, humanSymbol);
		}

		partial void Bseven_TouchUpInside (UIButton sender)
		{
			MakeHumanMove(7, humanSymbol);
		}

		partial void Beight_TouchUpInside (UIButton sender)
		{
			MakeHumanMove(8, humanSymbol);
		}

		partial void Bnine_TouchUpInside (UIButton sender)
		{
			MakeHumanMove(9, humanSymbol);
		}

		async void MakeHumanMove (int Bnumber, string symbol)
		{
			UIButton aButton;
			MoveResponse aMove;

			if (TicBoard [Bnumber] != "?") {
				StatusLabel.Text = "Please select an empty position";
				StatusLabel.BackgroundColor = UIColor.Red;

				return;
			}

			aButton = getButtonFrom (Bnumber);

			aButton.SetImage (UIImage.FromFile (symbol=="X" ? XImage : OImage), UIControlState.Normal);
			TicBoard [Bnumber] = symbol;

			try {
				ShowServicePiece();
				aMove = await CallAPIPost (TicBoard);

				OutputLabel.Text = aMove.ToString();

				if (aMove.hasMove) {
					MakeServiceMove (aMove.getMoveNumber (), serviceSymbol);
				}

				if (aMove.isTie) {
					OutputLabel.Text = "Tied Game";
					OutputLabel.TextColor = UIColor.LightGray;
				}
			}
			catch (Exception ex) {
				// Display the exception message for the demo
				OutputLabel.Text = "";
				StatusLabel.Text = ex.Message;
				StatusLabel.BackgroundColor = UIColor.Red;
			}
		}

		void MakeServiceMove (int Bnumber, string symbol)
		{
			UIButton aButton = getButtonFrom (Bnumber);

			aButton.SetImage (UIImage.FromFile (symbol=="X" ? XImage : OImage), UIControlState.Normal);
			TicBoard [Bnumber] = symbol;

			ShowHumanPiece ();
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

				throw ex;
			}
			finally
			{
				// Let the user know the operaion has completed
			}

			return aMove;
		}

		UIButton getButtonFrom(int Bnumber)
		{
			UIButton aButton = null;

			switch (Bnumber) {
			case 1:
				aButton = Bone;
				break;
			case 2:
				aButton = Btwo;
				break;
			case 3:
				aButton = Bthree;
				break;
			case 4:
				aButton = Bfour;
				break;
			case 5:
				aButton = Bfive;
				break;
			case 6:
				aButton = Bsix;
				break;
			case 7:
				aButton = Bseven;
				break;
			case 8:
				aButton = Beight;
				break;
			case 9:
				aButton = Bnine;
				break;
			default:
				throw new InvalidOperationException();
			}

			return aButton;
		}
	}
}
