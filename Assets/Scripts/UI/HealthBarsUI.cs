using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HealthBarsUI : MonoBehaviour
{

    [SerializeField]
    private GameObject container;

    public Collector collector;
    public Wall wall;

    [SerializeField]
    private Slider collectorHealth;
    [SerializeField]
    private Slider wallHealth;

    private void Start()
    {
        LevelLoader.instance.OnSceneLoaded += CheckScene;
    }

    private void OnDestroy()
    {
        LevelLoader.instance.OnSceneLoaded -= CheckScene;
    }

    private void CheckScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if(sceneName == "GameScene")
        {
            container.SetActive(true);
            InitializeBars();
        }
        else
        {
            container.SetActive(false);
        }
    }

    private void InitializeBars()
    {
        collector = GameManager.instance.collector;
        if (collector == null)
        {
            Debug.LogWarning("No active trash collector was found");
        }
        collector.OnDamaged += UpdateCollectorHealthBar;

        wall = GameManager.instance.wall;
        if (wall == null)
        {
            Debug.LogWarning("No active wall was found");
        }
        wall.OnDamaged += UpdateWallHealthBar;

        collectorHealth.maxValue = collector.maxHealth;
        collectorHealth.value = collector.currentHealth;

        wallHealth.maxValue = wall.maxHealth;
        wallHealth.value = wall.currentHealth;
    }

    private void UpdateCollectorHealthBar()
    {
        collectorHealth.value = collector.currentHealth;
    }

    private void UpdateWallHealthBar()
    {
        wallHealth.value = wall.currentHealth;
    }
}
