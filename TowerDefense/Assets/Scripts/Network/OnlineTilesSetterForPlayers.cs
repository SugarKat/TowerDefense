using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineTilesSetterForPlayers : MonoBehaviour
{
    public bool forHost = false;
    public List<Tile> tiles = new List<Tile>();

    void Start()
    {
        if (forHost && NetworkManager.instance.host)
        {
            foreach (Tile tile in tiles)
            {
                tile.canBuild = true;
            }
        }
        else if(!forHost && !NetworkManager.instance.host)
        {
            foreach (Tile tile in tiles)
            {
                tile.canBuild = true;
            }
        }
    }
}
