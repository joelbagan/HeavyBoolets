using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 5;
    public int currentHealth;

    public Material healthActive;
    public Material healthInactive;
    public GameObject[] healthIndicators;

    public bool invulnerable;

    PlayerMove playerMove ;
    PlayerShooting playerShooting;
    PlayerLook playerLook;
    bool isDead;
    bool damaged;

    private GameOverManager gameOverManager;


    void Awake ()
    {
        playerMove = GetComponent <PlayerMove> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        playerLook = GetComponentInChildren<PlayerLook>();
        currentHealth = startingHealth;

        GameObject gameManager = GameObject.FindGameObjectWithTag("Game Manager");
        if(gameManager == null)
        {
            Debug.Log("Cannot find GameManager by tag");
        }
        else
        {
            gameOverManager = gameManager.GetComponent<GameOverManager>();
        }
    }


    public void TakeDamage ()
    {
        if (invulnerable) return;
        currentHealth -= 1;
        healthIndicators[currentHealth].GetComponent<Renderer>().material = healthInactive;
        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;
        playerMove.enabled = false;
        playerShooting.enabled = false;
        playerLook.enabled = false;
        gameOverManager.playerDead = true;
    }
}
