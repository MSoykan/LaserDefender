using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] WaveConfigSO currentWave;
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    public WaveConfigSO getCurrentWave()
    {
        return currentWave;
    }

    IEnumerator SpawnEnemies()
    {

        for (int i = 0; i < currentWave.GetEnemyCount(); i++)
        {
            Instantiate(currentWave.GetEnemyPrefab(i),// 2nd parameter is for spawning it in the desired place -
                currentWave.GetStartingWayPoint().position, // -> If we dont specify the palce it will spawn -
                Quaternion.identity,
                transform); // -> where enemy spawner transform position is
            yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
        }


    }

}
