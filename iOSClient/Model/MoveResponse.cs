using System;

namespace iOSClient
{
	public class MoveResponse
	{
		public string moveResult { get; set; }
		public string winnerResult { get; set; }
		public bool hasMove { get { return moveResult != "n/a"; } }
		public bool isTie { get { return winnerResult == "Tie"; } }

		public MoveResponse ()
		{
			moveResult = string.Empty;
			winnerResult = string.Empty;
		}

		public override string ToString ()
		{
			return moveResult + ":" + winnerResult;
		}

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
