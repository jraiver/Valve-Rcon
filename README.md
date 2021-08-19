# Valve-Rcon
Connecting and managing the HLDS server.

### Standard methods are being developed for version Counter-Strike 1.6 , but you can also send / process commands manually in other games with a similar type of connection. To do this, select the type of returned string.

Example:

    var data = await serv.SendCommandAsync<string>("amx_who");

Connection example:

    using Valve_Rcon;
    using Valve_Rcon.CmdResponse.AMX;
    
    var serv = new Server("127.0.0.1", 27015, "rconpass", Server.Game.CS16);
    

In the near future, typed responses to commands will be added.
The command "status" is being processed as a test.

    var status = await serv.SendCommandAsync<Status.Response>("status");
    var metaList = await serv.SendCommandAsync<MetaList.Response>("meta list");
    
# List of processed commands
### For get typed response from AmxModX command use "using Valve_Rcon.CmdResponse.AMX;"
1. status
    >typeOf Status.Response
2. meta list 
    >typeOf MetaList.Response
3. amx_modules 
    >typeOf Modules.Response

Google Translate :)
