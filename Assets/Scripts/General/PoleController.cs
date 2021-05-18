using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleController : MonoBehaviour
{
    [SerializeField] private LevelData levelData;

    private void Start()
    {
        transform.localScale = new Vector3(1, levelData.ObstacleCount, 1);
        transform.localPosition = new Vector3(transform.position.x, transform.localScale.y, transform.position.z);
    }
}
