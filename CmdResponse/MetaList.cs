using System;
using System.Linq;
using Valve_Rcon;

namespace Rcontester.CmdResponse
{
	public static class MetaList
	{

		public static Response Process(string result, Server.Game gameType)
		{
			return new Response(result, gameType);
		}

		public class Response
		{

			public MetaPlugin[] Plugins = new MetaPlugin[0];

			/// <summary>
			/// Amx Module
			/// </summary>
			public class MetaPlugin
			{
				public string Description { get; private set; }
				public string Stat { get; private set; }
				public string Pend { get; private set; }
				public string File { get; private set; }
				public string Vers { get; private set; }
				public string Src { get; private set; }
				public string Load { get; private set; }
				public string Unload { get; private set; }

				public MetaPlugin(string description, string stat, string pend, string file, string vers, string src,
					string load, string unload)
				{
					Description = description;
					Stat = stat;
					Pend = pend;
					File = file;
					Vers = vers;
					Src = src;
					Load = load;
					Unload = unload;
				}
			}

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
				var plugins = result.Split('\n');

				for (int i = 2; i < plugins.Length - 1; i++)
				{
					var plugin = plugins[i].Split("  ").Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
					Array.Resize(ref Plugins, Plugins.Length + 1);

					string unload = string.Empty;
					if (plugin[6].Split(' ').Length == 2)
						unload = plugin[6].Split(' ')[1];

					Plugins[Plugins.Length - 1] = new MetaPlugin(
						plugin[0], 
						plugin[1], 
						plugin[2], 
						plugin[3],
						plugin[4],
						plugin[5],
						plugin[6].Split(' ')[0], unload);
				}
			}
		}
	}
}
