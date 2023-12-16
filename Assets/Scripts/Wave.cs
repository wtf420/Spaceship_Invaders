using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    //Does this sequence happens parallel with the previous sequence or after?
    public enum WaitBetweenSequenceType
    {
        Continous,
        Parallel
    }

    [System.Serializable]
    public class EnemySpawnInfo
    {
        [SerializeField] public Entity entity;
        [SerializeField] public uint quantity;
        [SerializeField] public float delayBetweenSpawn; // delay between spawning each enemy
    }

    [System.Serializable]
    public class SpawnSequence
    {
        [SerializeField] public Path path;
        //Use to make Enemies Orbit
        [SerializeField] public Path OrbitPath;

        [SerializeField] public EnemySpawnInfo[] enemySpawnInfos;
        //Does this sequence happens parallel with the previous sequence or after?
        [SerializeField] public WaitBetweenSequenceType WaitType;
        [SerializeField] public float delayPostSequence;
    }

    public class Wave : MonoBehaviour
    {
        //Time delay before spawning wave
        [SerializeField] public float delayBeforeSpawn;
        [SerializeField] public List<SpawnSequence> spawnSequences;
    }
}