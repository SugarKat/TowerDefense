using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// commands templates
// build;{(int)turretID};{(int)nodeID} - to build a given turret on a given node id
// sell;{(int)nodeID} - to sell a turret from a given node
// wave;{(int)waveID} - to lauch a specified wave
// extra;{(int)groupID} - to send an extra wave of enemies for a player receing this command (this may not be implemented)

public class CommandInstantiator : MonoBehaviour
{
    public static CommandInstantiator instance;
    List<string> commandsToRun = new List<string>();

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        foreach (string command in commandsToRun)
        {
            string[] Values = command.Split(';');
            switch (Values[0])
            {
                case "build":

                    break;
                case "sell":

                    break;
                case "wave":

                    break;
                default:
                    Debug.LogError("received incorect or unknown command");
                    break;
            }
        }
        commandsToRun.Clear();
    }
    public void AddCommandToList(string comm)
    {
        commandsToRun.Add(comm);
    }
}
