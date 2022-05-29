using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCheckpointManager : MonoBehaviour
{
    public Transform currentActiveSpawnPoint;

    [SerializeField] Transform[] spawnPoints;

    private void Awake()
    {
        currentActiveSpawnPoint = spawnPoints[0];
    }
}
