using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int startLevel = 1;
    private const int startObstacleCount = 8;

    public static GameManager instance;

    [SerializeField] LevelData levelData;
    [SerializeField] private int score;

    private void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        levelData.Level = startLevel;
        levelData.ObstacleCount = startObstacleCount;
        SoundController.instance.StartBackgroundMusic();
    }

    private void Singleton()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddScore()
    {
        score++;
    }

    public void ResetScore()
    {
        score = 0;
    }
}
