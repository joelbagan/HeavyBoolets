using UnityEngine;
using UnityEngine.AI;

/* All normal enemies are 1-hit-kill so there is no health
 * this script just handles the enemy death
 */
public class EnemyHealth : MonoBehaviour
{
    public float sinkSpeed = 2.5f;
    public float coinOffset = 2.5f;
    public AudioClip deathClip;
    public GameObject coin;
    public int coinValue;
    public bool isDead;

    Vector3 coinSpawn;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    SphereCollider sphereCollider;
    bool isSinking;

    NavMeshAgent nav;

    void Awake()
    {
        enemyAudio = GetComponent<AudioSource>();
        sphereCollider = GetComponent<SphereCollider>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage()
    {
        Death();
    }


    void Death()
    {
        nav.enabled = false;
        isDead = true;
        enemyAudio.clip = deathClip;
        enemyAudio.Play();
        sphereCollider.isTrigger = true;
        StartSinking();
        SpawnCoins();
    }

    void SpawnCoins()
    {
        float spawnHeight = 1;
        for (int i = 0; i < coinValue; i++)
        {
            coinSpawn = transform.position + Random.onUnitSphere * coinOffset;
            coinSpawn.y = spawnHeight;
            Instantiate(coin, coinSpawn, transform.rotation);
        }
    }


    public void StartSinking()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        Destroy(gameObject, 2f);
    }
}
