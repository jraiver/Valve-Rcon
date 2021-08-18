using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Valve_Rcon.Models
{
	public class Players
	{
		private Player[] players = new Player[0];

		public Players(string []data)
		{
			for (int i = 7; i < data.Length - 2; i++)
			{
				Array.Resize(ref players, players.Length + 1);
				players[players.Length - 1] = new Player(data[i]);
			}
		}

		public int GetTotalPlayers() => players.Length;

		public Player GetPlayer(int id) => players[id];

		public class Player
		{
			public string Name { get; private set; }
			public string UserId { get; private set; }
			public string UniqueId { get; private set; }
			public string Frag { get; private set; }
			public TimeSpan Time { get; private set; }
			public string Ping { get; private set; }
			public string Loss { get; private set; }
			public string Adr { get; private set; }

			public Player(string data)
			{
				var user = data.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x));
				Name = user.ElementAt(2);
				UserId = user.ElementAt(3);
				UniqueId = user.ElementAt(4);
				Frag = user.ElementAt(5);
				Time = TimeSpan.Parse(user.ElementAt(6));
				Ping = user.ElementAt(7);
				Loss = user.ElementAt(8);
				if (user.Count() > 9)
					Adr = user.ElementAt(9);
			}

			public void Kick()
			{

			}
		}
	}
}
