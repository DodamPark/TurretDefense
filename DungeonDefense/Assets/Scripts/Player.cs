using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static int goldAmount;
    public int startGold = 5;

    public Text goldText;

    public static int playerLife;
    public int startLife = 30;

    public Text lifeText;

    public static int waveRounds;

    // Start is called before the first frame update
    void Start()
    {
        goldAmount = startGold;
        playerLife = startLife;
        waveRounds = 0;
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = "$" + goldAmount.ToString();
        lifeText.text = "Life : " + playerLife.ToString();
    }
}
