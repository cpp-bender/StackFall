using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    private const int obstaclePrefabCount = 4;

    [SerializeField] private GameObject[] obstacleModel;
    [SerializeField] private GameObject winPrefab;
    [HideInInspector] public GameObject[] obstaclePrefab = new GameObject[obstaclePrefabCount];

    private GameObject temp1Obstacle, temp2Obstacle;
    private int level = 1, addNumber = 7;

    public int obstacleCount = 1;

    private void Start()
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        //Generates a level randomly
        CreateObstacles();
        InstantiateObstacles();
    }

    private void CreateObstacles()
    {
        int randomIndex = Random.Range(0, obstaclePrefab.Length);
        for (int i = 0; i < obstaclePrefab.Length; i++)
        {
            obstaclePrefab[i] = obstacleModel[i + (randomIndex * obstaclePrefabCount)];
        }
    }

    private void InstantiateObstacles()
    {
        float obstacleStartHeight = obstacleCount / 2;
        for (float i = obstacleStartHeight; i > 0; i -= .5f)
        {
            GameObject randomObstacle = obstaclePrefab[Random.Range(0, 4)];
            Vector3 obstaclePosition = new Vector3(0f, i, 0f);
            Quaternion obstacleQuaternion = Quaternion.Euler(0, i * 10, 0);
            temp1Obstacle = Instantiate(randomObstacle, obstaclePosition, obstacleQuaternion);
        }
        temp2Obstacle = Instantiate(winPrefab, Vector3.zero, Quaternion.identity);
    }
}
