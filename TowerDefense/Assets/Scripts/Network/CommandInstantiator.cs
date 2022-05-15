using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// commands templates
// build;{(int)turretID};{(int)nodeID} - to build a given turret on a given node id
// sell;{(int)nodeID} - to sell a turret from a given node
// wave;{(int)waveID} - to lauch a specified wave
// extra;{(int)groupID} - to send an extra wave of enemies for a player receing this command (this may not be implemented)
// roomUpdate;{hostName};{guestName} - receive an update about room

public class CommandInstantiator : MonoBehaviour
{
    public static CommandInstantiator instance;
    static List<string> commandsToRun = new List<string>();

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        foreach (string command in commandsToRun)
        {
            Debug.Log("processing commnand " + command);
            string[] values = command.Split(';');
            Debug.Log(values[0]);
            if (values[0] == "build")
            {

            }
            else if (values[0] == "sell")
            {

            }
            else if (values[0] == "wave")
            {

            }
            else if (values[0] == "roomUpdate")
            {
                Debug.Log("updating room info");
                LobbyMenuMangaer.instance.OpenRoom($"{values[1]};{values[2]}");
            }
            //switch (Values[0])
            //{
            //    case "build":

            //        break;
            //    case "sell":

            //        break;
            //    case "wave":

            //        break;
            //    case "roomUpdate":
            //        Debug.Log("updating room info");
            //        LobbyMenuMangaer.instance.OpenRoom($"{Values[1]};{Values[2]}");
            //        break;
            //    default:
            //        Debug.LogError("received incorect or unknown command");
            //        break;
            //}
        }
        commandsToRun.Clear();
    }
    public void AddCommandToList(string comm)
    {
        Debug.Log("received commnand " + comm);
        commandsToRun.Add(comm);
    }
}
