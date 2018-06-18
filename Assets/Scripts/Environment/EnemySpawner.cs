using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject[] spawnableEnemies;
    
    public void SpawnEnemy()
    {
        GameObject randomEnemy = spawnableEnemies[Random.Range(0, spawnableEnemies.Length)];
        Instantiate(randomEnemy, transform.position, transform.rotation);
    }
}
