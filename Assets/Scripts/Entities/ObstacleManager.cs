using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private Obstacle[] obstacles;

    public void Shatter()
    {
        foreach (var obstacle in obstacles)
        {
            obstacle.GetComponent<Obstacle>().PlayShatterAnimation();
        }
    }
}
