using Rcontester.CmdResponse;
using System;
using System.Collections.Generic;

namespace Valve_Rcon.Converter
{
	public static class CommandBase
	{
		static Command[] CommandList = new Command[0];

		static void AddCommand(string Command, List<Server.Game> GameType, Func<string, Server.Game, object> Handler)
		{
			Array.Resize(ref CommandList, CommandList.Length + 1);
			CommandList[CommandList.Length - 1] = new Command(Command, GameType, Handler);
		}

		/// <summary>
		/// Handler convert response to type
		/// </summary>
		/// <param name="cmd"></param>
		/// <param name="GameType"></param>
		/// <returns></returns>
		public static Func<string, Server.Game, object> Handler(string cmd, Server.Game GameType)
		{
			for (int i = 0; i < CommandList.Length; i++)
			{
				if (CommandList[i].GetCmd() == cmd)
				{
					foreach (var game in CommandList[i].GetGameSuppport())
					{
						if (game == GameType)
						{
							return CommandList[i].GetHandler();
						}
					}
				}
			}

			return null;
		}

		/// <summary>
		/// Init cmd list
		/// </summary>
		public static void Initialize()
		{
			if (CommandList.Length < 1)
			{
				AddCommand("status", new List<Server.Game>() {Server.Game.CS16}, Status.Process);
				AddCommand("amx_modules", new List<Server.Game>() {Server.Game.CS16}, CmdResponse.AMX.Modules.Process);
				AddCommand("meta list", new List<Server.Game>() {Server.Game.CS16}, MetaList.Process);
			}
		}

		/// <summary>
		/// CMD
		/// </summary>
		public class Command
		{
			private string Cmd { get; set; }

			private List<Server.Game> GameSupport { get; set; }

			private Func<string, Server.Game, object> Handler;

			/// <summary>
			/// 
			/// </summary>
			/// <param name="Command">CMD HLDS</param>
			/// <param name="gameSupport">Support game</param>
			/// <param name="handler">Handler to type</param>
			public Command(string Command, List<Server.Game> gameSupport, Func<string, Server.Game, object> handler)
			{
				Cmd = Command;
				GameSupport = gameSupport;
				Handler = handler;
			}

			public string GetCmd() => Cmd;
			public List<Server.Game> GetGameSuppport() => GameSupport;
			public Func<string, Server.Game, object> GetHandler() => Handler;

		}
	}
}
