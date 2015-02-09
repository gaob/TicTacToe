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
		public static UIColor serviceColor = UIColor.Red;
		private string XImage = "x@2x.png";
		private string OImage = "o@2x.png";

		private MobileServiceHelper client;
		private string[] TicBoard;

		private int[,] Direction = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 }, { 1, 4, 7 }, { 2, 5, 8 }, { 3, 6, 9 }, { 1, 5, 9 }, { 3, 5, 7 } };

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
				FirstLabel.Text = "Azure";
				FirstLabel.TextColor = UIColor.Blue;
				FirstLabel2.Text = serviceImple;
				FirstLabel2.TextColor = serviceColor;

				try {
					ShowServicePiece();
					aMove = await CallAPIPost (TicBoard);

					OutputLabel.Text = aMove.ToString ();

					if (aMove.hasMove) {
						MakeServiceMove (aMove.getMoveNumber (), serviceSymbol);
					}
					ShowHumanPiece ();
				} catch (Exception ex) {
					// Display the exception message for the demo
					OutputLabel.Text = "";
					StatusLabel.Text = ex.Message;
					StatusLabel.BackgroundColor = UIColor.Red;
				}
			} else {
				SecondPiece.Text = serviceSymbol;
				SecondLabel.Text = "Azure";
				SecondLabel.TextColor = UIColor.Blue;
				SecondLabel2.Text = serviceImple;
				SecondLabel2.TextColor = serviceColor;
				FirstPiece.Text = humanSymbol;
				FirstLabel.Text = "Human";
				FirstLabel.TextColor = UIColor.Yellow;

				ShowHumanPiece ();
			}
        }

		void ShowServicePiece ()
		{
			if (serviceSymbol == "X") {
				ShowFirstPiece (true);
				ShowSecondPiece (false);
			} else {
				ShowFirstPiece (false);
				ShowSecondPiece (true);
			}
		}

		void ShowHumanPiece ()
		{
			if (serviceSymbol == "X") {
				ShowFirstPiece (false);
				ShowSecondPiece (true);
			} else {
				ShowFirstPiece (true);
				ShowSecondPiece (false);
			}
		}

		void ShowFirstPiece(bool show)
		{
			FirstPiece.Hidden = FirstLabel.Hidden = FirstLabel2.Hidden = !show;
		}

		void ShowSecondPiece(bool show)
		{
			SecondPiece.Hidden = SecondLabel.Hidden = SecondLabel2.Hidden = !show;
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

		partial void BQuitGame_TouchUpInside (UIButton sender)
		{
			this.NavigationController.PopViewControllerAnimated(true);
		}
			
		partial void Bcover_TouchUpInside (UIButton sender)
		{
			this.NavigationController.PopViewControllerAnimated(true);
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
				ShowHumanPiece ();

				if (aMove.isTie) {
					OutputLabel.Text = "Tied Game";
					SetAllSymbol(UIColor.LightGray);
					Bcover.Hidden = false;
				} else if (aMove.isWin) {
					if (aMove.winnerResult == "X") {
						OutputLabel.Text = "<-Winner";
						ShowFirstPiece(true);
						ShowSecondPiece(false);
					} else {
						OutputLabel.Text = "Winner->";
						ShowFirstPiece(false);
						ShowSecondPiece(true);
					}

					SetThreeSymbol(UIColor.Green, UIColor.Gray);
					Bcover.Hidden = false;
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
		}

		async Task<MoveResponse> CallAPIPost(string[] thisBoard)
		{
			MoveResponse aMove = new MoveResponse ();

			try
			{
				// Let the user know something is happening
				StatusLabel.Text = "Azure making her move, please wait...!";
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

		void SetAllSymbol (UIColor color)
		{
			Bone.TintColor = Btwo.TintColor = Bthree.TintColor = 
				Bfour.TintColor = Bfive.TintColor = Bsix.TintColor = 
					Bseven.TintColor = Beight.TintColor = Bnine.TintColor = color;
		}

		void SetThreeSymbol (UIColor color, UIColor other_color)
		{
			SetAllSymbol(other_color);

			for (int i=0;i<8;i++)
			{
				if (DetectWinner(TicBoard, i)) {
					getButtonFrom(Direction[i, 0]).TintColor = color;
					getButtonFrom(Direction[i, 1]).TintColor = color;
					getButtonFrom(Direction[i, 2]).TintColor = color;
				}
			}
		}

		private bool DetectWinner(string[] TicBoard, int d)
		{
			if (TicBoard[Direction[d, 0]] == TicBoard[Direction[d, 1]] &&
				TicBoard[Direction[d, 1]] == TicBoard[Direction[d, 2]])
			{
				if (TicBoard[Direction[d, 0]] == "O" || TicBoard[Direction[d, 0]] == "X")
				{
					return true;
				}
			}

			return false;
		}
	}
}
