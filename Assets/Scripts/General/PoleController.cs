using UnityEngine;

public class PoleController : MonoBehaviour
{
    [SerializeField] private LevelData levelData;

    private void Start()
    {
        transform.localScale = new Vector3(1, levelData.CurrentObstacleCount, 1);
        transform.localPosition = new Vector3(transform.position.x, transform.localScale.y, transform.position.z);
    }
}
