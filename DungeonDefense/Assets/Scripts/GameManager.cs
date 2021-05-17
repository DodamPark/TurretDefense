using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool defeatGame;

    public GameObject gameOverUI;

    // Start is called before the first frame update
    void Start()
    {
        defeatGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (defeatGame)
            return;

        if(Player.playerLife <= 0)
        {
            DefeatGame();
        }
        else if (Input.GetKey("e"))
        {
            DefeatGame();
        }
    }

    void DefeatGame()
    {
        defeatGame = true;

        gameOverUI.SetActive(true);
    }

    
}
