using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public PlayerManager playerManager;
    public MapMaker mapMaker;
    public GameOverManager gameOverManager;

    private void Start()
    {
        //PlayerManager Init

        //mapMaker Init
        mapMaker.GenerateMap();

        //gameOverManager Init
    }
}
