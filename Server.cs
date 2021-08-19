using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Valve_Rcon.Converter;

namespace Valve_Rcon
{
	public class Server
	{
		private string ip;
		private int port;
		private string rcon;
		private Game gameType;

		UdpClient client = new UdpClient();

		public enum Game
		{
			CS16
		}

		public Server(string IP, int Port, string RconPassword, Game GameType)
		{
			CommandBase.Initialize();

			ip = IP;
			port = Port;
			rcon = RconPassword;
			gameType = GameType;

			client.Connect(ip, port);
		}

		#region Send CMD
		async public Task<T> SendCommandAsync<T>(string cmd)
		{
			//sending challenge command to counter strike server 
			string getChallenge = "challenge rcon\n";
			byte[] bufferSend = this.CMDPrepare(getChallenge);

			//send challenge command and get response
			IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
			await client.SendAsync(bufferSend, bufferSend.Length);
			var data = await client.ReceiveAsync();
			var bufferRec = data.Buffer;

			//retrive number from challenge response 
			string challenge_rcon = Encoding.ASCII.GetString(bufferRec);
			challenge_rcon = string.Join(null, Regex.Split(challenge_rcon, "[^\\d]"));

			//preparing rcon command to send
			string command = "rcon \"" + challenge_rcon + "\" " + rcon + " " + cmd.ToLower() + "\n";
			bufferSend = this.CMDPrepare(command);

			await client.SendAsync(bufferSend, bufferSend.Length);
			data = await client.ReceiveAsync();
			bufferRec = data.Buffer;
			var response = Encoding.UTF8.GetString(bufferRec);

			var ckeck = CheckError.Check(response);

			if (ckeck != null)
				throw new Exception(ckeck);

			var Handler = CommandBase.Handler(cmd, gameType);

			if (Handler != null)

				return (T)Convert.ChangeType(Handler(response, gameType), typeof(T));
			else
				return (T)Convert.ChangeType(response, typeof(string));
		}

		byte[] CMDPrepare(string command)
		{
			byte[] bufferTemp = Encoding.ASCII.GetBytes(command);
			byte[] bufferSend = new byte[bufferTemp.Length + 4];

			//intial 5 characters as per standard
			bufferSend[0] = byte.Parse("255");
			bufferSend[1] = byte.Parse("255");
			bufferSend[2] = byte.Parse("255");
			bufferSend[3] = byte.Parse("255");

			//copying bytes from challenge rcon to send buffer
			int j = 4;

			for (int i = 0; i < bufferTemp.Length; i++)
			{
				bufferSend[j++] = bufferTemp[i];
			}
			return bufferSend;
		}

		#endregion
	}
}
