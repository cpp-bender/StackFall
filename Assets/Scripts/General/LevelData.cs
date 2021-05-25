using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Stack Fall/Level/Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField] private int level = 1;
    [SerializeField] private int currentObstacleCount;
    [SerializeField] private int shatteredObstacleCount;
    [SerializeField] private int startIndex;
    [SerializeField] private int endIndex;

    public int Level { get => level; set => level = value; }
    public int CurrentObstacleCount { get => currentObstacleCount; set => currentObstacleCount = value; }
    public int StartIndex { get => startIndex; set => startIndex = value; }
    public int EndIndex { get => endIndex; set => endIndex = value; }
    public int ShatteredObstacleCount { get => shatteredObstacleCount; set => shatteredObstacleCount = value; }
}
