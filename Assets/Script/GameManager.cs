using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float gameTime;
    public float maxTime;
    public Text levelText;


    public PoolManager pool;
    public Player Player;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
        Player playerLogic = Player.GetComponent<Player>();
        levelText.text = "Level " + playerLogic.level;
    }

}
