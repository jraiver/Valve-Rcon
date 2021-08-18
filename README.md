# Valve-Rcon
Connecting and managing the HLDS server

Standard methods are being developed for version Counter-Strike 1.6 , but you can also send / process commands manually in other games with a similar type of connection.

Connection example:

    using Valve_Rcon;
    
    var serv = new Server("127.0.0.1", 27015, "rconpass", Server.Game.CS16);
    var data = await serv.SendCommandAsync<string>("amx_who");

In the near future, typed responses to commands will be added.
The command "status" is being processed as a test.

    var data = await serv.SendCommandAsync<Status>("status");
    
# List of processed commands

1. status

