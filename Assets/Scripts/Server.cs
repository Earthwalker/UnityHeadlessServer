using UnityEngine;
using System;

public class Server : MonoBehaviour
{
    /// <summary>
    /// Executed at the game's start
    /// </summary>
	void Start ()
    {
        int conn = 1; //max number of connections allowed
        int port = 26500; //port number
        int useNat = 1; //whether the server should use NAT punchthrough

        //get the command line arguments
        foreach (string arg in Environment.GetCommandLineArgs())
        {
            switch (arg[0])
            {
                case 'c':
                    if (!int.TryParse(arg.Substring(2), out conn))
                        continue;
                    break;
                case 'p':
                    if (!int.TryParse(arg.Substring(2), out port))
                        continue;
                    break;
                case 'n':
                    if (!int.TryParse(arg.Substring(2), out useNat))
                        continue;
                    break;
            }

            Debug.LogError("Unknown argument: " + arg);
        }

        //start the server
        Network.InitializeServer(conn, port, useNat == 1);
        Debug.Log(DateTime.Now.ToString() + " - Server Started");

        //log settings
        Debug.Log("conn:" + conn.ToString());
        Debug.Log("port:" + port.ToString());
        Debug.Log("useNat:" + useNat.ToString());
    }
	
    /// <summary>
    /// Called when the application exits
    /// </summary>
    void OnApplicationQuit()
    {
        //notify the network we are shutting down
        Network.Disconnect(500);

        Debug.Log(DateTime.Now.ToString() + " - Server Shutdown");
    }
}
