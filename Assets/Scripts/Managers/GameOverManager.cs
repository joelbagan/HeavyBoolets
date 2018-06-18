using UnityEngine.UI;
using UnityEngine;

public class GameOverManager : MonoBehaviour {

    public Text gameOverText;
    public float timeBetweenWords = 1.0f;

    private float timer = 0.0f;
    private string[] wordArray;
    private string wordsShowing;
    private int counter = 0;

    public bool playerDead = false;

    private void Awake()
    {
    }

    private void Start()
    {
        string gameOverMessage = "ALIVE = \nFALSE";
        wordArray = gameOverMessage.Split(' ');
        gameOverText.text = wordsShowing;
    }

    void Update () {
	    if(playerDead)
        {
            if (counter < wordArray.Length)
            {
                timer += Time.deltaTime;
                
                if(timer >= timeBetweenWords)
                {
                    wordsShowing = wordsShowing + ' ' + wordArray[counter];
                    gameOverText.text = wordsShowing;
                    counter += 1;
                    timer = 0.0f;
                }
            }
        }	
	}
}
