using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// commands templates
// start - command received to load a level to start the game
// readyToStart - sent to host to confirm that guest player is ready to start the game (this may not be used)
// build;{(int)turretID};{(int)xPos;{(int)yPos} - to build a given turret on a given node id
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
            string[] values = command.Split(';');
            if (values[0] == "build")
            {
                int buildingID = int.Parse(values[1]);
                float xPos = float.Parse(values[2]);
                float yPos = float.Parse(values[3]);
                BuildingManager.Instance.BuildNT(buildingID, xPos, yPos);
            }
            else if (values[0] == "start")
            {
                LevelLoading.instance.LoadLevel(2);
            }
            else if (values[0] == "sell")
            {

            }
            else if (values[0] == "wave")
            {
                WaveSpawner.instance.StartNextWaveNT(int.Parse(values[1]));
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
        commandsToRun.Add(comm);
    }
}
