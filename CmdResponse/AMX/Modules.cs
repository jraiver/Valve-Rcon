using System;
using System.Linq;

namespace Valve_Rcon.CmdResponse.AMX
{
	public static class Modules
	{
		public static Response Process(string result, Server.Game gameType)
		{
			return new Response(result, gameType);
		}


		public class Response
		{

			public Module[] Modules = new Module[0];

			/// <summary>
			/// Amx Module
			/// </summary>
			public class Module
			{
				public string Name { get; private set; }
				public string Version { get; private set; }
				public string Author { get; private set; }
				public string Status { get; private set; }

				public Module(string name, string ver, string auth, string status)
				{
					Name = name;
					Version = ver;
					Author = auth;
					Status = status;
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
				var modulesList = result.Split('\n');

				for (int i = 2; i < modulesList.Length - 2; i++)
				{
					var module = modulesList[i].Split("  ").Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
					Array.Resize(ref Modules, Modules.Length + 1);
				

					Modules[Modules.Length - 1] = new Module(module[0], module[1], module[2], module[3]);
				}



			}
		}

	}
}
