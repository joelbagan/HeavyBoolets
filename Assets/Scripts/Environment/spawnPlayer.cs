using UnityEngine;

public class spawnPlayer : MonoBehaviour {
    public GameObject player;
    public Transform playerSpawn;

    private PlayerManager playerManager;

    private void Awake()
    {
        playerManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerManager>();
        player = playerManager.player;
    }

    // Use this for initialization
    void Start () {
        player.transform.SetPositionAndRotation(playerSpawn.position, playerSpawn.rotation);
        player.SetActive(true);
	}	
}
