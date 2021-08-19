using Valve_Rcon;
using Valve_Rcon.Models;

namespace Rcontester.CmdResponse
{
	public static class Status
	{

		public static Response Process(string result, Server.Game gameType)
		{
			return new Response(result, gameType);
		}


		public class Response
		{
			public string HostName { get; private set; }
			public string Version { get; private set; }
			public string Ip { get; private set; }
			public string Map { get; private set; }
			public string TotalPlayers { get; private set; }

			public Players Players { get; set; }

			public Response(string result, Server.Game gameType)
			{
				// Call supported method
				switch (gameType)
				{
					case Server.Game.CS16:
						cs16(result);
						break;
				}

			}

			void cs16(string result)
			{
				var data = result.Split('\n');
				string indexString = "hostname:  ";
				HostName = data[0].Substring(data[0].IndexOf(indexString) + indexString.Length,
					data[0].Length - data[0].IndexOf(indexString) - indexString.Length);

				indexString = "version :  ";
				Version = data[1].Substring(data[1].IndexOf(indexString) + indexString.Length,
					data[1].Length - data[1].IndexOf(indexString) - indexString.Length);

				indexString = "tcp/ip  :  ";
				Ip = data[2].Substring(data[2].IndexOf(indexString) + indexString.Length,
					data[2].Length - data[2].IndexOf(indexString) - indexString.Length);

				indexString = "map     :  ";
				Map = data[3].Substring(data[3].IndexOf(indexString) + indexString.Length,
					data[3].Length - data[3].IndexOf(indexString) - indexString.Length);

				indexString = "players :  ";
				TotalPlayers = data[4].Substring(data[4].IndexOf(indexString) + indexString.Length,
					data[4].Length - data[4].IndexOf(indexString) - indexString.Length);

				Players = new Players(data);
			}
		}
	
	}
}
