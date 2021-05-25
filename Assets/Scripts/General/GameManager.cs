using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] UIController uiController;
    [SerializeField] LevelData levelData;

    private void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        uiController.ChangeLevelText(levelData.Level);
    }

    private void Singleton()
    {
        if (instance)
        {
            return;
        }
        else
        {
            instance = this;
        }
    }

    public void AddScore()
    {
        uiController.ChangeScore();
    }

    public void ResetScore()
    {
        uiController.ResetScore();
    }
}
