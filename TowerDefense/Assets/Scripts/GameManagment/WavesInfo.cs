using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Info", menuName = "Libraries/EnemyWaves")]
public class WavesInfo : ScriptableObject
{
    [System.Serializable]
    public class Wave
    {
        public EnemyGroupInfo[] groups;
        public float timeTillNextWave = 5f;
    }

    public Wave[] waves;
}
