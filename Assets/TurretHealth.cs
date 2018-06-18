using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHealth : MonoBehaviour {
    public GameObject parentObj;
    public GameObject deadTurret;

    public float coinOffset = 2.5f;
    public AudioClip deathClip;
    public GameObject coin;
    public int coinValue;
    public bool isDead;

    private Transform parentTransform;

    public void TakeDamage()
    {
        Death();
    }

    private void Death()
    {
        parentTransform = parentObj.transform;
        Instantiate(deadTurret, parentTransform.position, parentTransform.rotation);
        SpawnCoins();
        Destroy(parentObj);

    }

    void SpawnCoins()
    {
        Vector3 coinSpawn;
        float spawnHeight = 1;
        for (int i = 0; i < coinValue; i++)
        {
            coinSpawn = parentTransform.position + Random.onUnitSphere * coinOffset;
            coinSpawn.y = spawnHeight;
            Instantiate(coin, coinSpawn, parentTransform.rotation);
        }
    }
}
