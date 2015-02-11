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
		// Store the backend implementation symbol, "JavaScript" or ".NET". Changed in IOSClientView.
		public static string serviceImple = ".NET";
		// Store the color to backend implementation, ".NET" == Red, "JavaScript" == Pink. Changed in IOSClientView.
		public static UIColor serviceColor = UIColor.Red;
		// Store the default image for O and X game pieces.
		private string XImage = "x@2x.png";
		private string OImage = "o@2x.png";

		private MobileServiceHelper client;
		// Store the board from index 1 to index 9, each position has "O", "X", or "?".
		private string[] TicBoard;

		// The two-dimensional array to store 8 directions of the board.
		// The first dimension indicates which direction.
		// The second dimension indicates one of the three positions in that direction.
		private int[,] Direction = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 }, { 1, 4, 7 }, { 2, 5, 8 }, { 3, 6, 9 }, { 1, 5, 9 }, { 3, 5, 7 } };
		// The default line images for each direction as indicated in the Direction array.
		private string[] lineImage = new string[] {"win1.png", "win2.png", "win3.png", "win4.png", "win5.png", "win6.png", "win7.png", "win8.png"};

		public PlayViewController (IntPtr handle) : base (handle)
		{
		}

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

			MoveResponse aMove;

            // Perform any additional setup after loading the view, typically from a nib.
            client = MobileServiceHelper.DefaultService;
			// Initial the board to be all empty.
			TicBoard = new string[10];
			for (int i=0;i<=9;i++) {
				TicBoard[i] = "?";
			}

			// If backend plays first.
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
					// Show backend game piece label before requesting move.
					ShowServicePiece();
					aMove = await CallAPIPost (TicBoard);

					// Set the output string to the response for debugging purpose.
					OutputLabel.Text = aMove.ToString ();

					// If the response contains a move, then perform it.
					if (aMove.hasMove) {
						MakeServiceMove (aMove.getMoveNumber (), serviceSymbol);
					}
					// After the move, show human game piece label.
					ShowHumanPiece ();
				} catch (Exception ex) {
					// Display the exception message.
					OutputLabel.Text = "";
					StatusLabel.Text = ex.Message;
					StatusLabel.BackgroundColor = UIColor.Red;
				}
			}
			// If human plays first.
			else {
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

		/// <summary>
		/// Show the backend game piece labels.
		/// Disable user interactions when backend is making move.
		/// </summary>
		void ShowServicePiece ()
		{
			if (serviceSymbol == "X") {
				ShowFirstPiece (true);
				ShowSecondPiece (false);
			} else {
				ShowFirstPiece (false);
				ShowSecondPiece (true);
			}

			AllowInteractions (false);
		}

		/// <summary>
		/// Show the human game piece labels.
		/// Enable user interactions.
		/// </summary>
		void ShowHumanPiece ()
		{
			if (serviceSymbol == "X") {
				ShowFirstPiece (false);
				ShowSecondPiece (true);
			} else {
				ShowFirstPiece (true);
				ShowSecondPiece (false);
			}

			AllowInteractions (true);
		}

		/// <summary>
		/// Show/Hide the game piece labels in the left side.
		/// </summary>
		/// <param name="show">If set to <c>true</c> show.</param>
		void ShowFirstPiece(bool show)
		{
			FirstPiece.Hidden = FirstLabel.Hidden = FirstLabel2.Hidden = !show;
		}

		/// <summary>
		/// Show/Hide the game piece labels in the right side.
		/// </summary>
		/// <param name="show">If set to <c>true</c> show.</param>
		void ShowSecondPiece(bool show)
		{
			SecondPiece.Hidden = SecondLabel.Hidden = SecondLabel2.Hidden = !show;
		}

		/// <summary>
		/// Enable/Disable the user interaction to game positions.
		/// Prevent race condition to backend.
		/// </summary>
		/// <param name="allow">If set to <c>true</c> allow.</param>
		void AllowInteractions(bool allow)
		{
			Bone.UserInteractionEnabled = Btwo.UserInteractionEnabled = Bthree.UserInteractionEnabled = 
				Bfour.UserInteractionEnabled = Bfive.UserInteractionEnabled = Bsix.UserInteractionEnabled = 
					Bseven.UserInteractionEnabled = Beight.UserInteractionEnabled = Bnine.UserInteractionEnabled = allow;
		}

		/// <summary>
		/// Board position 1 event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Bone_TouchUpInside (UIButton sender)
		{
			MakeHumanMove(1, humanSymbol);
		}

		/// <summary>
		/// Board position 2 event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Btwo_TouchUpInside (UIButton sender)
		{
			MakeHumanMove(2, humanSymbol);
		}

		/// <summary>
		/// Board position 3 event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Bthree_TouchUpInside (UIButton sender)
		{
			MakeHumanMove(3, humanSymbol);
		}

		/// <summary>
		/// Board position 4 event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Bfour_TouchUpInside (UIButton sender)
		{
			MakeHumanMove(4, humanSymbol);
		}

		/// <summary>
		/// Board position 5 event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Bfive_TouchUpInside (UIButton sender)
		{
			MakeHumanMove(5, humanSymbol);
		}

		/// <summary>
		/// Board position 6 event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Bsix_TouchUpInside (UIButton sender)
		{
			MakeHumanMove(6, humanSymbol);
		}

		/// <summary>
		/// Board position 7 event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Bseven_TouchUpInside (UIButton sender)
		{
			MakeHumanMove(7, humanSymbol);
		}

		/// <summary>
		/// Board position 8 event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Beight_TouchUpInside (UIButton sender)
		{
			MakeHumanMove(8, humanSymbol);
		}

		/// <summary>
		/// Board position 9 event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Bnine_TouchUpInside (UIButton sender)
		{
			MakeHumanMove(9, humanSymbol);
		}

		/// <summary>
		/// Quit Game button event.
		/// Pop to return to the Home View.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void BQuitGame_TouchUpInside (UIButton sender)
		{
			this.NavigationController.PopViewControllerAnimated(true);
		}

		/// <summary>
		/// Cover button event.
		/// Covers the screen after game ends.
		/// Pop to return to the Home View.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Bcover_TouchUpInside (UIButton sender)
		{
			this.NavigationController.PopViewControllerAnimated(true);
		}

		/// <summary>
		/// Make the Human move.
		/// </summary>
		/// <param name="Bnumber">Game position</param>
		/// <param name="symbol">Human symbol</param>
		async void MakeHumanMove (int Bnumber, string symbol)
		{
			UIButton aButton;
			MoveResponse aMove;

			// Prevent clicking a occupied position.
			if (TicBoard [Bnumber] != "?") {
				StatusLabel.Text = "Please select an empty position";
				StatusLabel.BackgroundColor = UIColor.Red;

				return;
			}

			// Get the game position button from position number.
			aButton = getButtonFrom (Bnumber);

			// Put the symbol on the game position, and mark the internal board.
			aButton.SetImage (UIImage.FromFile (symbol=="X" ? XImage : OImage), UIControlState.Normal);
			TicBoard [Bnumber] = symbol;

			try {
				// Show backend game piece label before requesting move.
				ShowServicePiece();
				aMove = await CallAPIPost (TicBoard);

				// Set the output string to the response for debugging purpose.
				OutputLabel.Text = aMove.ToString();

				if (aMove.hasMove) {
					MakeServiceMove (aMove.getMoveNumber (), serviceSymbol);
				}
				// After the move, show human game piece label.
				ShowHumanPiece ();

				// If it's a tied game, update the board.
				if (aMove.isTie) {
					OutputLabel.Text = "Tied Game";
					SetAllSymbol(UIColor.LightGray);
					ShowFirstPiece(true);
					ShowSecondPiece(true);
					//Cover the board with a return button.
					Bcover.Hidden = false;
				}
				// If it's a Win, update the board.
				else if (aMove.isWin) {
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
					//Cover the board with a return button.
					Bcover.Hidden = false;
				}
			}
			catch (Exception ex) {
				// Display the exception message.
				OutputLabel.Text = "";
				StatusLabel.Text = ex.Message;
				StatusLabel.BackgroundColor = UIColor.Red;
			}
		}

		/// <summary>
		/// Makes the backend move.
		/// </summary>
		/// <param name="Bnumber">Game position</param>
		/// <param name="symbol">Backend symbol</param>
		void MakeServiceMove (int Bnumber, string symbol)
		{
			// Get the game position button from position number.
			UIButton aButton = getButtonFrom (Bnumber);

			// Put the symbol on the game position, and mark the internal board.
			aButton.SetImage (UIImage.FromFile (symbol=="X" ? XImage : OImage), UIControlState.Normal);
			TicBoard [Bnumber] = symbol;
		}

		/// <summary>
		/// Post the message to the backend.
		/// </summary>
		/// <returns>The move response</returns>
		/// <param name="thisBoard">This board.</param>
		async Task<MoveResponse> CallAPIPost(string[] thisBoard)
		{
			MoveResponse aMove = new MoveResponse ();

			try
			{
				// Indeterminate progress indicator while backend is making the move.
				StatusLabel.Text = "Azure making her move, please wait...!";
				StatusLabel.TextColor = UIColor.White;
				StatusLabel.BackgroundColor = UIColor.Blue;

				// Create the json from the board.
				JToken payload = JObject.FromObject(new { one = thisBoard[1],
														  two = thisBoard[2],
														  three = thisBoard[3],
														  four = thisBoard[4],
														  five = thisBoard[5],
														  six = thisBoard[6],
														  seven = thisBoard[7],
														  eight = thisBoard[8],
														  nine = thisBoard[9]});
				// Make the call to the executemove resource asynchronously using POST verb
				var resultJson = await client.ServiceClient.InvokeApiAsync("executemove", payload);

				// Change the status when request completes.
				StatusLabel.BackgroundColor = UIColor.FromRGB(9, 125, 2);
				StatusLabel.Text = "Request completed!";

				// Verfiy that a result was returned
				if (resultJson.HasValues)
				{
					// Extract the value from the result
					aMove.moveResult = resultJson.Value<string>("move");
					aMove.winnerResult = resultJson.Value<string>("winner");
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
				// Display the exception message.
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

		/// <summary>
		/// Return the button from position number.
		/// </summary>
		/// <returns>The button</returns>
		/// <param name="Bnumber">Board position number</param>
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
			// This case is an invalid operation.
			default:
				throw new InvalidOperationException();
			}

			return aButton;
		}

		/// <summary>
		/// Change the color of all positions.
		/// </summary>
		/// <param name="color">Color.</param>
		void SetAllSymbol (UIColor color)
		{
			Bone.TintColor = Btwo.TintColor = Bthree.TintColor = 
				Bfour.TintColor = Bfive.TintColor = Bsix.TintColor = 
					Bseven.TintColor = Beight.TintColor = Bnine.TintColor = color;
		}

		/// <summary>
		/// Change the color of the winning positions and display line.
		/// </summary>
		/// <param name="color">Winning position colors</param>
		/// <param name="other_color">Other color.</param>
		void SetThreeSymbol (UIColor color, UIColor other_color)
		{
			SetAllSymbol(other_color);

			for (int i=0;i<8;i++)
			{
				if (DetectWinner(TicBoard, i)) {
					getButtonFrom(Direction[i, 0]).TintColor = color;
					getButtonFrom(Direction[i, 1]).TintColor = color;
					getButtonFrom(Direction[i, 2]).TintColor = color;
					Bcover.SetImage (UIImage.FromFile (lineImage [i]), UIControlState.Normal);
					break;
				}
			}
		}

		/// <summary>
		/// Detect if a direction of the board has a winner.
		/// </summary>
		/// <returns><c>true</c>, if winner was detected, <c>false</c> otherwise.</returns>
		/// <param name="TicBoard">The board.</param>
		/// <param name="d">The direction.</param>
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
