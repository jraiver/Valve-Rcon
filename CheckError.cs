using System;
using System.Collections.Generic;
using System.Text;

namespace Valve_Rcon
{
	public static class CheckError
	{
		private static string[] ErrorList = new[]
		{
			"You have been banned from this server."
		};

		public static string Check(string data)
		{
			for (int i = 0; i < ErrorList.Length; i++)
			{
				if (data.IndexOf(ErrorList[i]) > -1)
					return ErrorList[i];
			}

			return null;
		}   
	}
}
