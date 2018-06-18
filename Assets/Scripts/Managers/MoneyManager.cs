using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour {

    public int startingCoins = 0;
    public int coins = 0;

    private Text coinText;

    private void UpdateCoinText()
    {
        coinText.text = coins.ToString();
    }

    public void OnCoinPickup()
    {
        ++coins;
        UpdateCoinText();
    }

    private void Awake()
    {
        GameObject coinTextObj = GameObject.FindGameObjectWithTag("Coin Text");
        if(coinTextObj == null)
        {
            Debug.Log("Could not find Coin Text by tag");
            return;
        }
        coinText = coinTextObj.GetComponent<Text>();
        UpdateCoinText();
    }
}
