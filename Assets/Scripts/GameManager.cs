using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrashType {organic, metal, plastic}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public bool gameIsPaused;

    public Cannon cannon;
    public Collector collector;
    public Wall wall;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        gameIsPaused = false;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        collector.OnDeath += GameOver;
        wall.OnDeath += GameOver;
    }

    private void GameOver()
    {
        Time.timeScale = 0f;
        Debug.LogWarning("Game over");
        CanvasManager.instance.SwitchMenu(Menu.GameOverMenu);
    }
}
