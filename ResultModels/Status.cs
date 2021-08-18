using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valve_Rcon.Models;

namespace Rcontester.ResultModels
{
	public class Status
	{
		public string HostName { get; set; }
		public string Version { get; set; }
		public string Ip { get; set; }
		public string Map { get; set; }
		public string TotalPlayers { get; set; }

		public Players Players { get; set; }

		public Status(string result)
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
