using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    private const int obstaclePrefabCount = 4;

    [SerializeField] private GameObject[] obstacleModel;
    [SerializeField] private GameObject winPrefab;
    [SerializeField] private LevelData levelData;
    [HideInInspector] public GameObject[] obstaclePrefab = new GameObject[obstaclePrefabCount];

    private GameObject polySurface = null, win = null;

    public event System.Action<int, int> OnLevelChanged;

    private void Awake()
    {
        OnLevelChanged += ChangeDifficulty;
    }

    private void ChangeDifficulty(int StartIndex, int EndIndex)
    {
        levelData.StartIndex = StartIndex;
        levelData.EndIndex = EndIndex;
    }

    private void Start()
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        //Generates a level randomly
        CheckLevel();
        CreateObstacles();
        InstantiateObstacles();
    }

    private void CheckLevel()
    {
        if (levelData.Level < 20)
        {
            OnLevelChanged?.Invoke(0, 2);
        }
        else if (levelData.Level >= 20 && levelData.Level < 50)
        {
            OnLevelChanged?.Invoke(1, 3);
        }
        else if (levelData.Level >= 50 && levelData.Level < 100)
        {
            OnLevelChanged?.Invoke(2, 4);
        }
        else
        {
            OnLevelChanged?.Invoke(3, 4);
        }
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
        float obstacleStartHeight = levelData.ObstacleCount / 2;
        for (float posY = obstacleStartHeight; posY > 0; posY -= .5f)
        {
            polySurface = obstaclePrefab[Random.Range(levelData.StartIndex, levelData.EndIndex)];
            Vector3 obstaclePosition = new Vector3(0f, posY, 0f);
            polySurface.transform.eulerAngles = new Vector3(0f, posY * 10, 0f);
            polySurface.transform.eulerAngles += Roll(20);
            polySurface = Instantiate(polySurface, obstaclePosition, polySurface.transform.rotation, transform);
        }
        win = Instantiate(winPrefab, Vector3.zero, Quaternion.identity, transform);
    }

    private Vector3 Roll(float percent)
    {
        float dice = Random.Range(0, 100 / percent);
        if ((int)dice == 0)
        {
            return (Vector3.up * 180);
        }
        else
        {
            return Vector3.zero;
        }
    }
}
