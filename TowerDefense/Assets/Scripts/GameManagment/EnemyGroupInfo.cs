using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Group Info", menuName = "Libraries/EnemyGroup")]
public class EnemyGroupInfo : ScriptableObject
{
    public GameObject[] enemies;
    public int groupSize = 0;
    public float spawnIntervals = .5f;
    public float timeTillNextGroup = 3f;
}
