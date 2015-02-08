using System;

namespace iOSClient
{
	public class MoveResponse
	{
		public string moveResult { get; set; }
		public string winnerResult { get; set; }

		public MoveResponse ()
		{
			moveResult = string.Empty;
			winnerResult = string.Empty;
		}

		public override string ToString ()
		{
			return moveResult + ":" + winnerResult;
		}
	}
}
