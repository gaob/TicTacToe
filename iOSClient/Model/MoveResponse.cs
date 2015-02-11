using System;

namespace iOSClient
{
	/// <summary>
	/// Class for move response from backend.
	/// </summary>
	public class MoveResponse
	{
		// Store the "move" of response.
		public string moveResult { get; set; }
		// Store the "winner" of response.
		public string winnerResult { get; set; }
		// If move == "O" or "X", then has move.
		public bool hasMove { get { return moveResult != "n/a"; } }
		public bool isTie { get { return winnerResult == "Tie"; } }
		public bool isWin { get { return winnerResult == "O" || winnerResult == "X"; } }

		public MoveResponse ()
		{
			moveResult = string.Empty;
			winnerResult = string.Empty;
		}

		public override string ToString ()
		{
			return moveResult + ":" + winnerResult;
		}

		/// <summary>
		/// Get the position based on the move string.
		/// </summary>
		/// <returns>The move position.</returns>
		public int getMoveNumber()
		{
			switch (moveResult) {
			case "one":
				return 1;
			case "two":
				return 2;
			case "three":
				return 3;
			case "four":
				return 4;
			case "five":
				return 5;
			case "six":
				return 6;
			case "seven":
				return 7;
			case "eight":
				return 8;
			case "nine":
				return 9;
			default:
				throw new InvalidOperationException();
			}
		}
	}
}
