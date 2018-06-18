using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour {
    public EnemySpawner[] enemySpawners;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < enemySpawners.Length; i++)
        {
            enemySpawners[i].SpawnEnemy();
        }
	}
}
