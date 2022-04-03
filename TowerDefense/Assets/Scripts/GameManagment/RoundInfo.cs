using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Round", menuName = "Libraries/RoundInfo")]
public class RoundInfo : ScriptableObject
{
    public int roundNR;
    public int enemyCount;
    public List<int> enemyIDs;
}
