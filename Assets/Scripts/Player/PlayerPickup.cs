using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    private PlayerShooting playerShooting;
    private MoneyManager moneyManager;

    private void Awake()
    {
        moneyManager = GetComponent<MoneyManager>();

        //The PlayerShooting script is a component of the child object "PlayerView"
        playerShooting = GetComponentInChildren<PlayerShooting>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boolet"))
        {
            Destroy(other.gameObject);
            playerShooting.IncrementHoldingAmmoCount();
        }
        else if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            moneyManager.OnCoinPickup();
        }
    }
}
